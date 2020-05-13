using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GameEnum;

 
    [Serializable]
    public class SkillData
    {
        /// <summary>
        /// 技能ID
        /// </summary>
        public int skillID;

        /// <summary>
        /// 技能名称
        /// </summary>
        public string skillName;

        /// <summary>
        /// 技能描述
        /// </summary>
        public string skillDesc;

        /// <summary>
        /// 技能要求
        /// </summary>
        public string skillNeed;

        /// <summary>
        /// 技能类别
        /// </summary>
        public RoleSkillType skillType;

        /// <summary>
        /// 技能持续类别
        /// </summary>
        public SkillLastingType skillLastType;

        /// <summary>
        /// 技能成本
        /// </summary>
        public int cost;

        /// <summary>
        /// 自身buff列表
        /// </summary>
        public List<int> selfBuffList;

        /// <summary>
        /// 目标buff列表
        /// </summary>
        public List<int> targetBuffList;

        /// <summary>
        /// 支持的收支方式列表
        /// </summary>
        public List<SZFSType> supportSZFS;

        /// <summary>
        /// 支持的现金流结构列表
        /// </summary>
        public List<CashFlowType> supportCashFlow;

        /// <summary>
        /// 是否支持免费
        /// </summary>
        public bool supportFree;

        /// <summary>
        /// 是否支持第三方目标
        /// </summary>
        public bool supportThird;

        /// <summary>
        /// 技能贡献
        /// </summary>
        public int skillContribution;

        /// <summary>
        /// 基础分成比例
        /// </summary>
        public float baseDivide;

        /// <summary>
        /// 解锁自身被动技能列表
        /// </summary>
        public List<int> selfSkillUnlock;

        /// <summary>
        /// 解锁目标被动技能列表
        /// </summary>
        public List<int> targetSkillUnlock;
    } 


