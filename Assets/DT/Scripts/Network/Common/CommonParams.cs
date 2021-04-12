using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonParams
{
    public const int rankPageMaxNum = 10;
    public static string[] fteList =  {"FTE_0.5", "FTE_0.6", "FTE_0.7", "FTE_1.5", "FTE_1.6", "FTE_2.5", "FTE_3.5", "FTE_4.5" };
    
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

}
