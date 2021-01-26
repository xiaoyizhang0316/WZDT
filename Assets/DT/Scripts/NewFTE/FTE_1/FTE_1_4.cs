using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
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

=======
using UnityEngine;

public class FTE_1_4 : BaseGuideStep
{
    public GameObject inBorder;
    public GameObject outBorder;
    public override IEnumerator StepStart()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.5f);
        yield return null;
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish && missiondatas.data[1].isFinish;
    }

    void CheckGoal()
    {
        if (!inBorder.activeInHierarchy)
        {
            missiondatas.data[0].isFinish = true;
        }

        if (!outBorder.activeInHierarchy)
        {
            missiondatas.data[1].isFinish = true;
        }
>>>>>>> origin/hwj
    }

    public override IEnumerator StepEnd()
    {
<<<<<<< HEAD
        yield return new WaitForSeconds(0.5f);
    }
}
=======
        yield return new WaitForSeconds(2);
    }
}
>>>>>>> origin/hwj
