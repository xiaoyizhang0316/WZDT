using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraConsumer : BaseExtraSkill
{
    public List<BuildingManager.WaveConfig> waveConfigs;

    public override void SkillOn(TradeSign sign)
    {
        base.SkillOn(sign);
        BuildingManager.My.extraConsumer.AddRange(waveConfigs);
    }

    public override void SkillOff(TradeSign sign)
    {
        base.SkillOff(sign);
        foreach (BuildingManager.WaveConfig w in waveConfigs)
        {
            if (BuildingManager.My.extraConsumer.Contains(w))
            {
                BuildingManager.My.extraConsumer.Remove(w);
            }
        }
    }
}
