using UnityEngine;
using UnityEngine.UI;

public class TextCreation : MonoBehaviour {

  [SerializeField] Text textPrefab; // Reference to your Text prefab
  [SerializeField] Transform textParent; // Parent transform for the instantiated Text objects
  public bool textCreated = false;

  private void Start() {
    
  }

  private void Update() {

  }

  public void ReadStringInput(string givenText) {
    //Debug.Log("text input is: " + givenText); //print text that is given
    AddTextToPlaceholder(givenText); //call method
  }

  private void AddTextToPlaceholder(string inputText) { //method to put text in placeholder
    Text newText = Instantiate(textPrefab, textParent); //instantiate new obj from prefab

    newText.text = inputText; //set text value to placeholder from input field to variable
    textCreated = true;
    //Debug.Log("text created should be true... its: " + textCreated);
    newText.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f); //position new text obj
  }
}
