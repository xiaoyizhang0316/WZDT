using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
using System;

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

    public int timeCount = 0;

    public Tweener waveTween;

    private bool wudi = false;

    #region UI

    public Text playerGoldText;

    public Text playerSatisfyText;

    public Text playerTechText;

    public Image playerHealthBar;

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

    #endregion

    #region 统计

    public int killNumber = 0;

    public int totalCost = 0;

    public int tradeCost = 0;

    public int productCost = 0;

    public int totalIncome = 0;

    public int consumeIncome = 0;

    public List<DataStat> dataStats;

    public Dictionary<string, int> otherIncomes = new Dictionary<string, int>();

    public Dictionary<BaseMapRole, int> npcIncomes = new Dictionary<BaseMapRole, int>();

    public Dictionary<BaseMapRole, int> buildingCosts = new Dictionary<BaseMapRole, int>();

    public Dictionary<string, int> extraCost = new Dictionary<string, int>();

    #endregion

    /// <summary>
    /// 当前关卡敌人波数数据
    /// </summary>
    public List<StageEnemyData> enemyDatas;

    /// <summary>
    /// 倒计时
    /// </summary>
    public WaveCount waveCountItem;

    /// <summary>
    /// 玩家消耗金币
    /// </summary>
    /// <param name="num"></param>
    public void CostPlayerGold(int num)
    {
        playerGold -= num;
        if(playerGold < maxMinusGold)
        {
            if (!isOverMaxMinus)
            {
                isOverMaxMinus = true;
                foreach (BaseMapRole role in PlayerData.My.MapRole)
                {
                    if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
                    {
                        role.GetComponent<BaseSkill>().CancelSkill();
                        AudioManager.My.PlaySelectType(GameEnum.AudioClipType.MinusMoney);
                    }
                }
            }
        }
        else
        {
            if (isOverMaxMinus)
            {
                isOverMaxMinus = false;
                foreach (BaseMapRole role in PlayerData.My.MapRole)
                {
                    if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
                    {
                        role.GetComponent<BaseSkill>().ReUnleashSkills();
                        AudioManager.My.PlaySelectType(GameEnum.AudioClipType.PosivityMoney);
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
            return false;
        }
        playerTechPoint -= num;
        SetInfo();
        return true;
    }

    /// <summary>
    /// 获得人脉值
    /// </summary>
    /// <param name="num"></param>
    public void GetTechPoint(int num)
    {
        playerTechPoint += num;
        SetInfo();
    }

    /// <summary>
    /// 玩家获得金币
    /// </summary>
    /// <param name="num"></param>
    public void GetPlayerGold(int num)
    {
        playerGold += num;
        if (playerGold < maxMinusGold)
        {
            if (!isOverMaxMinus)
            {
                isOverMaxMinus = true;
                foreach (BaseMapRole role in PlayerData.My.MapRole)
                {
                    if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
                    {
                        role.GetComponent<BaseSkill>().CancelSkill();
                        AudioManager.My.PlaySelectType(GameEnum.AudioClipType.MinusMoney);
                    }
                }
            }
        }
        else
        {
            if (isOverMaxMinus)
            {
                isOverMaxMinus = false;
                foreach (BaseMapRole role in PlayerData.My.MapRole)
                {
                    if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
                    {
                        role.GetComponent<BaseSkill>().ReUnleashSkills();
                        AudioManager.My.PlaySelectType(GameEnum.AudioClipType.PosivityMoney);
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
        playerSatisfy += num;
        SetInfo();
    }

    /// <summary>
    /// 玩家失去生命值
    /// </summary>
    /// <param name="num"></param>
    public void LostHealth(int num)
    {
        playerHealth += num;
        SetInfo();
        CheckDead();
    }

    /// <summary>
    /// 设置两个条的长度    
    /// </summary>
    public void SetInfo()
    {
        float per = playerHealth / (float)playerMaxHealth;
        playerHealthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(maxHealtherBarLength * per, playerHealthBar.GetComponent<RectTransform>().sizeDelta.y);
        playerGoldText.text = playerGold.ToString();
        if (playerGold > 0)
            playerGoldText.color = Color.white;
        else if (playerGold >= maxMinusGold)
            playerGoldText.color = Color.yellow;
        else
            playerGoldText.color = Color.red;
        playerSatisfyText.text = playerSatisfy.ToString();
        playerTechText.text = playerTechPoint.ToString();   
    }

    /// <summary>
    /// 检测死亡条件
    /// </summary>
    public void CheckDead()
    {
        if (playerHealth < 0)
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
                print("胜利");
                Win();
            }
        }
    }

    /// <summary>
    /// 通关时调用函数
    /// </summary>
    public void Win()
    {
        BaseLevelController.My.CancelInvoke("CheckStarTwo");
        BaseLevelController.My.CancelInvoke("CheckStarOne");
        BaseLevelController.My.CancelInvoke("CheckStarThree");
        BaseLevelController.My.CancelInvoke("UpdateInfo");
        NewCanvasUI.My.GamePause();
        WinManager.My.InitWin();
    }

    /// <summary>
    /// 失败时调用函数
    /// </summary>
    public void Lose()
    {
        if (playerHealth < 0)
        {
            NewCanvasUI.My.GamePause();
            NewCanvasUI.My.lose.SetActive(true);
            //NewCanvasUI.My.Panel_Lose.SetActive(true);
        }
        else
            return;
    }

    /// <summary>
    /// 根据波数刷敌人
    /// </summary>
    public void WaveCount()
    {
        timeCount++;
        if (currentWave <= maxWaveNumber)
        {
            if (timeCount == waitTimeList[currentWave - 1])
            {
                BuildingManager.My.WaveSpawnConsumer(currentWave);
                currentWave++;
            }
            waveTween = transform.DOScale(1f, 1f).OnComplete(() =>
            {
                stageWaveText.text = (currentWave - 1).ToString() + "/" + maxWaveNumber.ToString();
                WaveCount();
                if (timeCount % 5 == 0)
                {
                    Stat();
                }
            });
        }
        else
        {
            CheckWin();
            waveTween = transform.DOScale(1f, 1f).OnComplete(() =>
            {
                WaveCount();
                stageWaveText.text = (currentWave - 1).ToString() + "/" + maxWaveNumber.ToString();
                if (timeCount % 5 == 0)
                {
                    Stat();
                }
            });
        }
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
        switch(starNumber)
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
        maxHealtherBarLength = playerHealthBar.GetComponent<RectTransform>().sizeDelta.x;
        playerGoldText = transform.parent.Find("UserInfo/Image_money/PlayerMoney").GetComponent<Text>();
        playerSatisfyText = transform.parent.Find("UserInfo/PlayerScore/PlayerScoreText").GetComponent<Text>();
        playerTechText = transform.parent.Find("UserInfo/Image_money/PlayerTech").GetComponent<Text>();
        stageWaveText = transform.parent.Find("UserInfo/Image_level/StageLevel").GetComponent<Text>();
        foreach (PlayerGear p in PlayerData.My.playerGears)
        {
            p.isEquiped = false;
        }
        foreach (PlayerWorker p in PlayerData.My.playerWorkers)
        {
            p.isEquiped = false;
        }
        InitStageData();
        SetInfo();
        WaveCount();
    }

    /// <summary>
    /// Helper Function
    /// </summary>
    public void InitStageData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(ReadStageEnemyData(sceneName));
        StageData data = GameDataMgr.My.GetStageDataByName(sceneName);
        playerGold = data.startPlayerGold;
        playerSatisfy = 0;
        playerHealth = data.startPlayerHealth;
        playerMaxHealth = playerHealth;
        maxWaveNumber = data.maxWaveNumber;
        playerTechPoint = data.startTech;
        foreach (int i in data.waveWaitTime)
        {
            waitTimeList.Add(i);
        }
    }

    /// <summary>
    /// 读取当前关卡敌人配置
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator ReadStageEnemyData(string sceneName)
    {
        string path = "file://" + Application.streamingAssetsPath + @"/Data/StageEnemy/" + sceneName + ".json";
        WWW www = new WWW(@path);
        yield return www;
        if (www.isDone)
        {
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield return null;
            }
            else
            {
                string json = www.text.ToString();
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
        foreach(StageEnemyItem s in data.stageEnemyItems)
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
        waveCountItem.Init(enemyDatas);
    }

    public void MenuHide()
    {
        GetComponent<RectTransform>().DOAnchorPosX(160.27f,0.3f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => {
            menuCloseButton.gameObject.SetActive(false);
            menuOpenButton.gameObject.SetActive(true);
        }).Play().timeScale = 1f / DOTween.timeScale;
    }

    public void MenuShow()
    {
        GetComponent<RectTransform>().DOAnchorPosX(-178f, 0.3f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(()=> {
            menuCloseButton.gameObject.SetActive(true);
            menuOpenButton.gameObject.SetActive(false);
        }).Play().timeScale = 1f / DOTween.timeScale;
    }



    // Start is called before the first frame update
    void Start()
    {
        InitStage();
        MenuHide();
        //GetAllPreviousAward();
        cameraPos = Camera.main.transform.position;
    }
    private Vector3 cameraPos;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100,20),"4倍速"))
        {
            DOTween.PlayAll();
            DOTween.timeScale = 16f;
            DOTween.defaultAutoPlay = AutoPlay.All;
        }
        if (GUI.Button(new Rect(0, 20, 100, 20), "通关"))
        {
            Win();
        }
        if (GUI.Button(new Rect(0, 40, 100, 20), "一键加钱"))
        {
            GetPlayerGold(10000);
            GetTechPoint(1000);
            playerHealth = playerMaxHealth;
        }
    }


    public void Income(int num, IncomeType incomeType, BaseMapRole npc =null, string otherName="")
    {
        totalIncome += num;
        switch (incomeType)
        {
            case IncomeType.Consume:
                consumeIncome += num;
                break;
            case IncomeType.Npc:
                if (npcIncomes.ContainsKey(npc))
                {
                    npcIncomes[npc] += num;
                }
                else
                {
                    npcIncomes.Add(npc, num);
                }
                break;
            case IncomeType.Other:
                if (otherIncomes.ContainsKey(otherName))
                {
                    otherIncomes[otherName] += num;
                }
                else
                {
                    otherIncomes.Add(otherName,num);
                }
                break;
        }
        //if (DataStatPanel.My.isShow)
        //{
        //    DataStatPanel.My.ShowStat();
        //}
        DataStatPanel.My.RefreshIncome(totalIncome, consumeIncome, npcIncomes, otherIncomes, timeCount);
    }

    public void Expend(int num, ExpendType expendType, BaseMapRole build = null, string extraName = "")
    {
        totalCost += num;
        switch (expendType)
        {
            case ExpendType.TradeCosts:
                tradeCost += num;
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

    private void Stat()
    {
        if(dataStats == null)
        {
            dataStats = new List<DataStat>();
        }
        dataStats.Add(new DataStat(playerHealth, playerSatisfy, totalIncome, consumeIncome, totalCost, tradeCost, productCost, playerGold));
    }

    public string ShowStat()
    {
        string list = "";
        foreach(var ds in dataStats)
        {
            //list += string.Format($"{ds.blood}\t{ds.score}\t{ds.cost}\t{ds.tradeCost}\t{ds.totalGold}\n");
        }
        return list;
    }
}
