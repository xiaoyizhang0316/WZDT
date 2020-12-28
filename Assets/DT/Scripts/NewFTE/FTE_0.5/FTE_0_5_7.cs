using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_7 : BaseGuideStep
{
    public  BaseMapRole role;
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
      
       yield break; 
    }


   
    public override bool ChenkEnd()
    {
      
   
        
                    for (int j = 0; j <  role.warehouse.Count; j++)
                    {
                        if (role.warehouse[j].damage < 250)
                        {
                            role.warehouse.Remove(role.warehouse[j]);
                        }
                    }
                   
       
            missiondatas.data[0].currentNum = role.warehouse.Count;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true; 
                return true;
            }
            else
            {
                return false;
            }

    }
 
}
