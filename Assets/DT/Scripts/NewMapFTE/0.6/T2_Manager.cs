﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class T2_Manager : MonoSingleton<T2_Manager>
{
    public GameObject QualitySeed;
    public GameObject QualityMerchant;
    
    public GameObject seed_sign;
    public GameObject peasant_sign;
    public GameObject merchant_sign;
    public GameObject dealer_sign;

    public GameObject time_panel;
    public Text time_text;
    
    /// <summary>
    /// 角色信息界面上移
    /// </summary>
    public void SetRoleInfoUp()
    {
        NewCanvasUI.My.Panel_Update.transform.localPosition += new Vector3(0,5000,0);
    }

    /// <summary>
    /// 角色信息界面恢复原来的位置
    /// </summary>
    public void SerRoleInfoDown()
    {
        NewCanvasUI.My.Panel_Update.transform.localPosition -= new Vector3(0,5000,0);
    
    }

    public void SetRoleMaxLevel(int level=1)
    {
        StageGoal.My.maxRoleLevel = level;
    }

    /// <summary>
    /// 配置装备按钮开启和关闭
    /// </summary>
    /// <param name="setActive"></param>
    public void SetUpdateButton(bool setActive)
    {
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().update.gameObject.SetActive(setActive);
    }

    /// <summary>
    /// 清空仓库按钮的开启和关闭
    /// </summary>
    /// <param name="setActive"></param>
    public void SetClearWHButton(bool setActive)
    {
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().clearWarehouse.gameObject.SetActive(setActive);

    }

    /// <summary>
    /// 删除角色按钮的开启和关闭
    /// </summary>
    /// <param name="setActive"></param>
    public void SetDeleteButton(bool setActive)
    {
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().delete.gameObject.SetActive(setActive);
    
    }

    /// <summary>
    /// 设置建立角色扣除科技值的数量,cost = -1时，恢复默认
    /// </summary>
    /// <param name="cost"></param>
    public void SetRoleCost(int cost = -1)
    {
        seed_sign.GetComponent<CreatRole_Button>().ReadCostTech(cost);
        peasant_sign.GetComponent<CreatRole_Button>().ReadCostTech(cost);
        merchant_sign.GetComponent<CreatRole_Button>().ReadCostTech(cost);
        dealer_sign.GetComponent<CreatRole_Button>().ReadCostTech(cost);
    }

    /// <summary>
    /// 生成消费者
    /// </summary>
    /// <param name="type"></param>
    public void BornConsumer(int type)
    {
        
    }

    /// <summary>
    /// 停止生成消费者
    /// </summary>
    public void StopBornConsumer()
    {
        
    }

    /// <summary>
    /// 删除所有的消费者
    /// </summary>
    public void DeleteAllConsumer()
    {
        
    }

    /// <summary>
    /// 删除所有的角色
    /// </summary>
    public void DeleteAllRole()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            PlayerData.My.DeleteRole(PlayerData.My.MapRole[i].baseRoleData.ID);
        }
    }

    /// <summary>
    /// 升起角色
    /// </summary>
    /// <param name="role"></param>
    /// <param name="endPosY"></param>
    public void DoMoveRoleUp(GameObject role, float endPosY=2.6f)
    {
        role.SetActive(true);
        role.GetComponent<Transform>().DOMoveY(endPosY, 0.5f).Play();
    }

    /// <summary>
    /// 降下角色
    /// </summary>
    /// <param name="role"></param>
    /// <param name="endPosY"></param>
    public void DoMoveRoleDown(GameObject role, float endPosY = -0.6f)
    {
        DeleteRoleTrade(role.GetComponent<BaseMapRole>());
        role.GetComponent<Transform>().DOMoveY(endPosY, 0.5f).Play().OnComplete(() =>
        {
            role.SetActive(false);
        });
    }

    /// <summary>
    /// 删除相关角色的交易
    /// </summary>
    /// <param name="mapRole"></param>
    public void DeleteRoleTrade(BaseMapRole mapRole)
    {
        TradeManager.My.DeleteRoleAllTrade(mapRole.baseRoleData.ID);
    }

    /// <summary>
    /// 删除所有交易
    /// </summary>
    public void DeleteAllTrade()
    {
        foreach (var trade in TradeManager.My.tradeList.Keys)
        {
            TradeManager.My.DeleteTrade(trade);
        }
    }

    /// <summary>
    /// 隐藏某个角色的交易按钮
    /// </summary>
    /// <param name="mapRole"></param>
    public void HideTradeButton(BaseMapRole mapRole)
    {
        if (mapRole == null)
        {
            return;
        }
        mapRole.tradeButton.transform.DOScale(0, 0.5f).Play();
    }
    
    /// <summary>
    /// 显示某个角色的交易按钮
    /// </summary>
    /// <param name="mapRole"></param>
    public void ShowTradeButton(BaseMapRole mapRole)
    {
        if (mapRole == null)
        {
            return;
        }
        mapRole.tradeButton.transform.DOScale(1, 0.5f).Play();
    }

    #region Time count down

    public int time_remain = 0;
    private int startTime = 0;
    /// <summary>
    /// 开始或充值倒计时
    /// </summary>
    /// <param name="time_remain"></param>
    public void ResetTimeCountDown(int time_remain)
    {
        CancelInvoke("TimeCountDown");
        this.time_remain = time_remain;
        startTime = StageGoal.My.timeCount;
        InvokeRepeating("TimeCountDown", 0, 0.5f);
    }

    /// <summary>
    /// 停止计时
    /// </summary>
    public void StopTimeCountDown()
    {
        time_panel.SetActive(false);
        CancelInvoke("TimeCountDown");
    }
    
    private void TimeCountDown()
    {
        time_remain -= (StageGoal.My.timeCount - startTime);
        ShowCountDown();
    }
    
    private void ShowCountDown()
    {
        time_text.text = time_remain.ToString();
        time_panel.SetActive(true);
    }

    #endregion
    
}