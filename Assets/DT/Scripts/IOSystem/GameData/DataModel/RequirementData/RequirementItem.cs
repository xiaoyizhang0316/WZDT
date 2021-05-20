using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RequirementItem
{
    public string requirementId;

    public string requirementDesc;

    public string effectDesc;

    public string targetBuffId;
    
    public string requirementList;

    public string isRealTime;
}

public class RequirementsData
{
    public List<RequirementItem> requirementDataSigns = new List<RequirementItem>();
}
