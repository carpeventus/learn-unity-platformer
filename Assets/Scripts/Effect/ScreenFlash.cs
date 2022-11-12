using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour {

    public static ScreenFlash Instance;
    private Image image;
    private Color defaultColor;
    // Start is called before the first frame update

    private void Awake() {
        Instance = this;
    }

    void Start() {
        image = GetComponent<Image>();
        defaultColor = image.color;
    }

    public void FlashScreen(Color color, float time) {
        StartCoroutine(Flash(color, time));
    }

    private IEnumerator Flash(Color color, float time) {
        image.color = color;
        yield return new WaitForSeconds(time);
        image.color = defaultColor;
    }
}
