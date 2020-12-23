using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FTE_2_5_Goal4 : BaseGuideStep
{
    public GameObject bornPoint4;
    public GameObject bornPoint5;
    private int lastMoney = 0;
    private int lastSatisfy = 0;
    private int lastTimeCount = 0;
    public GameObject endPanel;
    public override IEnumerator StepStart()
    {
        GetComponent<Image>().raycastTarget = true;
        StartCoroutine( ChangeRoad());
        yield return new WaitForSeconds(3);
        GetComponent<Image>().raycastTarget = false;
        lastMoney = StageGoal.My.playerGold;
        lastSatisfy = StageGoal.My.playerSatisfy;
        lastTimeCount = StageGoal.My.timeCount;
        bornPoint4.GetComponent<Building>().BornEnemyForFTE_2_5(302);
        bornPoint5.GetComponent<Building>().BornEnemyForFTE_2_5(301);
        InvokeRepeating("CheckGoal", 0.01f, 0.1f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2f);
        endPanel.GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayerData.My.playerGears.Clear();
            PlayerData.My.playerWorkers.Clear();
            SceneManager.LoadScene("Map");
        });
        endPanel.SetActive(true);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish&&missiondatas.data[3].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = FTE_2_5_Manager.My.packageKillNum;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
        
        if (missiondatas.data[1].isFinish == false)
        {
            missiondatas.data[1].currentNum = FTE_2_5_Manager.My.saleKillNum;
            if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
            {
                missiondatas.data[1].isFinish = true;
            }
        }

        if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish)
        {
            if (missiondatas.data[2].isFinish == false)
            {
                missiondatas.data[2].currentNum = (StageGoal.My.playerGold - lastMoney) * 5 /
                                                  ((StageGoal.My.timeCount - lastTimeCount)==0?1:(StageGoal.My.timeCount - lastTimeCount));

                if (missiondatas.data[2].currentNum >= missiondatas.data[2].maxNum)
                {
                    missiondatas.data[2].isFinish = true;
                }
            }

            if (missiondatas.data[3].isFinish == false)
            {
                missiondatas.data[3].currentNum = (StageGoal.My.playerSatisfy - lastSatisfy) * 5 /
                                                  ((StageGoal.My.timeCount - lastTimeCount) <= 0
                                                      ? 1
                                                      : (StageGoal.My.timeCount - lastTimeCount));
                if (missiondatas.data[3].currentNum >= missiondatas.data[3].maxNum)
                {
                    missiondatas.data[3].isFinish = true;
                }
            }
        }
    }

    public List<GameObject> oldRoadList1;
    public List<GameObject> oldRoadList2;
    public List<GameObject> oldRoadList3;
    public List<GameObject> groudList1;
    public List<GameObject> groudList2;
    public List<GameObject> groudList3;
    public List<GameObject> oldGroudList1;
    public List<GameObject> oldGroudList2;
    public List<GameObject> roadList1;
    public List<GameObject> roadList2;
    IEnumerator ChangeRoad()
    {
        for (int i = 0; i < oldRoadList1.Count; i++)
        {
            oldRoadList1[i].transform.DOMoveY(-10, 0.5f);
        }

        for (int i = 0; i < oldRoadList2.Count; i++)
        {
            oldRoadList2[i].transform.DOMoveY(-10, 0.5f);
        }
        
        for (int i = 0; i < oldRoadList3.Count; i++)
        {
            oldRoadList3[i].transform.DOMoveY(-10, 0.5f);
        }

        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i < groudList1.Count; i++)
        {
            groudList1[i].transform.DOMoveY(0, 0.5f);
        }
        for (int i = 0; i < groudList2.Count; i++)
        {
            groudList2[i].transform.DOMoveY(0, 0.5f);
        }
        for (int i = 0; i < groudList1.Count; i++)
        {
            groudList2[i].transform.DOMoveY(0, 0.5f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < oldGroudList1.Count; i++)
        {
            oldGroudList1[i].transform.DOMoveY(-10, 0.5f);
        }
        for (int i = 0; i < oldGroudList2.Count; i++)
        {
            oldGroudList2[i].transform.DOMoveY(-10, 0.5f);
        }
        
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < roadList1.Count; i++)
        {
            roadList1[i].transform.DOMoveY(0, 0.5f);
        }
        for (int i = 0; i < roadList2.Count; i++)
        {
            roadList2[i].transform.DOMoveY(0, 0.5f);
        }
    }
}
