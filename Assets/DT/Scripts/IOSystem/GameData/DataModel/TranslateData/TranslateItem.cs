using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class TranslateItem
{
    public string sourceName;

    public string translateName;
}

public class TranslatesData
{
    public List<TranslateItem> translateSigns = new List<TranslateItem>();
}