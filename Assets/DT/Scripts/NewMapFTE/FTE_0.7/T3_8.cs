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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    
    public override IEnumerator StepStart()
    {
      FTE_0_6Manager.My.consumerSpot.SetActive(true);
      FTE_0_6Manager.My.endPoint.SetActive(true);
      FTE_0_6Manager.My.dealer.SetActive(true);
      FTE_0_6Manager.My.dealer2.SetActive(true);
      FTE_0_6Manager.My.UpRole(FTE_0_6Manager.My.dealer);
      FTE_0_6Manager.My.UpRole(FTE_0_6Manager.My.dealer2);
  

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
       
        missiondatas.data[0].currentNum = StageGoal.My.killNumber;
     
        if (StageGoal.My.killNumber >= missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
            return true;

        }
     
      
     
        return false;
       

    }

}
