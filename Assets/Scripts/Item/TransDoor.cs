using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransDoor : AbstractInteractive {
    public Transform anotherDoor;
    private Transform playerTransForm;
    // Start is called before the first frame update
    void Start() {
        playerTransForm = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    protected override void WhenTrigger() {
        playerTransForm.position = anotherDoor.position;
    }

    protected override bool TriggerOnlyOnce() {
        return false;
    }
}
