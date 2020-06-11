using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraConsumer : BaseExtraSkill
{
    public int targetBuildingId;

    public List<Building.WaveConfig> waveConfigs;

    public override void SkillOn()
    {
        base.SkillOn();
        Building targetBuilding = BuildingManager.My.GetBuildingByIndex(targetBuildingId);
        targetBuilding.extraConsumer.AddRange(waveConfigs);
    }

    public override void SkillOff()
    {
        base.SkillOff();
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
