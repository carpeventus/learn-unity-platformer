using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {
    [SerializeField] protected int damage;
    
    private void OnTriggerEnter2D(Collider2D col) {
        if (GameController.isPlayerInteractiveCollider(col)) {
            col.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
