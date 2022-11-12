using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : AbstractInteractive
{
    // Start is called before the first frame update
    public TMP_Text dialogueText;
    public GameObject dialogue;
    public string displayText;

    protected override void OnTriggerExit2D(Collider2D other) {
        base.OnTriggerExit2D(other);
        dialogue.SetActive(false);
    }

    protected override void WhenTrigger() {
        dialogueText.text = displayText;
        dialogue.SetActive(true);
    }

    protected override bool TriggerOnlyOnce() {
        return false;
    }
}
