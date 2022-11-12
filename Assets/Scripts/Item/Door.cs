using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : AbstractInteractive
{

    protected override void WhenTrigger() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    protected override bool TriggerOnlyOnce() {
        return true;
    }
}
