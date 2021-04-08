using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

[SerializeField]
[CreateAssetMenu(menuName = "ActivityData")]
public class ActivityData : ScriptableObject
{
    /// <summary>
    /// ID
    /// </summary>
    public int ID;

    /// <summary>
    /// 活动名称
    /// </summary>
    public string activityName;

    /// <summary>
    /// 产能加成
    /// </summary>
    public float capacityAdd;

    /// <summary>
    /// 质量加成
    /// </summary>
    public float qualityAdd;

    /// <summary>
    /// 品牌加成
    /// </summary>
    public float brandAdd;

    /// <summary>
    /// 对应技能名称
    /// </summary>
    public string skillName;

    /// <summary>
    /// 执行逻辑：将指定的1茬输入产品（若无则不需要）从输入口放置到仓库，并且使该产品种类变为指定的输出产品种类。
    /// 使该产品的数量变为原数量*（100% + （产能值 * 产能系数）%），若无指定输入，则输出数量为1.
    /// 使该产品的质量变为原质量值*（100% + （质量值 * 质量系数）%）
    /// 使该产品的品牌变为原品牌值*（100% + （品牌值 * 品牌系数）%）
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
