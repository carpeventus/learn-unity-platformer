using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatPoint : MonoBehaviour {
    [SerializeField] private float time;

    private RectTransform _rectTransform;
    private TMP_Text text;
    private float timer;
    // Start is called before the first frame update
    void Start() {
        _rectTransform = GetComponent<RectTransform>();
        text = GetComponent<TMP_Text>();
        timer = time;
    }

    private void Update() {
        if (timer > 0) {
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y + 0.01f);
            text.fontSize -= 0.02f;
            timer -= Time.deltaTime;
        }
        else {
            Destroy(gameObject);
        }
    }
}
