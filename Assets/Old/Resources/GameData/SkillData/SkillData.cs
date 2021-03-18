using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

[SerializeField]
[CreateAssetMenu(menuName = "SkillData")]
public class SkillData : ScriptableObject
{
    /// <summary>
    /// 发起方
    /// </summary>
    public RoleType startRole;

    /// <summary>
    /// 承受方
    /// </summary>
    public RoleType endRole;

    /// <summary>
    /// 操作方
    /// </summary>
    public RoleType conductRole;

    /// <summary>
    /// 验收方
    /// </summary>
    public RoleType anotherRole;

    /// <summary>
    /// 是否解锁
    /// </summary>
    public bool isLock;

    /// <summary>
    /// 技能名称
    /// </summary>
    public string skillName;

    /// <summary>
    /// 技能描述
    /// </summary>
    public string skillDesc;

    /// <summary>
    /// 技能类别
    /// </summary>
    public RoleSkillType skillType;

    /// <summary>
    /// 内部搜寻比例A
    /// </summary>
    public float searchInPerA;

    /// <summary>
    /// 内部搜寻比例B
    /// </summary>
    public float searchInPerB;

    /// <summary>
    /// 内部搜寻额外比例
    /// </summary>
    public float searchInAdd;

    /// <summary>
    /// 内部议价比例A
    /// </summary>
    public float bargainInPerA;

    /// <summary>
    /// 内部议价比例B
    /// </summary>
    public float bargainInPerB;

    /// <summary>
    /// 内部议价额外比例
    /// </summary>
    public float bargainInAdd;

    /// <summary>
    /// 内部交付比例A
    /// </summary>
    public float deliverInPerA;

    /// <summary>
    /// 内部交付比例B
    /// </summary>
    public float deliverInPerB;

    /// <summary>
    /// 内部交付额外比例
    /// </summary>
    public float deliverInAdd;

    /// <summary>
    /// 内部风险比例A
    /// </summary>
    public float riskInPerA;

    /// <summary>
    /// 内部风险比例B
    /// </summary>
    public float riskInPerB;

    /// <summary>
    /// 外部搜寻比例A
    /// </summary>
    public float searchOutPerA;

    /// <summary>
    /// 外部搜寻比例B
    /// </summary>
    public float searchOutPerB;

    /// <summary>
    /// 外部议价比例A
    /// </summary>
    public float bargainOutPerA;

    /// <summary>
    /// 外部议价比例B
    /// </summary>
    public float bargainOutPerB;

    /// <summary>
    /// 外部交付比例A
    /// </summary>
    public float deliverOutPerA;

    /// <summary>
    /// 外部交付比例B
    /// </summary>
    public float deliverOutPerB;

    /// <summary>
    /// 外部风险比例A
    /// </summary>
    public float riskOutPerA;

    /// <summary>
    /// 外部风险比例B
    /// </summary>
    public float riskOutPerB;

    /// <summary>
    /// 技能成本
    /// </summary>
    public int cost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
