using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GameEnum;

namespace WZDT.GameData
{
    [Serializable]
    public class RoleTemplateModelData
    {
        /// <summary>
        /// 角色临时变量
        /// </summary>
        public BaseRoleData tempRoleData;

        /// <summary>
        /// 角色类型
        /// </summary>
        public RoleType roleType;

        /// <summary>
        /// 模板提供效果值
        /// </summary>
        public int effect;

        /// <summary>
        /// 模板提供效率值
        /// </summary>
        public int effeciency;

        /// <summary>
        /// 模板提供范围值
        /// </summary>
        public int range;

        /// <summary>
        /// 模板提供交易成本
        /// </summary>
        public int tradeCost;

        /// <summary>
        /// 模板提供风险抗力
        /// </summary>
        public int riskResistance;

        /// <summary>
        /// 模板提供成本
        /// </summary>
        public int cost;

        /// <summary>
        /// 模板提供弹药装载量
        /// </summary>
        public int bulletCapacity;

        /// <summary>
        /// Icon路径地址
        /// </summary>
        public string SpritePath;

        /// <summary>
        /// 模型路径地址
        /// </summary>
        public string PrePath;

        /// <summary>
        /// 模型空间2D路径地址
        /// </summary>
        public string RoleSpacePath;


        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            //SpritePath = CommonData.My.SpritePath + "Role/" + roleType.ToString() + "_" + level;
            //RoleSpacePath =  CommonData.My.SpritePath + "RoleSpace/" + roleType.ToString() + "_" + level;
            //PrePath = CommonData.My.PrefabPath + "Role/" + roleType.ToString() + "_" + level;
            SpritePath = "Sprite/Role/" + roleType.ToString() + "_1";
            RoleSpacePath = "Sprite/RoleSpace/" + roleType.ToString() + "_1";
            PrePath = "Prefabs/Role/" + roleType.ToString() + "_1";
            tempRoleData = new BaseRoleData();
            tempRoleData.roleType = roleType;
            tempRoleData.level = 1;
            tempRoleData.effect = effect;
            tempRoleData.effeciency = effeciency;
            tempRoleData.range = range;
            tempRoleData.riskResistance = riskResistance;
            tempRoleData.tradeCost = tradeCost;
            tempRoleData.cost = cost;
            tempRoleData.bulletCapacity = bulletCapacity;
            tempRoleData.SpritePath = SpritePath;
            tempRoleData.PrePath = PrePath;
            tempRoleData.RoleSpacePath = RoleSpacePath;
            //tempRoleData.activityId = ActivityId;
            //tempRoleData.activityList = new List<int>();
            //foreach (int i in activityList)
            //{
            //    tempRoleData.activityList.Add(i);
            //}
        }
    }
}


