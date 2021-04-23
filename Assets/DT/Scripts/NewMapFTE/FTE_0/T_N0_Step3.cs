using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N0_Step3 : BaseGuideStep
{
    public GameObject hand;
    public GameObject mask;
    public override IEnumerator StepStart()
    {
        isStepEnd = false;
        if (CheckDealer())
        {
            isStepEnd = true;
        }
        else
        {
            RoleListManager.My.outButton.onClick.Invoke();
            yield return new WaitForSeconds(1);
            hand.SetActive(true);
            mask.SetActive(true);
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
        
        yield return new WaitForSeconds(0.5f);
    }
}
