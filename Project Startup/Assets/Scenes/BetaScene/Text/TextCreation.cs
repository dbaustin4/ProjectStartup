using UnityEngine;
using UnityEngine.UI;

public class TextCreation : MonoBehaviour {

  [SerializeField] Text textPrefab; // Reference to your Text prefab
  [SerializeField] Transform textParent; // Parent transform for the instantiated Text objects


  private void Start() {
    
  }

  private void Update() {

  }

  public void ReadStringInput(string givenText) {
    Debug.Log("text input is: " + givenText);
    AddTextToPlaceholder(givenText);
  }

  private void AddTextToPlaceholder(string inputText) {
    // Instantiate a new Text GameObject from the prefab
    Text newText = Instantiate(textPrefab, textParent);

    // Set the text value of the instantiated Text
    newText.text = inputText;

    newText.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
  }
}
