using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrashBinBar : MonoBehaviour {

    public static TrashBinBar Instance;
    public int maxNum;
    private int currentNUm;
    private Image trashBinCoin;

    public TMP_Text trashBinCoinText;

    private void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start() {
        trashBinCoin = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        trashBinCoin.fillAmount = (float) currentNUm / maxNum;
        trashBinCoinText.text = currentNUm + "/" + maxNum;
    }

    public void DropCoinInTrashBin(int num) {
        currentNUm += num;
    }
}
