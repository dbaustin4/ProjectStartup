using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorPicker : MonoBehaviour, IPointerClickHandler {

  public Material targetMaterial; // Reference to the material you want to change
  private Color pickedColor; // Store the picked color
  public bool colorPicked = false;

  void Start() {
    // Ensure that a material is assigned to the script
    if (targetMaterial == null) {
      Debug.LogError("Please assign a material to the ColorPicker script.");
    }
  }

  void Update() {
  
  }

  public void OnPointerClick(PointerEventData eventData) {
    pickedColor = Pick(Camera.main.WorldToScreenPoint(eventData.position), GetComponent<Image>());
    SetMaterialColor(pickedColor);
  }

  Color Pick(Vector2 screenPoint, Image imageToPick) {
    Vector2 point;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(imageToPick.rectTransform, screenPoint, Camera.main, out point);
    point += imageToPick.rectTransform.sizeDelta / 2;
    Texture2D t = imageToPick.sprite.texture;
    Vector2Int m_point = new Vector2Int((int)((t.width * point.x) / imageToPick.rectTransform.sizeDelta.x), (int)((t.height * point.y) / imageToPick.rectTransform.sizeDelta.y));
    return t.GetPixel(m_point.x, m_point.y);
  }

  public void SetMaterialColor(Color color) {
    // Check if the material has the "_Color" property
    if (targetMaterial.HasProperty("_Color")) {
      // Set the color of the material
      targetMaterial.SetColor("_Color", color);
      colorPicked = true;
    }
    else {
      Debug.LogError("Material does not have a '_Color' property.");
    }
  }

  // Expose the picked color through a public method
  public Color GetPickedColor() {
    return pickedColor;
  }

}
