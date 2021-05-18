using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraConsumer : BaseServiceSkill
{
    public List<BuildingManager.WaveConfig> waveConfigs;

    public override void Skill(TradeData data)
    {
        base.Skill(data);
        BuildingManager.My.extraConsumer.AddRange(waveConfigs);
    }

    public override void SkillOff(TradeData data)
    {
        base.SkillOff(data);
        foreach (BuildingManager.WaveConfig w in waveConfigs)
        {
            if (BuildingManager.My.extraConsumer.Contains(w))
            {
                BuildingManager.My.extraConsumer.Remove(w);
            }
        }
    }
}
