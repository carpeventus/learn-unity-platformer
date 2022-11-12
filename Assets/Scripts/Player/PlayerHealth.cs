using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerHealth : MonoBehaviour, Damageable {
    private Animator anim;
    private Rigidbody2D rb;
    private PolygonCollider2D hurtCollider;
    public float health { get; private set; }
    public float maxHealth;

    private bool isHurt;
    private const string HURT_ANIM = "isHurt";
    private const string DEATH_ANIM = "death";
    private bool isDeath;
    
    [SerializeField] private UnityEvent unityEvent;
    
    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hurtCollider = GetComponent<PolygonCollider2D>();
    }
    
    public void TakeDamageWithKnockBack(int damage, float knockBack) {
        isHurt = true;
        anim.SetBool(HURT_ANIM, isHurt);
        DecreaseHealthByDamage(damage);
        HurtBack(knockBack);
        ScreenFlash.Instance.FlashScreen(new Color(0.6f, 0, 0 ,0.1f), 0.1f);
        StartCoroutine(ChangeColliderEnable());
        CheckHealth();
    }
    
    private IEnumerator ChangeColliderEnable() {
        hurtCollider.enabled = false;
        yield return new WaitForSeconds(1);
        hurtCollider.enabled = true;
    }
    
    public void TakeDamage(int damage) {
        TakeDamageWithKnockBack(damage, 0f);
    }

    private void HurtBack(float knockBack) {
        rb.velocity = new Vector2(knockBack, rb.velocity.y);
    }

    private void DecreaseHealthByDamage(float damage) {
        health -= damage;
        if (health < 0) {
            health = 0;
        }
    }

    private void FinishHurt() {
        isHurt = false;
        anim.SetBool(HURT_ANIM, isHurt);
    }

    private void Death() {
        unityEvent.Invoke();
        Destroy(gameObject);
    }

    private void CheckHealth() {
        if (health <= 0 && !isDeath) {
            GameController.isGameAlive = false;
            isDeath = true;
            anim.SetBool(DEATH_ANIM, isDeath);
        }
    }
}
