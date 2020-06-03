using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class BaseLevelController : MonoSingleton<BaseLevelController>
{
    /// <summary>
    /// 1星条件描述
    /// </summary>
    public string starOneCondition;

    /// <summary>
    /// 2星条件描述
    /// </summary>
    public string starTwoCondition;

    /// <summary>
    /// 3星条件描述
    /// </summary>
    public string starThreeCondition;

    /// <summary>
    /// 1星状态
    /// </summary>
    public bool starOneStatus;

    /// <summary>
    /// 2星状态
    /// </summary>
    public bool starTwoStatus;

    /// <summary>
    /// 3星状态
    /// </summary>
    public bool starThreeStatus;

    /// <summary>
    /// 1星条件检测
    /// </summary>
    /// <returns></returns>
    public virtual void CheckStarOne()
    {
        starOneStatus = StageGoal.My.playerHealth > 0;
    }

    /// <summary>
    /// 2星条件检测
    /// </summary>
    /// <returns></returns>
    public virtual void CheckStarTwo()
    {
        starTwoStatus = true;
    }

    /// <summary>
    /// 3星条件检测
    /// </summary>
    /// <returns></returns>
    public virtual void CheckStarThree()
    {
        starThreeStatus = true;
    }

    /// <summary>
    /// 1星条件字符串生成
    /// </summary>
    /// <returns></returns>
    public virtual void GenerateStarOneCondition()
    {
        starOneCondition = "";
    }

    /// <summary>
    /// 2星条件字符串生成
    /// </summary>
    /// <returns></returns>
    public virtual void GenerateStarTwoCondition()
    {
        starTwoCondition = "";
    }

    /// <summary>
    /// 3星条件字符串生成
    /// </summary>
    /// <returns></returns>
    public virtual void GenerateStarThreeCondition()
    {
        starThreeCondition = "";
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        InvokeRepeating("CheckStarTwo", 1f, 1f);
        InvokeRepeating("CheckStarThree", 1f, 1f);
    }
}
