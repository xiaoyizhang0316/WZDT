using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FTE_2_5_Goal3 : BaseGuideStep
{
    public GameObject peasant1;
    public GameObject peasant2;
    public GameObject peasant3;
    public GameObject bornPoint1;
    public GameObject bornPoint2;
    public GameObject bornPoint3;
    public GameObject endPoint1;
    public GameObject endPoint2;
    public GameObject endPoint3;
    public override IEnumerator StepStart()
    {
        peasant1.SetActive(false);
        peasant2.SetActive(false);
        peasant3.SetActive(false);
        bornPoint1.GetComponent<Building>().BornEnemyForFTE_2_5(305);
        bornPoint2.GetComponent<Building>().BornEnemyForFTE_2_5(304);
        bornPoint3.GetComponent<Building>().BornEnemyForFTE_2_5(303);
        InvokeRepeating("CheckGoal", 0.01f, 0.1f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        bornPoint1.GetComponent<Building>().isBornForFTE_2_5 = false;
        bornPoint2.GetComponent<Building>().isBornForFTE_2_5 = false;
        bornPoint3.GetComponent<Building>().isBornForFTE_2_5 = false;
        yield return new WaitForSeconds(2f);
        bornPoint1.SetActive(false);
        bornPoint2.SetActive(false);
        bornPoint3.SetActive(false);
        endPoint1.SetActive(false);
        endPoint2.SetActive(false);
        endPoint3.SetActive(false);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = FTE_2_5_Manager.My.sweetKillNum;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
        if (missiondatas.data[1].isFinish == false)
        {
            missiondatas.data[1].currentNum = FTE_2_5_Manager.My.crispKillNum;
            if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
            {
                missiondatas.data[1].isFinish = true;
            }
        }
        if (missiondatas.data[2].isFinish == false)
        {
            missiondatas.data[2].currentNum = FTE_2_5_Manager.My.softKillNum;
            if (missiondatas.data[2].currentNum >= missiondatas.data[2].maxNum)
            {
                missiondatas.data[2].isFinish = true;
            }
        }
    }
}
