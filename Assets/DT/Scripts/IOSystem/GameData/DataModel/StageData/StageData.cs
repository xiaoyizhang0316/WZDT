using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StageData
{
    /// <summary>
    /// 场景名称
    /// </summary>
    public string sceneName;

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

    /// <summary>
    /// 1星奖励装备
    /// </summary>
    public List<int> starOneEquip;

    /// <summary>
    /// 1星奖励工人
    /// </summary>
    public List<int> starOneWorker;

    /// <summary>
    /// 2星奖励装备
    /// </summary>
    public List<int> starTwoEquip;

    /// <summary>
    /// 2星奖励工人
    /// </summary>
    public List<int> starTwoWorker;

    /// <summary>
    /// 3星奖励装备
    /// </summary>
    public List<int> starThreeEquip;

    /// <summary>
    /// 3星奖励工人
    /// </summary>
    public List<int> starThreeWorker;
}