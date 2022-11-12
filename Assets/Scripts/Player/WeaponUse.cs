using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponUse : MonoBehaviour {

    public GameObject[] weaponPrefabs;
    
    private int currentIndex;
    // Start is called before the first frame update

    public void InputChangeItem(InputAction.CallbackContext ctx) {
        if (ctx.action.WasPerformedThisFrame()) {
            ChangeWeapon();
        }
    }
    
    public void InputItem(InputAction.CallbackContext ctx) {
        if (ctx.action.WasPerformedThisFrame()) {
            Instantiate(weaponPrefabs[currentIndex], InstantiatePosition(), Quaternion.identity);
        }
    }

    private void ChangeWeapon() {
        currentIndex++;
        if (currentIndex % weaponPrefabs.Length == 0) {
            currentIndex = 0;
        }
    }

    protected virtual Vector2 InstantiatePosition() {
        return transform.position;
    }
}
