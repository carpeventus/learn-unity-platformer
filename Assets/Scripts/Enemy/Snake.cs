using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : AbstractEnemy {

    private Rigidbody2D rb;
    [SerializeField] private float xOffset;
    private float leftLimit;
    private float rightLimit;
    private bool faceRight;
    
    protected override void Move() {
        rb.velocity = new Vector2(faceRight ? moveSpeed : -moveSpeed, rb.velocity.y);

        if (!faceRight && transform.position.x < leftLimit ) {
            Flip();
            rb.velocity = Vector2.zero;
        }
        else if (faceRight && transform.position.x > rightLimit) {
            Flip();
            rb.velocity = Vector2.zero;
        }
    }

    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        leftLimit = transform.position.x - xOffset;
        rightLimit = transform.position.x + xOffset;
        faceRight = true;
    }
    
    void Flip() {
        faceRight = !faceRight;
        transform.rotation = Quaternion.Euler(0, faceRight ? 0 : 180, 0);
    }
    
}
