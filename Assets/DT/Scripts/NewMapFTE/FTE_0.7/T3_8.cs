using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class T3_8: BaseGuideStep
{
 
    public  GameEnum.ConsumerType type; 
    public int count;
    public int time;

    public bool islast;
    public GameObject red;

    public BaseMapRole maoyi;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    
    public override IEnumerator StepStart()
    {
      FTE_0_6Manager.My.consumerSpot.SetActive(true);
      FTE_0_6Manager.My.endPoint.SetActive(true);
      
      for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
      {
          if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant &&
              !PlayerData.My.MapRole[i].isNpc)
          {
              maoyi   =   PlayerData.My.MapRole[i];
          }
      }

      if (!islast)
      {
          TradeManager.My.DeleteRoleAllTrade(maoyi.baseRoleData.ID);
      }

      FTE_0_6Manager.My.DownRole( FTE_0_6Manager.My.dealerJC1); 
      FTE_0_6Manager.My.DownRole( FTE_0_6Manager.My.dealerJC2);
      FTE_0_6Manager.My.DownRole( FTE_0_6Manager.My.dealerJC3);
      FTE_0_6Manager.My.DownRole( FTE_0_6Manager.My.dealerJC4);
        var list = FindObjectsOfType<ConsumeSign>();
        for (int i = 0; i <list.Length ; i++)
        {
            Destroy(list[i].gameObject);
        }
        StageGoal.My.maxRoleLevel = 5;
        StageGoal.My.killNumber = 0;
        transform.DOScale(1, 1).OnComplete(() =>
        {
            StartCoroutine(BuildingManager.My.buildings[0]
                .BornSingleTypeConsumer(type, count));
         
            Addxiaofei();
        }).Play();
         

        yield return new WaitForSeconds(1f);

    }
    private Tween t;

    public void Addxiaofei()
    {
      
        t=       transform.DOScale(1, time).OnComplete(() =>
        {
            StartCoroutine(BuildingManager.My.buildings[0]
                .BornSingleTypeConsumer(type, count));
            StageGoal.My.killNumber = 0;
            missiondatas.data[0].isFinish = false;

            Addxiaofei();
        }).Play();
    }

    public override IEnumerator StepEnd()
    {
        t.Kill();
        yield return new WaitForSeconds(2f);

    }

    public override bool ChenkEnd()
    {
        if (islast)
        {
            if (PlayerData.My.dealerCount <2)
            {
                red.SetActive(true);
            }

            else
            {
                red.SetActive(false);

            }
        }

        missiondatas.data[0].currentNum = StageGoal.My.killNumber;
     
        if (StageGoal.My.killNumber >= missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
            return true;

        }
     
      
     
        return false;
       

    }

}
