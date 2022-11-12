using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatform : MonoBehaviour {

    private Animator anim;
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    private void DisableCollider() {
        GetComponent<Collider2D>().enabled = false;
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }
    

    private void OnTriggerEnter2D(Collider2D col) {
        if (GameController.isPlayerInteractiveCollider(col) &&  col.GetComponent<Transform>().position.y > transform.position.y) {
            anim.SetTrigger("destroy");
        }
    }
}
