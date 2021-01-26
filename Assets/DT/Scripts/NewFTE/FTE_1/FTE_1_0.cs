using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_1_0 : BaseGuideStep
{
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
        //RoleListManager.My.OutButton();

        FTE_1Manager.My.blood.transform.DOLocalMoveY(FTE_1Manager.My.blood.transform.localPosition.y + 100, 0.02f)
            .Play();
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