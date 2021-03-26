using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_3_12 : BaseGuideStep
{

    public BaseMapRole role;
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
        NewCanvasUI.My.GamePause(); 
        NewCanvasUI.My.Panel_NPC.SetActive(true);
        NPCListInfo .My.ShowNpcInfo(role.transform);
        yield return new WaitForSeconds(1);
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(1);

        
    }

      
}
