using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bat : AbstractEnemy
{
    private Transform target;
    private bool follow;
    public float discoverDistance;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        follow = true;
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    
    protected override void Move() {
        if (!follow) {
            return;
        }
        if (Vector2.Distance(transform.position, target.position) < discoverDistance) {
            transform.position =Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    public void StopFollow() {
        follow = false;
    }

}
