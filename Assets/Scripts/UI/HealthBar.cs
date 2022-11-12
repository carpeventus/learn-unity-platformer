using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    private Image health;
    private PlayerHealth playerHealth;

    public TMP_Text healthText;

    // Start is called before the first frame update
    void Start() {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        health = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        health.fillAmount = (float) playerHealth.health / playerHealth.maxHealth;
        healthText.text = playerHealth.health + "/" + playerHealth.maxHealth;
    }
}
