using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Arrow {
    protected override void Move() {
        rb.velocity = transform.right * speed;
    }
}
