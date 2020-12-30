using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_15 : BaseGuideStep
{
    public BaseMapRole maoyi;
    public BaseMapRole lingshou;

    public GameObject roleImage;

    public int count;
    public int time;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        StageGoal.My.killNumber = 0;
         transform.DOScale(1, 1).OnComplete(() =>
        {
            StartCoroutine(BuildingManager.My.buildings[0]
                .BornSingleTypeConsumer(GameEnum.ConsumerType.ConsumerModel1, count));
            Addxiaofei();
        }).Play();
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant)
            {
                maoyi = PlayerData.My.MapRole[i];
            }
        }

        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer &&
                !PlayerData.My.MapRole[i].isNpc)
            {
                lingshou = PlayerData.My.MapRole[i];
            }

        }
        yield return new WaitForSeconds(1f);

    }

    private Tween t;
   
    public void Addxiaofei()
    {
          t= transform.DOScale(1, time).OnComplete(() =>
        {
            StartCoroutine(BuildingManager.My.buildings[0]
                .BornSingleTypeConsumer(GameEnum.ConsumerType.ConsumerModel1, count));
            Addxiaofei();
        }).Play();
          
        }

    public override IEnumerator StepEnd()
    {
      
        t.Kill();
   
        roleImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        roleImage.SetActive(false);

    }

    public override bool ChenkEnd()
    {
        if (TradeManager.My.CheckTwoRoleHasTrade(maoyi.baseRoleData, lingshou.baseRoleData))
        {
            missiondatas.data[0].isFinish = true;
        }
        missiondatas.data[1].currentNum = StageGoal.My.killNumber;
        if (StageGoal.My.killNumber >= missiondatas.data[1].maxNum)
        {
            missiondatas.data[1].isFinish = true;
        }
        if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
   
}
 