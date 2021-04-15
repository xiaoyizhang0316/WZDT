using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class T8_5 : BaseGuideStep
{
  
    public  GameEnum.ConsumerType type; 
    public int count;
    public BornType bornType1;
    public BornType bornType2;
    public int time;

    public Text Info;
    public bool islast;
    public GameObject red;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    
    public override IEnumerator StepStart()
    {
        T8Manager.My.consumerPort1.SetActive(true);
        
        T8Manager.My.endPointPort1.SetActive(true);
    
        
        StageGoal.My.totalIncome = 0;
        StageGoal.My.totalCost = 0;
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
                .BornSingleTypeConsumer(bornType1.type, bornType1.count));
            StartCoroutine(BuildingManager.My.buildings[1]
                .BornSingleTypeConsumer(bornType2.type, bornType2.count));
            Addxiaofei();
        }).Play();
        NewCanvasUI.My.GameNormal();

        yield return new WaitForSeconds(1f);

    }
    private Tween t;

    public void Addxiaofei()
    {
      
        t=       transform.DOScale(1, time).OnComplete(() =>
        {
            StartCoroutine(BuildingManager.My.buildings[0]
                .BornSingleTypeConsumer(bornType1.type, bornType1.count));
            StartCoroutine(BuildingManager.My.buildings[1]
                .BornSingleTypeConsumer(bornType2.type, bornType2.count));

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
     
        missiondatas.data[0].currentNum = T8Manager.My.saleKillNum;
        missiondatas.data[1].currentNum =  T8Manager.My.packageKillNum;

        if (missiondatas.data[0].currentNum>=missiondatas.data[0].maxNum  )
        {
            missiondatas.data[0].isFinish = true;
         
        }
         
        if (missiondatas.data[1].currentNum>=missiondatas.data[1].maxNum  )
        {
            missiondatas.data[1].isFinish = true;
         
        }

        if (StageGoal.My.totalIncome - StageGoal.My.totalCost < -10000)
        {
            missiondatas.data[0].isFinish = false;
            missiondatas.data[1].isFinish = false;
            missiondatas.data[0].currentNum = 0;
            missiondatas.data[1].currentNum = 0;
            StageGoal.My.totalIncome = 0;
            StageGoal.My.totalCost = 0;
        }

        missiondatas.data[2].currentNum = StageGoal.My.totalIncome - StageGoal.My.totalCost;
        if (  missiondatas.data[2].currentNum >= missiondatas.data[2].maxNum&&  missiondatas.data[0].isFinish&&  missiondatas.data[1].isFinish)
        {
            return true;
        }
        else
        {
            return false;

        }

       

    }
 
}
