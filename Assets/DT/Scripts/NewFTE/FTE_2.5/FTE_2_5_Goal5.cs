using System.Collections;
using UnityEngine;

public class FTE_2_5_Goal5 : BaseGuideStep
{
    public GameObject seed;
    public GameObject peasant;
    public GameObject merchant;
    public GameObject dealer;
    public override IEnumerator StepStart()
    {
        seed.SetActive(true);
        peasant.SetActive(true);
        merchant.SetActive(true);
        dealer.SetActive(true);
        TradeManager.My.AutoCreateTrade(seed.GetComponent<BaseMapRole>().baseRoleData.ID.ToString(), peasant.GetComponent<BaseMapRole>().baseRoleData.ID.ToString());
        TradeManager.My.AutoCreateTrade(peasant.GetComponent<BaseMapRole>().baseRoleData.ID.ToString(), merchant.GetComponent<BaseMapRole>().baseRoleData.ID.ToString());
        TradeManager.My.AutoCreateTrade(merchant.GetComponent<BaseMapRole>().baseRoleData.ID.ToString(), dealer.GetComponent<BaseMapRole>().baseRoleData.ID.ToString());
        InvokeRepeating("CheckGoal", 0.01f, 0.1f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2f);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish&&missiondatas.data[3].isFinish;
    }

    void CheckGoal()
    {
        
    }
}
