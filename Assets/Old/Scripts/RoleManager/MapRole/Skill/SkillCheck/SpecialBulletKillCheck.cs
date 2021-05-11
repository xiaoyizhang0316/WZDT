using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBulletKillCheck : SkillCheckBase
{
    public int startKillNum;

    protected override void InitCheck()
    {
        startKillNum = SkillCheckManager.My.specialBulletKillNum;
    }


    private int currentKillNum;
    protected override void Check()
    {
        currentKillNum = SkillCheckManager.My.specialBulletKillNum - startKillNum;
        currentText.text = "当前：" +currentKillNum;
        isSuccess = currentKillNum >= int.Parse(target);
        base.Check();
    }
}
