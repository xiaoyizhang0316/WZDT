using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillItem
{
    /// <summary>
	/// 
	/// </summary>
	public string skillID;
    /// <summary>
    /// 
    /// </summary>
    public string skillName;
    /// <summary>
    /// 
    /// </summary>
    public string skillDesc;
    /// <summary>
    /// 
    /// </summary>
    public string skillType;
    /// <summary>
    /// 
    /// </summary>
    public string skillLastType;
    /// <summary>
    /// 送货
    /// </summary>
    public string cost;

    /// <summary>
    /// 
    /// </summary>
    public string selfBuffList;
    /// <summary>
    /// 
    /// </summary>
    public string targetBuffList;

    /// <summary>
    /// 
    /// </summary>
    public string supportSZFS;

    /// <summary>
    /// 
    /// </summary>
    public string supportCashFlow;

    /// <summary>
    /// 
    /// </summary>
    public string supportFree;

    /// <summary>
    /// 
    /// </summary>
    public string supportThird;

    /// <summary>
    /// 
    /// </summary>
    public string skillContribution;

    public string baseDivide;

    public string selfSkillUnlock;

    public string targetSkillUnlock;

    public string skillNeed;
}

[Serializable]
public class SkillsData
{
    public List<SkillItem> skillSigns = new List<SkillItem>();
}