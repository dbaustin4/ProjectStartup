using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  [SerializeField] GameObject drawMenuBackground;
  public bool canDraw = false;
  bool isDrawMenuClicked = false;

  [SerializeField] private GameObject transformUploadPosition;
  private TransformPosition transformPositionScript;

  [SerializeField] GameObject colorWheel;
  [SerializeField] ColorPicker colorPicker;
  private bool canPick = false;

  [SerializeField] GameObject textInput;

  // Start is called before the first frame update
  void Start() {
    transformPositionScript = transformUploadPosition.GetComponent<TransformPosition>();
    transformPositionScript.enabled = true;
  }

  // Update is called once per frame
  void Update() {
    ColorWheelCheck();
  }

  public void OpenDrawMenu() {
    if (!isDrawMenuClicked) {
      drawMenuBackground.SetActive(true);
      isDrawMenuClicked = true;
      transformPositionScript.enabled = true;
      //Debug.Log("open menu");
    }
    else {
      drawMenuBackground.SetActive(false);
      isDrawMenuClicked = false;
      transformPositionScript.enabled = false;
      //Debug.Log("close menu");
    }
  }

  public void CanDrawCheck() {  //check if we can draw
    if (!canDraw) {
      canDraw = true; //if bool is false set to true
      transformPositionScript.enabled = false;
      //Debug.Log("disabled the script");
      drawMenuBackground.SetActive(false);
      isDrawMenuClicked = false;
      //Debug.Log("close menu");
    }
    else {
      canDraw = false; //else (if its true) set to false 
      transformPositionScript.enabled = true;
      //Debug.Log("enabled the script");
    }
  }

  public void DisableDraw() {
    canDraw = false;
    isDrawMenuClicked = false;
    transformPositionScript.enabled = true;
  }

  public void ShowPicker() {
    if (!canPick) {
      canPick = true;
      colorPicker.colorPicked = false;
    }
    else {
      canPick = false;
    }
  }

  private void ColorWheelCheck() {
    if (!canPick || colorPicker.colorPicked) {
      colorWheel.SetActive(false);
      canPick = false;
    }
    else if (canPick) {
      colorWheel.SetActive(true);
    }
  }

  public void ShowTextInput() {
    textInput.SetActive(true);
  }

  
}

