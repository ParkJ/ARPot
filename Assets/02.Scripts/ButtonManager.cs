using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject ShipGroup;
    public GameObject flowerCharPref;

    private List<GameObject> targetObjList = new List<GameObject>();

    private GameObject selectedTarget;

    private Vector2 camCenterPos;

    public void Start()
    {
        camCenterPos = new Vector2(Screen.width/2, Screen.height);
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
    //     if(flowerCharPref.activeSelf)
    //     {
    //         flowerCharPref.SetActive(false);
    //     }
    //     else
    //     {
    //         flowerCharPref.SetActive(true);
    //     }
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

    public void SearchTargetInArea()
    {
        GameObject nearestObject;

        foreach(var obj in targetObjList)
        {
            Debug.Log(obj.name);
        }
    }
}
