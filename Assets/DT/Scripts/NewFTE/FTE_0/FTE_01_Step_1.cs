using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FTE_01_Step_1 : BaseGuideStep
{
    //public GameObject hand;
    public List<Transform> roadList;
    bool isOver = false;
    

    private void Start()
    {
        isOver = false;
        for(int i=0; i<roadList.Count; i++)
        {
            roadList[i].DOLocalMoveY(-1, 0.02f);
        }
        StartCoroutine(OwnStep());
    }

    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        yield break;
    }

    public override bool ChenkEnd()
    {
        return isOver;
    }

    IEnumerator OwnStep()
    {
        yield return new WaitForSeconds(1.5f);
        for(int i =0; i < roadList.Count; i++)
        {
            roadList[i].DOLocalMoveY(0.1f, 0.8f);
        }
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < roadList.Count; i++)
        {
           roadList[i].DOLocalMoveY(0, 0.2f);
        }
        yield return new WaitForSeconds(0.5f);
        GuideManager.My.BornEnemy();
        yield return new WaitForSeconds(3.5f);
        isOver = true;
    }

    //private void BornEnemy()
    //{
    //    Debug.Log("born");
    //    StartCoroutine(GameObject.Find("Build/ConsumerSpot").GetComponent<Building>().BornEnemy());
    //}
}
