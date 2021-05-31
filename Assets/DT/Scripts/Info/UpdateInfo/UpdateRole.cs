using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpdateRole : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isUpdate;
    public Image hammer;

    public Text upgradeNumber;


    public Role tempBaseRoleData;

    public void Init()
    {
        GetComponent<Button>().interactable = true;

        if (RoleUpdateInfo.My.currentRole.baseRoleData.level ==StageGoal.My.maxRoleLevel)
        {
            GetComponent<Button>().interactable = false;

            upgradeNumber.gameObject.SetActive(false);
            return;
        }

        tempBaseRoleData = new Role();

        upgradeNumber.gameObject.SetActive(true);
        int costNumber = 0;
        if (RoleUpdateInfo.My.currentRole.freeUpdate)
        {
              costNumber = 0;
        }
        else
        {
            costNumber = RoleUpdateInfo.My.currentRole.baseRoleData.upgradeCost;
        }
       
        upgradeNumber.text = costNumber.ToString();
        if (costNumber == 0)
        {
            upgradeNumber.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 变形时无升级预览
        BaseMapRole _role = PlayerData.My.GetMapRoleById(RoleUpdateInfo.My.currentRole.ID);
        
        if (_role.GetComponent<RoleTransition>())
        {
            if (_role.GetComponent<RoleTransition>().IsTransition)
            {
                return;
            }
        }
        if (isUpdate || !GetComponent<Button>().interactable || RoleUpdateInfo.My.currentRole.baseRoleData.level ==StageGoal.My.maxRoleLevel)
        {
            return;
        }

        RoleBaseDataStruct basedata = new RoleBaseDataStruct();
        basedata.cost = RoleUpdateInfo.My.currentRole.cost - RoleUpdateInfo.My.currentRole.baseRoleData.cost;
        basedata.effect = RoleUpdateInfo.My.currentRole.effect - RoleUpdateInfo.My.currentRole.baseRoleData.effect;
        basedata.efficiency = RoleUpdateInfo.My.currentRole.efficiency -
                              RoleUpdateInfo.My.currentRole.baseRoleData.efficiency;
        basedata.range = RoleUpdateInfo.My.currentRole.range - RoleUpdateInfo.My.currentRole.baseRoleData.range;
        basedata.riskResistance = RoleUpdateInfo.My.currentRole.riskResistance -
                                  RoleUpdateInfo.My.currentRole.baseRoleData.riskResistance;
        basedata.tradeCost = RoleUpdateInfo.My.currentRole.tradeCost -
                             RoleUpdateInfo.My.currentRole.baseRoleData.tradeCost;


        tempBaseRoleData.cost = RoleUpdateInfo.My.currentRole.cost;
        tempBaseRoleData.effect = RoleUpdateInfo.My.currentRole.effect;
        tempBaseRoleData.efficiency = RoleUpdateInfo.My.currentRole.efficiency;
        tempBaseRoleData.range = RoleUpdateInfo.My.currentRole.range;
        tempBaseRoleData.bulletCapacity = RoleUpdateInfo.My.currentRole.bulletCapacity;
        tempBaseRoleData.equipCost = RoleUpdateInfo.My.currentRole.equipCost;
        tempBaseRoleData.landCost = RoleUpdateInfo.My.currentRole.landCost;
        tempBaseRoleData.riskResistance = RoleUpdateInfo.My.currentRole.riskResistance;
        tempBaseRoleData.techAdd = RoleUpdateInfo.My.currentRole.techAdd;
        tempBaseRoleData.tradeCost = RoleUpdateInfo.My.currentRole.tradeCost;
        tempBaseRoleData.workerCost = RoleUpdateInfo.My.currentRole.workerCost;
        tempBaseRoleData.ID = RoleUpdateInfo.My.currentRole.ID;
        tempBaseRoleData.EquipList = RoleUpdateInfo.My.currentRole.EquipList;
        tempBaseRoleData.peoPleList = RoleUpdateInfo.My.currentRole.peoPleList;
        tempBaseRoleData.baseRoleData = GameDataMgr.My.GetModelData(RoleUpdateInfo.My.currentRole.startType,
            RoleUpdateInfo.My.currentRole.baseRoleData.level + 1);
        tempBaseRoleData.baseRoleData.roleName = RoleUpdateInfo.My.currentRole.baseRoleData.roleName;


        tempBaseRoleData.CalculateAllAttribute();
        tempBaseRoleData.cost = tempBaseRoleData.baseRoleData.cost + basedata.cost;
        tempBaseRoleData.effect = tempBaseRoleData.baseRoleData.effect + basedata.effect;
        tempBaseRoleData.efficiency = tempBaseRoleData.baseRoleData.efficiency + basedata.efficiency;
        tempBaseRoleData.range = tempBaseRoleData.baseRoleData.range + basedata.range;
        tempBaseRoleData.riskResistance = tempBaseRoleData.baseRoleData.riskResistance + basedata.riskResistance;
        tempBaseRoleData.tradeCost = tempBaseRoleData.baseRoleData.tradeCost + basedata.tradeCost;
        RoleUpdateInfo.My.ReInit(tempBaseRoleData);
        isUpdate = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isUpdate || !GetComponent<Button>().interactable)
        {
            return;
        }

        //tempBaseRoleData.baseRoleData = GameDataMgr.My.GetModelData(RoleUpdateInfo.My.currentRole.baseRoleData.roleType, RoleUpdateInfo.My.currentRole.baseRoleData.level);
        //tempBaseRoleData.baseRoleData.roleName = RoleUpdateInfo.My.currentRole.baseRoleData.roleName;

        //tempBaseRoleData.CalculateAllAttribute();
        RoleUpdateInfo.My.ReInit(RoleUpdateInfo.My.currentRole);
        isUpdate = false;
    }

    private Tweener tew;

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateRole1(eventData);
    }

    public void UpdateRole1(PointerEventData eventData = null)
    {
        BaseMapRole _role = PlayerData.My.GetMapRoleById(RoleUpdateInfo.My.currentRole.ID);
        if (RoleUpdateInfo.My.currentRole.baseRoleData.level == StageGoal.My.maxRoleLevel || (tew != null && tew.IsPlaying()) ||
            !GetComponent<Button>().interactable)
        {
            //Debug.Log(hammer.transform.eulerAngles);
            return;
        }

        int costNumber = 0;
        if (RoleUpdateInfo.My.currentRole.freeUpdate)
        {
            costNumber = 0;
        }
        else
        {
            costNumber = RoleUpdateInfo.My.currentRole.baseRoleData.upgradeCost;
        }
        if (costNumber == 0)
        {
            upgradeNumber.gameObject.SetActive(false);
        }
    
        if (costNumber <= StageGoal.My.playerGold)
        {
            if (!PlayerData.My.isSOLO)
            {
                string str1 = "UpdateRole|";
                str1 += RoleUpdateInfo.My.currentRole.ID.ToString();
                str1 += ",";
                str1 += RoleUpdateInfo.My.currentRole.baseRoleData.roleName.ToString();
                if (PlayerData.My.isServer)
                {
                    PlayerData.My.server.SendToClientMsg(str1);
                }
                else
                {
                    PlayerData.My.client.SendToServerMsg(str1);
                }
            }
            DataUploadManager.My.AddData(DataEnum.角色_升级);
            switch (RoleUpdateInfo.My.currentRole.startType)
            {
                case GameEnum.RoleType.Seed:
                    DataUploadManager.My.AddData(DataEnum.角色_升级种子商);
                    break;
                case GameEnum.RoleType.Peasant:
                    DataUploadManager.My.AddData(DataEnum.角色_升级农民);
                    break;
                case GameEnum.RoleType.Merchant:
                    DataUploadManager.My.AddData(DataEnum.角色_升级贸易商);
                    break;
                case GameEnum.RoleType.Dealer:
                    DataUploadManager.My.AddData(DataEnum.角色_升级零售商);
                    break;
                default:
                    break;
            }
            GetComponent<Button>().interactable = false;

            StageGoal.My.CostPlayerGold(costNumber, true);
            StageGoal.My.Expend(costNumber, ExpendType.AdditionalCosts,
                null, "升级");
            UpgradeRoleRecord(RoleUpdateInfo.My.currentRole);
            RoleUpdateInfo.My.currentRole.baseRoleData = GameDataMgr.My.GetModelData(
                RoleUpdateInfo.My.currentRole.startType,
                RoleUpdateInfo.My.currentRole.baseRoleData.level + 1);
            // 角色变形时，不改变其变形后的类型
            if (_role.GetComponent<RoleTransition>())
            {
                RoleUpdateInfo.My.currentRole.baseRoleData.roleType =
                    _role.GetComponent<RoleTransition>().currentRoleType;
            }
            RoleUpdateInfo.My.currentRole.CalculateAllAttribute();
            RoleUpdateInfo.My.currentRole.baseRoleData.roleName = RoleUpdateInfo.My.roleName;
            PlayerData.My.GetMapRoleById(RoleUpdateInfo.My.currentRole.ID).ReaddAllBuff();
            PlayerData.My.GetMapRoleById(RoleUpdateInfo.My.currentRole.ID).RecalculateEncourageLevel();
            PlayerData.My.GetMapRoleById(RoleUpdateInfo.My.currentRole.ID).totalUpgradeCost += costNumber;
            RoleUpdateInfo.My.Init(RoleUpdateInfo.My.currentRole);
            tew = hammer.transform.DOLocalRotate(new Vector3(0, 0, -42f), 0.3f).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                //RoleUpdateInfo.My.currentRole.baseRoleData = GameDataMgr.My.GetModelData(RoleUpdateInfo.My.currentRole.baseRoleData.roleType, RoleUpdateInfo.My.nextLevel);
                //RoleUpdateInfo.My.currentRole.CalculateAllAttribute();
                //RoleUpdateInfo.My.currentRole.baseRoleData.roleName = RoleUpdateInfo.My.roleName;
                //RoleUpdateInfo.My.Init(RoleUpdateInfo.My.currentRole);

                tew = hammer.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.3f).Play();

                GetComponent<Button>().interactable = true;
                PlayerData.My.GetMapRoleById(RoleUpdateInfo.My.currentRole.ID).CheckLevel();
                if (RoleUpdateInfo.My.currentRole.baseRoleData.level == StageGoal.My.maxRoleLevel)
                {
                    upgradeNumber.gameObject.SetActive(false);
                    GetComponent<Button>().interactable = false;
                }
                else
                {
                    upgradeNumber.gameObject.SetActive(true);
                    if (costNumber == 0)
                    {
                        upgradeNumber.gameObject.SetActive(false);
                    }
                    if (eventData != null)
                    {
                        //Debug.Log("初始化下一级");
                        OnPointerEnter(eventData);

                    }

                    // upgradeNumber.text = RoleUpdateInfo.My.currentRole.baseRoleData.upgradeCost.ToString();
                    // tempBaseRoleData.baseRoleData = GameDataMgr.My.GetModelData(RoleUpdateInfo.My.currentRole.baseRoleData.roleType, RoleUpdateInfo.My.currentRole.baseRoleData.level);
                    // tempBaseRoleData.baseRoleData.roleName = RoleUpdateInfo.My.currentRole.baseRoleData.roleName;
                    // PlayerData.My.GetMapRoleById(RoleUpdateInfo.My.currentRole.ID).ResetAllBuff();
                    // //RoleUpdateInfo.My.currentRole.CalculateAllAttribute();
                    // RoleUpdateInfo.My.ReInit(tempBaseRoleData);
                }
            }).Play();
            StageGoal.My.RefreshAllCost();
        }
        else
        {
            HttpManager.My.ShowTip("金钱不足，无法升级！");
        }
    }

    public void UpgradeRoleRecord(Role role)
    {
        List<string> param = new List<string>();
        param.Add(role.ID.ToString());
        param.Add(role.baseRoleData.level.ToString());
        StageGoal.My.RecordOperation(GameEnum.OperationType.UpgradeRole, param);
    }
}