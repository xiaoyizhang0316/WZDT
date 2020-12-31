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

    public GameObject dealer2;
    public GameObject dealer3;
    public GameObject place2;
    public GameObject place3;
    

    public List<GameObject> factorys;
    public List<GameObject> places;

    //public GameObject endPanel;

    private List<BaseMapRole> dealers;
    private int currentTimeCount = 0;
    public override IEnumerator StepStart()
    {
        FTE_2_5_Manager.My.isClearGoods = false;
        NewCanvasUI.My.GamePause(false);
        FactoryUp();
        currentTimeCount = StageGoal.My.timeCount;
        FTE_2_5_Manager.My.packageKillNum = 0;
        FTE_2_5_Manager.My.saleKillNum = 0;
        FTE_2_5_Manager.My.nolikeKillNum = 0;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().needLimit = true;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitSeedCount = 2;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitPeasantCount = 2;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitMerchantCount = 2;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitDealerCount = -1;
        dealer2.SetActive(true);
        dealer3.SetActive(true);
        dealer2.transform.DOMoveY(0.32f, 1).Play();
        place2.transform.DOMoveY(0, 1).Play();
        dealer3.transform.DOMoveY(0.32f, 1).Play();
        place3.transform.DOMoveY(0, 1).Play();
        /*bornPoint1.GetComponent<Building>().BornEnemyForFTE_2_5(302);
        bornPoint2.GetComponent<Building>().BornEnemyForFTE_2_5(301);
        bornPoint3.GetComponent<Building>().BornEnemyForFTE_2_5(-1);*/
        BornPackage();
        BornSale();
        BornNoLike();
        InvokeRepeating("CheckReset", 3, 0.5f);
        //InvokeRepeating("CheckGoal", 2f, 0.5f);
        InvokeRepeating("CheckBuffOut", 3, 20);
        yield return new WaitForSeconds(0.5f);
    }

    void FactoryUp()
    {
        for (int i = 0; i < factorys.Count; i++)
        {
            factorys[i].SetActive(true);
            factorys[i].transform.DOMoveY(0.32f, 1f).Play();
        }

        for (int i = 0; i < places.Count; i++)
        {
            places[i].transform.DOMoveY(0, 1).Play();
        }
    }

    void BornPackage()
    {
        StartCoroutine(bornPoint1.GetComponent<Building>().BornEnemyForFTE_2_5_1(27, 12));
        InvokeRepeating("CheckPackage", 2f, 0.5f);
    }
    void BornSale()
    {
        StartCoroutine(bornPoint2.GetComponent<Building>().BornEnemyForFTE_2_5_1(28, 12));
        InvokeRepeating("CheckSale", 2f, 0.5f);
    }
    void BornNoLike()
    {
        StartCoroutine(bornPoint3.GetComponent<Building>().BornEnemyForFTE_2_5_1(29, 12));
        InvokeRepeating("CheckNolike", 2f, 0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        /*endPanel.GetComponent<Button>().onClick.AddListener(() =>
        {
            NetworkMgr.My.UpdatePlayerFTE("2.5", ()=>SceneManager.LoadScene("Map"));
        });*/
        yield return new WaitForSeconds(1f);
        //endPanel.SetActive(true);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish;
    }

    private bool isPackageFail = false;
    private bool isSaleFail = false;
    private bool isNoLikeFail = false;
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
                if (!CheckHasConsume(bornPoint1.transform) && (StageGoal.My.timeCount-currentTimeCount>3))
                {
                    isPackageFail = true;
                    missiondatas.data[0].isFail = true;

                    CancelInvoke("CheckPackage");
                    /*HttpManager.My.ShowTip("任务1完成条件已无法满足，该任务重置！");
                    CancelInvoke("CheckPackage");
                    FTE_2_5_Manager.My.packageKillNum = 0;
                    missiondatas.data[0].currentNum = FTE_2_5_Manager.My.packageKillNum;
                    BornPackage();*/
                }
            }
        }else
        {
            if (isSaleFail || isNoLikeFail)
            {
                isPackageFail = true;
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
                if (!CheckHasConsume(bornPoint2.transform)&& (StageGoal.My.timeCount-currentTimeCount>3))
                {
                    isSaleFail = true;
                    missiondatas.data[1].isFail = true;

                    CancelInvoke("CheckSale");
                    /*HttpManager.My.ShowTip("任务2完成条件已无法满足，该任务重置！");
                    CancelInvoke("CheckSale");
                    FTE_2_5_Manager.My.saleKillNum = 0;
                    missiondatas.data[1].currentNum = FTE_2_5_Manager.My.saleKillNum;
                    BornSale();*/
                }
            }
        }
        else
        {
            if (isPackageFail || isNoLikeFail)
            {
                isSaleFail = true;
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
                if (!CheckHasConsume(bornPoint3.transform)&& (StageGoal.My.timeCount-currentTimeCount>3))
                {
                    isNoLikeFail = true;
                    missiondatas.data[2].isFail = true;
                    CancelInvoke("CheckNolike");
                    /*HttpManager.My.ShowTip("任务3完成条件已无法满足，该任务重置！");
                    CancelInvoke("CheckNolike");
                    FTE_2_5_Manager.My.nolikeKillNum = 0;
                    missiondatas.data[2].currentNum = FTE_2_5_Manager.My.nolikeKillNum;
                    BornNoLike();*/
                }
            }
        }else
        {
            if (isPackageFail || isSaleFail)
            {
                isNoLikeFail = true;
            }
        }
    }

    void CheckReset()
    {
        if (isPackageFail && isSaleFail && isNoLikeFail)
        {
            HttpManager.My.ShowTip("任务未达成，任务重置！");
            CancelInvoke("CheckReset");
            /*CancelInvoke("CheckNolike");
            CancelInvoke("CheckSale");
            CancelInvoke("CheckPackage");*/
            isPackageFail = false;
            isSaleFail = false;
            isNoLikeFail = false;
            FTE_2_5_Manager.My.nolikeKillNum = 0;
            missiondatas.data[2].currentNum = FTE_2_5_Manager.My.nolikeKillNum;
            missiondatas.data[2].isFinish = false;
            missiondatas.data[2].isFail = false;

            FTE_2_5_Manager.My.saleKillNum = 0;
            missiondatas.data[1].currentNum = FTE_2_5_Manager.My.saleKillNum;
            missiondatas.data[1].isFinish = false;
            missiondatas.data[1].isFail = false;

            FTE_2_5_Manager.My.packageKillNum = 0;
            missiondatas.data[0].currentNum = FTE_2_5_Manager.My.packageKillNum;
            missiondatas.data[0].isFinish = false;
            missiondatas.data[0].isFail = false;

            StageGoal.My.playerSatisfy = 0;
            StageGoal.My.playerSatisfyText.text = "0";
            currentTimeCount = StageGoal.My.timeCount;
            BornNoLike();
            BornSale();
            BornPackage();
            InvokeRepeating("CheckReset", 3, 0.5f);
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
                for (int j = dealers[i].warehouse.Count-1; j >=0; j--)
                {
                    if (dealers[i].warehouse[j].wasteBuffList!=null&& dealers[i].warehouse[j].wasteBuffList.Count >0)
                    {
                        HttpManager.My.ShowTip("有口味效果被浪费！请注意检查‘<color=green>"+dealers[i].baseRoleData.baseRoleData.roleName+"</color>’相关的生产链！");
                        break;
                    }
                }
            }
        }
    }
}
