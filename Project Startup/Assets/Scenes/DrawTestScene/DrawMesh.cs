using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMesh : MonoBehaviour {
  // Start is called before the first frame update
  void Start() {
    Mesh mesh = new Mesh();

    Vector3[] vertices = new Vector3[4];
    Vector2[] uv = new Vector2[4];
    int[] triangles = new int[6];

    vertices[0] = new Vector3(-1, +1);
    vertices[1] = new Vector3(-1, -1);
    vertices[2] = new Vector3(+1, -1);
    vertices[3] = new Vector3(+1, +1);

    uv[0] = Vector2.zero;
    uv[1] = Vector2.zero;
    uv[2] = Vector2.zero;
    uv[3] = Vector2.zero;

    triangles[0] = 0;
    triangles[1] = 3;
    triangles[3] = 1;

    triangles[4] = 1;
    triangles[5] = 3;
    triangles[6] = 1;

    mesh.vertices = vertices;
    mesh.uv = uv;
    mesh.triangles = triangles;
    mesh.MarkDynamic();

    GetComponent<MeshFilter>().mesh = mesh;

  }

  // Update is called once per frame
  void Update() {
    Vector3 mousePositionScreen = Input.mousePosition;
    mousePositionScreen.z = 0f;
    Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

    transform.position = mousePositionWorld;
  }

  void GetMousePos() {
    Vector3 mousePositionScreen = Input.mousePosition;
    mousePositionScreen.z = 0f;
    Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
  }
}
