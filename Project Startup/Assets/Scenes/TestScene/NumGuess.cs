using UnityEngine;
using Yarn.Unity;

public class NumGuess : MonoBehaviour {

  InMemoryVariableStorage variableStorage;
  int randomNum;
  bool generatedNum = false;
  bool startGuessing = false;
  [SerializeField]
  GameObject numsDisplay;
  [SerializeField]
  NumsManager numsManager;
  [SerializeField]
  GameObject dialogueSystem;
  bool guessedRight = false;
  
  private void Start() {
    variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
  }

  private void Update(  ) {
    CheckGameStart();
    PlayerInput();

    if (!generatedNum && CheckGameStart()) {
      RandomNum();
      generatedNum = true;
    }

    //Debug.Log("numGen: " + generatedNum);
  }

  private void RandomNum() {
  randomNum = Random.Range(1, 11);
    variableStorage.SetValue("$num", randomNum);
    //Debug.Log("randomNum: " + randomNum);
  }

  private bool CheckGameStart() {
    bool result;
    variableStorage.TryGetValue("$gameStart", out result);
    if (result) generatedNum = false;
    //Debug.Log("result: " + result);
    return result;
  }

  private void PlayerInput() {
    bool result;
    variableStorage.TryGetValue("$canGuess", out result);
    if (result) startGuessing = true;

    if (startGuessing) {
      numsDisplay.SetActive(true);
      //dialogueSystem.SetActive(false);
      if (numsManager.clickedNum == randomNum) {
        Debug.Log("You guessed right!");
        guessedRight = true;
        variableStorage.SetValue("$guessedRight", guessedRight);
        numsDisplay.SetActive(false);
        //dialogueSystem.SetActive(true);
      }
      else Debug.Log("Wrong!");
    }
  }
}