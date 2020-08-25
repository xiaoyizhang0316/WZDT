using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_2 : BaseGuideStep
{
    public GameObject land;
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
        land.GetComponent<MapSign>().isCanPlace = true;
        yield return new WaitForSeconds(0.2f); 
    }

    public override IEnumerator StepEnd()
    {

        
        yield break;
    }

    public override bool ChenkEnd()
    {
        if (land.GetComponent<MapSign>().baseMapRole != null&&land.GetComponent<MapSign>().baseMapRole.baseRoleData.inMap)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
