using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideMenu : MonoBehaviour
{
    [SerializeField] KeyCode toggleKey = KeyCode.O;
    [SerializeField] GameObject menu = null;

    void Update()
    {
        if(Input.GetKeyDown(toggleKey))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
}
