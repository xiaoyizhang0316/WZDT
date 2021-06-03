using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;
using static GameEnum;
using static StageGoal;

public class ReviewPanel : MonoSingleton<ReviewPanel>
{

    public Button play;

    public Button accelarate;

    public Button pause;

    public Slider playSlider;

    public List<MapState> mapStates;

    public VectorObject2D line;

    public VectorObject2D healthLine;

    //public Vector3 pos;

    private bool isStart = false;

    private Tweener twe;

    private float speed;

    public List<DataStat> mydataStats;

    public string talent;

    public Transform talentPanel;

    public GameObject consumableIconPrb;

    /// <summary>
    /// 正常速度播放
    /// </summary>
    public void Normal()
    {
        speed = 1f;
        twe.timeScale = speed;
        play.interactable = false;
        pause.interactable = true;
        accelarate.interactable = true;
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void Pause()
    {
        speed = 0f;
        twe.timeScale = speed;
        play.interactable = true;
        pause.interactable = false;
        accelarate.interactable = true;
    }

    /// <summary>
    /// 加速
    /// </summary>
    public void Accerlate()
    {
        speed = 4f;
        twe.timeScale = speed;
        play.interactable = true;
        pause.interactable = true;
        accelarate.interactable = false;
    }

    /// <summary>
    /// 设置自动播放
    /// </summary>
    public void AutoPlay()
    {
        twe = transform.DOScale(1f, 0.1f).OnComplete(() =>
        {
            if (playSlider.value < playSlider.maxValue)
            {
                playSlider.value += 0.1f;
                OnSliderValueChange();
            }
            AutoPlay();
        }).Play();
        twe.timeScale = speed;
    }

    /// <summary>
    /// 当根据时间点播放对应的复盘状态
    /// </summary>
    public void OnSliderValueChange()
    {
        bool isPlay = false;
        for (int i = 0; i < mapStates.Count; i++)
        {
            if (mapStates[i].time > playSlider.value)
            {
                if (i == 0)
                {
                    ReviewManager.My.ShowCurrentReview(0);
                }
                else
                {
                    ReviewManager.My.ShowCurrentReview(i - 1);
                    isPlay = true;
                    break;
                }
            }
        }
        if (!isPlay)
            ReviewManager.My.ShowCurrentReview(mapStates.Count - 1);
    }

    /// <summary>
    /// 初始化（录像复盘）
    /// </summary>
    /// <param name="playerOperations"></param>
    /// <param name="datas"></param>
    /// <param name="timeCount"></param>
    public void MapInit(List<PlayerOperation> playerOperations, List<DataStat> datas, int timeCount,string talent)
    {
        mydataStats = datas;
        AutoPlay();
        Pause();
        ClearConsumableUse();
        playSlider.maxValue = timeCount;
        playSlider.value = 0;
        GenerateMapStates(playerOperations);
        ReviewManager.My.content.GetComponent<RectTransform>().localPosition = Vector3.zero; 
        InitMoneyLine(datas, timeCount);
        Show();
        InitTalentPanel(talent);
    }

    /// <summary>
    /// 初始化（游戏内复盘）
    /// </summary>
    /// <param name="playerOperations"></param>
    public void Init(List<PlayerOperation> playerOperations)
    {
        AutoPlay();
        Pause();
        ClearConsumableUse();
        playSlider.maxValue = StageGoal.My.timeCount;
        playSlider.value = 0;
        GenerateMapStates(playerOperations);
        //line = playSlider.transform.GetChild(0).GetComponent<VectorObject2D>();
        InitMoneyLine(StageGoal.My.dataStats, StageGoal.My.timeCount);
    }

    /// <summary>
    /// 清除所有消耗品使用记录（初始化用）
    /// </summary>
    public void ClearConsumableUse()
    {
        Transform tf = playSlider.transform.Find("ConsumableList");
        for (int i = 0; i < tf.childCount; i++)
        {
            Destroy(tf.GetChild(0).gameObject, 0f);
        }
    }

    /// <summary>
    /// 初始化天赋面板（已作废）
    /// </summary>
    /// <param name="talentStr"></param>
    public void InitTalentPanel(string talentStr)
    {
        //if (talentStr == null || talentStr.Length != 6)
        //{
        //    Debug.LogWarning("--------复盘天赋读取错误！");
        //    talentStr = "000000";
        //}
        //if (NetworkMgr.My.levelProgressList.Count < 4)
        //{
        talentPanel.gameObject.SetActive(false);
            //talentPanel.DOScale(0f, 0f).Play();
            return;
        //}
        Text[] list = talentPanel.GetComponentsInChildren<Text>();
        char[] charList = talentStr.ToCharArray();
        for (int i = 0; i < 6; i++)
        {
            list[i + 1].text = charList[i].ToString();
        }
    }

    /// <summary>
    /// 生成钱线和血量线
    /// </summary>
    /// <param name="datas"></param>
    /// <param name="timeCount"></param>
    public void InitMoneyLine(List<DataStat> datas, int timeCount)
    {
        if (datas.Count == 0)
            return;
        float score = 0;
        int maxAmount = datas[0].restMoney;
        int maxHealth = datas[0].blood;
        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i].restMoney > maxAmount)
                maxAmount = datas[i].restMoney;
        }
        line.vectorLine.points2.Clear();
        healthLine.vectorLine.points2.Clear();
        float perf = (timeCount / datas.Count / 5f);
        for (int i = 0; i < datas.Count; i++)
        {
            line.vectorLine.points2.Add(new Vector2(1326f / datas.Count* i , datas[i].restMoney / (float)maxAmount * 100f));
            healthLine.vectorLine.points2.Add(new Vector2(1326f / datas.Count * i , datas[i].blood / (float)maxHealth * 100f));
            //if (datas[i].restMoney > 0)
            //    score += datas[i].restMoney * 0.05f;
            //if(i==datas.Count-1)
            //    Debug.LogError(datas[i].restMoney);
        }
        //line.vectorLine.points2.Add(new Vector2(1326f, Mathf.Min(datas[datas.Count - 1].restMoney / (float)maxAmount * 100f,100f)));
        healthLine.transform.SetAsLastSibling();
        healthLine.vectorLine.Draw();
        line.transform.SetAsLastSibling();
        line.vectorLine.Draw();
        //Debug.Log(score / 4);
    }

    /// <summary>
    /// 根据角色操作生成对应的关键时间状态
    /// </summary>
    /// <param name="playerOperations"></param>
    public void GenerateMapStates(List<PlayerOperation> playerOperations)
    {
        mapStates = new List<MapState>();
        MapState tempState = new MapState();
        tempState.Init();
        tempState.time = 0;
        mapStates.Add(tempState);
        foreach (PlayerOperation p in playerOperations)
        {
            MapState mapState = new MapState();
            mapState.Init();
            if (mapStates.Count > 0)
                mapState = mapStates[mapStates.Count - 1];
            if (mapStates.Count > 0)
            {
                if (mapStates[mapStates.Count - 1].time != p.operateTime)
                {
                    mapStates.Add(CheckOperationState(p, mapState));
                }
                else
                {
                    CheckOperationState(p, mapState);
                }
            }
            else
            {
                mapStates.Add(CheckOperationState(p, mapState));
            }
        }
    }

    /// <summary>
    /// 根据每个角色操作生成对应的状态
    /// </summary>
    /// <param name="p"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public MapState CheckOperationState(PlayerOperation p, MapState state)
    {
        MapState result = new MapState();
        result.Init();
        if (p.operateTime == state.time)
        {
            result = state;
        }
        else
        {
            result = state.CopyNewState();
            result.time = p.operateTime;
        }
        switch (p.type)
        {
            case OperationType.PutRole:
                ReviewRole role = new ReviewRole();
                role.roleId = double.Parse(p.operationParam[0]);
                role.isNPC = bool.Parse(p.operationParam[1]);
                role.roleName = p.operationParam[2];
                role.roleType = (RoleType)Enum.Parse(typeof(RoleType), p.operationParam[3]);
                if (p.operationParam.Count < 5)
                {
                    role.level = 1;
                }
                else
                {
                    role.level = int.Parse(p.operationParam[4]);
                }
                role.buffList = new List<int>();
                result.mapRoles.Add(role);
                break;
            case OperationType.ChangeRole:
                {
                    for (int i = 0; i < result.mapRoles.Count; i++)
                    {
                        if (Mathf.Abs((float)(result.mapRoles[i].roleId - double.Parse(p.operationParam[0]))) <= 0.01f)
                        {
                            result.mapRoles[i].buffList.Clear();
                            for (int j = 1; j < p.operationParam.Count; j++)
                            {
                                result.mapRoles[i].buffList.Add(int.Parse(p.operationParam[j]));
                            }
                            if (!result.mapRoles[i].isNPC)
                            {
                                SpecialOperation temp = new SpecialOperation();
                                temp.type = OperationType.ChangeRole;
                                temp.operationParams = new List<string>();
                                temp.operationParams.Add(p.operationParam[0]);
                                result.specialOperations.Add(temp);
                            }
                            break;
                        }
                    }
                    break;
                }
            case OperationType.DeleteRole:
                {
                    for (int i = 0; i < result.mapRoles.Count; i++)
                    {
                        if (Mathf.Abs((float)(result.mapRoles[i].roleId - double.Parse(p.operationParam[0]))) <= 0.01f)
                        {
                            result.mapRoles.RemoveAt(i);
                            break;
                        }
                    }
                    break;
                }
            case OperationType.UpgradeRole:
                {
                    for (int i = 0; i < result.mapRoles.Count; i++)
                    {
                        if (Mathf.Abs((float)(result.mapRoles[i].roleId - double.Parse(p.operationParam[0]))) <= 0.01f)
                        {
                            result.mapRoles[i].SetLevel();
                            break;
                        }
                    }
                    SpecialOperation temp = new SpecialOperation();
                    temp.type = OperationType.UpgradeRole;
                    temp.operationParams = new List<string>();
                    temp.operationParams.Add(p.operationParam[0]);
                    result.specialOperations.Add(temp);
                    break;
                }
            case OperationType.SellRole:
                {
                    for (int i = 0; i < result.mapRoles.Count; i++)
                    {
                        if (Mathf.Abs((float)(result.mapRoles[i].roleId - double.Parse(p.operationParam[0]))) <= 0.01f)
                        {
                            result.mapRoles[i].isNPC = true;
                            break;
                        }
                    }
                    break;
                }
            case OperationType.CreateTrade:
                {
                    ReviewTrade tempTrade = new ReviewTrade();
                    tempTrade.tradeId = int.Parse(p.operationParam[0]);
                    tempTrade.startRole = double.Parse(p.operationParam[1]);
                    tempTrade.endRole = double.Parse(p.operationParam[2]);
                    tempTrade.cashFlowType = (CashFlowType)Enum.Parse(typeof(CashFlowType), p.operationParam[3]);
                    result.mapTrades.Add(tempTrade);
                    break;
                }
            case OperationType.ChangeTrade:
                {
                    for (int i = 0; i < result.mapTrades.Count; i++)
                    {
                        if (result.mapTrades[i].tradeId == int.Parse(p.operationParam[0]))
                        {
                            //Debug.Log("改变了交易结构  ");
                            //Debug.Log((CashFlowType)Enum.Parse(typeof(CashFlowType), p.operationParam[1]));
                            result.mapTrades[i].SetCashFlow((CashFlowType)Enum.Parse(typeof(CashFlowType), p.operationParam[1]));
                            break;
                        }
                    }
                    SpecialOperation temp = new SpecialOperation();
                    temp.type = OperationType.ChangeTrade;
                    temp.operationParams = new List<string>();
                    temp.operationParams.Add(p.operationParam[0]);
                    result.specialOperations.Add(temp);
                    break;
                }
            case OperationType.DeleteTrade:
                {
                    for (int i = 0; i < result.mapTrades.Count; i++)
                    {
                        if (result.mapTrades[i].tradeId == int.Parse(p.operationParam[0]))
                        {
                            result.mapTrades.RemoveAt(i);
                            break;
                        }
                    }
                    break;
                }
            case OperationType.UseConsumable:
                {
                    Vector2 pos = new Vector2(1326f * p.operateTime / playSlider.maxValue, -57f);
                    GameObject go = Instantiate(consumableIconPrb, playSlider.transform.Find("ConsumableList"));
                    go.transform.localPosition = pos;
                    go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Consumable/" + p.operationParam[0]);
                    break;
                }
            default:
                {
                    break;
                }
        }
        return result;
    }

    public void Hide()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector3(-3000f, 0f, 0f);
    }

    public void Show()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 0f, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// 每个关键时间对应的状态
    /// </summary>
    [Serializable]
    public class MapState
    {
        public List<ReviewRole> mapRoles;

        public List<ReviewTrade> mapTrades;

        public List<SpecialOperation> specialOperations;

        public int time;

        public void Init()
        {
            mapRoles = new List<ReviewRole>();
            mapTrades = new List<ReviewTrade>();
            specialOperations = new List<SpecialOperation>();
        }

        public MapState CopyNewState()
        {
            MapState result = new MapState();
            result.Init();
            for (int i = 0; i < mapRoles.Count; i++)
            {
                result.mapRoles.Add(mapRoles[i].CopyNewRole());
            }
            for (int i = 0; i < mapTrades.Count; i++)
            {
                result.mapTrades.Add(mapTrades[i].CopyNewTrade());
            }
            return result;
        }
    }

    public class SpecialOperation
    {
        public OperationType type;

        public List<string> operationParams;
    }

    /// <summary>
    /// 角色状态
    /// </summary>
    [Serializable]
    public class ReviewRole
    {
        public double roleId;

        public bool isNPC;

        public string roleName;

        public RoleType roleType;

        public List<int> buffList;

        public int level;

        public void SetLevel()
        {
            level++;
        }

        public ReviewRole CopyNewRole()
        {
            ReviewRole result = new ReviewRole();
            result.roleId = roleId;
            result.isNPC = isNPC;
            result.roleType = roleType;
            result.roleName = roleName;
            result.buffList = new List<int>();
            for (int i = 0; i < buffList.Count; i++)
            {
                result.buffList.Add(buffList[i]);
            }
            result.level = level;
            return result;
        }
    }

    /// <summary>
    /// 交易状态
    /// </summary>
    [Serializable]
    public class ReviewTrade
    {
        public int tradeId;

        public double startRole;

        public double endRole;

        public CashFlowType cashFlowType;

        public void SetCashFlow(CashFlowType type)
        {
            cashFlowType = type;
        }

        public ReviewTrade CopyNewTrade()
        {
            ReviewTrade result = new ReviewTrade();
            result.tradeId = tradeId;
            result.startRole = startRole;
            result.endRole = endRole;
            result.cashFlowType = cashFlowType;
            return result;
        }
    }
}
