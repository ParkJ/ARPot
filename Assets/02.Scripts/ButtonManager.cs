using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject ShipGroup;

    public GameObject aminCharObj;

    private Camera arCam;

    private List<GameObject> targetObjList = new List<GameObject>();

    private GameObject selectedTarget;

    private Vector2 camCenterPos;



    public void Start()
    {
        arCam = Camera.main;
        camCenterPos = new Vector2(Screen.width/2, Screen.height/2);

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
                this.selectedTarget.transform.Find("Canvas").transform.Find("Panel - Bold").GetComponent<Image>().enabled = true;
            }
            else if(this.selectedTarget.name != nearestTarget.name)
            {
                this.selectedTarget.transform.Find("Canvas").transform.Find("Panel - Bold").GetComponent<Image>().enabled = false;
                this.selectedTarget = nearestTarget;
                this.selectedTarget.transform.Find("Canvas").transform.Find("Panel - Bold").GetComponent<Image>().enabled = true;
            }
        }
        else if(this.selectedTarget)
        {
            this.selectedTarget.transform.Find("Canvas").transform.Find("Panel - Bold").GetComponent<Image>().enabled = false;
            this.selectedTarget = null;
        }
    }
}
