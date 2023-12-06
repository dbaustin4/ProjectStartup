using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TextCreation : MonoBehaviour {
  public TMP_InputField yourInputField;
  public GameObject gameTextPrefab;
  public Transform textSpawnPoint;

  private void Start() {
    yourInputField.onEndEdit.AddListener(OnEndEditCallback);
  }

  private void OnEndEditCallback(string editedText) {
    // Handle the edited text
    Debug.Log("Edited Text: " + editedText);

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

    newTextObject.transform.SetParent(textSpawnPoint);
  }
}
