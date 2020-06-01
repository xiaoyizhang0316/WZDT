using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLevelController : MonoBehaviour
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
    public virtual bool CheckStarOne()
    {
        return true;
    }

    /// <summary>
    /// 2星条件检测
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckStarTwo()
    {
        return true;
    }

    /// <summary>
    /// 3星条件检测
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckStarThree()
    {
        return true;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
