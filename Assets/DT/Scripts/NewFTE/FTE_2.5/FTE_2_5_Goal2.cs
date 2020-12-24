using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FTE_2_5_Goal2 : BaseGuideStep
{
    // 0.32 
    public Transform peasant1;
    public Transform peasant2;
    public Transform peasant3;
    public Transform place1;
    public Transform place2;
    public Transform place3;
    public GameObject oldPlace1;
    public GameObject oldPlace2;
    public GameObject oldPlace3;
    private int sweetCount = 0;
    private int crispCount = 0;
    private int softCount = 0;
    public override IEnumerator StepStart()
    {
        peasant1.gameObject.SetActive(true);
        peasant2.gameObject.SetActive(true);
        peasant3.gameObject.SetActive(true);
        SeedBuildRise();
        InvokeRepeating("CheckGoal", 0.01f, 0.1f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2f);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            CheckSeed(peasant1, 305);
            missiondatas.data[0].currentNum = sweetCount;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
        
        if (missiondatas.data[1].isFinish == false)
        {
            CheckSeed(peasant2, 304);
            missiondatas.data[1].currentNum = sweetCount;
            if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
            {
                missiondatas.data[1].isFinish = true;
            }
        }
        
        if (missiondatas.data[2].isFinish == false)
        {
            CheckSeed(peasant3, 303);
            missiondatas.data[2].currentNum = sweetCount;
            if (missiondatas.data[2].currentNum >= missiondatas.data[2].maxNum)
            {
                missiondatas.data[2].isFinish = true;
            }
        }
    }

    void CheckSeed(Transform peasant, int buff)
    {
        int tempCount = 0;
        for (int i = 0; i < peasant.GetComponent<BaseMapRole>().warehouse.Count; i++)
        {
            if (peasant.GetComponent<BaseMapRole>().warehouse[i].buffList.Contains(buff))
            {
                tempCount++;
            }
        }

        if (buff == 305)
        {
            sweetCount = tempCount;
        }else if (buff == 304)
        {
            crispCount = tempCount;
        }
        else
        {
            softCount = tempCount;
        }

        tempCount = 0;
    }

    void SeedBuildRise()
    {
        oldPlace1.transform.DOMoveY(-10, 0.5f).OnComplete(() =>
        {
            place1.DOMoveY(0, 0.5f);
            peasant1.DOMoveY(0.35f, 0.5f);
        });
        oldPlace2.transform.DOMoveY(-10, 0.5f).OnComplete(() =>
        {
            place2.DOMoveY(0, 0.5f);
            peasant2.DOMoveY(0.35f, 0.5f);
        });
        oldPlace3.transform.DOMoveY(-10, 0.5f).OnComplete(() =>
        {
            place3.DOMoveY(0, 0.5f);
            peasant3.DOMoveY(0.35f, 0.5f);
        });
    }
}
