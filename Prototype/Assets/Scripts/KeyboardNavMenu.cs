using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardNavMenu : MonoBehaviour
{
    public GameObject firstClick;
    void Start()
    {
        //EventSystem.current.SetSelectedGameObject(firstClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }   
    }
}
