using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorDisplayImage : MonoBehaviour
{
  public ColorPicker colorPicker; // Reference to the ColorPicker script
  public Image colorDisplayImage;

  void Start() {
    ChangeColor();
    colorPicker.SetMaterialColor(Color.black); //set to black at start
  }

  private void Update() {
    ChangeColor();
  }

  void ChangeColor() {
    // Get the picked color from the ColorPicker
    Color pickedColor = colorPicker.GetPickedColor();

    // Set the alpha component to 1.0 to ensure the image is fully opaque
    pickedColor.a = 1.0f;

    // Set the color of the Image component to the picked color
    colorDisplayImage.color = pickedColor;
  }
}
