using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraConsumer : BaseExtraSkill
{
    public int targetBuildingId;

    public List<Building.WaveConfig> waveConfigs;

    public override void SkillOn(TradeSign sign)
    {
        base.SkillOn(sign);
        Building targetBuilding = BuildingManager.My.GetBuildingByIndex(targetBuildingId);
        targetBuilding.extraConsumer.AddRange(waveConfigs);
    }

    public override void SkillOff(TradeSign sign)
    {
        base.SkillOff(sign);
        Building targetBuilding = BuildingManager.My.GetBuildingByIndex(targetBuildingId);
        foreach (Building.WaveConfig w in waveConfigs)
        {
            if (targetBuilding.extraConsumer.Contains(w))
            {
                targetBuilding.extraConsumer.Remove(w);
            }
        }
    }
}
