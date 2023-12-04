using UnityEngine;
using System.Collections.Generic;

public class Transform : MonoBehaviour {
  private bool isDragging = false;
  private Vector2 offset;

  private void OnMouseDown() {
    // Calculate the offset between the mouse click position and the object's position
    offset = transform.position - (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    isDragging = true;
  }

  private void OnMouseUp() {
    isDragging = false;
  }

  private void Update() {
    if (isDragging) {
      // Update the object's position based on the mouse movement
      Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
      transform.position = mousePosition + offset;
    }
  }
}
