using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_4 : BaseGuideStep
{
    public GameObject kuang;
    public GameObject hand;
    // Start is called before the first frame update
 
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        kuang.SetActive(false);

//        hand.transform.position = PlayerData.My.MapRole[PlayerData.My.MapRole.Count - 1].transform.position;
        
     yield return  null;
    }

    public override IEnumerator StepEnd()
    {
        kuang.SetActive(true);
//        hand.SetActive(false);
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
        return kuang.GetComponent<IsPointOn>().IsOn;
    }
 
}
