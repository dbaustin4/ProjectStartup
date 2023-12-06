using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TextCreation : MonoBehaviour {
  public TMP_InputField yourInputField;
  public GameObject gameTextPrefab;
  public Transform textSpawnPoint;

  private void Start() {
    // Subscribe to the OnEndEdit event
    yourInputField.onEndEdit.AddListener(OnEndEditCallback);
  }

  private void OnEndEditCallback(string editedText) {
    // Handle the edited text
    Debug.Log("Edited Text: " + editedText);

    // Optionally, you can call another method or perform actions based on the edited text
    CreateTextObject(editedText);

    // Clear the TMP InputField after adding the text (if desired)
    yourInputField.text = "";
  }

  private void CreateTextObject(string text) {
    // Create a new Text GameObject
    GameObject newTextObject = Instantiate(gameTextPrefab, textSpawnPoint.position, Quaternion.identity);

    // Attach the TextMeshProUGUI component to the new GameObject
    TextMeshProUGUI textComponent = newTextObject.GetComponent<TextMeshProUGUI>();

    // Set the text content
    textComponent.text = text;

    // Optional: You may want to modify the appearance or behavior of the text here

    // Optional: You may want to organize the new text objects in a specific way
    newTextObject.transform.SetParent(textSpawnPoint);
  }
}
