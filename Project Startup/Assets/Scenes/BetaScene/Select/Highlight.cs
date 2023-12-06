using UnityEngine;

public class Highlight : MonoBehaviour {
  private SpriteRenderer spriteRenderer;
  private Color originalColor;
  private Color outlineColor = new Color(1f, 0f, 0f); // Red
  private Color hoverColor = new Color(1.5f, 1.5f, 1.5f, 0.5f); // Grey with 50% alpha
  private bool isOutlined = false;

  void Start() {
    spriteRenderer = GetComponent<SpriteRenderer>();
    originalColor = spriteRenderer.color;
  }

  void OnMouseDown() {
    ToggleOutline();
  }

  void OnMouseEnter() {
    ToggleHover();
  }

  void OnMouseExit() {
    if (isOutlined) {
      // If it's outlined, revert to the outline color
      spriteRenderer.color = outlineColor;
    }
    else {
      // Otherwise, revert to the original color
      spriteRenderer.color = originalColor;
    }
  }

  void ToggleOutline() {
    if (!isOutlined) {
      spriteRenderer.color = outlineColor;
      isOutlined = true;
    }
    else {
      spriteRenderer.color = originalColor;
      isOutlined = false;
    }
  }

  void ToggleHover() {
    // Only apply hover effect if the object is not outlined (red)
    if (!isOutlined) {
      spriteRenderer.color = hoverColor;
    }
  }
}
