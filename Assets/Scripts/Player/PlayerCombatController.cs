using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatController : MonoBehaviour {
    
    public Transform attackArea;
    private bool isAttacking;
    public float attackTimeDelay = 0.1f;
    public float attackRadius = 0.5f;
    public int attackDamage = 10;
    private float attackTimer;
    private bool canAttack;
    public LayerMask damageableLayer;
    
    private Animator anim;
    
    private string ANIM_ATTACK1 = "attack1";
    private string ANIM_IS_ATTACKING = "isAttacking";

    private void Start() {
        anim = GetComponent<Animator>();
        canAttack = true;
    }

    private void Update() {
        if (!GameController.isGamePlay()) {
            return;
        }
        CheckAttacks();
    }
    

    public void InputAttack(InputAction.CallbackContext ctx) {
        if (ctx.action.WasPerformedThisFrame()) {
            attackTimer = Time.time + attackTimeDelay;
        }
    }


    private void CheckAttacks() {
        if (Time.time < attackTimer && !isAttacking && canAttack) {
            isAttacking = true;
            anim.SetBool(ANIM_ATTACK1, true);
            anim.SetBool(ANIM_IS_ATTACKING, isAttacking);
            attackTimer = 0f;
        }
    }

    private void CheckAttackHitBox() {
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackArea.position, attackRadius, damageableLayer);
        foreach (var col in cols) {
            if (col == null) {
                continue;
            }
            col.transform.SendMessage("TakeDamage", attackDamage);
        }
    }
    
    private void FinishAttack() {
        isAttacking = false;
        anim.SetBool(ANIM_ATTACK1, false);
        anim.SetBool(ANIM_IS_ATTACKING, isAttacking);
    }


    private void EnableAttack() {
        canAttack = true;
    }
    private void DisableAttack() {
        canAttack = false;
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }
}
