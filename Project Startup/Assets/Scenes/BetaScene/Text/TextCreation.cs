using UnityEngine;
using UnityEngine.UI;

public class TextCreation : MonoBehaviour {

  private string textInput;

  private void Start() {

  }

  private void Update() {
    
  }

  public void ReadStringInput(string givenText) {
    textInput = givenText;
    Debug.Log("text input is: " + textInput);
  }
}
