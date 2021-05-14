using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIButtonManager : MonoBehaviour
{
    private bool state = true;
    private GameObject menu;
    private GameObject menuDetail;
    [SerializeField]
    private GameObject SNSPanel;
    
    //Menu Button Group
    [SerializeField]
    private GameObject Btn_Back;
    [SerializeField]
    private GameObject Btn_Capture;
    [SerializeField]
    private GameObject Btn_GrowSlider;
    [SerializeField]
    private GameObject Btn_Detail;


    private GameObject arManager;

    public GameObject growthSliderObj;

    public GameObject DetailPanel;




    public TMP_Text[] componentTexts;


    public void Start()
    {

        menu = GameObject.FindWithTag("PANEL-MENUBTN");
        menuDetail = GameObject.FindWithTag("PANEL-MENUBACK");

        menu.SetActive(state);
        menuDetail.SetActive(!state);
        SNSPanel.SetActive(false);
        growthSliderObj.SetActive(false);

        arManager = GameObject.FindGameObjectWithTag("ARManager");

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
        growthSliderObj.SetActive(false);
    }
    
    public void OnGrowthButtonClick()
    {
        growthSliderObj.SetActive(!growthSliderObj.activeSelf);
    }

    public void OnTouchCapture()
    {
        Btn_Back.GetComponent<Button>().interactable = false;
        Btn_Capture.GetComponent<Button>().interactable = false;
        Btn_GrowSlider.GetComponent<Button>().interactable = false;
        Btn_Detail.GetComponent<Button>().interactable = false;

        SNSPanel.SetActive(true);        
    }
    public void OnTouchSNSCancel()
    {
        Btn_Back.GetComponent<Button>().interactable = true;
        Btn_Capture.GetComponent<Button>().interactable = true;
        Btn_GrowSlider.GetComponent<Button>().interactable = true;
        Btn_Detail.GetComponent<Button>().interactable = true;

        SNSPanel.SetActive(false);        
    }

    public void OnInfoButtonClick()
    {
        SetDetailPanelData();

        DetailPanel.SetActive(true);

    }

    public void OnDetailExit()
    {
        Debug.Log("OnClick");
        DetailPanel.SetActive(false);
    }

    bool SetDetailPanelData()
    {
        IDictionary data = GameObject.FindGameObjectWithTag("GAMEMANAGER").GetComponent<FBManager>().iTargetDataDict;

        if(data == null)
        {
            Debug.Log("SetDetailPanelData fail");
            return false;
        }

        componentTexts[0].text = (string)data["식재시기"] + "\n" + (string)data["개화시기"];
        componentTexts[1].text = (string)data["최적온도"] + "\n" + (string)data["월동온도"];
        componentTexts[2].text = (string)data["키"] + "\n" + (string)data["꽃크기"];
        componentTexts[3].text = (string)data["물주기"];
        componentTexts[4].text = (string)data["환경조건"] + "\n" + (string)data["난이도"];
        componentTexts[5].text = (string)data["용도"] + "\n" + (string)data["분류"];

        componentTexts[6].text = (string)data["재배포인트"];

        return true;

    }

    public void OnGrowthSliderChanged(GameObject slider)
    {
        arManager.GetComponent<ARButtonManager>().OnGrowthSliderChanged(slider);
    }
}
