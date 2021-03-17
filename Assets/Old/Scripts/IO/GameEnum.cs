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
        Fertilizer,
        /// <summary>
        /// 研发中心
        /// </summary>
        ResearchInstitute,
        /// <summary>
        /// 网红
        /// </summary>
        Youtuber,
        /// <summary>
        /// 订单公司
        /// </summary>
        OrderCompany,
        /// <summary>
        /// 营销公司
        /// </summary>
        Marketing,
        /// <summary>
        /// 垃圾处理厂
        /// </summary>
        RubishProcess,
        /// <summary>
        /// 大数据中心
        /// </summary>
        DataCenter,
        /// <summary>
        /// 消费者消耗品厂
        /// </summary>
        ConsumerItemFactory,
        /// <summary>
        /// 角色消耗品厂
        /// </summary>
        RoleItemFactory,
        /// <summary>
        /// 任意
        /// </summary>
        All
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
        /// 高草地
        /// </summary>
        HighGrass,
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
        /// <summary>
        /// 海
        /// </summary>
        Sea
    }

    public enum LandOptionType
    {
        /// <summary>
        /// 游戏开始时隐藏/下沉的地块
        /// </summary>
        MoveDown,
        /// <summary>
        /// 消费者出生地
        /// </summary>
        ConsumerSpot,
        /// <summary>
        /// 消费者终点
        /// </summary>
        End
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
        /// <summary>
        /// 首领
        /// </summary>
        Boss,
        /// <summary>
        /// 假人1
        /// </summary>
        ConsumerModel1,
        /// <summary>
        /// 假人2
        /// </summary>
        ConsumerModel2,
        /// <summary>
        /// 假人3
        /// </summary>
        ConsumerModel3,
        /// <summary>
        /// 假人4
        /// </summary>
        ConsumerModel4,
        /// <summary>
        /// 假人5
        /// </summary>
        ConsumerModel5,
        /// <summary>
        /// 假人6
        /// </summary>
        ConsumerModel6,
        /// <summary>
        /// 假人7
        /// </summary>
        ConsumerModel7,
        /// <summary>
        /// 假人8
        /// </summary>
        ConsumerModel8,
        /// <summary>
        /// 假人9
        /// </summary>
        ConsumerModel9,
        /// <summary>
        /// 假人10
        /// </summary>
        ConsumerModel10
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
        Debuff,

        /// <summary>
        /// 给予消耗品类
        /// </summary>
        ConsumableGive
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
        SpawnItem,

        Role,

        AOE,

        LastingAOE
    }

    public enum AudioClipType
    {
        /// <summary>
        /// 开箱子
        /// </summary>
        BoxOpen,
        /// <summary>
        /// 三镜
        /// </summary>
        ThreeMirror,
        /// <summary>
        /// 发起交易
        /// </summary>
        StartTrade,
        /// <summary>
        /// 发起交易（承受者）
        /// </summary>
        EndTrade,
        /// <summary>
        /// 拿起装备工人
        /// </summary>
        GrabEquip,
        /// <summary>
        /// 放下装备工人
        /// </summary>
        PutEquip,
        /// <summary>
        /// 当钱变为负数
        /// </summary>
        MinusMoney,
        /// <summary>
        /// 当钱变为正数
        /// </summary>
        PosivityMoney,
        /// <summary>
        /// 鼠标点击
        /// </summary>
        PointerClick,
        /// <summary>
        /// 切换游戏速度
        /// </summary>
        TimeScaleChange,
        /// <summary>
        /// 菜单打开
        /// </summary>
        MenuOpen,
        /// <summary>
        /// 胜利
        /// </summary>
        Victory,
        /// <summary>
        /// 失败
        /// </summary>
        Defeat
    }

    /// <summary>
    /// 玩家操作类型  
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// 放置角色
        /// </summary>
        PutRole,
        /// <summary>
        /// 删除角色
        /// </summary>
        DeleteRole,
        /// <summary>
        /// 变更角色装备
        /// </summary>
        ChangeRole,
        /// <summary>
        /// 升级角色
        /// </summary>
        UpgradeRole,
        /// <summary>
        /// 发起交易
        /// </summary>
        CreateTrade,
        /// <summary>
        /// 删除交易
        /// </summary>
        DeleteTrade,
        /// <summary>
        /// 修改交易
        /// </summary>
        ChangeTrade,
        /// <summary>
        /// 出售角色
        /// </summary>
        SellRole,
        /// <summary>
        /// 使用消耗品
        /// </summary>
        UseConsumable
    }

    public enum StageType
    {
        /// <summary>
        /// 常规模式
        /// </summary>
        Normal,
        /// <summary>
        /// Boss模式
        /// </summary>
        Boss,

        Turn
    }

    public enum CursorType
    {
        /// <summary>
        /// 自己角色
        /// </summary>
        Role,
        /// <summary>
        /// NPC角色
        /// </summary>
        NPC,
        /// <summary>
        /// 装备和工人
        /// </summary>
        Equip,
        /// <summary>
        /// 消费者
        /// </summary>
        Consumer,
        /// <summary>
        /// 默认
        /// </summary>
        Standard
    }

}
