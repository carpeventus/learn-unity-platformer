using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform shootPosition;

    // Update is called once per frame
    void Update() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        // 枪指向鼠标的方向，指向被减向量
        Vector3 gunDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Instantiate(bulletPrefab, shootPosition.position, Quaternion.Euler(transform.eulerAngles));
        }
    }
}
