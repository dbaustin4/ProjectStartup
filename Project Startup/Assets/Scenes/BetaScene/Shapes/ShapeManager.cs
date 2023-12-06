using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShapeManager : MonoBehaviour {
  [SerializeField] private Sprite[] shapes; // array of shape options
  [SerializeField] private GameObject shapePrefab; // the prefab for creating objects
  [SerializeField] private GameObject shapesContainer; //parent obj 
  [SerializeField] private GameObject background; //set background img
  [SerializeField] private GameObject AddedShapeContainer;
  private bool clicked = false;

  private void Start() {
    CreateShapes(); //call method to create shapes once at start
  }

  private void Update() {

  }

  private void CreateShapes() { //create the possible shapes
    Transform parentTransform = shapesContainer.transform; //use parentTransform to orgin

    for (int i = 0; i < shapes.Length; i++) { //for every shape in shapes
      GameObject newObject = Instantiate(shapePrefab, parentTransform); //instantiate a new obj from prefab

      Image imageComponent = newObject.GetComponent<Image>(); //set sprite from img component on new obj
      if (imageComponent != null) {
        imageComponent.sprite = shapes[i]; //add correct sprite to img component
        newObject.name = shapes[i].name;
      }

      Vector3 position = newObject.transform.localPosition; //store obj position
      position.y += -30 + i * 30;  //position the y pos so img have 30px between each other
      newObject.transform.position = position; //put position on the obj
      newObject.transform.localPosition = position; //put changed position back to local position

      RectTransform rectTransform = imageComponent.rectTransform; //get rect transform from obj
      rectTransform.localScale = new Vector3(1.0f / 4, 1.0f / 4, 1.0f); //scale img down 4 times as small

      Button buttonComponent = newObject.AddComponent<Button>(); //add button
      // Add onClick event                                  
      buttonComponent.onClick.AddListener(() => AddShape(newObject));

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

  private void AddShape(GameObject clickedObject) {
    //Get the Image component from the clicked button
    Image clickedImage = clickedObject.GetComponent<Image>();

    // Check if the Image component exists
    if (clickedImage != null) {
      // Get the sprite from the clicked Image
      Sprite clickedSprite = clickedImage.sprite;

      // Instantiate a new GameObject using the shapePrefab
      GameObject newShape = Instantiate(shapePrefab, Vector3.zero, Quaternion.identity);

      // Set the sprite of the new GameObject
      Image newImageComponent = newShape.GetComponent<Image>();
      if (newImageComponent != null) {
        newImageComponent.sprite = clickedSprite;
      }

      newShape.transform.SetParent(AddedShapeContainer.transform);

      // Set the position of the new GameObject
      newShape.transform.position = new Vector3(Screen.width / 2, Screen.height /2, 0);

      newShape.AddComponent<BoxCollider2D>();

      // Activate the new GameObject
      newShape.SetActive(true);
    }
  }
}