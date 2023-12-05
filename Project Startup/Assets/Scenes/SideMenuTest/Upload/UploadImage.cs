using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SimpleFileBrowser;
using System.IO;
using System.Collections;

public class UploadImage : MonoBehaviour {
  [SerializeField] private GameObject imagePrefab; //prefab with SpriteRenderer component
  Coroutine CoroutineCheck;
  [SerializeField] private GameObject uploadContainer;

  private void Start() {
  
  }

  bool openDialog = false;
  private void Update() {
    if (Input.GetKeyUp(KeyCode.B) && !openDialog) { //open when B is pressed and its not open
      StopAllCoroutines(); //stop all the coroutines from running
      openDialog = true; //say we are allowed to open the dialog
    }
  }

  private void LateUpdate() {
    if(openDialog) { //if the dialog is allowed to open
      OpenDialog(); //open the dialog
      openDialog = false; //close the dialog
    }
  }

  public void OpenDialog() {
    FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png")); //set filters for files
    FileBrowser.SetDefaultFilter(".jpg"); //set default filter when load dialog is open
    FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe"); //exclude these
    FileBrowser.AddQuickLink("Users", "C:\\Users", null); //add quick link to proper folder if its selected once

    if (CoroutineCheck != null) { //check if coroutine is still running
      StopCoroutine(CoroutineCheck); //stop the coroutine
    }
    CoroutineCheck = StartCoroutine(ShowLoadDialogCoroutine()); //reset coroutine
  }
  
  private void LoadOneImage(string imagePath, GameObject parent) {
    GameObject imageObject = Instantiate(imagePrefab); //instantiate image obj 
    imageObject.tag = "Image"; //set the obj tag for removing

    imageObject.transform.parent = parent.transform; //have the upload container obj be the parent of uploaded img
    imageObject.layer = LayerMask.NameToLayer("Upload");
    SpriteRenderer spriteRenderer = imageObject.GetComponent<SpriteRenderer>(); //get sprite renderer component from image obj

    byte[] imageData = System.IO.File.ReadAllBytes(imagePath); //read image file and store as bytes
    Texture2D texture = new Texture2D(2, 2); //create new texture
    texture.LoadImage(imageData); //put image data into created texture
    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero); //create sprite from texture and set pivot point to center
    spriteRenderer.sprite = sprite; //assign sprite to sprite renderer so it shows

    imageObject.AddComponent<BoxCollider2D>(); //add a 2d box collider so we can transform the img
  }

  private IEnumerator ShowLoadDialogCoroutine() {
    yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load"); //show load dialog and allow multiple files to be picked
    //Debug.Log(FileBrowser.Success); //print if files are selected or cancelled

    if (FileBrowser.Success) { //print path of selected files if choosing one was a succes
      for (int i = 0; i < FileBrowser.Result.Length; i++) { //loop through all file paths
        string fileToBeLoaded = FileBrowser.Result[i]; //sets the path to a string so it can be loaded
        //Debug.Log($"{fileToBeLoaded} - {i}"); //print file path
        LoadOneImage(fileToBeLoaded, uploadContainer); //load path
      }
    }
  }
}

// Show a save file dialog 
// onSuccess event: not registered (which means this dialog is pretty useless)
// onCancel event: not registered
// Save file/folder: file, Allow multiple selection: false
// Initial path: "C:\", Initial filename: "Screenshot.png"
// Title: "Save As", Submit button text: "Save"
// FileBrowser .ShowSaveDialog( null, null, FileBrowser.PickMode.Files, false, "C:\\", "Screenshot.png", "Save As", "Save" );