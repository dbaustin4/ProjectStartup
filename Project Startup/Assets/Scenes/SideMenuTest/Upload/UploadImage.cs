using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SimpleFileBrowser;
using System.IO;
using System.Collections;

public class UploadImage : MonoBehaviour {
  [SerializeField] private GameObject imagePrefab; //prefab with SpriteRenderer component

  private List<string> chosenImage = new List<string>(); //list to hold file paths of images

  private void Start() {
  
  }

  bool open = false;
  private void Update() {
    if (Input.GetKeyUp(KeyCode.B) && !open) { //open when B is pressed
                                              //call method to open file explorer
      StopAllCoroutines();
      open = true;
    }
  }

  private void LateUpdate() {
    if(open) {
      OpenDialog();
      open = false;
    }

  }

  public void OpenDialog() {
    //OpenFileExplorer();
    // Set filters (optional)
    // It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
    // if all the dialogs will be using the same filters
    FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"));

    // Set default filter that is selected when the dialog is shown (optional)
    // Returns true if the default filter is set successfully
    // In this case, set Images filter as the default filter
    FileBrowser.SetDefaultFilter(".jpg");

    // Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
    // Note that when you use this function, .lnk and .tmp extensions will no longer be
    // excluded unless you explicitly add them as parameters to the function
    FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

    // Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
    // It is sufficient to add a quick link just once
    // Name: Users
    // Path: C:\Users
    // Icon: default (folder icon)
    FileBrowser.AddQuickLink("Users", "C:\\Users", null);

    // Show a save file dialog 
    // onSuccess event: not registered (which means this dialog is pretty useless)
    // onCancel event: not registered
    // Save file/folder: file, Allow multiple selection: false
    // Initial path: "C:\", Initial filename: "Screenshot.png"
    // Title: "Save As", Submit button text: "Save"
    // FileBrowser.ShowSaveDialog( null, null, FileBrowser.PickMode.Files, false, "C:\\", "Screenshot.png", "Save As", "Save" );

    // Show a select folder dialog 
    // onSuccess event: print the selected folder's path
    // onCancel event: print "Canceled"
    // Load file/folder: folder, Allow multiple selection: false
    // Initial path: default (Documents), Initial filename: empty
    // Title: "Select Folder", Submit button text: "Select"
    // FileBrowser.ShowLoadDialog( ( paths ) => { Debug.Log( "Selected: " + paths[0] ); },
    //						   () => { Debug.Log( "Canceled" ); },
    //						   FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select" );

    // Coroutine example\
    if (mimimi != null) {
      Debug.Log("somehow, mimimi is still alive!");
      StopCoroutine(mimimi);
    }
    mimimi = StartCoroutine(ShowLoadDialogCoroutine());
  }

  Coroutine mimimi;

  int c = 0;

  //opens file explorer
  private void OpenFileExplorer() {
    //Debug.Log($"Check {++c}");
    string path = EditorUtility.OpenFilePanel("Select Image", "", "png,jpg,jpeg"); //pick image of type png/jpg/jpeg through explorer and store its path in a variable

    if (path != null && path != "") { //if image path isnt null or empty
      chosenImage.Add(path); //add chosen image path to the list
      
      //LoadImages(); //call method to load and display the chosen image
    }
  }

  //load and display the chosen image
  private void LoadImages() {
    //ClearImageContainer(); //call method to remove previous images so they dont stack

    //GameObject imageContainer = new GameObject("ImageContainer"); //instantiate new container obj for images

    for (int i = 0; i < chosenImage.Count; i++) {

      LoadOneImage(chosenImage[i]);
      //imageObject.transform.parent = imageContainer.transform; //have the image container be sprite parent
    }
    chosenImage.Clear();
  }

  //remove image container so they dont stack
  private void ClearImageContainer() {
    GameObject existingContainer = GameObject.Find("ImageContainer"); //find the image container obj
    if (existingContainer != null) { //check if image container is found
      Destroy(existingContainer); //remove existing image container
    }
  }

  private void LoadOneImage(string imagePath) {
    GameObject imageObject = Instantiate(imagePrefab); //instantiate image obj 
    imageObject.tag = "Image"; //set the obj tag for removing

    //imageObject.transform.position = new Vector3(1, 0, 0); //set image spawn position

    SpriteRenderer spriteRenderer = imageObject.GetComponent<SpriteRenderer>(); //get sprite renderer component from image obj

    byte[] imageData = System.IO.File.ReadAllBytes(imagePath); //read image file and store as bytes
    Texture2D texture = new Texture2D(2, 2); //create new texture
    texture.LoadImage(imageData); //put image data into created texture
    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero); //create sprite from texture and set pivot point to center
    spriteRenderer.sprite = sprite; //assign sprite to sprite renderer so it shows

    imageObject.AddComponent<Transform>(); //add a transform script
    imageObject.AddComponent<BoxCollider2D>(); //add a 2d box collider so we can transform the img
  }


  IEnumerator ShowLoadDialogCoroutine() {
    Debug.Log($"Starting coroutine {++c}");
    // Show a load file dialog and wait for a response from user
    // Load file/folder: both, Allow multiple selection: true
    // Initial path: default (Documents), Initial filename: empty
    // Title: "Load File", Submit button text: "Load"
    yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

    // Dialog is closed
    // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
    Debug.Log(FileBrowser.Success);

    if (FileBrowser.Success) {
      // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
      for (int i = 0; i < FileBrowser.Result.Length; i++) {
        string fileToBeLoaded = FileBrowser.Result[i];
        Debug.Log($"{fileToBeLoaded} - {i}");
        //chosenImage.Add(FileBrowser.Result[i]);
        //LoadImages();
        LoadOneImage(fileToBeLoaded);
      }

      // Read the bytes of the first file via FileBrowserHelpers
      // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
      //byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

      // Or, copy the first file to persistentDataPath
      //string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
      //FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
      //chosenImage.Add(destinationPath);
      //open = false;
      //
    }

    Debug.Log("I'm ded");
  }
}

