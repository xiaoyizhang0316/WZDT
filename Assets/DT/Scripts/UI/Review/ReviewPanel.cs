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

    private bool isStart = false;

    private Tweener twe;

    private float speed;

    public void Normal()
    {
        speed = 1f;
        twe.timeScale = speed;
        play.interactable = false;
        pause.interactable = true;
        accelarate.interactable = true;
    }

    public void Pause()
    {
        speed = 0f;
        twe.timeScale = speed;
        play.interactable = true;
        pause.interactable = false;
        accelarate.interactable = true;
    }

    public void Accerlate()
    {
        speed = 2f;
        twe.timeScale = speed;
        play.interactable = true;
        pause.interactable = true;
        accelarate.interactable = false;
    }

    public void AutoPlay()
    {
        twe = transform.DOScale(1f, 0.1f).OnComplete(() =>
        {
            playSlider.value += 0.1f;
            OnSliderValueChange();
            AutoPlay();
        }).Play();
        twe.timeScale = speed;
    }

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
                    //PlayClip(mapStates[i - 1])
                }
            }
        }
        if (!isPlay)
            ReviewManager.My.ShowCurrentReview(mapStates.Count - 1);

    }

    public void Init(List<PlayerOperation> playerOperations)
    {
        AutoPlay();
        Pause();
        GenerateMapStates(playerOperations);
        playSlider.maxValue = StageGoal.My.timeCount;
        InitMoneyLine();
    }


    public void InitMoneyLine()
    {
        if (StageGoal.My.dataStats.Count == 0)
            return;
        int maxAmount = StageGoal.My.dataStats[0].restMoney * 150 / 100;
        line.vectorLine.points2.Clear();
        for (int i = 0; i < StageGoal.My.dataStats.Count; i++)
        {
            line.vectorLine.points2.Add(new Vector2(1326 / StageGoal.My.timeCount * 5 * i, StageGoal.My.dataStats[0].restMoney / (float)maxAmount * 100f));
        }
        line.vectorLine.points2.Add(new Vector2(1326, StageGoal.My.playerGold / (float)maxAmount * 100f));
        line.vectorLine.Draw();
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
            if (mapStates.Count > 0 )
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
    public MapState CheckOperationState(PlayerOperation p,MapState state)
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
                role.roleName = p.operationParam[1];
                role.roleType = (RoleType)Enum.Parse(typeof(RoleType), p.operationParam[2]);
                role.level = 1;
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
                            result.mapTrades[i].SetCashFlow((CashFlowType)Enum.Parse(typeof(CashFlowType), p.operationParam[1]));
                            break;
                        }
                    }
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
            default:
                break;
        }
        return result;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 每个关键时间对应的状态
    /// </summary>
    [Serializable]
    public struct MapState
    {
        public List<ReviewRole> mapRoles;

        public List<ReviewTrade> mapTrades;

        public int time;

        public void Init()
        {
            mapRoles = new List<ReviewRole>();
            mapTrades = new List<ReviewTrade>();
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

    /// <summary>
    /// 角色状态
    /// </summary>
    [Serializable]
    public struct ReviewRole
    {
        public double roleId;

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
    public struct ReviewTrade
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
