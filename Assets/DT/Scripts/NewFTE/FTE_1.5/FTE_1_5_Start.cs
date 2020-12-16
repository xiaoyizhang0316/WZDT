using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Start : BaseGuideStep
{
    private bool isEnd = false;
    public override IEnumerator StepStart()
    {
        isEnd = false;
        yield return new WaitForSeconds(0.5f);
        // 升级免费，更换角色模版
        // 生成消费者
        NewGuideManager.My.BornEnemy();
        yield return new WaitForSeconds(4);
        isEnd = true;
    }

    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override bool ChenkEnd()
    {
        return isEnd;
    }
}
