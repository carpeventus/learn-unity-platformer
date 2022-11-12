using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bomb : AbstractWeapon {
    private Rigidbody2D rb;
    private Animator anim;
    
    public float upForce;
    public float horizontalSpeed;
    public float explosionDelay;
    public Vector2 positionOffset;
    public Transform attackArea;
    public float attackRadius;
    public LayerMask playerLayerMask;
    public LayerMask damageableLayerMask;
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.position =  positionOffset + (Vector2)transform.position;
        rb.velocity = playerTransform.up * upForce + playerTransform.right * horizontalSpeed;
        Invoke(nameof(Explode), explosionDelay);
    }

    void Explode() {
        anim.SetTrigger("explode");
    }

    void Gone() {
        Destroy(gameObject);
    }
    
    private void CheckAttackHitBox() {
        Collider2D[] playerCols = Physics2D.OverlapCircleAll(attackArea.position, attackRadius, playerLayerMask);
        foreach (var col in playerCols) {
            if (col == null) {
                continue;
            }
            if (GameController.isPlayerInteractiveCollider(col)) {
                col.transform.SendMessage("TakeDamage", damage);
            }
        }
        Collider2D[] enemyCols = Physics2D.OverlapCircleAll(attackArea.position, attackRadius, damageableLayerMask);
        foreach (var col in enemyCols) {
            if (col == null) {
                continue;
            }
            col.transform.SendMessage("TakeDamage", damage);
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }
    
    
    protected override void OnTriggerEnter2D(Collider2D col) {
        // do nothing
    }

}
