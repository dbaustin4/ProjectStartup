using UnityEngine;
using UnityEngine.UI;

public class AddShape : MonoBehaviour {
  public GameObject shapeContainer; // Prefab for the options (images)
  public Transform shapeMenu; // Panel to hold the options
  public Sprite[] shapes; // Array of option images

  private void Start() {
    // Hide the options panel initially
    shapeMenu.gameObject.SetActive(false);
  }

  public void ShowMenu() {
    // Show the options panel when the button is clicked
    shapeMenu.gameObject.SetActive(true);

    // Populate options based on the array of sprites
    foreach (Sprite optionSprite in shapes) {
      // Instantiate an option GameObject from the prefab
      GameObject optionObject = Instantiate(shapeContainer, shapeMenu);

      // Set the sprite for the option
      optionObject.GetComponent<Image>().sprite = optionSprite;

      // Attach a click listener to the option
      optionObject.GetComponent<Button>().onClick.AddListener(() => OnShapeClick(optionSprite));
    }
  }

  private void OnShapeClick(Sprite chosenShape) {
    // Handle the click event for the selected option
    Debug.Log("Selected Option: " + chosenShape.name);

    // Instantiate the selected option as a new GameObject in the game
    GameObject newGameObject = Instantiate(shapeContainer, Vector3.zero, Quaternion.identity);
    newGameObject.GetComponent<Image>().sprite = chosenShape;

    // You can further customize the newGameObject's properties or add it to a specific location in your game.
  }
}