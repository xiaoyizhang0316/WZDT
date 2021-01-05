using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_1 : BaseGuideStep
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isover;
    
    public override IEnumerator StepStart()
    {
        NewCanvasUI.My.GamePause();
       RoleListManager.My.OutButton();
       NewCanvasUI.My.Panel_Update.SetActive(true);
       RoleUpdateInfo.My.changeRoleButton.gameObject.SetActive(false);
       NewCanvasUI.My.Panel_Update.transform.localPosition = new Vector3(0,5000,0);
       
       
       isover = false;
       yield return new WaitForSeconds(0.4f);
       isover = true;
    }

    public override IEnumerator StepEnd()
    {

        
        yield break;
    }

    public override bool ChenkEnd()
    {
        return isover;
    }

}
