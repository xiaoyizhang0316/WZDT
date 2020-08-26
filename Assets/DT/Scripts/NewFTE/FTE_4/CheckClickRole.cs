using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckClickRole : BaseGuideStep
{

    public Transform targetNPC;

    public override IEnumerator StepEnd()
    {
        NewCanvasUI.My.Panel_NPC.SetActive(true);
        NPCListInfo.My.ShowUnlckPop(targetNPC);
        yield break;
    }

    public override IEnumerator StepStart()
    {
        yield break;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
