using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class T6_5 : BaseGuideStep
{
 
    public  GameEnum.ConsumerType type; 
    public int count;
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
        T6Manager.My.sumSpor.SetActive(true);
        T6Manager.My.sumSpor2.SetActive(true);
        T6Manager.My.endPoint.SetActive(true);
        T6Manager.My.endPoint2.SetActive(true);
        NewCanvasUI.My.GameNormal();
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
                .BornSingleTypeConsumer(type, count));
            StartCoroutine(BuildingManager.My.buildings[ 1]
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
            StartCoroutine(BuildingManager.My.buildings[ 1]
                .BornSingleTypeConsumer(GameEnum.ConsumerType.ConsumerModel9, count));
      

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
        if (NewCanvasUI.My.Panel_AssemblyRole.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            
        }

        if (StageGoal.My.totalIncome - StageGoal.My.totalCost < -12000)
        {
            StageGoal.My.totalIncome = 0;
            StageGoal.My.totalCost = 0;
        }

        Info.text = "当前利润： "+(StageGoal.My.totalIncome - StageGoal.My.totalCost).ToString()+"   目标利润："+12000 ;
        if (StageGoal.My.totalIncome - StageGoal.My.totalCost > 12000)
        {
            missiondatas.data[0].isFinish = true;
            return true;
        }
        else
        {
            return false;
            
        }

      

    }
}
