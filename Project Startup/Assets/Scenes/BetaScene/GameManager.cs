using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  public bool canDraw = false;

  [SerializeField] private GameObject transformPosition;
  private TransformPosition transformPositionScript;

  [SerializeField] GameObject colorWheel;
  private bool canPick = false;

  [SerializeField] GameObject textInput;

  // Start is called before the first frame update
  void Start() {
    transformPositionScript = transformPosition.GetComponent<TransformPosition>();
    transformPositionScript.enabled = true;

  }

  // Update is called once per frame
  void Update() {
    if (canPick) colorWheel.SetActive(true);
    else colorWheel.SetActive(false);
  }
  public void CanDrawCheck() {  //check if we can draw
    if (!canDraw) {
      canDraw = true; //if bool is false set to true
      transformPositionScript.enabled = false;
      Debug.Log("disabled the script");

    }
    else {
      canDraw = false; //else (if its true) set to false 
      transformPositionScript.enabled = true;
      Debug.Log("enabled the script");

    }
  }

  public void ShowPicker() {
    if (!canPick) canPick = true;
    else canPick = false;
  }

  public void ShowTextInput() {
    textInput.SetActive(true);
  }
}

