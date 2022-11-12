using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : AbstractWeapon
{
    public float moveSpeed;
    public float rotateAngle;
    public float slowdownMultiplier = 0.8f;
    public Vector2 positionOffset;
    private Vector2 startVelocity;
    private Rigidbody2D rb;
    
    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        transform.position = (Vector2)transform.position + positionOffset;
        rb.velocity = playerTransform.right * moveSpeed;
        startVelocity = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, rotateAngle);
        rb.velocity -=  startVelocity * slowdownMultiplier;
        transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, playerTransform.position.y, 0.1f));
        // 返回途中 接近玩家了，要回收（消失）
        if (Vector2.Distance(transform.position, playerTransform.position) < Mathf.Abs(0.5f) &&  Mathf.Sign(startVelocity.x) != Mathf.Sign(rb.velocity.x)) {
            Destroy(gameObject);
        }
    }
    
    protected override bool DestroyWhenDamage() {
        return false;
    }
}
