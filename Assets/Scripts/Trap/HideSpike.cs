using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpike : MonoBehaviour {

    private Animator anim;

    public Transform attackArea;
    public int damage;

    public Vector2 size;
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (GameController.isPlayerInteractiveCollider(col)) {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack() {
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("attack");
    }

     
    private void CheckAttackHitBox() {
        Collider2D[] playerCols = Physics2D.OverlapBoxAll(attackArea.position, size ,0,LayerMask.GetMask("Player"));
        foreach (var col in playerCols) {
            if (col == null) {
                return;
            }
            if (GameController.isPlayerInteractiveCollider(col)) { 
                col.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(attackArea.position, size);
    }
}
