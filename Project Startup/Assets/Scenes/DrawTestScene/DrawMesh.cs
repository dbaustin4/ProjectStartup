using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMesh : MonoBehaviour {

  [SerializeField]
  private List<Mesh> meshes;
  private Mesh mesh; //create new mesh
  private Vector3 lastMousePos;
  private bool canDraw = false; 

  //adjustable variables
  [SerializeField] private float lineThickness = 1f;
  [SerializeField] private float minDistance = 0.1f;

  //for debug
  [SerializeField] private Transform debugVisual1;
  [SerializeField] private Transform debugVisual2;

  private void Start() {
    meshes = new List<Mesh>(); //create our own new mesh
  }

  private void Update() {
    if (canDraw) MeshCreation(); //draw if we're allowed to
  }

  private void MeshCreation() {
    if (Input.GetMouseButtonDown(0)) { //if mouse is pressed once

      mesh = new Mesh(); //initialize new mesh
      meshes.Add(mesh);

      //create arrays for each mesh component
      Vector3[] vertices = new Vector3[4];
      Vector2[] uv = new Vector2[4];
      int[] triangles = new int[6];

      //set first vertext pos
      vertices[0] = GetMouseWorldPos();
      vertices[1] = GetMouseWorldPos();
      vertices[2] = GetMouseWorldPos();
      vertices[3] = GetMouseWorldPos();

      //set uv values
      uv[0] = Vector2.zero;
      uv[1] = Vector2.zero;
      uv[2] = Vector2.zero;
      uv[3] = Vector2.zero;

      //set first triangle values (for a quad)
      triangles[0] = 0;
      triangles[1] = 3;
      triangles[2] = 1;

      //set second triangle values (for a quad)
      triangles[3] = 1;
      triangles[4] = 3;
      triangles[5] = 2;

      //assign each value to their mesh component
      mesh.vertices = vertices;
      mesh.uv = uv;
      mesh.triangles = triangles;
      mesh.MarkDynamic(); //say the shape we're making is dynamic

      GetComponent<MeshFilter>().mesh = mesh; //assign mesh values to the in game mesh

      lastMousePos = GetMouseWorldPos(); //catch initial mouse pos
    }

    if (Input.GetMouseButton(0)) { //if mouse is held down

      if (Vector3.Distance(GetMouseWorldPos(), lastMousePos) > minDistance) { //allow draw when mouse is further than minimum distance
        //expand mesh to add the new vertices
        Vector3[] vertices = new Vector3[mesh.vertices.Length + 2];
        Vector2[] uv = new Vector2[mesh.uv.Length + 2];
        int[] triangles = new int[mesh.triangles.Length + 6];

        //copy existing mesh data
        mesh.vertices.CopyTo(vertices, 0);
        mesh.uv.CopyTo(uv, 0);
        mesh.triangles.CopyTo(triangles, 0);

        //calculate index for new vertices
        int vIndex = vertices.Length - 4;
        int vIndex0 = vIndex + 0;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        //calculate new vertice according to mouse movement
        Vector3 mouseForwardVector = (GetMouseWorldPos() - lastMousePos).normalized;
        Vector3 normal2D = new Vector3(0, 0, -1f);
        Vector3 newVertexUp = GetMouseWorldPos() + Vector3.Cross(mouseForwardVector, normal2D) * lineThickness;
        Vector3 newVertexDown = GetMouseWorldPos() + Vector3.Cross(mouseForwardVector, normal2D * -1f) * lineThickness;

        //for debug check new vertice vectors 
        /*debugVisual1.position = newVertexUp;
        debugVisual2.position = newVertexDown;*/

        //set new vertex values
        vertices[vIndex2] = newVertexUp;
        vertices[vIndex3] = newVertexDown;

        //set new uv values
        uv[vIndex2] = Vector2.zero;
        uv[vIndex3] = Vector2.zero;

        //calculate index for new triangles 
        int tIndex = triangles.Length - 6;

        //set new first triangle values (for a quad)
        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex2;
        triangles[tIndex + 2] = vIndex1;

        //set new first triangle values (for a quad)
        triangles[tIndex + 3] = vIndex1;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;

        //assign updated mesh data
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        lastMousePos = GetMouseWorldPos(); //get current mouse pos
      }
    }
  }

  private static Vector3 GetMouseWorldPos() { //gets mouse pos in world space (without z for 2d)
    Vector3 vec = GetMouseWorldPosWithZ(Input.mousePosition, Camera.main);
    vec.z = 0f;
    return vec;
  }

  private static Vector3 GetMouseWorldPosWithZ(Vector3 screenPosition, Camera worldCamera) { //gets mouse pos in world space (z included)
    Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
    return worldPosition;
  }

  public void CanDrawCheck() {  //check if we can draw
    if(!canDraw) canDraw = true; //if bool is false set to true
    else canDraw = false; //else (if its true) set to false 
  }
}