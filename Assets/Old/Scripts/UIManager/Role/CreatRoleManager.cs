using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class CreatRoleManager : MonoSingleton<CreatRoleManager>
{
    /// <summary>
    /// 当前角色信息
    /// </summary>
    public Role CurrentRole = new Role();

    public Text roleTitleText;

    public Image roleBg;

    /// <summary>
    /// 当前创建装备的位置坐标
    /// </summary>
    public Transform equipPos;

    /// <summary>
    /// 当前创建装备的位置坐标
    /// </summary>
    public Transform workerPos;

    /// <summary>
    /// 装备列表预制体
    /// </summary>
    public GameObject equipPrb;

    /// <summary>
    /// 工人列表预制体
    /// </summary>
    public GameObject workerPrb;

    /// <summary>
    /// 装备模板槽
    /// </summary>
    public Transform gearTemplate;

    /// <summary>
    /// 工人模板槽
    /// </summary>
    public Transform workerTemplate;

    /// <summary>
    /// 角色模板槽
    /// </summary>
    public Transform roleTemplate;

    public TemplateManager CurrentTemplateManager;

    /// <summary>
    /// 模板创建位置
    /// </summary>
    public Transform template_BottomPos;

    public Transform template_MidPos;
    public Transform template_TopPos;

    private GameObject templateOBJ;

    /// <summary>
    /// 确认按钮
    /// </summary>
    public GameObject ensureButton;

    /// <summary>
    /// 工人是否都在装备上
    /// </summary>
    public bool isWorkerOnEquip;

    /// <summary>
    /// 是否至少一个装备和一个工人
    /// </summary>
    public bool isAtLeastOneWorkerEquip;

    /// <summary>
    /// 装备是否满足模板需求
    /// </summary>
    //public bool isNeedTemplate;

    /// <summary>
    /// 所有装备槽位
    /// </summary>
    public PlotSign[] gearPlot;

    /// <summary>
    /// 所有工人槽位
    /// </summary>
    public PlotSign[] workerPlot;

    public Dictionary<int, Vector3> EquipList = new Dictionary<int, Vector3>();
    public Dictionary<int, Vector3> peoPleList = new Dictionary<int, Vector3>();

    public Button showWorker;

    public Button showGear;

    /// <summary>
    /// 工人加起来的4项属性值（用于UI显示）
    /// </summary>

    #region workerAttribute

    public int workerCapacity;

    public int workerEfficiency;
    public int workerQuality;
    public int workerBrand;

    #endregion

    /// <summary>
    /// 装备加起来的4项属性值（用于UI显示）
    /// </summary>

    #region gearAttribute

    public int gearCapacity;

    public int gearEfficiency;
    public int gearQuality;
    public int gearBrand;

    #endregion

    /// <summary>
    /// 最终的4项属性值（用于UI显示和最终结算）
    /// </summary>

    #region finalAttribute

    public int finalEffect;

    public int finalEfficiency;
    public int finalRange;
    public int finalTradeCost;
    public int finalCost;
    public int finalBulletCapacity;
    public int finalRiskResistance;
    public int finalTechAdd;
    public int finalEncourageAdd;

    public GameObject seedInfo;
    public GameObject peasantInfo;
    public GameObject merchantInfo;
    public GameObject dealerInfo;


    public List<EffectMove> effects;

    public Text souxun;
    public Text yijia;
    public Text jiaodu;
    public Text fengxian;

    public Canvas currentCanvas;

    #endregion


    public void ShowEquipListPOPDatal(int ID)
    {
        //var data = GameDataMgr.My.GetGearData(ID);
        //souxun.text = data.effect.ToString();
        //yijia.text = data.efficiency.ToString();
        //jiaodu.text = data.range.ToString();
        ////fengxian.text = data.riskAdd.ToString();
    }

    public void ShowWorkListPOPDatal(int ID)
    {
        // var data = GameDataMgr.My.GetWorkerData(ID);
        // souxun.text = data.effect.ToString();
        // yijia.text = data.efficiency.ToString();
        // jiaodu.text = data.range.ToString();
        // //fengxian.text = data.riskAdd.ToString();
    }

    private bool isPause;

    private List<int> originalEquip = new List<int>();

    private List<int> originalWorker = new List<int>();

    /// <summary>
    /// 初始化角色创建界面，将要创建的角色模板赋值给管理类
    /// </summary> 
    public void Open(Role tempRole)
    {
        isPause = DOTween.defaultAutoPlay == AutoPlay.None;
        NewCanvasUI.My.GamePause(false);
        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].Move();
        }

        NewCanvasUI.My.Panel_ChoseRole.SetActive(false);
        CurrentRole = tempRole;
        //print(tempRole.baseRoleData.roleType);
        //print(CurrentRole.baseRoleData.roleType);
        EquipList = new Dictionary<int, Vector3>();
        peoPleList = new Dictionary<int, Vector3>();
        originalEquip.Clear();
        originalWorker.Clear();
        foreach (var v in tempRole.EquipList)
        {
            originalEquip.Add(v.Key);
            EquipList.Add(v.Key, v.Value);
        }

        foreach (var v in tempRole.peoPleList)
        {
            originalWorker.Add(v.Key);
            peoPleList.Add(v.Key, v.Value);
        }

        if (template_BottomPos.childCount > 0)
        {
            Destroy(template_BottomPos.GetChild(0).gameObject);
        }

        roleBg.sprite = Resources.Load<Sprite>("Sprite/RoleBg/" + tempRole.baseRoleData.roleType.ToString() +
                                               tempRole.baseRoleData.level.ToString());
        templateOBJ =
            Instantiate(
                Resources.Load<GameObject>(GameDataMgr.My.GetModelData(CurrentRole.baseRoleData.roleType, 1)
                    .RoleSpacePath), template_BottomPos);
        CurrentTemplateManager = templateOBJ.GetComponent<TemplateManager>();
        SetCreateRoleTitle();
        EquipListManager.My.Init();
        WorkerListManager.My.Init();
        CheckAllConditions();
    }

    /// <summary>
    /// 显示组装中的角色的职业名称
    /// </summary>
    public void SetCreateRoleTitle()
    {
        seedInfo.SetActive(false);
        peasantInfo.SetActive(false);
        merchantInfo.SetActive(false);
        dealerInfo.SetActive(false);
        switch (CurrentRole.baseRoleData.roleType)
        {
            case RoleType.Seed:
                roleTitleText.text = "种子商";
                seedInfo.SetActive(true);
                break;
            case RoleType.Peasant:
                roleTitleText.text = "农民";
                peasantInfo.SetActive(true);

                break;
            case RoleType.Merchant:
                roleTitleText.text = "贸易商";
                merchantInfo.SetActive(true);

                break;
            case RoleType.Dealer:
                roleTitleText.text = "零售商";
                dealerInfo.SetActive(true);

                break;
            default:
                roleTitleText.text = "未知";
                break;
        }
    }

    private GameObject tempGo;

    /// <summary>
    /// 将组装好的角色的装备复位
    /// </summary>
    public void PutEquip()
    {
        foreach (var v in EquipList)
        {
            tempGo = Instantiate(Resources.Load<GameObject>(GameDataMgr.My.GetGearData(v.Key).GearSpacePath),
                EquipListManager.My.equipPos);

            Vector3 V2 = new Vector3(v.Value.x - Screen.width / 2, v.Value.y - Screen.height / 2);

            tempGo.transform.localPosition = v.Value;
            tempGo.name = "EquipOBJ_" + v.Key;
            tempGo.GetComponent<DragUI>().dragType = DragUI.DragType.equip;
        }
    }

    /// <summary>
    /// 将组装好的角色的工人复位
    /// </summary>
    public void PutWorker()
    {
        foreach (var v in peoPleList)
        {
            tempGo = Instantiate(Resources.Load<GameObject>(GameDataMgr.My.GetWorkerData(v.Key).WorkerSpacePath),
                WorkerListManager.My.workerPos);

            Vector3 V2 = new Vector3(v.Value.x - Screen.width / 2, v.Value.y - Screen.height / 2);
            tempGo.transform.localPosition = v.Value;
            tempGo.name = "workerOBJ_" + v.Key;
            tempGo.GetComponent<DragUI>().dragType = DragUI.DragType.people;
        }
    }

    /// <summary>
    /// 判断角色组装是否满足所有条件
    /// </summary>
    public void CheckAllConditions()
    {
        CalculateAllAttribute();
        GetComponentInChildren<BaseRoleInfoAdd>().UpdateBuff();
        GetComponentInChildren<BaseRoleInfoAdd>().UpdateBar();
        GetComponentInChildren<BaseRoleInfoAdd>().Init();
    }

    /// <summary>
    /// 计算角色属性数值
    /// </summary>
    public void CalculateAllAttribute()
    {
        InitRoleValue();
        foreach (var i in EquipList)
        {
            GearData tempData = GameDataMgr.My.GetGearData(i.Key);
            finalEffect += (int) (tempData.effect);
            finalEfficiency += (int) (tempData.efficiency );
            finalRange += (int) (tempData.range );
            finalTradeCost += tempData.tradeCost;
            finalCost += (int) (tempData.cost );
            finalRiskResistance += tempData.riskResistance;
            finalBulletCapacity += tempData.bulletCapacity;
            finalEncourageAdd += tempData.encourageAdd;
            CurrentRole.equipCost += tempData.cost;
        }

        PDPCheck();
        foreach (var i in peoPleList)
        {
            WorkerData tempData = GameDataMgr.My.GetWorkerData(i.Key);
            finalEffect += (int) (tempData.effect );
            finalEfficiency += (int) (tempData.efficiency );
            finalRange += (int) (tempData.range );
            finalTradeCost += tempData.tradeCost;
            finalRiskResistance += tempData.riskResistance;
            finalBulletCapacity += tempData.bulletCapacity;
            finalTechAdd += tempData.techAdd;
            finalCost += (int) (tempData.cost);
            CurrentRole.workerCost += tempData.cost;
        }

        FinalCheck();
    }

    /// <summary>
    /// 初始化角色数值，放置多重叠加
    /// </summary>
    public void InitRoleValue()
    {
        finalEffect = CurrentRole.baseRoleData.effect;
        finalEfficiency = CurrentRole.baseRoleData.efficiency;
        finalRange = CurrentRole.baseRoleData.range;
        finalTradeCost = CurrentRole.baseRoleData.tradeCost;
        finalCost = CurrentRole.baseRoleData.cost;
        finalBulletCapacity = CurrentRole.baseRoleData.bulletCapacity;
        finalRiskResistance = CurrentRole.baseRoleData.riskResistance;
        finalTechAdd = 0;
        finalEncourageAdd = 0;
        CurrentRole.equipCost = 0;
        CurrentRole.workerCost = 0;
        //CurrentRole.cost = CurrentRole.baseRoleData.cost;
    }

    /// <summary>
    /// 计算工人PDP性格
    /// </summary>
    public void PDPCheck()
    {
    }

    /// <summary>
    /// 最后结算角色实际的预览属性
    /// </summary>
    public void FinalCheck()
    {
    }

    /// <summary>
    /// 点确认后保存所有信息
    /// </summary>
    public void QuitAndSave()
    {
        CurrentRole.effect = finalEffect;
        CurrentRole.efficiency = finalEfficiency;
        CurrentRole.riskResistance = finalRiskResistance;
        CurrentRole.range = finalRange;
        CurrentRole.cost = finalCost;
        CurrentRole.tradeCost = finalTradeCost;
        CurrentRole.bulletCapacity = finalBulletCapacity;
        CurrentRole.techAdd = finalTechAdd;
        CurrentRole.gearEncourageAdd = finalEncourageAdd;
        List<int> keys = EquipList.Keys.ToList();
        CurrentRole.EquipList.Clear();
        CurrentRole.peoPleList.Clear();
        string str1 = "UpdateRoleEquipAndWorker|";
        str1 += RoleUpdateInfo.My.currentRole.ID.ToString();
        str1 += ",";

        for (int i = 0; i < keys.Count; i++)
        {
            CurrentRole.EquipList.Add(keys[i], EquipList[keys[i]]);
            str1 += keys[i].ToString();
            if (keys.Count - 1 > i)
            {
                str1 += "_";
            }
        }

        str1 += "&";
        List<int> keys2 = peoPleList.Keys.ToList();
        for (int i = 0; i < keys2.Count; i++)
        {
            CurrentRole.peoPleList.Add(keys2[i], peoPleList[keys2[i]]);
            str1 += keys2[i].ToString();
            if (keys2.Count - 1 > i)
            {
                str1 += "_";
            }
        }

        if (!PlayerData.My.isSOLO)
        {
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str1);
            }
            else
            {
                PlayerData.My.client.SendToServerMsg(str1);
            }
        }

        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (System.Math.Abs(CurrentRole.ID - PlayerData.My.RoleData[i].ID) < 0.1f)
            {
                if (ExecutionManager.My.SubExecution(ExecutionManager.My.modifyRole))
                {
                    PlayerData.My.RoleData[i] = CurrentRole;
                }
            }
        }

        if (originalEquip.Count == 0 && originalWorker.Count == 0)
        {
            if (CurrentRole.EquipList.Count != 0 || CurrentRole.peoPleList.Count != 0)
            {
                DataUploadManager.My.AddData(DataEnum.装备_增加);
            }
        }
        else
        {
            bool isChange = false;
            for (int i = 0; i < originalEquip.Count; i++)
            {
                if (!CurrentRole.EquipList.ContainsKey(originalEquip[i]))
                {
                    isChange = true;
                    break;
                }
            }

            for (int i = 0; i < originalWorker.Count; i++)
            {
                if (!CurrentRole.peoPleList.ContainsKey(originalWorker[i]))
                {
                    isChange = true;
                    break;
                }
            }

            if (isChange)
            {
                DataUploadManager.My.AddData(DataEnum.装备_增加);
            }
        }

        PlayerData.My.GetMapRoleById(CurrentRole.ID).RecalculateEncourageLevel();
        PlayerData.My.GetMapRoleById(CurrentRole.ID).ReaddAllBuff();
        PlayerData.My.GetMapRoleById(CurrentRole.ID).CheckGearConsumable();
        WorkerListManager.My.QuitAndSave();
        EquipListManager.My.QuitAndSave();
        ChangeRoleRecord(CurrentRole);
        DeleteTemplate();
        StageGoal.My.RefreshAllCost();
    }

    /// <summary>
    /// 记录修改角色操作
    /// </summary>
    /// <param name="role"></param>
    public void ChangeRoleRecord(Role role)
    {
        List<string> param = new List<string>();
        param.Add(role.ID.ToString());
        BaseMapRole mapRole = PlayerData.My.GetMapRoleById(role.ID);
        List<int> buffList = new List<int>();
        buffList.AddRange(mapRole.GetEquipBuffList());
        for (int i = 0; i < buffList.Count; i++)
        {
            param.Add(buffList[i].ToString());
        }

        StageGoal.My.RecordOperation(OperationType.ChangeRole, param);
    }

    /// <summary>
    /// 删除所有模板中的子物体
    /// </summary>
    public void DeleteTemplate()
    {
        for (int i = 0; i < template_BottomPos.childCount; i++)
        {
            Destroy(template_BottomPos.GetChild(i).gameObject, 0.1f);
        }

        for (int i = 0; i < template_TopPos.childCount; i++)
        {
            Destroy(template_TopPos.GetChild(i).gameObject, 0.1f);
        }

        for (int i = 0; i < template_MidPos.childCount; i++)
        {
            Destroy(template_MidPos.GetChild(i).gameObject, 0.1f);
        }

        for (int i = 0; i < workerPos.childCount; i++)
        {
            Destroy(workerPos.GetChild(i).gameObject, 0.1f);
        }

        for (int i = 0; i < equipPos.childCount; i++)
        {
            Destroy(equipPos.GetChild(i).gameObject, 0.1f);
        }

        for (int i = 0; i < WorkerListManager.My.transform.Find("Viewport/Content").childCount; i++)
        {
            Destroy(WorkerListManager.My.transform.Find("Viewport/Content").GetChild(i).gameObject, 0.1f);
        }

        for (int i = 0; i < EquipListManager.My.transform.Find("Viewport/Content").childCount; i++)
        {
            Destroy(EquipListManager.My.transform.Find("Viewport/Content").GetChild(i).gameObject, 0.1f);
        }

        WorkerListManager.My._signs.Clear();
        EquipListManager.My._signs.Clear();
        CloseMenu();
    }


    /// <summary>
    /// 重置角色
    /// </summary>
    public void ResetRole()
    {
        for (int i = 0; i < workerPos.childCount; i++)
        {
            Destroy(workerPos.GetChild(i).gameObject, 0f);
        }

        for (int i = 0; i < equipPos.childCount; i++)
        {
            Destroy(equipPos.GetChild(i).gameObject, 0f);
        }

        for (int i = 0; i < gearPlot.Length; i++)
        {
            gearPlot[i].isOccupied = false;
        }

        for (int i = 0; i < workerPlot.Length; i++)
        {
            workerPlot[i].isOccupied = false;
        }

        foreach (var v in peoPleList)
        {
            WorkerListManager.My.UninstallWorker(v.Key);
        }

        peoPleList.Clear();
        foreach (var v in EquipList)
        {
            EquipListManager.My.UninstallEquip(v.Key);
        }

        EquipList.Clear();
        CheckAllConditions();
    }

    /// <summary>
    /// 关闭当前菜单
    /// </summary>
    public void CloseMenu()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].Back();
        }
   
     
           
              
          
               
         

        transform.DOScale(1, 0.5f).OnComplete(() =>
        {
            if (!isPause)
            {
                if (DOTween.timeScale > 1f)
                {
                    NewCanvasUI.My.GameAccelerate();
                }
                else
                {
                    NewCanvasUI.My.GameNormal();
                }
            
            }
            else
            {
                NewCanvasUI.My.GamePause();
            }

            gameObject.SetActive(false);
        })  .Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        showWorker.onClick.AddListener(() =>
        {
            template_BottomPos.GetComponentInChildren<TemplateManager>().OpenTopTemplate(0.3f);
        });
        showGear.onClick.AddListener(() =>
        {
            template_BottomPos.GetComponentInChildren<TemplateManager>().OpenMidTemplate(0.3f);
        });
    }

    /// <summary>
    /// 创建装备啊列表
    /// </summary>
    public void CreatEquipList()
    {
    }

    /// <summary>
    /// 创建工人列表
    /// </summary>
    public void CreatWorkerList()
    {
    }
}