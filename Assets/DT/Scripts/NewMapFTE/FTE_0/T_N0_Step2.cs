using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N0_Step2 : BaseGuideStep
{
    public GameObject box_consumer;
    public GameObject box_consumer_detail;
    public GameObject hand1;
    public GameObject hand2;
    public GameObject wave;
    public GameObject c_mask;
    public GameObject cd_mask;
    public override IEnumerator StepStart()
    {
        isStepEnd = false;
        isShow = false;
        yield return null;
        box_consumer.SetActive(true);
        hand1.SetActive(true);
        c_mask.SetActive(true);
        InvokeRepeating("Check", 0.5f, 0.5f);
    }

    private bool isStepEnd = false;
    private bool isShow = false;
    void Check()
    {
        
        if (wave.activeInHierarchy)
        {
            if (!isShow)
            {
                isShow = true;
                box_consumer.SetActive(false);
                hand1.SetActive(false);
                c_mask.SetActive(false);
                hand2.SetActive(true);
                box_consumer_detail.SetActive(true);
                cd_mask.SetActive(true);
            }
        }
        else
        {
            isShow = false;
            box_consumer.SetActive(true);
            hand1.SetActive(true);
            c_mask.SetActive(true);
            hand2.SetActive(false);
            box_consumer_detail.SetActive(false);
            cd_mask.SetActive(false);
        }
        
        if (isShow)
        {
            if (!box_consumer_detail.activeInHierarchy)
            {
                isStepEnd = true;
                hand2.SetActive(false);
                cd_mask.SetActive(false);
            }
        }
    }

    public override bool ChenkEnd()
    {
        return isStepEnd;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
