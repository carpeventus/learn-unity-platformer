using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public Vector2 lastOffset;
    public Vector2 firstOffset;
    public float waitTime = 0.5f;
    public float speed = 1;
    
    private Vector2 pointFirst;
    private Vector2 pointLast;
    private float waitTimer;
    private bool towardsFirst;

    private void Start() {
        pointFirst = (Vector2)transform.position + firstOffset;
        pointLast = (Vector2)transform.position + lastOffset;
        towardsFirst = true;
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    private void Move() {
        Vector2 target = towardsFirst ? pointFirst : pointLast;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target) < Mathf.Epsilon) {
            CheckTurn();
        }
    }

    private void CheckTurn() {
        waitTimer += Time.deltaTime;
        if (waitTimer > waitTime) {
            towardsFirst = !towardsFirst;
            waitTimer = 0f;
        }
    }
}
