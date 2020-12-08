using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_17 : BaseGuideStep
{
   

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isover;

    public override void InitHighlight3d()
    {
        Camera3DTarget[0].radius = 0;
        Camera3DTarget[0].EndRandius= 80;
        Camera3DTarget[0].target= PlayerData.My.MapRole[0].transform;
        Camera3DTarget[0].speed=2 ;
        
    }

    public override IEnumerator StepStart()
    {
        NewCanvasUI.My.GamePause(false);
        PlayAnim();
        NewCanvasUI.My.Panel_Update.transform.localPosition = new Vector3(0,0,0);
        yield return new WaitForSeconds(0.2f);
        NewCanvasUI.My.Panel_Update.SetActive(false);
    }

    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override bool ChenkEnd()
    {
        if (NewCanvasUI.My.Panel_Update.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
