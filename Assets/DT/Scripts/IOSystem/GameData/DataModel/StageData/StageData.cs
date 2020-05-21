using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class StageData
{
    /// <summary>
    /// 场景名称
    /// </summary>
    public string sceneName;

    /// <summary>
    /// 初始工人列表
    /// </summary>
    public List<int> startWorker;

    /// <summary>
    /// 初始装备列表
    /// </summary>
    public List<int> startEquip;

    /// <summary>
    /// 关卡总波数
    /// </summary>
    public int maxWaveNumber;

    /// <summary>
    /// 每波间隔时间
    /// </summary>
    public List<int> waveWaitTime;

    /// <summary>
    /// 玩家初始血量
    /// </summary>
    public int startPlayerHealth;

    /// <summary>
    /// 玩家初始金币
    /// </summary>
    public int startPlayerGold;
}