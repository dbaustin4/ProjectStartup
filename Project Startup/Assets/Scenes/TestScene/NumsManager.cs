using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumsManager : MonoBehaviour {

  public int clickedNum = 0;

  void Start() {

  }

  void Update() {
    Debug.Log(clickedNum);
  }

  public void onNumClick1() {
    clickedNum = 1;
  }

  public void onNumClick2() {
    clickedNum = 2;
  }

  public void onNumClick3() {
    clickedNum = 3;
  }

  public void onNumClick4() {
    clickedNum = 4;
  }

  public void onNumClick5() {
    clickedNum = 5;
  }

  public void onNumClick6() {
    clickedNum = 6;
  }

  public void onNumClick7() {
    clickedNum = 7;
  }

  public void onNumClick8() {
    clickedNum = 8;
  }

  public void onNumClick9() {
    clickedNum = 9;
  }

  public void onNumClick10() {
    clickedNum = 10;
  }

}
