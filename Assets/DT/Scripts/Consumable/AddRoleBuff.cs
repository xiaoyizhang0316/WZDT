using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AddRoleBuff : BaseSpawnItem
{

    public int duration;

    public override void Init(int id)
    {
        base.Init(id);
        duration = GameDataMgr.My.GetBuffDataByID(id).duration;
    }

    private void Update()
    {
        if (duration > 0 && StageGoal.My.timeCount - startTime >= duration)
        {
            Destroy(gameObject, 0f);
        }
    }
}
