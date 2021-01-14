using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_16 : BaseGuideStep
{
 
    public  GameEnum.ConsumerType type; 
    public int count;
    public int time;

    public bool islast;
    public GameObject red;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    
    public override IEnumerator StepStart()
    {
      
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
        NewCanvasUI.My.GamePause();

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
        yield return new WaitForSeconds(2f);

    }

    public override bool ChenkEnd()
    {
        if (islast)
        {
            if (PlayerData.My.dealerCount < 3)
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
 