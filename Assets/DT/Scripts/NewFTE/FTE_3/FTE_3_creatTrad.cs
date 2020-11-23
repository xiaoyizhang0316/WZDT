using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_3_creatTrad : BaseGuideStep
{
    public BaseMapRole gas;

    public BaseMapRole dealer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        NewCanvasUI.My.Panel_Update.transform.localPosition+=new Vector3(0,9000,0);
        NewCanvasUI.My.Panel_NPC.transform.localPosition+=new Vector3(0,9000,0);
      TradeManager.My.transform.localPosition+=new Vector3(0,9000,0);
        yield return null;
    }

    public override bool ChenkEnd()
    {
      return   TradeManager.My.CheckTwoRoleHasTrade(gas.baseRoleData, dealer.baseRoleData);
    }

    public override IEnumerator StepEnd()
    {
        NewCanvasUI.My.Panel_Update.transform.localPosition=new Vector3(0,0,0);
        NewCanvasUI.My.Panel_NPC.transform.localPosition=new Vector3(0,0,0);
        TradeManager.My.transform.localPosition=new Vector3(0,0,0);

        NewCanvasUI.My.Panel_Update.SetActive(false);
        NewCanvasUI.My.Panel_NPC.SetActive(false);
        TradeManager.My.gameObject.SetActive(false);
        yield return null;
       
    }

    void Update()
    {
        
    }
}
