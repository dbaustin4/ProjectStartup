using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPolygonMesh : MonoBehaviour {

  //mesh variables
  Mesh mesh;
  [SerializeField] private Vector3[] polygonPoints;
  [SerializeField] private int[] polygonTriangles;

  //polygon variables
  [SerializeField] private int polygonSides; //how many sides is the polygon (more = rounder)
  [SerializeField] private float polygonRadius; //how big is the polygon

  //Vector3 polygonPosition;

  private void Start() {
    mesh = new Mesh(); //create new instance of mesh
    GetComponent<MeshFilter>().mesh = mesh; //assign mesh to Meshfilter of this game obj
    //GetComponent<Transform>().position = polygonPosition;
  }

  private void Update() {
    Vector3 mousePosition = Input.mousePosition;
    mousePosition.z = 0;

    // Update the position of the object
    transform.position = mousePosition;

    //if (Input.GetMouseButton(0)) {
    DrawPolygon(polygonSides, polygonRadius/*, mousePosition*/); //puts the polygon on the screen
    //}


  }

  //draws a polygon
  private void DrawPolygon(int sides, float radius/*, Vector3 pPosition*/) {
    polygonPoints = GetCircumferencePoints(sides, radius).ToArray(); //calculate points on the outside of the shape and put them in an array
    polygonTriangles = DrawTriangles(polygonPoints); //figure out how to connect the points to fill the shape
    mesh.Clear(); //clear any previous drawings on the screen
    mesh.vertices = polygonPoints; //draw the shape using the calculated points
    mesh.triangles = polygonTriangles; //connect the points to create the shape
    /*transform.position = polygonPosition;
    Debug.Log("polygon pos " + polygonPosition);*/
  }

  //calculate evenly spaced points on polygon circumference based on amount of sides and radius 
  private List<Vector3> GetCircumferencePoints(int sides, float radius) {
    List<Vector3> points = new List<Vector3>(); //create a list to store the points
    float circumferenceProgressPerStep = (float)1 / sides; //evenly space points around the shape using its circumference
    float TAU = 2 * Mathf.PI; //say TAU is a full circle 360 degrees 2PI radians to calculate the progress in radians per loop
    float radianProgressPerStep = circumferenceProgressPerStep * TAU; //progress per step multiplied with TAU to know amount of radians of progress

    for (int i = 0; i < sides; i++) { //for each corner point
      float currentRadian = radianProgressPerStep * i; //calculate angle in radians based on progress per step and i
      points.Add(new Vector3(Mathf.Cos(currentRadian) * radius, Mathf.Sin(currentRadian) * radius, 0)); //calculate x and y of point on circumference and add it to list of points
    }
    return points; //returns the points list given
  }

  //draw triangles based on points
  private int[] DrawTriangles(Vector3[] points) {
    int triangleAmount = points.Length - 2; //calculate triangles needed to fill the polygon
    List<int> newTriangles = new List<int>(); //list to store indices of vertices that make the triangles
    for (int i = 0; i < triangleAmount; i++) { //loop to generate triangles connecting the points to fill the polygon
      newTriangles.Add(0); //first vertex (first in polygon)
      newTriangles.Add(i + 2); //2nd vertex
      newTriangles.Add(i + 1); //3rd vertex
    }
    return newTriangles.ToArray(); //convert the list of triangle indices to an array and return the values
  }
}
