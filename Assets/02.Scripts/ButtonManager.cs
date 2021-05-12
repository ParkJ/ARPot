using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject ShipGroup;

    public GameObject aminCharObj;

    public Image targetPanel;

    private Camera arCam;

    private List<GameObject> targetObjList = new List<GameObject>();

    private GameObject selectedTarget;

    private Vector2 camCenterPos;

    private Color tpColor;



    public void Start()
    {
        arCam = Camera.main;
        camCenterPos = new Vector2(Screen.width/2, Screen.height/2);
        tpColor = targetPanel.color;

    }

    public void Update()
    {
        OnTargetSearchButtonClick();
    }

    public void OnBuyButtonClick()
    {
        ShipGroup.SetActive(true);
        Invoke("SceneChange", 3.0f);
    }

    void SceneChange()
    {
        SceneManager.LoadScene("ARScene");
    }


    public void OnInfoClick()
    {
        
    }

    public void OnCaptureClick()
    {

    }

    public void OnGrowthClick()
    {

    }

    public void OnAnimClick()
    {
        aminCharObj.SetActive(false);
    }

    public void OnVuTargetFound(GameObject newTarget)
    {
        if(!targetObjList.Contains(newTarget))
        {
            targetObjList.Add(newTarget);
        }
    }
    
    public void OnVuTargetLost(GameObject oldTarget)
    {
        if(targetObjList.Contains(oldTarget))
        {
            targetObjList.Remove(oldTarget);
        }
    }

    public void OnTargetSearchButtonClick()
    {
        GameObject nearestTarget = SearchTargetInArea();
        SelectedTargetUpdate(nearestTarget);
    }

    GameObject SearchTargetInArea()
    {
        GameObject nearestTarget = null;

        float minDist = 987654321;
        foreach(var obj in targetObjList)
        {
            Vector3 objScreenPos = arCam.WorldToScreenPoint(obj.transform.position);
            Vector2 objectXYPos = new Vector2(objScreenPos.x, objScreenPos.y);

            float curDist = Vector2.Distance(camCenterPos, objectXYPos);

            if(minDist > curDist)
            {
                nearestTarget = obj;
                minDist = curDist;
            }
        }
        return nearestTarget;
    }

    void SelectedTargetUpdate(GameObject nearestTarget)
    {
        if(nearestTarget)
        {
            if(!this.selectedTarget)
            {
                this.selectedTarget = nearestTarget;
                SetSelectedTargetColor(this.selectedTarget, true);
            }
            else if(this.selectedTarget.name != nearestTarget.name)
            {
                SetSelectedTargetColor(this.selectedTarget, false);
                this.selectedTarget = nearestTarget;
                SetSelectedTargetColor(this.selectedTarget, true);
            }
        }
        else if(this.selectedTarget)
        {
            SetSelectedTargetColor(this.selectedTarget, false);
            this.selectedTarget = null;
        }
    }

    void SetSelectedTargetColor(GameObject obj, bool isSelected)
    {
        if(isSelected)
        {
            obj.transform.Find("Canvas").transform.Find("Panel").GetComponent<Image>().color = new Color(this.tpColor.r, this.tpColor.g, this.tpColor.b, this.tpColor.a);
        }
        else
        {
            obj.transform.Find("Canvas").transform.Find("Panel").GetComponent<Image>().color = new Color(this.tpColor.r, this.tpColor.g, this.tpColor.b, 60);
        }
    }
}
