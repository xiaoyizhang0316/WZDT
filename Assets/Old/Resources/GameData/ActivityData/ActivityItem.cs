using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ActivityItem
{
    /// <summary>
    /// ID
    /// </summary>
    public string ID;

    /// <summary>
    /// 活动名称
    /// </summary>
    public string activityName;

    /// <summary>
    /// 输入产品种类
    /// </summary>
    public string inputProduct;

    /// <summary>
    /// 输出产品种类
    /// </summary>
    public string outputProduct;

    /// <summary>
    /// 产能加成
    /// </summary>
    public string capacityAdd;

    /// <summary>
    /// 质量加成
    /// </summary>
    public string qualityAdd;

    /// <summary>
    /// 品牌加成
    /// </summary>
    public string brandAdd;
}

[Serializable]
public class ActivitysData
{
    public List<ActivityItem> activityItems = new List<ActivityItem>();
}
