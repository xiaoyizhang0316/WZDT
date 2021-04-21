using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N1_Step2 : BaseGuideStep
{
    public GameObject hand;
    //public BaseGuideStep setStep;
    private DarkEffect.Item item;
    private BaseMapRole dealer;
    public GameObject mask;
    public override IEnumerator StepStart()
    {
        StageGoal.My.skipToFirstWave.interactable = false;
        isStepEnd = false;
        item = new DarkEffect.Item();
        StageGoal.My.skipToFirstWave.interactable = false;
        dealer = CheckDealer();
        if (dealer)
        {
            /*item.target = dealer.transform;
            item.radius = 0;
            item.EndRandius = 100;
            item.waitTime = 1;
            item.speed = 5;
            setStep.Camera3DTarget.Add(item);*/
            isStepEnd = true;
        }
        else
        {
            RoleListManager.My.outButton.onClick.Invoke();
            yield return new WaitForSeconds(1);
            mask.SetActive(true);
            hand.SetActive(true);
            InvokeRepeating("Check", 0.5f, 0.5f);
        }
        yield return null;
    }

    private bool isStepEnd = false;

    void Check()
    {
        dealer = CheckDealer();
        if (dealer)
        {
            /*item.target = dealer.transform;
            item.radius = 0;
            item.EndRandius = 100;
            item.waitTime = 1;
            item.speed = 5;
            setStep.Camera3DTarget.Add(item);*/
            isStepEnd = true;
        }
    }

    BaseMapRole CheckDealer()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
            {
                return PlayerData.My.MapRole[i];
            }
        }

        return null;
    }
    
    public override bool ChenkEnd()
    {
        return isStepEnd;
    }

    public override IEnumerator StepEnd()
    {
        T_N1_Manager.My.SetEquipButton(true);
        StageGoal.My.skipToFirstWave.interactable = true;
        yield return new WaitForSeconds(0.5f);
    }
}
