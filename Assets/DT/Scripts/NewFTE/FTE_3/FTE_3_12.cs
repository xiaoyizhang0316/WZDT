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
        yield return new WaitForSeconds(1);
        NewCanvasUI.My.Panel_NPC.GetComponent<NPCListInfo>().ShowNpcInfo(role.transform);
         
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(1);

        
    }

      
}
