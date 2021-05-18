using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMegaCheck : SkillCheckBase
{
    public int startMega;

    protected override void InitCheck()
    {
        startMega = SkillCheckManager.My.getMega;
    }

    private int currentMega;
    protected override void Check()
    {
        currentMega = SkillCheckManager.My.getMega - startMega;
        currentText.text = "当前：" +currentMega;
        isSuccess = currentMega >= int.Parse(target);
        base.Check();
    }
}
