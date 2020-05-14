using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageGoal : MonoSingleton<StageGoal>
{

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

    public Image bossFace;

    public Image consumerFace;

    /// <summary>
    /// 所有产品的列表
    /// </summary>
    public List<ProductData> productList;

    private float currentBossSatisfy;

    private float currentConsumerSatisfy;

    public GameObject sweetGo;

    public GameObject crispGo;

    public Text qualityText;

    public Text contributionText;

    public Image buttonSprite;

    public List<Sprite> switchSprite;

    private bool isMoving;

    private bool isMenuOpen;

    private bool wudi = false;

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
        CheckWinOrDead();
    }

    /// <summary>
    /// 满意度阶段性表情
    /// </summary>
    public void CheckFaceStatus()
    {
        if ((bossSatisfy - currentBossSatisfy) > 1000f)
        {
            bossFace.sprite = Resources.Load<Sprite>("Sprite/UI/Face/Boss_face1");
        }
        else if ((bossSatisfy - currentBossSatisfy) > 500f)
        {
            bossFace.sprite = Resources.Load<Sprite>("Sprite/UI/Face/Boss_face2");
        }
        else if ((bossSatisfy - currentBossSatisfy) > 000f)
        {
            bossFace.sprite = Resources.Load<Sprite>("Sprite/UI/Face/Boss_face3");
        }
        else if ((bossSatisfy - currentBossSatisfy) > -500f)
        {
            bossFace.sprite = Resources.Load<Sprite>("Sprite/UI/Face/Boss_face4");
        }
        else
        {
            bossFace.sprite = Resources.Load<Sprite>("Sprite/UI/Face/Boss_face5");
        }
        if ((customerSatisfy - currentConsumerSatisfy) > 800f)
        {
            consumerFace.sprite = Resources.Load<Sprite>("Sprite/UI/Face/Consumer_face1");
        }
        else if ((customerSatisfy - currentConsumerSatisfy) > 300f)
        {
            consumerFace.sprite = Resources.Load<Sprite>("Sprite/UI/Face/Consumer_face2");
        }
        else if ((customerSatisfy - currentConsumerSatisfy) > 0f)
        {
            consumerFace.sprite = Resources.Load<Sprite>("Sprite/UI/Face/Consumer_face3");
        }
        else if ((customerSatisfy - currentConsumerSatisfy) > -300f)
        {
            consumerFace.sprite = Resources.Load<Sprite>("Sprite/UI/Face/Consumer_face4");
        }
        else
        {
            consumerFace.sprite = Resources.Load<Sprite>("Sprite/UI/Face/Consumer_face5");
        }
        currentBossSatisfy = bossSatisfy;
        currentConsumerSatisfy = customerSatisfy;
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

        if (bossSatisfy < 0f)
        {
            for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
            {
                PlayerData.My.MapRole[i].OnBeforeDead();
            }
            if (wudi)
            {
                bossSatisfy = 100f;
                return;
            }
            Lose();
        }
        else if (customerSatisfy > maxCustomerSatisfy)
        {
            if (wudi)
            {
                customerSatisfy = 3000f;
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
        UIManager.My.Panel_Win.SetActive(true);
    }

    /// <summary>
    /// 失败时调用函数
    /// </summary>
    public void Lose()
    {
        if (bossSatisfy < 0f)
            UIManager.My.Panel_Lose.SetActive(true);
        else
            return;
    }
 

    /// <summary>
    /// 股东每月固定扣除满意度
    /// </summary>
    public void MonthlyReduceBoss()
    {
        int monthNum = TimeManager.My.month;
        print("固定扣股东");
        if (monthNum < 10)
            CostMoney(500 + monthNum * 10);
        else if (monthNum < 15)
            CostMoney(500 * monthNum);
        else
            CostMoney(500 * Mathf.Pow(monthNum, 1.5f));
    }

    /// <summary>
    /// 统计玩家的贡献度
    /// </summary>
    public void RecheckContribution()
    {
        int total = 0;
        int player = 0;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].isNpc)
            {
                total += PlayerData.My.MapRole[i].contributionNum;
            }
            else
            {
                player += PlayerData.My.MapRole[i].contributionNum;
            }
        }
        if (player != 0)
        {
            playerContributionPerf = (float)player  / (float)(player + total);
        }
        else
        {
            playerContributionPerf = 0f;
        }
        contributionText.text = Mathf.Floor(playerContributionPerf * 100f) + "%";
    }

    /// <summary>
    /// 重新统计所有角色信息
    /// </summary>
    public void RecheckMapRole()
    {
        if (PlayerData.My.MapRole.Count > 0)
        {
            foreach (BaseMapRole b in PlayerData.My.MapRole)
            {
                if (!b.baseRoleData.isNpc)
                    b.RecheckInfo();
            }
        }
    }

    /// <summary>
    /// 将关卡配置表读取到本关
    /// </summary>
    public void InitStage()
    {
        InitStageData();
        playerContributionPerf = 0f;
        sweetGo.GetComponent<RectTransform>().localPosition = new Vector3(standardSweet * 10, 0, 0);
        crispGo.GetComponent<RectTransform>().localPosition = new Vector3(standardCrisp * 10, 0, 0);
        SetInfo();
        productList = new List<ProductData>();
     
        InvokeRepeating("CheckFaceStatus", 1f, 10f);
        InvokeRepeating("MonthlyReduceBoss", 0f, 60f);
        InvokeRepeating("RecheckMapRole", 1f, 60f);
        InvokeRepeating("RecheckContribution",1f,2f);
        BuildingManager.My.InitAllBuilding();
        MenuHide();
    }

    /// <summary>
    /// Helper Function
    /// </summary>
    public void InitStageData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        StageData data = GameDataMgr.My.GetStageDataByName(sceneName);
        bossSatisfy = data.startBoss;
        maxBossSatisfy = data.maxBoss;
        customerSatisfy = data.startConsumer;
        maxCustomerSatisfy = data.maxConsumer;
        bankRate = data.bankRate;
        qualityRecognition = 1.5f;
        brandRecognition = 1f;
        standardSweet = Random.Range(-5, 5);
        standardCrisp = Random.Range(-5, 5);
        consumerQualityNeed = Random.Range(data.consumerQualityNeed - 5, data.consumerQualityNeed + 5);
        qualityText.text = consumerQualityNeed.ToString();
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
