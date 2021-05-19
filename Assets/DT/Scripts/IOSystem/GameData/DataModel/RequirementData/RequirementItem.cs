using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementItem
{
    public string requirementId;

    public string requirementDesc;

    public string effectDesc;

    public string targetBuffId;
    
    public string requireList;

    public string isRealTime;
}

public class RequirementsData
{
    public List<RequirementItem> requirementDataSigns = new List<RequirementItem>();
}
