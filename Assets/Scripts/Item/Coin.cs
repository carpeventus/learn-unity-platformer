using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col) {
        if (GameController.isPlayerInteractiveCollider(col)) {
            SoundManager.Instance.PlayPickupCoinSound();
            CoinUI.Instance.IncreaseCoinNum(1);
            Destroy(gameObject);
        }
    }
}
