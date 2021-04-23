using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EncourageSkillItem
{
    public string skillName;

    public string skillType;

    public string targetBuff;

    public string stratValue;

    public string add;

}

public class EncourageSkillsData
{
    public List<EncourageSkillItem> encourageSkillSigns = new List<EncourageSkillItem>();
}


