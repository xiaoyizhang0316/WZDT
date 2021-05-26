using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleTransitionMgr : MonoSingleton<RoleTransitionMgr>
{
    // 贸易商-> 饮料公司
    // 贸易商(自)-> 饮料公司（自）
    // 果汁厂-> 饮料公司
    // 贸易商（自）-> 包装厂（自）
    // 农民-> 采摘
    public GameObject drinksCom_model;
    public GameObject package_model;
    public GameObject pick_model;

    public void ChangeModel(BaseMapRole role, GameEnum.RoleType changeType, bool isRestore)
    {
        if (!isRestore)
        {
            if (!role.isNpc)
            {
                for (int i = 0; i < role.levelModels.Count; i++)
                {
                    role.levelModels[i].SetActive(false);
                }
            }
            GameObject go=null;
            switch (changeType)
            {
                case GameEnum.RoleType.DrinksCompany:
                    go = Instantiate(drinksCom_model, role.transform);
                    break;
                case GameEnum.RoleType.PackageFactory:
                    go = Instantiate(package_model, role.transform);
                    break;
                case GameEnum.RoleType.PickingGarden:
                    go = Instantiate(pick_model, role.transform);
                    break;
            }
            go?.transform.SetSiblingIndex(0);
            if (!role.isNpc)
            {
                CommonFunc.AddComponent(go.transform.GetChild(0).gameObject, "RoleDrag");
                go.transform.Find("BasicZoneBlue").gameObject.SetActive(false);
            }
            role.tradeButton = go.GetComponentInChildren<RoleTradeButton>().transform.parent.gameObject;
            role.GetComponent<RoleTransition>().trans_model = go;
        }
        else
        {
            DestroyImmediate(role.GetComponent<RoleTransition>().trans_model);
            if (!role.isNpc)
            {
                role.CheckLevel();
                role.HideTradeButton(true);
                role.TradeLightOff();
            }
            ///role.tradeButton = GetComponentInChildren<RoleTradeButton>().transform.parent.gameObject;
        }
        //role.transform.GetComponentInChildren<RoleTradeButton>().ReInit();
    }
}
