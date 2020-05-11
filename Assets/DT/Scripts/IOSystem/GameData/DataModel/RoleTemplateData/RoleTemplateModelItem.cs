using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace WZDT.GameData
{
    [Serializable]
    public class RoleTemplateModelItem
    {
        /// <summary>
        /// 角色类型
        /// </summary>
        public string roleType;

        /// <summary>
        /// 模板提供效果值
        /// </summary>
        public string effect;

        /// <summary>
        /// 模板提供效率值
        /// </summary>
        public string effeciency;

        /// <summary>
        /// 模板提供范围值
        /// </summary>
        public string range;

        /// <summary>
        /// 模板提供交易成本
        /// </summary>
        public string tradeCost;

        /// <summary>
        /// 模板提供风险抗力
        /// </summary>
        public string riskResistance;

        /// <summary>
        /// 模板提供成本
        /// </summary>
        public string cost;

        /// <summary>
        /// 模板提供弹药装载量
        /// </summary>
        public string bulletCapacity;
    }

    [Serializable]
    public class RoleTemplateModelsData
    {
        public List<RoleTemplateModelItem> roleTemplateModelItems = new List<RoleTemplateModelItem>();
    }
}

