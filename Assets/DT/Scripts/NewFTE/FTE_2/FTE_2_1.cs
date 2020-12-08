using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_1 : BaseGuideStep
{
    public BaseMapRole seed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InitHighlightUI()
    {
     //   highLight2DObjList.Add(WaveCount.My.transform.GetChild(0).GetChild(3).GetChild(0).gameObject);
    }

    public override IEnumerator StepStart()
    {
         NewCanvasUI.My.GamePause(false);
         
         RoleListManager.My.OutButton();
         yield return new  WaitForSeconds(2f);
         
    }

    public override IEnumerator StepEnd()
    {
        yield break;
        
    }

    public override bool ChenkEnd()
    {
        return true;
    }
}
