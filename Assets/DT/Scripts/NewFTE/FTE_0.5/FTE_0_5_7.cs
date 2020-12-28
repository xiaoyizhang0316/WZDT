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
      
    

        role.OnMoved += ChangeColor;

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
    public void ChangeColor(ProductData data)
    {
        if (data.damage >240)
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.seerJC1_ran,FTE_0_5Manager.My.sg );
        }

        else
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.seerJC1_ran,FTE_0_5Manager.My.sr ); 
        }
    }
}
