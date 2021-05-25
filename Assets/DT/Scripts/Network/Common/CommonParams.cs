using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DT.Fight.Bullet;
using static GameEnum;
using UnityEngine;

public static class CommonParams
{
    
    // 排名界面单页最大长度
    public const int rankPageMaxNum = 10;
    //public static string[] fteList =  {"FTE_0.5", "FTE_0.6", "FTE_0.7", "FTE_1.5", "FTE_1.6", "FTE_2.5", "FTE_3.5", "FTE_4.5" };
    // 教学关卡列表
    public static List<string> fteList;

    public static List<RoleType> financialList;

    // 血条最大长度
    public const float bloodTotalWidth = 471f;
    
    // 匹配括号内容
    public static Regex BracketsRegex = new Regex(@"\((.*\))");

    #region 交易费用计算
    //地图最大距离
    public const float maxMapDistance = 20f;
    
    //全局最大风险
    public const float maxMapRisk = 150f;
    
    //搜寻基础系数
    public const float searchBase = 0.25f;
    
    //议价基础系数
    public const float bargainBase = 2f;
    
    //交付基础系数
    public const float deliveryBase = 16f;
    #endregion

    #region 特效

    public static string overkillEffect = "Overkill";

    public static string peasantCritEffect = "PeasantCrit";

    #endregion

    /// <summary>
    /// 初始化静态类：静态构造函数，在调用静态变量之前，静态构造函数自动先调用，可用作初始化。
    /// </summary>
    static CommonParams()
    {
        fteList = new List<string>
            {"FTE_0.5", "FTE_0.6", "FTE_0.7", "FTE_1.5", "FTE_1.6", "FTE_2.5", "FTE_3.5", "FTE_4.5"};
        financialList = new List<RoleType>() {RoleType.Government,RoleType.DrinksGroup,RoleType.financialCompany};
    }
}
