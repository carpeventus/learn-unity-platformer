using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : AbstractWeapon
{
    public float speed;
    public float maxDistance;
    private Vector2 startPoint;
    protected Rigidbody2D rb;
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        startPoint = transform.position;
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(startPoint, transform.position) > maxDistance) {
            Destroy(gameObject);
        }
    }

    protected virtual void Move() {
        rb.velocity = playerTransform.right * speed;
    }
}
