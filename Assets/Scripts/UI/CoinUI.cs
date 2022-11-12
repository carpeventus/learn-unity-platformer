using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour {
    public static CoinUI Instance;
    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    public int coinNum { set; get; }
    public TMP_Text coinText;

    // Update is called once per frame
    void Update() {
        coinText.text = coinNum.ToString();
    }

    public void IncreaseCoinNum(int num) {
        coinNum += num;
    }
    public void DecreaseCoinNum(int num) {
        coinNum -= num;
        if (coinNum <= 0) {
            coinNum = 0;
        }
    }
}
