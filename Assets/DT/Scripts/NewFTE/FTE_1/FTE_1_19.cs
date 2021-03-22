using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_19 : BaseGuideStep
{

    public UpdateRole update;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
 

    public override IEnumerator StepStart()
    {
       ProductDetalUI.My.panel.SetActive(false);
        yield return new WaitForSeconds(0.2f);     
    }

    public override IEnumerator StepEnd()
    {
       
        yield break;
    }

    public override bool ChenkEnd()
    {
        if (StageGoal.My.extraCost.ContainsKey("升级") && StageGoal.My.extraCost["升级"] >= 10)
        {
            //Debug.Log("正确返回");
            NewCanvasUI.My.Panel_Update.SetActive(false);
            return true;
        }
        else
        {
           // Debug.Log("失败返回"+StageGoal.My.extraCost.ContainsKey("升级")+":"+(StageGoal.My.extraCost["升级"] >= 2400));
           //Debug.Log("失败"); 
           return false;
        }
    }
}
