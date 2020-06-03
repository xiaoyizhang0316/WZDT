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
        Investor,
        /// <summary>
        /// 切瓜厂
        /// </summary>
        CutFactory,
        /// <summary>
        /// 果汁厂
        /// </summary>
        JuiceFactory,
        /// <summary>
        /// 罐头厂
        /// </summary>
        CanFactory,
        /// <summary>
        /// 批发厂
        /// </summary>
        WholesaleFactory,
        /// <summary>
        /// 包装厂
        /// </summary>
        PackageFactory,
        /// <summary>
        /// 软厂
        /// </summary>
        SoftFactory,
        /// <summary>
        /// 脆厂
        /// </summary>
        CrispFactory,
        /// <summary>
        /// 甜厂
        /// </summary>
        SweetFactory,
        /// <summary>
        /// 保险公司
        /// </summary>
        Insurance,
        /// <summary>
        /// 咨询公司
        /// </summary>
        Consulting,
        /// <summary>
        /// 公关公司
        /// </summary>
        PublicRelation,
        /// <summary>
        /// 加油站
        /// </summary>
        GasStation,
        /// <summary>
        /// 广告公司
        /// </summary>
        Advertisment,
        /// <summary>
        /// 肥料厂
        /// </summary>
        Fertilizer
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
        /// 草地
        /// </summary>
        Grass,
        /// <summary>
        /// 土地
        /// </summary>
        Land,
        /// <summary>
        /// 居民区
        /// </summary>
        Road,
        /// <summary>
        /// 郊区
        /// </summary>
        OtherLandType1,
        /// <summary>
        /// 野外
        /// </summary>
        OtherLandType2,
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
        /// 普通老炮
        /// </summary>
        OldpaoNormal,
        /// <summary>
        /// 稀有老炮
        /// </summary>
        OldpaoRare,
        /// <summary>
        /// 强大老炮
        /// </summary>
        OldpaoEpic,
        /// <summary>
        /// 传奇老炮
        /// </summary>
        OldpaoLegendary,
        /// <summary>
        /// 普通白领
        /// </summary>
        WhitecollarNormal,
        /// <summary>
        /// 稀有白领
        /// </summary>
        WhitecollarRare,
        /// <summary>
        /// 强大白领
        /// </summary>
        WhitecollarEpic,
        /// <summary>
        /// 传奇白领
        /// </summary>
        WhitecollarLegendary,
        /// <summary>
        /// 普通蓝领
        /// </summary>
        BluecollarNormal,
        /// <summary>
        /// 稀有蓝领
        /// </summary>
        BluecollarRare,
        /// <summary>
        /// 强大蓝领
        /// </summary>
        BluecollarEpic,
        /// <summary>
        /// 传奇蓝领
        /// </summary>
        BluecollarLegendary,
        /// <summary>
        /// 普通金领
        /// </summary>
        GoldencollarNormal,
        /// <summary>
        /// 稀有金领
        /// </summary>
        GoldencollarRare,
        /// <summary>
        /// 强大金领
        /// </summary>
        GoldencollarEpic,
        /// <summary>
        /// 传奇金领
        /// </summary>
        GoldencollarLegendary,
        /// <summary>
        /// 普通精英
        /// </summary>
        EliteNormal,
        /// <summary>
        /// 稀有精英
        /// </summary>
        EliteRare,
        /// <summary>
        /// 强大精英
        /// </summary>
        EliteEpic,
        /// <summary>
        /// 传奇精英
        /// </summary>
        EliteLegendary,
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
