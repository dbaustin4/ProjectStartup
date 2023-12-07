using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ShapeManager : MonoBehaviour {
  [SerializeField] private Sprite[] shapes;
  [SerializeField] private GameObject shapePrefab;
  [SerializeField] private GameObject shapesContainer;
  [SerializeField] private GameObject background;
  [SerializeField] private GameObject AddedShapeContainer;
  [SerializeField] private float resizeImageDivision = 4f;
  [SerializeField] private float scaleShapeSize = 1.2f;

  private List<RectTransform> addedShapeRectTransforms = new List<RectTransform>(); // List to track added shapes
  private bool isDraggingShape;
  private bool shapeClicked = false;

  private void Start() {
    CreateShapes();
  }

  private void Update() {
    RepositionShape();
  }

  private void CreateShapes() {
    //in one vertical column
    /*Transform parentTransform = shapesContainer.transform;

    for (int i = 0; i < shapes.Length; i++) {
      GameObject newObject = Instantiate(shapePrefab, parentTransform);

      Image imageComponent = newObject.GetComponent<Image>();
      if (imageComponent != null) {
        imageComponent.sprite = shapes[i];
        newObject.name = shapes[i].name;
      }

      Vector3 position = newObject.transform.localPosition;
      position.y += -30 + i * 30;
      newObject.transform.position = position;
      newObject.transform.localPosition = position;

      RectTransform rectTransform = imageComponent.rectTransform;
      rectTransform.localScale = new Vector3(1.0f / 4, 1.0f / 4, 1.0f);

      Button buttonComponent = newObject.AddComponent<Button>();
      buttonComponent.onClick.AddListener(() => AddShape(newObject));

      newObject.SetActive(false);
    }*/

    //in rows and columns of 3
    Transform parentTransform = shapesContainer.transform;
    int shapesPerRow = 3;
    float spacingBetweenShapes = 10f; // Adjust this value based on your layout preferences

    for (int i = 0; i < shapes.Length; i++) {
      GameObject newObject = Instantiate(shapePrefab, parentTransform);

      Image imageComponent = newObject.GetComponent<Image>();
      if (imageComponent != null) {
        imageComponent.sprite = shapes[i];
        newObject.name = shapes[i].name;
      }

      int row = i / shapesPerRow;
      int col = i % shapesPerRow;

      RectTransform rectTransform = imageComponent.rectTransform;
      rectTransform.localScale = new Vector3(1.0f / resizeImageDivision, 1.0f / resizeImageDivision, 1.0f);

      // Calculate the position based on row and column
      float offsetX = col * (rectTransform.rect.width * rectTransform.localScale.x + spacingBetweenShapes);
      float offsetY = -row * (rectTransform.rect.height * rectTransform.localScale.y + spacingBetweenShapes);

      rectTransform.anchoredPosition = new Vector2(offsetX, offsetY);

      Button buttonComponent = newObject.AddComponent<Button>();
      buttonComponent.onClick.AddListener(() => AddShape(newObject));

      newObject.SetActive(false);
    }
  }

  public void DisplayShapes() {
    if (!background.activeSelf /*&& !shapeClicked*/) {
      background.SetActive(true);

      foreach (Transform child in shapesContainer.transform) {
        child.gameObject.SetActive(true);
      }
    }
    /*else if (shapeClicked) {
      foreach (Transform child in shapesContainer.transform) {
        child.gameObject.SetActive(false);
      }
      shapeClicked = false;
    }*/
    else {
      background.SetActive(false);
      shapeClicked = false;
      foreach (Transform child in shapesContainer.transform) {
        child.gameObject.SetActive(false);
      }
    }
  }

  private void AddShape(GameObject clickedObject) {
    Image clickedImage = clickedObject.GetComponent<Image>();

    if (clickedImage != null) {
      Sprite clickedSprite = clickedImage.sprite;

      GameObject newShape = Instantiate(shapePrefab, Vector3.zero, Quaternion.identity);

      Image newImageComponent = newShape.GetComponent<Image>();
      if (newImageComponent != null) {
        newImageComponent.sprite = clickedSprite;
        //shapeClicked = true;
      }

      newShape.transform.SetParent(AddedShapeContainer.transform);

      RectTransform rectTransform = newShape.GetComponent<RectTransform>();
      rectTransform.pivot = new Vector2(0.5f, 0.5f);
      rectTransform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f); //position in middle
      rectTransform.localScale = new Vector3(scaleShapeSize, scaleShapeSize, 1.0f); //scale shape size

      newShape.AddComponent<BoxCollider2D>();

      newShape.SetActive(true);

      // Add the RectTransform of the added shape to the list
      addedShapeRectTransforms.Add(rectTransform);
    }
  }

  private void RepositionShape() { //scuff movement fix
    if (Input.GetMouseButtonDown(0) && addedShapeRectTransforms.Count > 0) {
      foreach (RectTransform rectTransform in addedShapeRectTransforms) {
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition)) {
          isDraggingShape = true;
          break;
        }
      }
    }

    if (Input.GetMouseButtonUp(0)) {
      isDraggingShape = false;
    }

    if (isDraggingShape) {
      foreach (RectTransform rectTransform in addedShapeRectTransforms) {
        rectTransform.position = Input.mousePosition;
      }
    }
  }
}

