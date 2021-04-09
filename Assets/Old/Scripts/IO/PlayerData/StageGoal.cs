using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
using System;
using static GameEnum;

public class StageGoal : MonoSingleton<StageGoal>
{
    /// <summary>
    /// 玩家金币
    /// </summary>
    public int playerGold;

    /// <summary>
    /// 玩家满意度
    /// </summary>
    public int playerSatisfy;

    /// <summary>
    /// 玩家血量
    /// </summary>
    public int playerHealth;

    /// <summary>
    /// 玩家最大血量
    /// </summary>
    public int playerMaxHealth;

    /// <summary>
    /// 最大赤字
    /// </summary>
    public int maxMinusGold;

    /// <summary>
    /// 是否处于赤字状态
    /// </summary>
    public bool isOverMaxMinus = false;

    /// <summary>
    /// 玩家科技点数
    /// </summary>
    public int playerTechPoint;

    /// <summary>
    /// 当前波数
    /// </summary>
    public int currentWave = 1;

    /// <summary>
    /// 本关总波数
    /// </summary>
    public int maxWaveNumber;

    public List<int> waitTimeList = new List<int>();

    public int timeCount = 1;

    public Tweener waveTween;

    private bool wudi = false;

    public int produceTime;

    /// <summary>
    /// 玩家操作时间戳列表
    /// </summary>
    public List<PlayerOperation> playerOperations = new List<PlayerOperation>();
    public StatItemDatasList statItemDatasList = new StatItemDatasList();

    #region UI
    
    public Text playerGoldText;

    public Text playerSatisfyText;

    public Text playerTechText;

    public Image playerHealthBar;

    public Text playerHealthText;

    public Text stageWaveText;

    public float maxHealtherBarLength;

    public Button menuOpenButton;

    public Button menuCloseButton;

    public List<Sprite> starSprites = new List<Sprite>();

    public Image starOne;

    public Image starTwo;

    public Image starThree;

    public Text starOneText;

    public Text starTwoText;

    public Text starThreeText;

    public Button skipToFirstWave;

    #endregion

    #region 统计

    public int killNumber = 0;

    public int totalCost = 0;

    public int tradeCost = 0;

    public int productCost = 0;

    public int extraCosts = 0;

    public int totalIncome = 0;

    public int consumeIncome = 0;

    public int npcIncome = 0;

    public int otherIncome = 0;

    public int npcTpIncome = 0;
    public int workerTpIncome = 0;
    public int buffTpIncome = 0;
    public int buildTpCost = 0;
    public int mirrorTpCost = 0;
    public int unlockTpCost = 0;

    public List<DataStat> dataStats;

    public Dictionary<string, int> otherIncomes = new Dictionary<string, int>();

    public Dictionary<BaseMapRole, int> npcIncomes = new Dictionary<BaseMapRole, int>();
    public Dictionary<string, int> npcIncomesEx = new Dictionary<string, int>();

    public Dictionary<BaseMapRole, int> buildingCosts = new Dictionary<BaseMapRole, int>();

    public Dictionary<string, int> extraCost = new Dictionary<string, int>();

    public Dictionary<ScoreType, int> scoreStats = new Dictionary<ScoreType, int>();

    int starNum = 1;
    string[] stars = new string[] { "1", "0", "0" };
    PlayerReplay tempReplay;
    #endregion

    public int totalPauseTime = 0;

    public int totalMinusGoldTime = 0;

    public int startTime;

    public int endTime;

    public StageType currentType;

    /// <summary>
    /// 当前关卡敌人波数数据
    /// </summary>
    public List<StageEnemyData> enemyDatas;

    /// <summary>
    /// 倒计时
    /// </summary>
    public WaveCount waveCountItem;

    public int maxRoleLevel = 5;

    public GameObject lankuang;
    /// <summary>
    /// 玩家消耗金币
    /// </summary>
    /// <param name="num"></param>
    public void CostPlayerGold(int num, bool isNotTurn =false)
    {
        if (PlayerData.My.isPrediction)
        {
            predictCost -= num;
            predictGold -= num;
            return;
        }
        if (CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
        {
            if (playerGold - num <= 10000)
            {
                playerGold = 10000000;
            }
            else
            {
                playerGold -= num;
            }
        }
        else
        {
            playerGold -= num;
            if(!isNotTurn)
                UpdateTurnCost(num);
        }
        FloatInfoManager.My.MoneyChange(0 - num);
        if (playerGold < maxMinusGold)
        {
            if (!isOverMaxMinus)
            {
                isOverMaxMinus = true;
                AudioManager.My.PlaySelectType(AudioClipType.MinusMoney);
                DataUploadManager.My.AddData(DataEnum.赤字次数);
            }
            foreach (BaseMapRole role in PlayerData.My.MapRole)
            {
                if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
                {
                    role.GetComponent<BaseSkill>().CancelSkill();

                }
            }
        }
        else
        {
            if (isOverMaxMinus)
            {
                isOverMaxMinus = false;
                AudioManager.My.PlaySelectType(GameEnum.AudioClipType.PosivityMoney);
                foreach (BaseMapRole role in PlayerData.My.MapRole)
                {
                    if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
                    {
                        role.GetComponent<BaseSkill>().ReUnleashSkills();
                    }
                }
            }
        }
        SetInfo();
    }

    /// <summary>
    /// 消耗人脉值
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public bool CostTechPoint(int num)
    {
        if (playerTechPoint < num)
        {
            HttpManager.My.ShowTip("Mega值不足！");
            return false;
        }

        if (PlayerData.My.isPrediction )
        {
            predictTPcost -= num;
        }
        else
        {
            FloatInfoManager.My.TechChange(0 - num);
            playerTechPoint -= num;
        }
        SetInfo();
        return true;
    }

    /// <summary>
    /// 获得人脉值
    /// </summary>
    /// <param name="num"></param>
    public void GetTechPoint(int num)
    {
        if (PlayerData.My.isPrediction)
        {
            predictTPadd += num;
            return;
        }
        FloatInfoManager.My.TechChange(num);
        playerTechPoint += num;
        SetInfo();
    }

    /// <summary>
    /// 消费者到达终点时UI提示
    /// </summary>
    public void ConsumerAliveTip()
    {
        if (CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
        {
            return;
        }
        if (!PlayerData.My.isPrediction)
            CameraPlay.OneHit(new Color(1f, 0.3896085f, 0.07075471f, 0f), 0.5f);
        //CameraPlay.Hit(tipColor, 0.5f);
    }

    /// <summary>
    /// 消费者被满意的属性消灭时UI提示
    /// </summary>
    public void ConsumerExtraPerTip()
    {
        playerSatisfyText.transform.parent.transform.DOScale(new Vector3(1.3f, 1.3f, 1f), 0.3f).Play().SetEase(Ease.Linear).OnComplete(() =>
        {
            playerSatisfyText.transform.parent.transform.DOScale(Vector3.one, 0.2f).Play().SetEase(Ease.Linear).timeScale = 1f / DOTween.timeScale;
        }).timeScale = 1f / DOTween.timeScale;
    }


    /// <summary>
    /// 玩家获得金币
    /// </summary>
    /// <param name="num"></param>
    public void GetPlayerGold(int num , bool isNotTurn=false)
    {
        if (PlayerData.My.isPrediction)
        {
            predictIncome += num;
            predictGold += num;
            return;
        }
        if (SceneManager.GetActiveScene().name == "FTE_0-1" || SceneManager.GetActiveScene().name == "FTE_0-2")
        {
            if (playerGold + num >= 10000000)
            {
                playerGold = 10000000;
            }
            else
            {
                playerGold += num;

            }
        }
        else
        {
            if (currentType == StageType.Normal && !CommonParams.fteList.Contains(SceneManager.GetActiveScene().name) && !isNotTurn)
            {
                UpdateTurnIncome(num);
            }
            playerGold += num;
        }
        FloatInfoManager.My.MoneyChange(num);
        if (playerGold < maxMinusGold)
        {
            if (!isOverMaxMinus)
            {
                isOverMaxMinus = true;
                AudioManager.My.PlaySelectType(AudioClipType.MinusMoney);
                DataUploadManager.My.AddData(DataEnum.赤字次数);
            }
            foreach (BaseMapRole role in PlayerData.My.MapRole)
            {
                if (role.baseRoleData.baseRoleData.roleType == RoleType.Seed)
                {
                    role.GetComponent<BaseSkill>().CancelSkill();
                }
            }
        }
        else
        {
            if (isOverMaxMinus)
            {
                isOverMaxMinus = false;
                AudioManager.My.PlaySelectType(AudioClipType.PosivityMoney);
                foreach (BaseMapRole role in PlayerData.My.MapRole)
                {
                    if (role.baseRoleData.baseRoleData.roleType == RoleType.Seed)
                    {
                        role.GetComponent<BaseSkill>().ReUnleashSkills();
                    }
                }
            }
        }
        SetInfo();
    }

    /// <summary>
    /// 玩家获得满意度
    /// </summary>
    /// <param name="num"></param>
    public void GetSatisfy(int num)
    {
        if (PlayerData.My.isPrediction)
        {
            predictScore += num;
            return;
        }
        if (int.MaxValue - playerSatisfy < num)
        {
            playerSatisfy = int.MaxValue;
        }
        else
        {
            playerSatisfy += num;
        }
        SetInfo();
    }

    /// <summary>
    /// 玩家失去生命值
    /// </summary>
    /// <param name="num"></param>
    public void LostHealth(int num)
    {
        if (PlayerData.My.isPrediction)
        {
            predictHealth += num;
        }
        else
        {
            playerHealth += num;
            SetInfo();
        }
        CheckDead();
    }

    /// <summary>
    /// 设置两个条的长度    
    /// </summary>
    public void SetInfo(float time = 0.2f)
    {
        //        print("time:" + time);
        float per = playerHealth / (float)playerMaxHealth;
        if (per > 1f)
        {
            per = 1f;
            playerHealth = playerMaxHealth;
        }
        playerHealthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(maxHealtherBarLength * per, playerHealthBar.GetComponent<RectTransform>().sizeDelta.y);
        playerGoldText.DOText(playerGold.ToString(), time, true, ScrambleMode.None).Play();

        if (playerGold > 0)
            playerGoldText.DOColor(Color.white, time).Play();
        else if (playerGold >= maxMinusGold)
            playerGoldText.DOColor(Color.yellow, time).Play();
        else
            playerGoldText.DOColor(Color.red, time).Play();

        playerSatisfyText.DOText(playerSatisfy.ToString(), time, true, ScrambleMode.None).Play();

        if (PlayerData.My.cheatIndex1 || PlayerData.My.cheatIndex2 || PlayerData.My.cheatIndex3)
            playerSatisfyText.DOColor(Color.gray, time).Play();
        else
            playerSatisfyText.DOColor(Color.white, time).Play();
        playerHealthText.text = (playerHealth / (float)playerMaxHealth).ToString("P");
        if (playerHealth / (float)playerMaxHealth > 0.5f)
        {
            NewCanvasUI.My.EndLowHealth();
            playerHealthText.color = Color.white;
        }
        else if (playerHealth / (float)playerMaxHealth > 0.2f)
        {
            NewCanvasUI.My.EndLowHealth();
            playerHealthText.color = Color.yellow;
        }
        else
        {
            NewCanvasUI.My.StartLowHealth();
            playerHealthText.color = Color.red;
        }
        playerTechText.DOText(playerTechPoint.ToString(), time, true, ScrambleMode.None).Play();
    }

    /// <summary>
    /// 设置UI信息
    /// </summary>
    public void SetInfoImmidiate()
    {
        float per = playerHealth / (float)playerMaxHealth;
        if (per > 1f)
        {
            per = 1f;
            playerHealth = playerMaxHealth;
        }
        playerHealthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(maxHealtherBarLength * per, playerHealthBar.GetComponent<RectTransform>().sizeDelta.y);
        playerGoldText.text = playerGold.ToString();
        if (playerGold > 0)
            playerGoldText.DOColor(Color.white, 0.02f).Play();
        else if (playerGold >= maxMinusGold)
            playerGoldText.DOColor(Color.yellow, 0.02f).Play();
        else
            playerGoldText.DOColor(Color.red, 0.02f).Play();

        playerSatisfyText.text = playerSatisfy.ToString();
        if (PlayerData.My.cheatIndex1 || PlayerData.My.cheatIndex2 || PlayerData.My.cheatIndex3)
            playerSatisfyText.DOColor(Color.gray, 0.02f).Play();
        else
            playerSatisfyText.DOColor(Color.white, 0.02f).Play();
        playerHealthText.text = (playerHealth / (float)playerMaxHealth).ToString("P");
        if (playerHealth / (float)playerMaxHealth > 0.5f)
        {
            NewCanvasUI.My.EndLowHealth();
            playerHealthText.color = Color.white;
        }
        else if (playerHealth / (float)playerMaxHealth > 0.2f)
        {
            NewCanvasUI.My.EndLowHealth();
            playerHealthText.color = Color.yellow;
        }
        else
        {
            NewCanvasUI.My.StartLowHealth();
            playerHealthText.color = Color.red;
        }
        playerTechText.text = playerTechPoint.ToString();
    }

    /// <summary>
    /// 检测死亡条件
    /// </summary>
    public void CheckDead()
    {
        if (PlayerData.My.isPrediction)
        {
            if (predictHealth <= 0)
            {
                var list = FindObjectsOfType<ConsumeSign>();
                for (int i = 0; i <list.Length ; i++)
                {
                    Destroy(list[i].gameObject);
                }
                EndPredictionTurn();
            }
            return;
        }
        if (playerHealth <= 0)
        {
            //for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
            //{
            //    PlayerData.My.MapRole[i].OnBeforeDead();
            //}
            if (wudi)
            {
                playerHealth = 100;
                return;
            }
            Lose();
        }
    }

    /// <summary>
    /// 检测通关条件
    /// </summary>
    public void CheckWin()
    {
        if (currentType == StageType.Boss)
            return;
        ConsumeSign[] list = FindObjectsOfType<ConsumeSign>();
        bool isComplete = true;
        foreach (Building b in BuildingManager.My.buildings)
        {
            if (!b.isFinishSpawn)
            {
                isComplete = false;
                break;
            }
        }
        if (list.Length == 0 && isComplete)
        {
            if (playerHealth > 0)
            {
                //print("胜利");
                Win();
            }
        }
    }

    /// <summary>
    /// 检测本波是否结束
    /// </summary>
    public void CheckEndTurn()
    {
        if (currentType != StageType.Normal)
            return;
        ConsumeSign[] list = FindObjectsOfType<ConsumeSign>();
        bool isComplete = true;
        foreach (Building b in BuildingManager.My.buildings)
        {
            if (!b.isFinishSpawn && b.gameObject.activeInHierarchy)
            {
                isComplete = false;
                break;
            }
        }
        if (list.Length == 0 && isComplete)
        {
            if (playerHealth > 0)
            {
                //print("回合结束");
                if (PlayerData.My.isPrediction)
                {
                    EndPredictionTurn();
                }
                else
                {
                    EndTurn();
                }
            }
        }
    }

    public void CheckAfterReconnect()
    {
        CheckDead();
        CheckWin();
    }

    /// <summary>
    /// 通关时调用函数
    /// </summary>
    public void Win(bool isPassByKey=false)
    {
        BaseLevelController.My.CancelInvoke("CheckStarTwo");
        BaseLevelController.My.CancelInvoke("CheckStarOne");
        BaseLevelController.My.CancelInvoke("CheckStarThree");
        BaseLevelController.My.CancelInvoke("UpdateInfo");
        if (BaseLevelController.My.starTwoStatus)
        {
            starNum += 1;
            stars[1] = "1";
        }
        if (BaseLevelController.My.starThreeStatus)
        {
            starNum += 1;
            stars[2] = "1";
        }
        NewCanvasUI.My.GamePause(false);
        NewCanvasUI.My.EndLowHealth();
        UpdatePlayerScoreEnd();
        WinManager.My.InitWin(isPassByKey);
        PrintStat();
    }

    /// <summary>
    /// 显示玩家血量
    /// </summary>
    public void ShowHealthText()
    {
        playerHealthText.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏玩家血量
    /// </summary>
    public void HideHealthText()
    {
        playerHealthText.gameObject.SetActive(false);
    }

    void CommitProgress(Action doPass, Action doFail)
    {
        NetworkMgr.My.UpdateLevelProgress(NetworkMgr.My.currentLevel, starNum, stars[0] + stars[1] + stars[2], "000", playerSatisfy, doPass, doFail);
    }

    /// <summary>
    /// 失败时调用函数
    /// </summary>
    public void Lose()
    {
        if (currentType == StageType.Normal || currentType == StageType.Turn)
        {
            NewCanvasUI.My.GamePause(false);
            NewCanvasUI.My.lose.SetActive(true);
            NewCanvasUI.My.lose.GetComponent<LosePanel>().InitOtherKey();
            NewCanvasUI.My.EndLowHealth();
            //NewCanvasUI.My.Panel_Lose.SetActive(true);
            if (NetworkMgr.My.isUsingHttp)
            {
                CommitLose();
            }
        }
        else if (currentType == StageType.Boss)
        {
            if (BaseLevelController.My.starOneStatus)
            {
                Win();
            }
            else
            {
                NewCanvasUI.My.GamePause(false);
                NewCanvasUI.My.lose.SetActive(true);
                //NewCanvasUI.My.Panel_Lose.SetActive(true);
                if (NetworkMgr.My.isUsingHttp)
                {
                    CommitLose();
                }
            }
        }
    }

    public void CommitLose()
    {
        endTime = TimeStamp.GetCurrentTimeStamp();
        UpdatePlayerScoreEnd();
        // 第一回合退出不会上传相关信息
        if (currentWave == 1)
        {
            return;
        }
        //Debug.LogWarning("game time: " + (endTime - startTime) + "   operations nums: " + playerOperations.Count);
        if (endTime - startTime <= 20 || playerOperations.Count <= 5)
            return;
        tempReplay = new PlayerReplay(false);
        NetworkMgr.My.AddReplayData(tempReplay);
        NetworkMgr.My.UpdateLevelProgress( int.Parse(SceneManager.GetActiveScene().name.Split('_')[1]), 0,"000", "000", 0);
        PrintStat();
    }

    /// <summary>
    /// 根据波数刷敌人
    /// </summary>
    public void WaveCount()
    {
        if (!PlayerData.My.isPrediction)
        {
            if (playerGold < 0)
            {
                totalMinusGoldTime++;
            }
            stageWaveText.text = (currentWave - 1).ToString() + "/" + maxWaveNumber.ToString();
        }
        MapManager.My.CheckDive(timeCount);
        if (currentWave <= maxWaveNumber)
        {
            if (currentType != StageType.Normal)
            {
                if (timeCount >= waitTimeList[currentWave - 1])
                {
                    BuildingManager.My.WaveSpawnConsumer(currentWave);
                    currentWave++;
                    stageWaveText.text = (currentWave - 1).ToString() + "/" + maxWaveNumber.ToString();
                    stageWaveText.transform.DOPunchScale(new Vector3(1.3f, 1.3f, 1.3f), 1f, 1).Play();
                }
            }
            else
            {
                CheckEndTurn();
            }
            transform.DOScale(1f, 0.985f).SetEase(Ease.Linear).OnComplete(() =>
            {
                timeCount++;
                if (PlayerData.My.isPrediction)
                {
                    if (timeCount % 20 == 0)
                    {
                        if (predictGold > 0)
                        {
                            float add = 0.05f;
                            if (PlayerData.My.qiYeJiaZhi[3])
                            {
                                add = 0.1f;
                            }
                            if (!SceneManager.GetActiveScene().name.Equals("FTE_2.5"))
                                GetSatisfy((int)(predictGold * add));
                            //ScoreGet(ScoreType.金钱得分, (int)(playerGold * add));
                            if (PlayerData.My.xianJinLiu[5])
                            {
                                GetPlayerGold(predictGold * 5 / 100);
                                //Income(playerGold * 5 / 100,IncomeType.Other,null,"利息");
                            }
                        }
                        if (PlayerData.My.yeWuXiTong[3])
                        {
                            int count = 0;
                            for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
                            {
                                if (!PlayerData.My.MapRole[i].isNpc)
                                {
                                    count++;
                                }
                            }
                            if (count <= 2)
                            {
                                GetTechPoint(20);
                            }
                        }
                    }
                }
                else
                {
                    if (timeCount % 5 == 0)
                    {
                        Stat();
                    }
                    if (timeCount % 20 == 0)
                    {
                        if (playerGold > 0)
                        {
                            float add = 0.05f;
                            if (PlayerData.My.qiYeJiaZhi[3])
                            {
                                add = 0.1f;
                            }
                            if (!SceneManager.GetActiveScene().name.Equals("FTE_2.5"))
                                GetSatisfy((int)(playerGold * add));
                            ScoreGet(ScoreType.金钱得分, (int)(playerGold * add));
                            if (PlayerData.My.xianJinLiu[5])
                            {
                                GetPlayerGold(playerGold * 5 / 100);
                                Income(playerGold * 5 / 100, IncomeType.Other, null, "利息");
                            }
                        }
                        if (PlayerData.My.yeWuXiTong[3])
                        {
                            int count = 0;
                            for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
                            {
                                if (!PlayerData.My.MapRole[i].isNpc)
                                {
                                    count++;
                                }
                            }
                            if (count <= 2)
                            {
                                GetTechPoint(20);
                            }
                        }
                    }
                }
                WaveCount();
            });
        }
        else
        {
            if (!CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
                CheckWin();
            transform.DOScale(1f, 0.985f).SetEase(Ease.Linear).OnComplete(() =>
            {
                timeCount++;
                stageWaveText.text = (currentWave - 1).ToString() + "/" + maxWaveNumber.ToString();
                if (timeCount % 5 == 0)
                {
                    Stat();
                }
                if (timeCount % 20 == 0)
                {
                    if (playerGold > 0)
                    {
                        float add = 0.05f;
                        if (PlayerData.My.qiYeJiaZhi[3])
                        {
                            add = 0.1f;
                        }
                        if (!SceneManager.GetActiveScene().name.Equals("FTE_2.5"))
                            GetSatisfy((int)(playerGold * add));
                        ScoreGet(ScoreType.金钱得分, (int)(playerGold * add));
                        if (PlayerData.My.xianJinLiu[5])
                        {
                            GetPlayerGold(playerGold * 5 / 100);
                            Income(playerGold * 5 / 100, IncomeType.Other, null, "利息");
                        }
                    }
                    if (PlayerData.My.yeWuXiTong[3])
                    {
                        int count = 0;
                        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
                        {
                            if (!PlayerData.My.MapRole[i].isNpc)
                            {
                                count++;
                            }
                        }
                        if (count <= 2)
                        {
                            GetTechPoint(20);
                        }
                    }
                }
                WaveCount();
            });
        }
    }

    public bool isInTurnCreateTrade = false;


    public bool isTurnStart = false;
    /// <summary>
    /// 回合开始
    /// </summary>
    public void NextTurn()
    {
        Stat();
        predict_btn.gameObject.SetActive(false);
        NewCanvasUI.My.ToggleSpeedButton(true);
        NewCanvasUI.My.GameNormal();
        waveCountItem.Move();
        isTurnStart = true;
        BuildingManager.My.RestartAllBuilding();
        PlayerData.My.RoleTurnCheckBuff();
        // 重置回合收支
        ResetTurnIncomeAndCost();
        LockOperation();
        //TODO 更新金币消耗UI信息
        //TODO 检查错误操作（果汁厂没输入）
        transform.DOScale(1f, produceTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            BuildingManager.My.WaveSpawnConsumer(currentWave);
            stageWaveText.text = (currentWave - 1).ToString() + "/" + maxWaveNumber.ToString();
            stageWaveText.transform.DOPunchScale(new Vector3(1.3f, 1.3f, 1.3f), 1f, 1).Play();
            currentWave++;
        });
    }

    /// <summary>
    /// 回合结束时调用
    /// </summary>
    public void EndTurn()
    {
        Stat();
        predict_btn.gameObject.SetActive(true);
        NewCanvasUI.My.ToggleSpeedButton(false);
        NewCanvasUI.My.GamePause();
        skipToFirstWave.gameObject.SetActive(true);
        lankuang.SetActive(true);
        isTurnStart = false;
        waveCountItem.Init(enemyDatas, currentWave - 1);
        waveCountItem.Clear();
        PlayerData.My.RoleTurnEnd();
        TradeManager.My.TurnTradeCost();
        //TODO 解锁准备阶段的操作
        // 显示收支
        isEndTurn = true;
        ShowTurnIncomeAndCost();
        UnlockOperation();
        //TODO 结算buff/角色周期性效果
    }

    /// <summary>
    /// 生成玩家操作记录
    /// </summary>
    public void RecordOperation(OperationType _type, List<string> param)
    {
        PlayerOperation operation = new PlayerOperation();
        operation.type = _type;
        operation.operateTime = timeCount;
        operation.operationParam = new List<string>();
        operation.operationParam.AddRange(param);
        playerOperations.Add(operation);
    }

    /// <summary>
    /// 获得星数对应的装备
    /// </summary>
    /// <param name="starNumber"></param>
    /// <returns></returns>
    public List<GearData> GetStarGearData(int starNumber)
    {
        List<GearData> result = new List<GearData>();
        string sceneName = SceneManager.GetActiveScene().name;
        StageData data = GameDataMgr.My.GetStageDataByName(sceneName);
        List<int> gearList = new List<int>();
        switch (starNumber)
        {
            case 1:
                gearList = data.starOneEquip;
                break;
            case 2:
                gearList = data.starTwoEquip;
                break;
            case 3:
                gearList = data.starThreeEquip;
                break;
            default:
                throw new System.Exception("星数输入错误！");
        }
        foreach (int item in gearList)
        {
            GearData gearData = GameDataMgr.My.GetGearData(item);
            PlayerData.My.GetNewGear(item);
            result.Add(gearData);
        }
        return result;
    }

    /// <summary>
    /// 获得星数对应的工人
    /// </summary>
    /// <param name="starNumber"></param>
    /// <returns></returns>
    public List<WorkerData> GetStarWorkerData(int starNumber)
    {
        List<WorkerData> result = new List<WorkerData>();
        string sceneName = SceneManager.GetActiveScene().name;
        StageData data = GameDataMgr.My.GetStageDataByName(sceneName);
        List<int> workerList = new List<int>();
        switch (starNumber)
        {
            case 1:
                workerList = data.starOneWorker;
                break;
            case 2:
                workerList = data.starTwoWorker;
                break;
            case 3:
                workerList = data.starThreeWorker;
                break;
            default:
                throw new System.Exception("星数输入错误！");
        }
        foreach (int item in workerList)
        {
            WorkerData workerData = GameDataMgr.My.GetWorkerData(item);
            PlayerData.My.GetNewWorker(item);
            result.Add(workerData);
        }
        return result;
    }

    /// <summary>
    /// 获得之前关卡所有装备（调试用）
    /// </summary>
    public void GetAllPreviousAward()
    {
        int count = int.Parse(SceneManager.GetActiveScene().name.Split('_')[1]);
        for (int i = 1; i < count; i++)
        {
            StageData data = GameDataMgr.My.GetStageDataByName("FTE_" + i.ToString());
            List<int> award = new List<int>();
            List<int> awardEquip = new List<int>();
            award.AddRange(data.starOneWorker);
            award.AddRange(data.starTwoWorker);
            award.AddRange(data.starThreeWorker);
            awardEquip.AddRange(data.starOneEquip);
            awardEquip.AddRange(data.starTwoEquip);
            awardEquip.AddRange(data.starThreeEquip);
            foreach (int item in award)
            {
                PlayerData.My.GetNewWorker(item);
            }
            foreach (var item in awardEquip)
            {
                PlayerData.My.GetNewGear(item);
            }
        }
    }

    /// <summary>
    /// 将关卡配置表读取到本关
    /// </summary>
    public void InitStage()
    {
        playerHealthBar = transform.parent.Find("Blood/PlayerHealthBar").GetComponent<Image>();
        playerHealthText = transform.parent.Find("Blood/Text").GetComponent<Text>();
        maxHealtherBarLength = playerHealthBar.GetComponent<RectTransform>().sizeDelta.x;
        playerGoldText = transform.parent.Find("UserInfo/Image_money/PlayerMoney").GetComponent<Text>();
        playerSatisfyText = transform.parent.Find("UserInfo/PlayerScore/PlayerScoreText").GetComponent<Text>();
        playerTechText = transform.parent.Find("UserInfo/Image_money/PlayerTech").GetComponent<Text>();
        stageWaveText = transform.parent.Find("UserInfo/Image_level/StageLevel").GetComponent<Text>();
        skipToFirstWave = transform.parent.Find("TimeScale/SkipFirst").GetComponent<Button>();
        lankuang = transform.parent.Find("Image_ReadyTime").gameObject;
        skipToFirstWave.onClick.AddListener(() =>
        {
            NextTurn();
            lankuang.SetActive(false);
            skipToFirstWave.gameObject.SetActive(false);
        });
        foreach (PlayerGear p in PlayerData.My.playerGears)
        {
            p.isEquiped = false;
        }
        foreach (PlayerWorker p in PlayerData.My.playerWorkers)
        {
            p.isEquiped = false;
        }
        InitStageData();
        PlayerData.My.playerConsumables.Clear();
        WaveCount();
    }

    /// <summary>
    /// Helper Function
    /// </summary>
    public void InitStageData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (CommonParams.fteList.Contains(sceneName))
        {
            playerHealth = 1000;
            playerMaxHealth = 1000;
            playerGold = 100000;
            playerTechPoint = 0;
            wudi = true;
     
            SetInfoImmidiate();
            if(SceneManager.GetActiveScene().name.Equals("FTE_4.5"))
                ReadStageEnemyData(sceneName);
            return;
        }
        //StartCoroutine(ReadStageEnemyData(sceneName));

        if (sceneName != "FTE_0")
        {
            if (NetworkMgr.My.isUsingHttp)
            {
                // 获取游戏开始时间
                NetworkMgr.My.LevelStartTime();
                // 获取游戏关卡
                NetworkMgr.My.currentLevel = int.Parse(sceneName.Split('_')[1]);
            }
        }
        StageData data = GameDataMgr.My.GetStageDataByName(sceneName);
        playerGold = data.startPlayerGold;
        if (PlayerData.My.cheatIndex1)
        {
            playerGold += 10000;
        }

        if (PlayerData.My.dingWei[0])
        {
            playerGold = playerGold * 110 / 100;
        }
        timeCount = 1;
        playerSatisfy = 0;
        produceTime = 10;
        maxMinusGold = -8000;
        if (PlayerData.My.xianJinLiu[2])
        {
            maxMinusGold = -12000;
        }
        playerHealth = data.startPlayerHealth;
        if (PlayerData.My.cheatIndex3)
            playerHealth = (int)(playerHealth * 1.5f);
        playerMaxHealth = playerHealth;
        maxWaveNumber = data.maxWaveNumber;
        playerTechPoint = data.startTech;
        currentType = data.stageType;
        SetInfoImmidiate();
        foreach (int i in data.waveWaitTime)
        {
            waitTimeList.Add(i);
        }
        ReadStageEnemyData(sceneName);
        if (currentType == StageType.Normal)
        {

        }
        else
        {
            skipToFirstWave.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 读取当前关卡敌人配置
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    void ReadStageEnemyData(string sceneName)
    {
        //string path = "file://" + Application.streamingAssetsPath + @"/Data/StageEnemy/" + sceneName + ".json";
        //WWW www = new WWW(@path);
        //yield return www;
        //if (www.isDone)
        //{
        //    if (www.error != null)
        //    {
        //        Debug.Log(www.error);
        //        yield return null;
        //    }
        //    else
        //    {
        //        string json = www.text.ToString();
        //string json = OriginalData.My.jsonDatas.GetLevelData(sceneName);
        //Debug.Log("-------" + json);
        //        StageEnemysData stageEnemyData = JsonUtility.FromJson<StageEnemysData>(json);
        //        ParseStageEnemyData(stageEnemyData);
        //    }
        //}
        if (NetworkMgr.My.useLocalJson)
        {
            StartCoroutine(GetEnemyData(sceneName));
        }
        else
        {
            string json = OriginalData.My.jsonDatas.GetLevelData(sceneName);
            //Debug.Log("-------" + json);
            StageEnemysData stageEnemyData = JsonUtility.FromJson<StageEnemysData>(json);
            ParseStageEnemyData(stageEnemyData);
        }
        //StartCoroutine(GetEnemyData(sceneName));
    }

    IEnumerator GetEnemyData(string sceneName)
    {
        string path = "file://" + Application.streamingAssetsPath + @"/Data/StageEnemy/" + sceneName + ".json";
        WWW www = new WWW(@path);
        yield return www;
        if (www.isDone)
        {
            if (www.error != null)
            {
                //Debug.Log(www.error);
                yield return null;
            }
            else
            {
                string json = www.text.ToString();
                //string json = OriginalData.My.jsonDatas.GetLevelData(sceneName);
                //Debug.Log("-------" + json);
                StageEnemysData stageEnemyData = JsonUtility.FromJson<StageEnemysData>(json);
                ParseStageEnemyData(stageEnemyData);
            }
        }
    }

    /// <summary>
    /// 将当前关卡敌人配置转化成可用数据
    /// </summary>
    /// <param name="data"></param>
    public void ParseStageEnemyData(StageEnemysData data)
    {
        enemyDatas = new List<StageEnemyData>();
        foreach (StageEnemyItem s in data.stageEnemyItems)
        {
            StageEnemyData stageEnemyData = new StageEnemyData();
            stageEnemyData.waveNumber = int.Parse(s.waveNumber);
            stageEnemyData.point1 = s.point1.Split(',').ToList();
            stageEnemyData.point2 = s.point2.Split(',').ToList();
            stageEnemyData.point3 = s.point3.Split(',').ToList();
            stageEnemyData.point4 = s.point4.Split(',').ToList();
            stageEnemyData.point5 = s.point5.Split(',').ToList();
            stageEnemyData.point6 = s.point6.Split(',').ToList();
            enemyDatas.Add(stageEnemyData);
        }
        BuildingManager.My.InitAllBuilding(enemyDatas);
        if (currentType == StageType.Normal)
        {
            waveCountItem.Init(enemyDatas, 0);
        }
        else
        {
            waveCountItem.Init(enemyDatas);
        }

    }


    /// <summary>
    /// 右侧星数菜单隐藏
    /// </summary>
    public void MenuHide()
    {
        
        if (CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
        {
            GetComponent<RectTransform>().DOAnchorPosX(18000.4f, 0.3f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                menuCloseButton.gameObject.SetActive(false);
                menuOpenButton.gameObject.SetActive(true);
            }).Play();
            return;
        }
        GetComponent<RectTransform>().DOAnchorPosX(180.4f, 0.3f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            menuCloseButton.gameObject.SetActive(false);
            menuOpenButton.gameObject.SetActive(true);
        }).Play();
    }

    /// <summary>
    /// 右侧星数菜单隐藏
    /// </summary>
    public void MenuShow()
    {
        GetComponent<RectTransform>().DOAnchorPosX(-210f, 0.3f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            menuCloseButton.gameObject.SetActive(true);
            menuOpenButton.gameObject.SetActive(false);
        }).Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitStage();
        MenuHide();
        //GetAllPreviousAward();
        cameraPos = Camera.main.transform.position;
        Stat();
        startTime = TimeStamp.GetCurrentTimeStamp();
        menuOpenButton.onClick.AddListener(MenuShow);
        if (predict_btn !=null)
        predict_btn.onClick.AddListener(PredictionNextTurn);
        // 在第九关实时上传分数
        InitRtScore();
    }
    private Vector3 cameraPos;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    DOTween.PlayAll();
            //    DOTween.timeScale = 16f;
            //    DOTween.defaultAutoPlay = AutoPlay.All;
            //}
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    if(!CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
                        Win(true);
                }
            }
            //if (Input.GetKeyDown(KeyCode.M))
            //{
            //    GetPlayerGold(10000);
            //    GetTechPoint(1000);
            //    playerHealth = playerMaxHealth;
            //}
            if (Input.GetKeyDown(KeyCode.L))
            {
                Lose();
            }
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Map");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                //string sceneName = SceneManager.GetActiveScene().name;
                if (SceneManager.GetActiveScene().name.Contains("."))
                {
                    //Debug.LogWarning("skip jiaoxue ");
                    //NetworkMgr.My.AddTeachLevel(TimeStamp.GetCurrentTimeStamp()-startTime, SceneManager.GetActiveScene().name, 1);
                    NetworkMgr.My.UpdatePlayerFTE(SceneManager.GetActiveScene().name.Split('_')[1], () =>
                    {
                        SceneManager.LoadScene("Map");
                    });
                }
            }
        }
    }

    public void Income(int num, IncomeType incomeType, BaseMapRole npc = null, string otherName = "")
    {
        if (PlayerData.My.isPrediction)
        {
            //predictIncome += num;
            return;
        }
        totalIncome += num;
        switch (incomeType)
        {
            case IncomeType.Consume:
                consumeIncome += num;
                turnConsumerIncome += num;
                break;
            case IncomeType.Npc:
                npcIncome += num;
                if (npc == null)
                {
                    //print(otherName);
                    if (npcIncomesEx.ContainsKey(otherName))
                    {
                        npcIncomesEx[otherName] += num;
                    }
                    else
                    {
                        //print(npcIncomesEx.Count);
                        npcIncomesEx.Add(otherName, num);
                    }
                }
                else
                {
                    if (npcIncomes.ContainsKey(npc))
                    {
                        npcIncomes[npc] += num;
                    }
                    else
                    {
                        npcIncomes.Add(npc, num);
                    }
                }
                break;
            case IncomeType.Other:
                otherIncome += num;
                if (otherIncomes.ContainsKey(otherName))
                {
                    otherIncomes[otherName] += num;
                }
                else
                {
                    otherIncomes.Add(otherName, num);
                }
                break;
        }
        //if (DataStatPanel.My.isShow)
        //{
        //    DataStatPanel.My.ShowStat();
        //}
        DataStatPanel.My.RefreshIncome(totalIncome, consumeIncome, npcIncomesEx, npcIncomes, otherIncomes, timeCount);
    }

    public void Expend(int num, ExpendType expendType, BaseMapRole build = null, string extraName = "")
    {
        if (PlayerData.My.isPrediction)
        {
            //predictCost += num;
            return;
        }
        totalCost += num;
        switch (expendType)
        {
            case ExpendType.TradeCosts:
                tradeCost += num;
                turnTradeCost += num;
                break;
            case ExpendType.ProductCosts:
                productCost += num;
                if (buildingCosts.ContainsKey(build))
                {
                    buildingCosts[build] += num;
                }
                else
                {
                    buildingCosts.Add(build, num);
                }
                break;
            case ExpendType.AdditionalCosts:
                extraCosts += num;
                if (extraCost.ContainsKey(extraName))
                {
                    extraCost[extraName] += num;
                }
                else
                {
                    extraCost.Add(extraName, num);
                }
                break;
            default:
                break;
        }
        DataStatPanel.My.RefreshExpend(totalCost, tradeCost, buildingCosts, extraCost, timeCount);
    }

    public void IncomeTp(int num, IncomeTpType incomeTpType)
    {
        switch (incomeTpType)
        {
            case IncomeTpType.Npc:
                npcTpIncome += num;
                break;
            case IncomeTpType.Worker:
                workerTpIncome += num;
                break;
            case IncomeTpType.Buff:
                buffTpIncome += num;
                break;
        }
    }

    public void CostTp(int num, CostTpType costTpType)
    {
        switch (costTpType)
        {
            case CostTpType.Build:
                buildTpCost += num;
                break;
            case CostTpType.Mirror:
                mirrorTpCost += num;
                break;
            case CostTpType.Unlock:
                unlockTpCost += num;
                break;
        }
    }

    public void ScoreGet(ScoreType type, int num)
    {
        if (PlayerData.My.isPrediction)
        {
            return;
        }
        if (scoreStats.ContainsKey(type))
        {
            scoreStats[type] += num;
        }
        else
        {
            scoreStats.Add(type, num);
        }
    }


    private void Stat()
    {
        if (PlayerData.My.isPrediction)
        {
            return;
        }
        if (dataStats == null)
        {
            dataStats = new List<DataStat>();
        }
        dataStats.Add(new DataStat(playerHealth, playerSatisfy, totalIncome, consumeIncome, totalCost, tradeCost, productCost, playerGold));
        AddStatItem();
    }

    public string ShowStat()
    {
        string list = "";
        foreach (var ds in dataStats)
        {
            //list += string.Format($"{ds.blood}\t{ds.score}\t{ds.cost}\t{ds.tradeCost}\t{ds.totalGold}\n");
        }
        return list;
    }

    private void AddStatItem()
    {
        List<StatItemData> statItems = new List<StatItemData>();
        statItems.Add(new StatItemData("ti", totalIncome * 60 / (timeCount == 0 ? 1 : timeCount), totalIncome, StatItemType.TotalIncome));
        statItems.Add(new StatItemData("ci", consumeIncome * 60 / (timeCount == 0 ? 1 : timeCount), consumeIncome, StatItemType.ConsumeIncome));
        statItems.Add(new StatItemData("toc", totalCost * 60 / (timeCount == 0 ? 1 : timeCount), totalCost, StatItemType.TotalCost));
        statItems.Add(new StatItemData("trc", tradeCost * 60 / (timeCount == 0 ? 1 : timeCount), tradeCost, StatItemType.TradeCost));

        foreach (var key in npcIncomes.Keys)
        {
            statItems.Add(new StatItemData(key.baseRoleData.baseRoleData.roleName, npcIncomes[key] * 60 / timeCount, npcIncomes[key], StatItemType.NpcIncome));
        }

        foreach (var key in npcIncomesEx.Keys)
        {
            statItems.Add(new StatItemData(key, npcIncomesEx[key] * 60 / timeCount, npcIncomesEx[key], StatItemType.NpcIncome));
        }

        foreach (var key in otherIncomes.Keys)
        {
            statItems.Add(new StatItemData(key, otherIncomes[key] * 60 / timeCount, otherIncomes[key], StatItemType.OtherIncome));
        }

        foreach (var key in buildingCosts.Keys)
        {
            statItems.Add(new StatItemData(key.baseRoleData.baseRoleData.roleName, buildingCosts[key] * 60 / timeCount, buildingCosts[key], StatItemType.BuildCost));
        }

        foreach (var key in extraCost.Keys)
        {
            statItems.Add(new StatItemData(key, 0, extraCost[key], StatItemType.ExtraCost));
        }

        statItemDatasList.statItemDatasList.Add(new StatItemDatas(statItems));

    }

    private void PrintStat()
    {
        //Debug.Log(JsonUtility.ToJson(statItemDatasList).ToString());
    }


    /// <summary>
    /// 玩家操作结构
    /// </summary>
    [Serializable]
    public struct PlayerOperation
    {
        public int operateTime;

        public OperationType type;

        public List<string> operationParam;
    }
    private void OnGUI()
    {
        //if (GUILayout.Button("扣血"))
        //{
        //    Screen.SetResolution(1920, 1080, false);
        //}
        //if (GUILayout.Button("扣血12312321"))
        //{
        //    Screen.SetResolution(1920, 1080, true);
        //}
    }

    private int lastScore = 0;

    // 判断开启实时分数上传
    private void InitRtScore()
    {
        if (SceneManager.GetActiveScene().name.Equals("FTE_9"))
        {
            if (/*NetworkMgr.My.playerDatas.levelID<12 &&*/ NetworkMgr.My.playerGroupInfo.isOpenMatch)
            {
                InvokeRepeating("UpdateRTScore", 0.5f, 5);
            }
        }
    }

    // 在第九关实时更新分数
    private void UpdateRTScore()
    {
        if (playerSatisfy > lastScore)
        {
            lastScore = playerSatisfy;
            NetworkMgr.My.AddScore(lastScore, GameObject.FindObjectOfType<BossConsumer>() == null ? 0 : GameObject.FindObjectOfType<BossConsumer>().killCount, false, () =>
            {
                if (NetworkMgr.My.stopMatch)
                {
                    CancelInvoke("UpdateRTScore");
                }
            });
        }
    }

    // 在第九关，结束时上传最终分数
    public void UpdatePlayerScoreEnd()
    {
        if (SceneManager.GetActiveScene().name.Equals("FTE_9") && /*NetworkMgr.My.playerDatas.levelID < 12 &&*/ NetworkMgr.My.playerGroupInfo.isOpenMatch && !NetworkMgr.My.stopMatch)
        {
            CancelInvoke("UpdateRTScore");
            NetworkMgr.My.AddScore(playerSatisfy, GameObject.FindObjectOfType<BossConsumer>() == null ? 0 : GameObject.FindObjectOfType<BossConsumer>().killCount, true, () =>
            {

            });
        }
    }

    public void ResetAllCostAndIncome()
    {
        totalCost = 0;
        tradeCost = 0;
        productCost = 0;
        extraCosts = 0;
        totalIncome = 0;
        consumeIncome = 0;
        npcIncome = 0;
        otherIncome = 0;
        otherIncomes.Clear();
        npcIncomes.Clear();
        npcIncomesEx.Clear();
        buildingCosts.Clear();
        extraCost.Clear();
        DataStatPanel.My.RefreshIncome(totalIncome, consumeIncome, npcIncomesEx, npcIncomes, otherIncomes, timeCount);
        DataStatPanel.My.RefreshExpend(totalCost, tradeCost, buildingCosts, extraCost, timeCount);
    }

    #region 回合制

    private int turnTotalIncome = 0;
    private int turnTotalCost = 0;
    public int lastTurnTotalCost = 0;
    public int lastTurnTotalIncome = 0;
    public int lastTurnConsumerIncome = 0;
    public int turnConsumerIncome = 0;
    public int lastTurnTradeCost = 0;
    public int turnTradeCost = 0;
    public GameObject turnIncomeAndCostPanel;
    public Text turnTotalIncome_txt;
    public Text turnTotalCost_txt;
    public Text turnTitle_txt;
    public Button showTurn_btn;
    public Button hideTurn_btn;
    public bool isEndTurn = false;

    /// <summary>
    /// 重置回合收支（点击回合开始时）
    /// </summary>
    void ResetTurnIncomeAndCost()
    {
        if (isEndTurn)
        {
            lastTurnTotalCost = -turnTotalCost;
            turnTotalCost = 0;
            lastTurnTotalIncome = turnTotalIncome;
            turnTotalIncome = 0;
            lastTurnConsumerIncome = turnConsumerIncome;
            turnConsumerIncome = 0;
            lastTurnTradeCost = turnTradeCost;
            turnTradeCost = 0;
            UpdateTurnCost(0);
            UpdateTurnIncome(0);
        }
        turnTitle_txt.text = "本回合收支";
    }
    /// <summary>
    /// 展示本回合总收支
    /// </summary>
    public void ShowTurnIncomeAndCost()
    {
        if (isEndTurn)
        {
            turnTitle_txt.text = "上回合收支";
        }
        showTurn_btn.gameObject.SetActive(false);
        hideTurn_btn.gameObject.SetActive(true);
        turnIncomeAndCostPanel.GetComponent<RectTransform>().DOAnchorPosX(-214, 0.57f).Play();
    }

    public void HideTurnIncomeAndCost()
    {
        showTurn_btn.gameObject.SetActive(true);
        hideTurn_btn.gameObject.SetActive(false);
        turnIncomeAndCostPanel.GetComponent<RectTransform>().DOAnchorPosX(180, 0.57f).Play();
    }

    /// <summary>
    /// 统计本回合的收入
    /// </summary>
    /// <param name="income"></param>
    void UpdateTurnIncome(int income)
    {
        if (isEndTurn)
        {
            isEndTurn = false;
            turnTitle_txt.text = "本回合收支";
            turnTotalCost = 0;
            turnTotalIncome = 0;
            UpdateTurnCost(0);
        }
        turnTotalIncome += income;
        turnTotalIncome_txt.text = turnTotalIncome.ToString();
    }

    /// <summary>
    /// 统计本回合的支出
    /// </summary>
    /// <param name="cost"></param>
    void UpdateTurnCost(int cost)
    {
        if (isEndTurn)
        {
            isEndTurn = false;
            turnTitle_txt.text = "本回合收支";
            turnTotalCost = 0;
            turnTotalIncome = 0;
            UpdateTurnIncome(0);
        }
        turnTotalCost -= cost;
        turnTotalCost_txt.text = (-turnTotalCost).ToString();
    }

    /// <summary>
    /// 锁定操作
    /// </summary>
    void LockOperation()
    {
        // 锁三镜
        /*if (NewCanvasUI.My.transform.Find("ThreeMirror/GJJ"))
        {
            NewCanvasUI.My.transform.Find("ThreeMirror/GJJ").GetComponent<RectTransform>().DOAnchorPosY(-200, 1f).Play();
            NewCanvasUI.My.transform.Find("ThreeMirror/DLJ").GetComponent<RectTransform>().DOAnchorPosY(-200, 1f).Play();
            NewCanvasUI.My.transform.Find("ThreeMirror/TSJ").GetComponent<RectTransform>().DOAnchorPosY(-200, 1f).Play();
        }*/
        // 锁交易
        /*NewCanvasUI.My.hidePanel.gameObject.SetActive(false);
        NewCanvasUI.My.TurnToggleTradeButton(false);
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().createTradeButton.interactable = false;
        NewCanvasUI.My.Panel_NPC.GetComponent<NPCListInfo>().SetTradeButton(false);*/
        //NewCanvasUI.My.Panel_NPC.GetComponent<NPCListInfo>().unlockBtn.interactable = false;
        //TradeManager.My.HideAllIcon();
        // 锁删除
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().delete.interactable = false;
        // 禁止键角色
        if (SceneManager.GetActiveScene().name.Equals("FTE_1") || SceneManager.GetActiveScene().name.Equals("FTE_2"))
        {
            RoleListManager.My.transform.GetComponent<RectTransform>().DOAnchorPosY(-300, 1f).Play();
        }
        else
        {
            RoleListManager.My.LockRoleCreate();
        }
    }

    /// <summary>
    /// 解锁操作
    /// </summary>
    void UnlockOperation()
    {
        // 解三镜
        /*if (NewCanvasUI.My.transform.Find("ThreeMirror/GJJ"))
        {
            NewCanvasUI.My.transform.Find("ThreeMirror/GJJ").GetComponent<RectTransform>().DOAnchorPosY(-47, 1f).Play();
            NewCanvasUI.My.transform.Find("ThreeMirror/DLJ").GetComponent<RectTransform>().DOAnchorPosY(-47, 1f).Play();
            NewCanvasUI.My.transform.Find("ThreeMirror/TSJ").GetComponent<RectTransform>().DOAnchorPosY(-47, 1f).Play();
        }*/
        // 解交易
        /*NewCanvasUI.My.hidePanel.gameObject.SetActive(true);
        NewCanvasUI.My.TurnToggleTradeButton(true);
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().createTradeButton.interactable = true;
        NewCanvasUI.My.Panel_NPC.GetComponent<NPCListInfo>().SetTradeButton(true);*/
        //NewCanvasUI.My.Panel_NPC.GetComponent<NPCListInfo>().unlockBtn.interactable = true;
        //TradeManager.My.ShowAllIcon();
        // 解删除
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().delete.interactable = true;
        // 可以见角色
        if (SceneManager.GetActiveScene().name.Equals("FTE_1") || SceneManager.GetActiveScene().name.Equals("FTE_2"))
        {
            RoleListManager.My.transform.GetComponent<RectTransform>().DOAnchorPosY(67, 1f).Play();
        }
        else
        {
            RoleListManager.My.UnlockRoleCreate();
        }
    }

    #endregion

    #region Prediction

    public Button predict_btn;
    // 预测的金钱
    public int predictGold = 0;
    // 预测的收入
    public int predictIncome = 0;
    // 预测的支出
    public int predictCost = 0;
    // 预测的科技点支出
    public int predictTPcost = 0;
    // 预测的科技点收入
    public int predictTPadd = 0;
    // 预测的得分
    public int predictScore = 0;
    // 预测的满足消费者数量
    public int predictKillNum = 0;
    // 预测的血条
    public int predictHealth = 0;
    public Texture2D busy;
    public Texture2D normal;
    /// <summary>
    /// 预测回合
    /// </summary>
    public void PredictionNextTurn()
    {
        /*if (predict_btn.transform.GetComponentInChildren<Toggle>().isOn)
        {
            if (!CostTechPoint(10))
            {
                return;
            }
        }*/
        //Stat();
        //NewCanvasUI.My.ToggleSpeedButton(true);
        ResetPredictResult();
        PlayerData.My.isPrediction = true;
        TradeManager.My.RecycleAllGoods();
        PlayerData.My.SaveAllWarehourse();
        Cursor.SetCursor(busy, Vector2.zero, CursorMode.Auto);
        predict_btn.gameObject.SetActive(false);
        skipToFirstWave.gameObject.SetActive(false);
        NewCanvasUI.My.GameNormal();
        NewCanvasUI.My.GameAccelerate(20f);
        //waveCountItem.Move();
        BuildingManager.My.RestartAllBuilding();
        // 重置回合收支
        HideTurnIncomeAndCost();
        if(!SceneManager.GetActiveScene().name.Equals("FTE_1"))
            BaikePanel.My.gameObject.SetActive(false);
        RoleListManager.My.transform.GetComponent<RectTransform>().DOAnchorPosY(-300, 1f).Play();
        LockOperation();
        //TODO 更新金币消耗UI信息
        //TODO 检查错误操作（果汁厂没输入）
        transform.DOScale(1f, produceTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            BuildingManager.My.WaveSpawnConsumer(currentWave);
            /*stageWaveText.text = (currentWave - 1).ToString() + "/" + maxWaveNumber.ToString();
            stageWaveText.transform.DOPunchScale(new Vector3(1.3f, 1.3f, 1.3f), 1f, 1).Play();
            currentWave++;*/
        });
    }

    /// <summary>
    /// 回合预测结束
    /// </summary>
    public void EndPredictionTurn()
    {
        NewCanvasUI.My.ToggleSpeedButton(false);
        NewCanvasUI.My.GameNormal();
        NewCanvasUI.My.GamePause();
        Cursor.SetCursor(normal, Vector2.zero, CursorMode.Auto);
        predict_btn.gameObject.SetActive(true);
        skipToFirstWave.gameObject.SetActive(true);
        //waveCountItem.Init(enemyDatas, currentWave - 1);
        PlayerData.My.RoleTurnEnd();
        //PlayerData.My.ClearAllRoleWarehouse();
        PlayerData.My.RestoreAllWarehourse();
        TradeManager.My.TurnTradeCost();
        TradeManager.My.ClearAllGoods();
        PlayerData.My.isPrediction = false;
        //TODO 解锁准备阶段的操作
        // 显示收支
        //ShowTurnIncomeAndCost();
        PredictionPanel.My.ShowPrediction(/*predict_btn.transform.GetComponentInChildren<Toggle>().isOn*/);
        if(!SceneManager.GetActiveScene().name.Equals("FTE_1"))
            BaikePanel.My.gameObject.SetActive(true);
        UnlockOperation();
        RoleListManager.My.transform.GetComponent<RectTransform>().DOAnchorPosY(67, 1f).Play();
        //TODO 结算buff/角色周期性效果
    }

    private void ResetPredictResult()
    {
        predictGold = playerGold;
        predictIncome = 0;
        predictCost = 0;
        predictTPcost = 0;
        predictTPadd = 0;
        predictScore = playerSatisfy;
        predictKillNum = 0;
        predictHealth = playerHealth;
    }

    #endregion
}
