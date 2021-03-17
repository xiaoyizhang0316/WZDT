using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class EditorConsumerSpot : EditorLandItem
{
    public int index;

    public List<PathItem> paths;

    public void AddPath(MapSign sign)
    {
        PathItem item = new PathItem();
        item.x = sign.x;
        item.y = sign.y;
        paths.Add(item);
    }

    public void DeletePath(MapSign sign)
    {
        for (int i = 0; i < paths.Count; i++)
        {
            if (paths[i].x == sign.x && paths[i].y == sign.y)
            {
                paths.RemoveRange(i, paths.Count - i);
            }
        }
    }

    public override string GenerateSpecialOptionString()
    {
        string str = "";
        if (base.GenerateSpecialOptionString().Length != 0)
        {
            str += base.GenerateSpecialOptionString() + ",";
        }
        str += LandOptionType.ConsumerSpot.ToString() + "_" + index.ToString() + "_";
        for (int i = 0; i < paths.Count; i++)
        {
            string temp = paths[i].x.ToString() + "|" + paths[i].y.ToString() + ".";
            str += temp;
        }
        return str;
    }
}

[SerializeField]
public struct PathItem
{
    public int x;

    public int y;
}
