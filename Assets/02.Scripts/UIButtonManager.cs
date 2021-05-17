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

    private bool arTargetingState = true;

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

    void Update()
    {
        ARTargetingValidCheck();
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
        if(!growthSliderObj.activeSelf)
        {
            Btn_Back.GetComponent<Button>().interactable = false;
            Btn_Capture.GetComponent<Button>().interactable = false;
            Btn_Detail.GetComponent<Button>().interactable = false; 
        }
        else
        {
            Btn_Back.GetComponent<Button>().interactable = true;
            Btn_Capture.GetComponent<Button>().interactable = true;
            Btn_Detail.GetComponent<Button>().interactable = true; 
            growthSliderObj.GetComponent<Slider>().value = 0;
        }
        growthSliderObj.SetActive(!growthSliderObj.activeSelf);
    }

    public void OnGrowthSliderChanged(GameObject slider)
    {
        arManager.GetComponent<ARButtonManager>().OnGrowthSliderChanged(slider);
    }

    public void OnTouchCapture()
    {

        SetInteractable(false);
        SNSPanel.SetActive(true);        
    }
    public void OnTouchSNSCancel()
    {
        SetInteractable(true);
        SNSPanel.SetActive(false);        
    }

    public void OnInfoButtonClick()
    {
        SetInteractable(false);
        SetDetailPanelData();
        DetailPanel.SetActive(true);

    }

    public void OnDetailExit()
    {
        SetInteractable(true);
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

        componentTexts[0].text = "식재시기 : " + (string)data["식재시기"] + "\n" +  "개화시기 : " + (string)data["개화시기"];
        componentTexts[1].text = "최적온도 : " + (string)data["최적온도"] + "\n" + "월동온도 : " + (string)data["월동온도"];
        componentTexts[2].text = "키 : " + (string)data["키"] + "\n" + "꽃크기 : " + (string)data["꽃크기"];
        componentTexts[3].text = "물주기 : " + (string)data["물주기"];
        componentTexts[4].text = "환경조건 : " + (string)data["환경조건"] + "\n" + "난이도 : " + (string)data["난이도"];
        componentTexts[5].text = "용도 : " + (string)data["용도"] + "\n" + "분류 : " + (string)data["분류"];

        componentTexts[6].text = (string)data["재배포인트"];

        return true;

    }
    void SetInteractable(bool state)
    {
        Btn_Back.GetComponent<Button>().interactable = state;
        Btn_Capture.GetComponent<Button>().interactable = state;
        Btn_GrowSlider.GetComponent<Button>().interactable = state;
        Btn_Detail.GetComponent<Button>().interactable = state;
    }

    void ARTargetingValidCheck()
    {   
        bool curState;

        if(
        Btn_Back.GetComponent<Button>().interactable &&
        Btn_Capture.GetComponent<Button>().interactable &&
        Btn_GrowSlider.GetComponent<Button>().interactable &&
        Btn_Detail.GetComponent<Button>().interactable
        )
        {
            curState = true;
        }
        else
        {
            curState = false;
        }


        if(curState != this.arTargetingState)
        {
            this.arTargetingState = curState;
            this.arManager.GetComponent<ARButtonManager>().ARTargetingStateChanged(curState);
        }
    }
}
