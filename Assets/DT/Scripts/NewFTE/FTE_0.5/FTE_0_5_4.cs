using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;

public class FTE_0_5_4 : BaseGuideStep
{

    public GameObject roleInfo;
    public GameObject bulletInfo;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
      
        yield return new WaitForSeconds(1f);
     
    }

    public override IEnumerator StepEnd()
    {
       
        missiondatas.data[0].isFinish= true; 
        
      yield break; 
    }

    public override bool ChenkEnd()
    {
        if (roleInfo.activeSelf)
        {
            missiondatas.data[0].currentNum= 1;
            missiondatas.data[0].isFinish= true;
        }

        if (bulletInfo.activeSelf)
        {
            missiondatas.data[1].currentNum= 1;
            missiondatas.data[1].isFinish= true;
        }

        return false;

    }

 
}
