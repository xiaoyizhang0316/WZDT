using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalNotification : MonoSingleton<GoalNotification>
{
    
}

public class NotifySign
{
    public GoalType goalType;
    public MissionSign missionSign;
    public int startValue;
    public int currentValue;
}
public enum GoalType
{
    KillNumber,
    TotalCost
}