using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;
using System;
using System.Linq;

public class EditorConsumerSpot : EditorLandItem
{
    public int index;

    public List<PathItem> paths;


    public void ParsePathItem(string str)
    {
        List<string> pathPoint = str.Split('.').ToList();
        for (int i = 0; i < pathPoint.Count; i++)
        {
            if (pathPoint[i].Length > 0)
            {
                int x = int.Parse(pathPoint[i].Split('|')[0]);
                int y = int.Parse(pathPoint[i].Split('|')[1]);
                PathItem item = new PathItem();
                item.x = x;
                item.y = y;
                paths.Add(item);
            }
        }
    }

    public void AddPath( int x, int y)
    {
        if (!IsDuplicatePath(x,y))
        {
            PathItem item = new PathItem();
            item.x = x;
            item.y = y;
            paths.Add(item);
        }
    }

    public void RemovePath(int x, int y )
    {
        for (int i = 0; i < paths.Count; i++)
        {
            if (paths[i].x ==  x && paths[i].y ==  y)
            {
                paths.RemoveAt( i);
            }
        }
    }

    public void GetPath(int x,int y)
    {
        
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

    public bool IsDuplicatePath(int x,int y)
    {

        for (int i = 0; i < paths.Count; i++)
        {
            if (paths[i].x == x && paths[i].y == y)
            {
                return true;
            }    
        }
        return false;
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

[Serializable]
public struct PathItem
{
    public int x;

    public int y;
}
