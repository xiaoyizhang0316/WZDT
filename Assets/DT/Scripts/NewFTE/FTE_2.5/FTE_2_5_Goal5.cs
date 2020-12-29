using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_2_5_Goal5 : BaseGuideStep
{
    public GameObject bornPoint1;
    public GameObject bornPoint2;
    public GameObject bornPoint3;

    public List<GameObject> factorys;
    public List<GameObject> places;

    public GameObject endPanel;

    private List<BaseMapRole> dealers;
    public override IEnumerator StepStart()
    {
        FactoryUp();
        FTE_2_5_Manager.My.isClearGoods = false;
        NewCanvasUI.My.GameNormal();
        FTE_2_5_Manager.My.packageKillNum = 0;
        FTE_2_5_Manager.My.saleKillNum = 0;
        FTE_2_5_Manager.My.nolikeKillNum = 0;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitDealerCount = -1;
        /*bornPoint1.GetComponent<Building>().BornEnemyForFTE_2_5(302);
        bornPoint2.GetComponent<Building>().BornEnemyForFTE_2_5(301);
        bornPoint3.GetComponent<Building>().BornEnemyForFTE_2_5(-1);*/
        BornPackage();
        BornSale();
        BornNoLike();
        //InvokeRepeating("CheckGoal", 2f, 0.5f);
        InvokeRepeating("CheckBuffOut", 3, 30);
        yield return new WaitForSeconds(0.5f);
    }

    void FactoryUp()
    {
        for (int i = 0; i < factorys.Count; i++)
        {
            factorys[i].SetActive(true);
            factorys[i].transform.DOMoveY(0.32f, 1f);
        }

        for (int i = 0; i < places.Count; i++)
        {
            places[i].transform.DOMoveY(0, 1);
        }
    }

    void BornPackage()
    {
        StartCoroutine(bornPoint1.GetComponent<Building>().BornEnemyForFTE_2_5_1(302, 12));
        InvokeRepeating("CheckPackage", 2f, 0.5f);
    }
    void BornSale()
    {
        StartCoroutine(bornPoint2.GetComponent<Building>().BornEnemyForFTE_2_5_1(301, 12));
        InvokeRepeating("CheckSale", 2f, 0.5f);
    }
    void BornNoLike()
    {
        StartCoroutine(bornPoint3.GetComponent<Building>().BornEnemyForFTE_2_5_1(-1, 12));
        InvokeRepeating("CheckNolike", 2f, 0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        endPanel.GetComponent<Button>().onClick.AddListener(() =>
        {
            NetworkMgr.My.UpdatePlayerFTE("2.5", ()=>SceneManager.LoadScene("Map"));
        });
        yield return new WaitForSeconds(1f);
        endPanel.SetActive(true);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish;
    }

    void CheckPackage()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = FTE_2_5_Manager.My.packageKillNum;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
            else
            {
                if (!CheckHasConsume(bornPoint1.transform))
                {
                    CancelInvoke("CheckPackage");
                    FTE_2_5_Manager.My.packageKillNum = 0;
                    missiondatas.data[0].currentNum = FTE_2_5_Manager.My.packageKillNum;
                    BornPackage();
                }
            }
        }
    }

    void CheckSale()
    {
        if (missiondatas.data[1].isFinish == false)
        {
            missiondatas.data[1].currentNum = FTE_2_5_Manager.My.saleKillNum;
            if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
            {
                missiondatas.data[1].isFinish = true;
            }
            else
            {
                if (!CheckHasConsume(bornPoint2.transform))
                {
                    CancelInvoke("CheckSale");
                    FTE_2_5_Manager.My.saleKillNum = 0;
                    missiondatas.data[1].currentNum = FTE_2_5_Manager.My.saleKillNum;
                    BornSale();
                }
            }
        }
    }

    void CheckNolike()
    {
        if (missiondatas.data[2].isFinish == false)
        {
            missiondatas.data[2].currentNum = FTE_2_5_Manager.My.nolikeKillNum;
            if (missiondatas.data[2].currentNum >= missiondatas.data[2].maxNum)
            {
                missiondatas.data[2].isFinish = true;
            }
            else
            {
                if (!CheckHasConsume(bornPoint3.transform))
                {
                    CancelInvoke("CheckNolike");
                    FTE_2_5_Manager.My.nolikeKillNum = 0;
                    missiondatas.data[2].currentNum = FTE_2_5_Manager.My.nolikeKillNum;
                    BornNoLike();
                }
            }
        }
    }

    bool CheckHasConsume(Transform point)
    {
        if (point.childCount > 4)
        {
            return true;
        }

        return false;
    }

    void CheckBuffOut()
    {
        if (dealers == null)
        {
            dealers = new List<BaseMapRole>();
        }
        dealers.Clear();
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType ==
                GameEnum.RoleType.Dealer)
            {
                dealers.Add(PlayerData.My.MapRole[i]);
            }
        }

        for (int i = 0; i < dealers.Count; i++)
        {
            if (dealers[i] != null)
            {
                for (int j = 0; j < dealers[i].warehouse.Count; j++)
                {
                    if (dealers[i].warehouse[j].buffList.Count > 2)
                    {
                        HttpManager.My.ShowTip("有口味效果被顶掉！");
                        break;
                    }
                }
            }
        }
    }
}
