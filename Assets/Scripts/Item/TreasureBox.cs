using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : AbstractInteractive {
    private Animator anim;
    public GameObject coinPrefab;

    protected void Start() {
        anim = GetComponent<Animator>();
    }

    protected override void WhenTrigger() {
       anim.SetBool("open", true);
    }

    private void GenerateTreasure() {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }

    protected override bool TriggerOnlyOnce() {
        return true;
    }
}
