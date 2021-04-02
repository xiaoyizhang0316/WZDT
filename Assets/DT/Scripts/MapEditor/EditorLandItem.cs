using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class EditorLandItem : MonoBehaviour
{
    public bool isUnder;

    public int underTime;

    public int startPos;
    public int x;

    public int y;

    //public MapType type;

    public void Init(MapSign sign)
    {
        x = sign.x;
        y = sign.y;
    }

    public void Update()
    {
        if(   GetComponent<HexCell>())
      GetComponent<HexCell>().Elevation =   startPos ;
    }

    public virtual string GenerateSpecialOptionString()
    {
        string str = "";
        if (isUnder)
        {
            str += GameEnum.LandOptionType.MoveDown.ToString() + "_" + underTime.ToString();
        }
        return str;
    }
}
