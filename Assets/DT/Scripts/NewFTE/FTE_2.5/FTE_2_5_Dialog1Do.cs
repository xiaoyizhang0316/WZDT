using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_Dialog1Do : FTE_DialogDoBase
{
    public List<GameObject> npcs;
    public override void DoStart()
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].SetActive(true);
        }
    }

    public override void DoEnd()
    {
        
    }
}
