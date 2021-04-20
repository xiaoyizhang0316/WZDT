using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N1_Step2 : BaseGuideStep
{
    public GameObject hand;
    public override IEnumerator StepStart()
    {
        isStepEnd = false;
        StageGoal.My.skipToFirstWave.interactable = false;
        if (CheckDealer())
        {
            isStepEnd = true;
        }
        else
        {
            RoleListManager.My.outButton.onClick.Invoke();
            hand.SetActive(true);
            InvokeRepeating("Check", 0.5f, 0.5f);
        }
        yield return null;
    }

    private bool isStepEnd = false;

    void Check()
    {
        if (CheckDealer())
        {
            isStepEnd = true;
        }
    }

    bool CheckDealer()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
            {
                return true;
            }
        }

        return false;
    }
    
    public override bool ChenkEnd()
    {
        return isStepEnd;
    }

    public override IEnumerator StepEnd()
    {
        T_N1_Manager.My.SetEquipButton(true);
        yield return null;
    }
}
