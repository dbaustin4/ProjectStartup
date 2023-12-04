using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorPicker : MonoBehaviour, IPointerClickHandler {

  public Color output;
  private bool canPick = false;
  [SerializeField] private GameObject ColorWheel;

  void Start() {

  }

  void Update() {
    if (canPick) ColorWheel.SetActive(true);
    else ColorWheel.SetActive(false);
  }

  public void OnPointerClick(PointerEventData eventData) {
    output = Pick(Camera.main.WorldToScreenPoint(eventData.position), GetComponent<Image>());
  }

  Color Pick(Vector2 screenPoint, Image imageToPick) {
    Vector2 point;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(imageToPick.rectTransform, screenPoint, Camera.main, out point);
    point += imageToPick.rectTransform.sizeDelta / 2;
    Texture2D t = GetComponent<Image>().sprite.texture;
    Vector2Int m_point = new Vector2Int((int)((t.width * point.x) / imageToPick.rectTransform.sizeDelta.x), (int)((t.height * point.y) / imageToPick.rectTransform.sizeDelta.y)); return t.GetPixel(m_point.x, m_point.y);
  }

  public void ShowPicker() {
    if (!canPick) canPick = true;
    else canPick = false;
  }

}


