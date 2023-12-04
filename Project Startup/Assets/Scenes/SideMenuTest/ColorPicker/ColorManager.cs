using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

  [SerializeField] GameObject colorWheel;
  private bool canPick = false;

  void Start() {
    
  }

  void Update() {
    if (canPick) colorWheel.SetActive(true);
    else colorWheel.SetActive(false);
  }

  public void ShowPicker() {
    if (!canPick) canPick = true;
    else canPick = false;
  }
}
