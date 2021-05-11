using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonManager : MonoBehaviour
{
    private bool state = true;
    private GameObject menu;
    private GameObject menuDetail;

    public void Start()
    {
        menu = GameObject.FindWithTag("PANEL-MENUBTN");
        menuDetail = GameObject.FindWithTag("PANEL-MENUBACK");

        menu.SetActive(state);
        menuDetail.SetActive(!state);
    }

    public void OnTouchMenu()
    {
        menu.SetActive(!state);
        menuDetail.SetActive(state);
    }

    public void OnTouchMenuBack()
    {
        menu.SetActive(state);
        menuDetail.SetActive(!state);
    }
   
}
