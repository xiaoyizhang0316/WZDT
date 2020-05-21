using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnum
{
    public enum ProductType
    {
        /// <summary>
        /// 种子
        /// </summary>
        Seed,

        /// <summary>
        /// 瓜
        /// </summary>
        Melon,

        /// <summary>
        /// 腐烂的瓜
        /// </summary>
        DecayMelon,

        /// <summary>
        /// 无
        /// </summary>
        None,
    }

    public enum RoleType
    {
        /// <summary>
        /// 种子商
        /// </summary>
        Seed,
        /// <summary>
        /// 农民
        /// </summary>
        Peasant,
        /// <summary>
        /// 贸易商
        /// </summary>
        Merchant,
        /// <summary>
        /// 零售商
        /// </summary>
        Dealer,
        /// <summary>
        /// 学校
        /// </summary>
        School,
        /// <summary>
        /// 银行
        /// </summary>
        Bank,
        /// <summary>
        /// 大型零售
        /// </summary>
        BigDealer,
        /// <summary>
        /// 投资人
        /// </summary>
        Investor
    }

    public enum PDPType
    {
        /// <summary>
        /// 老虎
        /// </summary>
        Tiger,
        /// <summary>
        /// 考拉
        /// </summary>
        Koala,
        /// <summary>
        /// 孔雀
        /// </summary>
        Peacock,
        /// <summary>
        /// 猫头鹰
        /// </summary>
        Owl,
        /// <summary>
        /// 变色龙
        /// </summary>
        Chameleon,
        /// <summary>
        /// 液态金属人
        /// </summary>
        LiquidMetal,
        /// <summary>
        /// 机器人
        /// </summary>
        Robot
    }

    public enum RoleSkillType
    {
        /// <summary>
        /// 产品
        /// </summary>
        Product,

        /// <summary>
        /// 服务
        /// </summary>
        Service,

        /// <summary>
        /// 解决方案
        /// </summary>
        Solution
    }

    public enum SkillLastingType
    {
        /// <summary>
        /// 按次的
        /// </summary>
        Once,

        /// <summary>
        /// 持续性的
        /// </summary>
        Lasting
    }

    public enum TradeDestinationType
    {
        /// <summary>
        /// 仓库
        /// </summary>
        Warehouse,

        /// <summary>
        /// 输入口
        /// </summary>
        Import
    }

    public enum SZFSType
    {
        /// <summary>
        /// 固定
        /// </summary>
        固定,

        /// <summary>
        /// 剩余
        /// </summary>
        剩余,

        /// <summary>
        /// 分成
        /// </summary>
        分成
    }

    public enum CashFlowType
    {
        /// <summary>
        /// 先钱
        /// </summary>
        先钱,

        /// <summary>
        /// 后钱
        /// </summary>
        后钱
    }

    public enum ConfirmOrderType
    {
        /// <summary>
        /// 删除角色
        /// </summary>
        DeleteRole,

        /// <summary>
        /// 删除交易
        /// </summary>
        DeleteTrade

    }

    public enum MapType
    {
        /// <summary>
        /// 核心区
        /// </summary>
        CBD,
        /// <summary>
        /// 商业区
        /// </summary>
        business,
        /// <summary>
        /// 居民区
        /// </summary>
        residential,
        /// <summary>
        /// 郊区
        /// </summary>
        outskirts,
        /// <summary>
        /// 野外
        /// </summary>
        openCountry,
    }

    public enum BuildingType
    {
        /// <summary>
        /// 住宅
        /// </summary>
        Residential,

        /// <summary>
        /// 平房
        /// </summary>
        Bungalow,

        /// <summary>
        /// 写字楼
        /// </summary>
        Office,

        /// <summary>
        /// 别墅
        /// </summary>
        Villa,

        /// <summary>
        /// 占位符1
        /// </summary>
        BuildingType1,

        /// <summary>
        /// 占位符2
        /// </summary>
        BuildingType2
    }

    public enum ConsumerType
    {
        /// <summary>
        /// 老炮1
        /// </summary>
        Oldpao,
        /// <summary>
        /// 老炮2
        /// </summary>
        Oldpao_2,
        /// <summary>
        /// 老炮3
        /// </summary>
        Oldpao_3,
        /// <summary>
        /// 白领1
        /// </summary>
        Whitecollar_1,
        /// <summary>
        /// 白领2
        /// </summary>
        Whitecollar_2,
        /// <summary>
        /// 白领3
        /// </summary>
        Whitecollar_3,
        /// <summary>
        /// 蓝领1
        /// </summary>
        Bluecollar_1,
        /// <summary>
        /// 蓝领2
        /// </summary>
        Bluecollar_2,
        /// <summary>
        /// 蓝领3
        /// </summary>
        Bluecollar_3,
        /// <summary>
        /// 金领1
        /// </summary>
        Goldencollar_1,
        /// <summary>
        /// 金领2
        /// </summary>
        Goldencollar_2,
        /// <summary>
        /// 金领3
        /// </summary>
        Goldencollar_3,
        /// <summary>
        /// 精英1
        /// </summary>
        Elite_1,
        /// <summary>
        /// 精英2
        /// </summary>
        Elite_2,
        /// <summary>
        /// 精英3
        /// </summary>
        Elite_3
    }

    public enum BulletBuffType
    {
        /// <summary>
        /// 属性类
        /// </summary>
        Element,

        /// <summary>
        /// 攻击特效类
        /// </summary>
        AttackEffect,

        /// <summary>
        /// debuff类
        /// </summary>
        Debuff
    }

    public enum ProductElementType
    {
        /// <summary>
        /// 通常
        /// </summary>
        Normal,
        /// <summary>
        /// 折扣
        /// </summary>
        Discount,
        /// <summary>
        /// 精美包装
        /// </summary>
        GoodPack,
        /// <summary>
        /// 软
        /// </summary>
        Soft,
        /// <summary>
        /// 脆
        /// </summary>
        Crisp,
        /// <summary>
        /// 甜
        /// </summary>
        Sweet
    }

    public enum ConsumableType
    {
        /// <summary>
        /// 影响角色
        /// </summary>
        AffectRole,

        /// <summary>
        /// 影响消费者
        /// </summary>
        AffectConsumer,

        /// <summary>
        /// 生成物品
        /// </summary>
        SpawnItem
    }

}
