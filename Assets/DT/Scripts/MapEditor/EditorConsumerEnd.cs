using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class EditorConsumerEnd : EditorLandItem
{
    public override string GenerateSpecialOptionString()
    {
        string str = "";
        if (base.GenerateSpecialOptionString().Length != 0)
        {
            str += base.GenerateSpecialOptionString() + ",";
        }
        str += LandOptionType.End.ToString();
        return str;
    }

}
