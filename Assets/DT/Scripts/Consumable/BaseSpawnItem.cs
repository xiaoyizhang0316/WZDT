using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawnItem : MonoBehaviour
{
    public int startTime;

    public virtual void Init(int id)
    {
        startTime = StageGoal.My.timeCount;
    }
}
