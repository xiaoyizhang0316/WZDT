using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FTE_1_4 : BaseGuideStep
{
    // Start is called before the first frame update
   
    public override IEnumerator StepStart()
    {
       // NewCanvasUI.My.GamePause();
        //RoleListManager.My.OutButton();

     
        //NewCanvasUI.My.Panel_Update.SetActive(true);
        //RoleUpdateInfo.My.changeRoleButton.gameObject.SetActive(false);
        //NewCanvasUI.My.Panel_Update.transform.localPosition = new Vector3(0,5000,0);
        yield return new WaitForSeconds(0.5f);

    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
    }
}