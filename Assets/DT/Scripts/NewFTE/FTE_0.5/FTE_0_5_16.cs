using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_16 : BaseGuideStep
{
 
    public  GameEnum.ConsumerType type; 
    public int count;
    public int time;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        InvokeRepeating("Addxiaofei",1,time);
       
        yield return new WaitForSeconds(1f);

    }

    public void Addxiaofei()
        {
            StartCoroutine(BuildingManager.My.buildings[0]
                .BornSingleTypeConsumer(type, count));
            StageGoal.My.killNumber = 0;
        }

    public override IEnumerator StepEnd()
    {
        CancelInvoke("Addxiaofei"); 
        yield break;
    }

    public override bool ChenkEnd()
    {
      

        if (StageGoal.My.killNumber > missiondatas.data[1].maxNum)
        {
            missiondatas.data[1].isFinish = true;
            return true;

        }
     
      
     
            return false;
       

    }
   
}
 