using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;

public class StageGoal : MonoSingleton<StageGoal>
{

    /// <summary>
    /// 玩家金币
    /// </summary>
    public int playerGold;

    /// <summary>
    /// 关卡满意度目标
    /// </summary>
    public int goalSatisfy;

    /// <summary>
    /// 玩家满意度
    /// </summary>
    public int playerSatisfy;

    /// <summary>
    /// 玩家血量
    /// </summary>
    public int playerHealth;

    #region Old

    /// <summary>
    /// 股东满意度
    /// </summary>
    public float bossSatisfy;

    /// <summary>
    /// 消费者满意度
    /// </summary>
    public float customerSatisfy;

    /// <summary>
    /// 股东满意度上限
    /// </summary>
    public float maxBossSatisfy;

    /// <summary>
    /// 消费者满意度上限
    /// </summary>
    public float maxCustomerSatisfy;

    /// <summary>
    /// 银行利率
    /// </summary>
    public float bankRate;

    /// <summary>
    /// 质量认可度
    /// </summary>
    public float qualityRecognition;

    /// <summary>
    /// 质量认可度
    /// </summary>
    public float brandRecognition;

    /// <summary>
    /// 游戏内基准甜度
    /// </summary>
    public int standardSweet;

    /// <summary>
    /// 游戏内基准脆度
    /// </summary>
    public int standardCrisp;

    /// <summary>
    /// 消费者质量要求
    /// </summary>
    public int consumerQualityNeed;

    /// <summary>
    /// 玩家贡献度百分比
    /// </summary>
    public float playerContributionPerf;

    /// <summary>
    /// 股东满意度进度条物体
    /// </summary>
    public GameObject bossSatisfyGo;

    /// <summary>
    /// 消费者满意度进度条物体
    /// </summary>
    public GameObject consumerSatisfyGo;

    #endregion

    public Image bossFace;

    public Image consumerFace;

    public Image buttonSprite;

    public List<Sprite> switchSprite;

    private bool isMoving;

    private bool isMenuOpen;

    private bool wudi = false;

    public Text playerGoldText;

    public Text playerSatisfyText;

    public Text playerHealthText;

    public List<StageEnemyData> enemyDatas;

    /// <summary>
    /// 玩家消耗金币
    /// </summary>
    /// <param name="num"></param>
    public void CostPlayerGold(int num)
    {
        playerGold -= num;
        SetInfo();
    }

    /// <summary>
    /// 玩家获得金币
    /// </summary>
    /// <param name="num"></param>
    public void GetPlayerGold(int num)
    {
        playerGold += num;
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
    }

    /// <summary>
    /// 当扣除成本时调用函数计算股东满意度
    /// </summary>
    /// <param name="num"></param>
    public void CostMoney(float num)
    {
        bossSatisfy -= num / 10f;
        SetInfo();
    }

    /// <summary>
    /// 当获得利润时调用函数计算股东满意度
    /// </summary>
    /// <param name="num"></param>
    public void MakeProfit(int num)
    {
        bossSatisfy += ((float)num / 10f) / (1f + bankRate);
        SetInfo();
    }

    /// <summary>
    /// 当消费者满意度变更时调用函数
    /// </summary>
    /// <param name="num"></param>
    public void ChangeCustomerSatisfy(float num)
    {
        //print("满意度变化 :" + num.ToString());
        customerSatisfy += num;
        if (customerSatisfy < 0)
        {
            customerSatisfy = 0;
        }
        SetInfo();
    }

    /// <summary>
    /// 设置两个条的长度    
    /// </summary>
    public void SetInfo()
    {
        consumerSatisfyGo.transform.Find("Mask/Image").GetComponent<RectTransform>().sizeDelta = new Vector2(746f * customerSatisfy / maxCustomerSatisfy, consumerSatisfyGo.transform.Find("Mask/Image").GetComponent<RectTransform>().sizeDelta.y);
        bossSatisfyGo.transform.Find("Mask/Image").GetComponent<RectTransform>().sizeDelta = new Vector2(746f * bossSatisfy / maxBossSatisfy, bossSatisfyGo.transform.Find("Mask/Image").GetComponent<RectTransform>().sizeDelta.y);
        playerGoldText.text = "玩家金币:" + playerGold.ToString();
        playerSatisfyText.text = playerSatisfy.ToString() + "/" + goalSatisfy.ToString();
        playerHealthText.text = "玩家血量:" + playerHealth.ToString();
        CheckWinOrDead();
    }

    #region 菜单显示

    public void MenuShow()
    {
        isMoving = true;
        float amount = 16 * Screen.height / 9f / Screen.width;
        GetComponent<RectTransform>().DOAnchorPosX(-90f,0.1f).OnComplete(() => {
            isMoving = false;
            isMenuOpen = true;
            buttonSprite.sprite = switchSprite[1];
        }).SetUpdate(true);
        //transform.DOLocalMove(new Vector3(740f , 276f, 0f), 0.1f).OnComplete(()=> {
        //    isMoving = false;
        //    isMenuOpen = true;
        //    buttonSprite.sprite = switchSprite[1];
        //}).SetUpdate(true);
    }

    public void MenuHide()
    {
        isMoving = true;
        float amount = 16 * Screen.height / 9f / Screen.width;
        GetComponent<RectTransform>().DOAnchorPosX(56, 0.1f).OnComplete(() => {
            isMoving = false;
            isMenuOpen = false;
            buttonSprite.sprite = switchSprite[0];
        }).SetUpdate(true);
        //transform.DOLocalMove(new Vector3(875f, 276f,0f), 0.1f).OnComplete(() => {
        //    isMoving = false;
        //    isMenuOpen = false;
        //    buttonSprite.sprite = switchSprite[0];
        //}).SetUpdate(true);
    }

    public void ToggleShow()
    {
        if (!isMoving)
        {
            if (isMenuOpen)
            {
                MenuHide();
            }
            else
            {
                MenuShow();
            }
        }
    }

    #endregion

    /// <summary>
    /// 检测通关条件或者死亡条件
    /// </summary>
    public void CheckWinOrDead()
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
        else if (playerSatisfy >= goalSatisfy)
        {
            if (wudi)
            {
                playerSatisfy = 100;
                return;
            }
            Win();
        }
    }

    /// <summary>
    /// 通关时调用函数
    /// </summary>
    public void Win()
    {
        UIManager.My.GamePause();
        UIManager.My.Panel_Win.SetActive(true);
    }

    /// <summary>
    /// 失败时调用函数
    /// </summary>
    public void Lose()
    {
        if (playerHealth < 0)
        {
            UIManager.My.GamePause();
            UIManager.My.Panel_Lose.SetActive(true);
        }
        else
            return;
    } 

    /// <summary>
    /// 将关卡配置表读取到本关
    /// </summary>
    public void InitStage()
    {
        InitStageData();
        SetInfo();
        MenuHide();
    }

    /// <summary>
    /// Helper Function
    /// </summary>
    public void InitStageData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(ReadStageEnemyData(sceneName));
        StageData data = GameDataMgr.My.GetStageDataByName(sceneName);
        bossSatisfy = data.startBoss;
        maxBossSatisfy = data.maxBoss;
        customerSatisfy = data.startConsumer;
        maxCustomerSatisfy = data.maxConsumer;
        playerGold = 10000;
        playerSatisfy = 0;
        goalSatisfy = 1000;
        playerHealth = 300;
        InitEquipAndWorker(data);
    }

    /// <summary>
    /// 读取装备和工人
    /// </summary>
    /// <param name="data"></param>
    public void InitEquipAndWorker(StageData data)
    {
        foreach (int i in data.startWorker)
        {
            PlayerData.My.GetNewWorker(i);
        }
        foreach (int i in data.startEquip)
        {
            PlayerData.My.GetNewGear(i);
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
    }



    // Start is called before the first frame update
    void Start()
    {
        InitStage();
        cameraPos = Camera.main.transform.position;
    }
    private Vector3 cameraPos;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUILayout.Button("退出游戏",GUILayout.Width(120), GUILayout.Height(50)))
        {
            Application.Quit();
        }
        if (GUILayout.Button("一键通关", GUILayout.Width(120), GUILayout.Height(50)))
        {
            UIManager.My.Panel_Win.SetActive(true);
        }
        if (GUILayout.Button("增加行动力", GUILayout.Width(120), GUILayout.Height(50)))
        {
            ExecutionManager.My.AddExecution(20f);
        }
        if (GUILayout.Button("不会死也不会赢", GUILayout.Width(120), GUILayout.Height(50)))
        {
            wudi = !wudi;
        }
        if (GUILayout.Button("相机复位", GUILayout.Width(120), GUILayout.Height(50)))
        {
            Camera.main.transform.position = cameraPos;
        }
    }
}
