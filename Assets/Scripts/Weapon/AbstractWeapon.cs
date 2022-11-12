using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractWeapon : MonoBehaviour {
    public int damage;
    protected Transform playerTransform;
    // Start is called before the first frame update
    protected virtual void Start() {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    
    
    protected virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Enemy")) {
            col.GetComponent<AbstractEnemy>().TakeDamage(damage);
            if (DestroyWhenDamage()) {
                Destroy(gameObject);
            }
        }
    }
    

    protected virtual bool DestroyWhenDamage() {
        return true;
    }
    
}
