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

    public void Init()
    {
        if (RoleUpdateInfo.My.currentRole.baseRoleData.level == 5)
        {
            upgradeNumber.gameObject.SetActive(false);
            return;
        }
        upgradeNumber.gameObject.SetActive(true);
        upgradeNumber.text = GameDataMgr.My.GetModelData(RoleUpdateInfo.My.currentRole.baseRoleData.roleType, RoleUpdateInfo.My.nextLevel).upgradeCost.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isUpdate || !GetComponent<Button>().interactable || RoleUpdateInfo.My.currentLevel == 5)
        {
            return;
        }
        RoleUpdateInfo.My.currentRole.baseRoleData = GameDataMgr.My.GetModelData(RoleUpdateInfo.My.currentRole.baseRoleData.roleType, RoleUpdateInfo.My.nextLevel);
        RoleUpdateInfo.My.currentRole.baseRoleData.roleName = RoleUpdateInfo.My.roleName;
        PlayerData.My.GetMapRoleById(RoleUpdateInfo.My.currentRole.ID).ResetAllBuff();
        //RoleUpdateInfo.My.currentRole.CalculateAllAttribute();
        RoleUpdateInfo.My.ReInit(RoleUpdateInfo.My.currentRole);
        isUpdate = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isUpdate||!GetComponent<Button>().interactable  )
        {
            return;
        }

        RoleUpdateInfo.My.currentRole.baseRoleData = GameDataMgr.My.GetModelData(RoleUpdateInfo.My.currentRole.baseRoleData.roleType, RoleUpdateInfo.My.currentLevel);
        RoleUpdateInfo.My.currentRole.baseRoleData.roleName = RoleUpdateInfo.My.roleName;
        PlayerData.My.GetMapRoleById(RoleUpdateInfo.My.currentRole.ID).ResetAllBuff();
        //RoleUpdateInfo.My.currentRole.CalculateAllAttribute();
        RoleUpdateInfo.My.ReInit(RoleUpdateInfo.My.currentRole);
        isUpdate = false;
    }

    private Tweener tew;
    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateRole1();
    }

    public void UpdateRole1()
    {
        if (RoleUpdateInfo.My.currentLevel == 5 || (tew != null && tew.IsPlaying())||!GetComponent<Button>().interactable   )
        {
            Debug.Log(hammer.transform.eulerAngles);
            return;
        }
        if (RoleUpdateInfo.My.currentRole.baseRoleData.upgradeCost <= StageGoal.My.playerGold)
        {
            DataUploadManager.My.AddData(DataEnum.角色_升级);
            GetComponent<Button>().interactable = false;
            print(RoleUpdateInfo.My.currentRole.baseRoleData.roleName);
            print(RoleUpdateInfo.My.currentRole.baseRoleData.upgradeCost);
            StageGoal.My.CostPlayerGold(RoleUpdateInfo.My.currentRole.baseRoleData.upgradeCost);
            StageGoal.My.Expend(RoleUpdateInfo.My.currentRole.baseRoleData.upgradeCost, ExpendType.AdditionalCosts, null, "升级");
            UpgradeRoleRecord(RoleUpdateInfo.My.currentRole);
            RoleUpdateInfo.My.currentRole.baseRoleData = GameDataMgr.My.GetModelData(RoleUpdateInfo.My.currentRole.baseRoleData.roleType, RoleUpdateInfo.My.nextLevel);
            RoleUpdateInfo.My.currentRole.CalculateAllAttribute();
            RoleUpdateInfo.My.currentRole.baseRoleData.roleName = RoleUpdateInfo.My.roleName;
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
                if (RoleUpdateInfo.My.currentRole.baseRoleData.level == 5)
                {
                    upgradeNumber.gameObject.SetActive(false);
                }
                else
                {
                    upgradeNumber.gameObject.SetActive(true);
                    RoleUpdateInfo.My.currentRole.baseRoleData = GameDataMgr.My.GetModelData(RoleUpdateInfo.My.currentRole.baseRoleData.roleType, RoleUpdateInfo.My.currentLevel);
                    RoleUpdateInfo.My.currentRole.baseRoleData.roleName = RoleUpdateInfo.My.roleName;
                    PlayerData.My.GetMapRoleById(RoleUpdateInfo.My.currentRole.ID).ResetAllBuff();
                    //RoleUpdateInfo.My.currentRole.CalculateAllAttribute();
                    upgradeNumber.text = GameDataMgr.My.GetModelData(RoleUpdateInfo.My.currentRole.baseRoleData.roleType, RoleUpdateInfo.My.nextLevel).upgradeCost.ToString();
                    RoleUpdateInfo.My.ReInit(RoleUpdateInfo.My.currentRole);
                }
            }).Play();
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
