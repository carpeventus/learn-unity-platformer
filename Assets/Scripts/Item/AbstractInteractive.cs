using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInteractive : MonoBehaviour {

    protected bool collision;
    protected bool canInteractive = true;

    private PlayerInputActions _playerInputActions;

    private void Awake() {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Interactive.performed += ctx => InputInteractive();
        _playerInputActions.Enable();
    }

    private void OnEnable() {
        _playerInputActions.Enable();
    }

    private void OnDisable() {
        _playerInputActions.Disable();
    }

    public void InputInteractive() {
        if (collision && canInteractive) {
            WhenTrigger();
            if (TriggerOnlyOnce()) {
                canInteractive = false;
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col) {
        if (GameController.isPlayerInteractiveCollider(col)) {
            collision = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        collision = false;
        if (!TriggerOnlyOnce()) {
            canInteractive = true;
        }
    }
    
    protected abstract void WhenTrigger();

    protected abstract bool TriggerOnlyOnce();
}
