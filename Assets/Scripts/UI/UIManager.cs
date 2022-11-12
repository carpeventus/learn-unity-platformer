using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public RectTransform canvas;
    public RectTransform element;
    public float xOffset;
    public float yOffset;
    
    // Update is called once per frame
    void Update() {
        Vector2 transBinScreenPoint= Camera.main.WorldToScreenPoint(transform.position);
        Vector2 anchorsScreenPointOffset = new Vector2(transBinScreenPoint.x - canvas.sizeDelta.x * (element.anchorMax.x + element.anchorMin.x) * 0.5f + xOffset,
            transBinScreenPoint.y - canvas.sizeDelta.y * (element.anchorMax.y + element.anchorMin.y) * 0.5f + yOffset);
        element.anchoredPosition = anchorsScreenPointOffset;
    }
}
