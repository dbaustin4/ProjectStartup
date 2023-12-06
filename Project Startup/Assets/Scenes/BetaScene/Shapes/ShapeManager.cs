using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShapeManager : MonoBehaviour {
  [SerializeField] private Sprite[] shapes; // array of shape options
  [SerializeField] private GameObject shapePrefab; // the prefab for creating objects
  [SerializeField] private GameObject shapesContainer; //parent obj 
  [SerializeField] private GameObject background; //set background img
  private bool clicked = false;

  private void Start() {
    CreateShapes(); //call method to create shapes once at start
  }

  private void Update() {
    CheckClick();
  }

  private void CreateShapes() { //create the possible shapes
    Transform parentTransform = shapesContainer.transform; //use parentTransform to orgin

    for (int i = 0; i < shapes.Length; i++) { //for every shape in shapes
      GameObject newObject = Instantiate(shapePrefab, parentTransform); //instantiate a new obj from prefab

      Image imageComponent = newObject.GetComponent<Image>(); //set sprite from img component on new obj
      if (imageComponent != null) {
        imageComponent.sprite = shapes[i]; //add correct sprite to img component
      }

      Vector3 position = newObject.transform.localPosition; //store obj position
      position.y += -30 + i * 30;  //position the y pos so img have 30px between each other
      newObject.transform.position = position; //put position on the obj
      newObject.transform.localPosition = position; //put changed position back to local position

      RectTransform rectTransform = imageComponent.rectTransform; //get rect transform from obj
      rectTransform.localScale = new Vector3(1.0f / 4, 1.0f / 4, 1.0f); //scale img down 4 times as small

      //newObject.AddComponent<BoxCollider2D>(); //add a 2d box collider so we can click

      newObject.tag = "Shape";
      BoxCollider2D boxCollider = newObject.AddComponent<BoxCollider2D>();
      boxCollider.isTrigger = true; // Set collider as trigger to receive OnMouseDown events.

      // Set the collider size based on the size of the image
      RectTransform imageRectTransform = imageComponent.rectTransform;
      boxCollider.size = new Vector2(imageRectTransform.sizeDelta.x, imageRectTransform.sizeDelta.y);

      newObject.SetActive(false); //set obj to false
    }
  }

  public void DisplayShapes() { //shows the shape options
    if (!clicked) {
      clicked = true; 
      background.SetActive(true); //turn on background img

      foreach (Transform child in shapesContainer.transform) { //go through children of shapesContainer
        child.gameObject.SetActive(true); //turn on obj
      }
    }
    else {
      clicked = false;
      background.SetActive(false); //turn off background img

      foreach (Transform child in shapesContainer.transform) { //go through children of shapesContainer
        child.gameObject.SetActive(false); //turn off obj
      }
    }
  }

  private void AddShape() {

  }

  private void CheckClick() {
    if (Input.GetMouseButtonDown(0)) {
      int layerMask = LayerMask.GetMask("Shape");
      RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);

      if (hit.collider != null) {
        Debug.Log("Hit object: " + hit.collider.gameObject.name);

        if (hit.collider.CompareTag("Shape")) {
          Debug.Log("Shape clicked: " + hit.collider.gameObject.name);
          // Add your logic for handling the clicked shape here
        }
      }
    }
    else {
      //Debug.Log("No hit detected.");
    }
  }


}