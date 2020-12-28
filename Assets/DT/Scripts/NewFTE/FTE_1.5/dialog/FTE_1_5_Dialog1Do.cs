using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Dialog1Do : FTE_DialogDoBase
{
    public override void DoStart()
    {
        PlayerData.My.playerGears.Clear();
        PlayerData.My.playerWorkers.Clear();
        // 升级免费，更换角色模版
        // TODO
        // 生成消费者
        NewGuideManager.My.BornEnemy1();
    }

    public override void DoEnd()
    {
        
    }
}
