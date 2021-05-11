using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasteKillCheck : SkillCheckBase
{
    public int startKillNum;

    protected override void InitCheck()
    {
        startKillNum = SkillCheckManager.My.tasteKillNum;
    }


    private int currentKillNum;
    protected override void Check()
    {
        currentKillNum = SkillCheckManager.My.tasteKillNum - startKillNum;
        currentText.text = "当前：" +currentKillNum;
        isSuccess = currentKillNum >= int.Parse(target);
        base.Check();
    }
}
