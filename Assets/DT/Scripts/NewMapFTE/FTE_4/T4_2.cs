using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_2 : BaseGuideStep
{
    public GameObject kuang; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        kuang.SetActive(false); 

        
     yield return   new WaitForSeconds(1);
    }

    public override IEnumerator StepEnd()
    {
        kuang.SetActive(false); 
        yield return   new WaitForSeconds(1);
         
    }

    public override bool ChenkEnd()
    {
        if (NewCanvasUI.My.Panel_Update.activeSelf)
        {
            kuang.SetActive(true); 
        }
        else
        {
            kuang.SetActive(false); 

        }

        for (int i = 0; i <missiondatas.data.Count; i++)
        {
            if(kuang.GetComponent<IsPointOn>().IsOn )
            missiondatas.data[i].isFinish = true;
        }
        return  kuang.GetComponent<IsPointOn>().IsOn  ;
    }

    void Update()
    {
        
    }
}
