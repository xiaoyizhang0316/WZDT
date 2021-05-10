﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EncourageSkillItem
{
    public string skillId;

    public string skillDesc;

    public string skillType;

    public string targetBuff;

    public string startValue;

    public string encourageAdd;

    public string specialAddType;

    public string specialAdd;

}

public class EncourageSkillsData
{
    public List<EncourageSkillItem> encourageSkillSigns = new List<EncourageSkillItem>();
}


