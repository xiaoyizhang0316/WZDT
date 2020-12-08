using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_Map_2 : BaseGuideStep
{
    public GameObject hand;
    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        yield return new WaitForSeconds(0.5f);
        hand.SetActive(true);
        ShowInfos();
        NetworkMgr.My.UpdateUnlockStatus("1_1_0_0_0_0_0_0_0");
    }

    public override bool ChenkEnd()
    {
        //if (LevelInfoManager.My.stepOver)
        //{
        //    NetworkMgr.My.UpdateUnlockStatus("1_1_0_0_0_0_0_0_0");
        //}
        return false;
    }

    void ShowInfos()
    {
        for (int i = 0; i < contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }

    }
}
