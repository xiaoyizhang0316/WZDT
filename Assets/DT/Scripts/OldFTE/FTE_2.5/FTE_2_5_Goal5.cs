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

    //public GameObject dealer2;
    //public GameObject dealer3;
    public GameObject place2;
    public GameObject place3;
    

    /*public List<GameObject> factorys;
    public List<GameObject> places;*/
    //public Transform soft;
    //public Transform crisp;
    //public Transform sweet;
    public Transform softPlace;
    public Transform crispPlace;
    public Transform sweetPlace;

    //public GameObject endPanel;

    private List<BaseMapRole> dealers;
    private int currentTimeCount = 0;
    public override IEnumerator StepStart()
    {
        //FTE_2_5_Manager.My.isClearGoods = false;
        NewCanvasUI.My.GamePause(false);
        StageGoal.My.playerSatisfy = 0;
        StageGoal.My.playerSatisfyText.text = "0";
        StageGoal.My.GetSatisfy(0);
        currentTimeCount = StageGoal.My.timeCount;
        FTE_2_5_Manager.My.packageKillNum = 0;
        FTE_2_5_Manager.My.saleKillNum = 0;
        FTE_2_5_Manager.My.nolikeKillNum = 0;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().needLimit = true;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitSeedCount = 2;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitPeasantCount = 2;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitMerchantCount = 2;
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitDealerCount = -1;
        FTE_2_5_Manager.My.dealer2.SetActive(true);
        FTE_2_5_Manager.My.dealer2.SetActive(true);
        FTE_2_5_Manager.My.npcMerchant.GetComponent<ProductMerchant>().buffList.Clear();
        FTE_2_5_Manager.My.npcMerchant.GetComponent<ProductMerchant>().buffList.Add(301);
        
        /*bornPoint1.GetComponent<Building>().BornEnemyForFTE_2_5(302);
        bornPoint2.GetComponent<Building>().BornEnemyForFTE_2_5(301);
        bornPoint3.GetComponent<Building>().BornEnemyForFTE_2_5(-1);*/
        BornPackage();
        BornSale();
        BornNoLike();
        SkipButton();
        //InvokeRepeating("CheckGoal", 2f, 0.5f);
        yield return null;
        FactoryUp();
        //InvokeRepeating("CheckReset", 3, 0.5f);
        InvokeRepeating("CheckGoal", 1, 0.5f);
        InvokeRepeating("CheckBuffOut", 3, 20);
        FTE_2_5_Manager.My.UpRole(FTE_2_5_Manager.My.dealer2, 0.32f);
        place2.transform.DOLocalMoveY(0, 1).Play().OnPause(() =>
        {
            place2.transform.DOLocalMoveY(0, 1).Play();
        });
        FTE_2_5_Manager.My.UpRole(FTE_2_5_Manager.My.dealer3, 0.32f);

        place3.transform.DOLocalMoveY(0, 1).Play().OnPause(() =>
        {
            place3.transform.DOLocalMoveY(0, 1).Play();
        });
    }
    void SkipButton()
    {
        if (needCheck && FTE_2_5_Manager.My.needSkip)
        {
            if (endButton != null)
            {
                
                endButton.onClick.AddListener(() =>
                {
                    for (int i = 0; i < missiondatas.data.Count; i++)
                    {
                        missiondatas.data[i].isFinish = true;
                    }
                });
                endButton.interactable = true;
                endButton.gameObject.SetActive(true);
            }
        }
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = FTE_2_5_Manager.My.packageKillNum;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
                StopCoroutine(bornPoint1.GetComponent<Building>().BornEnemyForFTE_2_5_1(27,0));
            }
        }
        
        if (missiondatas.data[1].isFinish == false)
        {
            missiondatas.data[1].currentNum = FTE_2_5_Manager.My.saleKillNum;
            if (missiondatas.data[1].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[1].isFinish = true;
                StopCoroutine(bornPoint2.GetComponent<Building>().BornEnemyForFTE_2_5_1(28, 0));
            }
        }
        
        if (missiondatas.data[2].isFinish == false)
        {
            missiondatas.data[2].currentNum = FTE_2_5_Manager.My.nolikeKillNum;
            if (missiondatas.data[2].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[2].isFinish = true;
                StopCoroutine(bornPoint3.GetComponent<Building>().BornEnemyForFTE_2_5_1(29, 0));
            }
        }
    }

    void FactoryUp()
    {
        /*for (int i = 0; i < factorys.Count; i++)
        {
            factorys[i].SetActive(true);
            factorys[i].transform.DOMoveY(0.32f, 1f).Play().OnPause(() =>
            {
                factorys[i].transform.DOMoveY(0.32f, 1f).Play();
            });
        }

        for (int i = 0; i < places.Count; i++)
        {
            places[i].transform.DOMoveY(0, 1).Play().OnPause(() =>
            {
                places[i].transform.DOMoveY(0, 1).Play();
            });
        }*/
        FTE_2_5_Manager.My.sweetFactory.SetActive(true);
        FTE_2_5_Manager.My.UpRole(FTE_2_5_Manager.My.sweetFactory, 0.32f);
        sweetPlace.DOLocalMoveY(0f, 1f).Play().OnPause(() =>
        {
            sweetPlace.DOLocalMoveY(0f, 1f).Play();
        });
        FTE_2_5_Manager.My.crispFactory.SetActive(true);

        FTE_2_5_Manager.My.UpRole(FTE_2_5_Manager.My.crispFactory, 0.32f);
        
        crispPlace.DOLocalMoveY(0f, 1f).Play().OnPause(() =>
        {
            crispPlace.DOLocalMoveY(0f, 1f).Play();
        });
        FTE_2_5_Manager.My.softFactory.SetActive(true);

        FTE_2_5_Manager.My.UpRole(FTE_2_5_Manager.My.softFactory, 0.32f);
        
        softPlace.DOLocalMoveY(0f, 1f).Play().OnPause(() =>
        {
            softPlace.DOLocalMoveY(0f, 1f).Play();
        });
    }

    void BornPackage()
    {
        StartCoroutine(bornPoint1.GetComponent<Building>().BornEnemyForFTE_2_5_1(27, 0));
        //InvokeRepeating("CheckPackage", 2f, 0.5f);
    }
    void BornSale()
    {
        StartCoroutine(bornPoint2.GetComponent<Building>().BornEnemyForFTE_2_5_1(28, 0));
        //InvokeRepeating("CheckSale", 2f, 0.5f);
    }
    void BornNoLike()
    {
        StartCoroutine(bornPoint3.GetComponent<Building>().BornEnemyForFTE_2_5_1(29, 0));
        //InvokeRepeating("CheckNolike", 2f, 0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        FTE_2_5_Manager.My.bornEnemy = false;
        /*endPanel.GetComponent<Button>().onClick.AddListener(() =>
        {
            NetworkMgr.My.UpdatePlayerFTE("2.5", ()=>SceneManager.LoadScene("Map"));
        });*/
        yield return new WaitForSeconds(2f);
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
            for (int i = 0; i < MissionManager.My.signs.Count; i++)
            {
                MissionManager.My.signs[i].ResetSuccess();
            }
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
                        HttpManager.My.ShowTip("有口味效果被浪费！请注意检查‘<color=green>"+dealers[i].baseRoleData.baseRoleData.roleName+"</color>’相关的生产链！", null, 5);
                        break;
                    }
                }
            }
        }
    }
}
