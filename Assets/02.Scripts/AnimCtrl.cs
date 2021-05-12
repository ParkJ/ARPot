using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl : MonoBehaviour
{
    private Animator anim;

    private readonly int hashWaterOn = Animator.StringToHash("WaterOn");
    
    private bool isWaterEnough;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        WaterStateCheck();
    }


    void WaterStateCheck()
    {
        /* 
        :::Later:::

        if (BTManager.instance.isWaterEnough != this.isWaterEnough)
        {
            this.isWaterEnough = BTManager.instance.isWaterEnough;
            start coroutine AnimStateUpdate();
        }
        */ 



    }
    IEnumerator AnimStateUpdate()
    {
        if(this.isWaterEnough)
        {
            //Turn Anim hash bool On and Rendering turn off 
            anim.SetBool(hashWaterOn, true);
            yield return new WaitForSeconds(2.0f);
            SetAnimRendererState(this.anim, !isWaterEnough);
        }
        else
        {
            //Turn Anim hash bool Off and Rendering turn on.
            anim.SetBool(hashWaterOn, false);
            SetAnimRendererState(this.anim, !isWaterEnough);
        }
        yield break;
    }

    void SetAnimRendererState(Animator anim, bool state)
    {
        anim.transform.GetComponentInChildren<SkinnedMeshRenderer>().enabled = state;
    }

    

    public void OnAnimClick()
    {
        this.isWaterEnough = !this.isWaterEnough;
        StartCoroutine(AnimStateUpdate());
    }
}
