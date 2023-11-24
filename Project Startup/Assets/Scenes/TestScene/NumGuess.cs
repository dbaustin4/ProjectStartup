using UnityEngine;
using Yarn.Unity;

public class NumGuess : MonoBehaviour {

  InMemoryVariableStorage variableStorage;
  bool generatedNum = false;
  private void Start() {
    variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
  }

  private void Update() {
    //CheckGameStart();

    if(CheckGameStart() && !generatedNum) {
      RandomNum();
    }

    //Debug.Log("numGen: " + generatedNum);
  }

  private void RandomNum() {
    int num = Random.Range(1, 11);
    variableStorage.SetValue("$num", num);
    Debug.Log("num: " + num);
  }

  private bool CheckGameStart() {
    bool result;
    variableStorage.TryGetValue("$gameStart", out result);
    if (result) generatedNum = false;
    else generatedNum = true;
    //Debug.Log("result: " + result);
    return result;
  }

  private void PlayerInput() {

    //to be filled
  }
}