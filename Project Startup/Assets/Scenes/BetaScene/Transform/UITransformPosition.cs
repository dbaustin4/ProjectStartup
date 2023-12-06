using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITransformPosition : MonoBehaviour, IDragHandler {
  void Start() {

  }

  void Update() {

  }


  public void OnDrag(PointerEventData eventData) {
    transform.position = Input.mousePosition;
  }
}
