using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_16 : BaseGuideStep
{
 
    public  GameEnum.ConsumerType type; 
    public int count;
    public int time;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        StageGoal.My.maxRoleLevel = 5;
        transform.DOScale(1, 1).OnComplete(() =>
        {
            StartCoroutine(BuildingManager.My.buildings[0]
                .BornSingleTypeConsumer(type, count));
            StageGoal.My.killNumber = 0;
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
                Addxiaofei();
            }).Play();
        }

    public override IEnumerator StepEnd()
    {
        t.Kill();
        yield break;
    }

    public override bool ChenkEnd()
    {
        missiondatas.data[0].currentNum = StageGoal.My.killNumber;
     
        if (StageGoal.My.killNumber > missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
            return true;

        }
     
      
     
            return false;
       

    }
   
}
 