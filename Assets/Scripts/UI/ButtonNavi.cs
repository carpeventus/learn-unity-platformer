using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonNavi : MonoBehaviour {
    private GameObject lastSelected;
    
    // Update is called once per frame
    void Update()
    {
        if (ReferenceEquals(EventSystem.current.currentSelectedGameObject, null)) {
            EventSystem.current.SetSelectedGameObject(lastSelected);
        } else {
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }
    }
}
