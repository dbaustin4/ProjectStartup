using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UploadImage : MonoBehaviour {
  [SerializeField] private GameObject imagePrefab; //prefab with SpriteRenderer component

  private List<string> chosenImage = new List<string>(); //list to hold file paths of images

  private void Start() {
  
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.B)) { //open when B is pressed
      OpenFileExplorer(); //call method to open file explorer
    }
  }

  //opens file explorer
  private void OpenFileExplorer() {
    string path = EditorUtility.OpenFilePanel("Select Image", "", "png,jpg,jpeg"); //pick image of type png/jpg/jpeg through explorer and store its path in a variable

    if (path != null && path != "") { //if image path isnt null or empty
      chosenImage.Add(path); //add chosen image path to the list
      
      LoadImages(); //call method to load and display the chosen image
    }
  }

  //load and display the chosen image
  private void LoadImages() {
    ClearImageContainer(); //call method to remove previous images so they dont stack

    GameObject imageContainer = new GameObject("ImageContainer"); //instantiate new container obj for images

    for (int i = 0; i < chosenImage.Count; i++) {
      GameObject imageObject = Instantiate(imagePrefab); //instantiate image obj 
      imageObject.tag = "Image"; //set the obj tag for removing

      //imageObject.transform.position = new Vector3(1, 0, 0); //set image spawn position

      SpriteRenderer spriteRenderer = imageObject.GetComponent<SpriteRenderer>(); //get sprite renderer component from image obj

      byte[] imageData = System.IO.File.ReadAllBytes(chosenImage[i]); //read image file and store as bytes
      Texture2D texture = new Texture2D(2, 2); //create new texture
      texture.LoadImage(imageData); //put image data into created texture
      Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero); //create sprite from texture and set pivot point to center
      spriteRenderer.sprite = sprite; //assign sprite to sprite renderer so it shows

      imageObject.AddComponent<Transform>(); //add a transform script
      imageObject.AddComponent<BoxCollider2D>(); //add a 2d box collider so we can transform the img

      imageObject.transform.parent = imageContainer.transform; //have the image container be sprite parent
    }
  }

  //remove image container so they dont stack
  private void ClearImageContainer() {
    GameObject existingContainer = GameObject.Find("ImageContainer"); //find the image container obj
    if (existingContainer != null) { //check if image container is found
      Destroy(existingContainer); //remove existing image container
    }
  }
}

