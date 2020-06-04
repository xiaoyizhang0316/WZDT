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
    public bool starOneStatus = false;

    /// <summary>
    /// 2星状态
    /// </summary>
    public bool starTwoStatus = false;

    /// <summary>
    /// 3星状态
    /// </summary>
    public bool starThreeStatus = false;

    public int targetNumber = 0;

    public virtual void CountKillNumber(ConsumeSign sign)
    {
        targetNumber++;
    }

    /// <summary>
    /// 1星条件检测
    /// </summary>
    /// <returns></returns>
    public virtual void CheckStarOne()
    {
        starOneStatus = StageGoal.My.playerHealth > 0;
        starOneCondition = "玩家当前血量大于0";
    }

    /// <summary>
    /// 2星条件检测
    /// </summary>
    /// <returns></returns>
    public virtual void CheckStarTwo()
    {
        starTwoStatus = true;
    }

    public virtual void UpdateInfo()
    {
        StageGoal.My.starOne.sprite = StageGoal.My.starSprites[starOneStatus ? 0 : 1];
        StageGoal.My.starTwo.sprite = StageGoal.My.starSprites[starTwoStatus ? 0 : 1];
        StageGoal.My.starThree.sprite = StageGoal.My.starSprites[starThreeStatus ? 0 : 1];
        StageGoal.My.starOneText.text = starOneCondition;
        StageGoal.My.starTwoText.text = starTwoCondition;
        StageGoal.My.starThreeText.text = starThreeCondition;
    }

    /// <summary>
    /// 3星条件检测
    /// </summary>
    /// <returns></returns>
    public virtual void CheckStarThree()
    {
        starThreeStatus = true;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        InvokeRepeating("CheckStarTwo", 0f, 1f);
        InvokeRepeating("CheckStarThree", 0f, 1f);
        InvokeRepeating("CheckStarOne", 0f, 1f);
        InvokeRepeating("UpdateInfo", 0.1f, 1f);
    }
}
