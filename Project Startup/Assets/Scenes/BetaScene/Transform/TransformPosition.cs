using UnityEngine;
using System.Collections.Generic;

public class TransformPosition : MonoBehaviour {
  private bool isDragging = false;
  private Vector2 offset;
  public bool canMove = true;
  [SerializeField] private GameManager gameManager;

  private void Start() {
    gameManager = FindObjectOfType<GameManager>();
  }

  private void Update() {
    if (!gameManager.canDraw && isDragging) {
      // Update the object's position based on the mouse movement
      Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
      transform.position = mousePosition + offset;
    }
  }

  private void OnMouseDown() {
    // Calculate the offset between the mouse click position and the object's position
    offset = transform.position - (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    isDragging = true;
  }

  private void OnMouseUp() {
    isDragging = false;
  }
}
