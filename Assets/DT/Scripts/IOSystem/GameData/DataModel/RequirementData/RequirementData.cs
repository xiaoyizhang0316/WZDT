using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RequirementData
{
    public int requirementId;

    public string requirementDesc;

    public string effectDesc;

    public int targetBuffId;

    public List<string> requireList = new List<string>();

    public int isRealTime;
}
