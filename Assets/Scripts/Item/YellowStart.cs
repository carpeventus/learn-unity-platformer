using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowStart : MonoBehaviour {
    public GameObject[] giftPrefabs;
    
    public void TakeDamage(int damage) {
        Instantiate(giftPrefabs[Random.Range(0, giftPrefabs.Length)], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
