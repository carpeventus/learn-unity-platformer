using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractEnemy : MonoBehaviour, Damageable {

    [Header("Health")]
    public int maxHealth;
    public float flashColorTime;
    public int attack;
    public float attackBack;
    
    private int health;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("Move")]
    public float moveSpeed;

    [Header("Effects")]
    public GameObject bloodEffectPrefab;
    public GameObject floatPointPrefab;
    [Header("Gift")]
    public GameObject coinPrefab;
    

    // Start is called before the first frame update
    protected virtual void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        health = maxHealth;
    }

    protected abstract void Move();

    // Update is called once per frame
    protected virtual void FixedUpdate() {
        Move();
    }

    public void TakeDamage(int attackDamage) {
        Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
        GenerateDamageText(attackDamage);
        CinemachineShake.Instance.Shake(2f, 0.5f);
        FlashSpite(new Color(0.6f, 0f, 0f, 0.6f), flashColorTime);
        health -= attackDamage;
        CheckHealth();
    }

    private void GenerateDamageText(int attackDamage) {
        var gameObject = Instantiate(floatPointPrefab, transform.position, Quaternion.identity);
        TextMeshPro damageText = gameObject.GetComponent<TextMeshPro>();
        damageText.text = attackDamage.ToString();
    }
    
    public void FlashSpite(Color color, float time) {
        StartCoroutine(Flash(color, time));
    }

    private IEnumerator Flash(Color color, float time) {
        spriteRenderer.color = color;
        yield return new WaitForSeconds(time);
        spriteRenderer.color = originalColor;
    }
    
    private void CheckHealth() {
        if (health <=0 ) {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (GameController.isPlayerInteractiveCollider(col)) {
            PlayerHealth playerHealth = col.gameObject.GetComponent<PlayerHealth>();
            if (col.transform.position.x <= transform.position.x) {
                playerHealth.TakeDamageWithKnockBack(attack, -attackBack);
            }else {
                playerHealth.TakeDamageWithKnockBack(attack, attackBack);
            }
        }
    }
}
