using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    /// <summary>
    /// 角色
    /// </summary>
    public BaseMapRole role;

    /// <summary>
    /// 是否打开
    /// </summary>
    public bool IsOpen;

    /// <summary>
    /// 是否开始
    /// </summary>
    public bool isPlay;

    
    public bool isAvaliable = true;

    /// <summary>
    /// 是否播放动画
    /// </summary>
    public bool isAnimPlaying = false;

    /// <summary>
    /// 技能描述
    /// </summary>
    public string skillDesc;
    /// <summary>
    /// 附带buff列表
    /// </summary>
    public List<int> buffList=new List<int>();

    /// <summary>
    /// 当角色激活时有动画效果的物体
    /// </summary>
    public List<GameObject> animationPart = new List<GameObject>();

    /// <summary>
    /// 最大交易数量限制（0-默认为无限制）
    /// </summary>
    public int maxTradeLimit;

    // Start is called before the first frame update
    public virtual void Start()
    {
        role = GetComponent<BaseMapRole>();
    }

    /// <summary>
    /// 重启释放技能
    /// </summary>
    public abstract void ReUnleashSkills();

    /// <summary>
    ///  取消技能
    /// </summary>
    public virtual void CancelSkill()
    {
        IsOpen = false;
    }

    public virtual bool CheckIsOverLimit()
    {
        if (maxTradeLimit == 0)
            return false;
        else
        {
            if (role.tradeList.Count >= maxTradeLimit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public virtual void Init()
    {
        
    }
}
