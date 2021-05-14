using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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



    public GameObject growthSliderObj;

    private GameObject arManager;


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
        IDictionary data = GameObject.FindGameObjectWithTag("GAMEMANAGER").GetComponent<FBManager>().iTargetDataDict;

        if(data != null)
        {
            TargetInfo newTarget = new TargetInfo(data);
            
        }
    }


    public void OnGrowthSliderChanged(GameObject slider)
    {
        arManager.GetComponent<ARButtonManager>().OnGrowthSliderChanged(slider);
    }

    public class TargetInfo
    {
        private string name;

        public TargetInfo(IDictionary data)
        {
            this.name = (string)data["name"];
        }

        public string getInfoString()
        {
            string infoString = "";

            infoString += $"name : {this.name} \n";

            return infoString;
        }
    }
}
