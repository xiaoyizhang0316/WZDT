using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FTE_2_5_Dialog1Do : FTE_DialogDoBase
{
    public List<GameObject> npcs;
    public List<GameObject> npcPlace;

    public override void DoStart()
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].SetActive(true);
            npcs[i].transform.DOMoveY(0.32f, 1f);
        }

        for (int i = 0; i < npcPlace.Count; i++)
        {
            npcPlace[i].transform.DOMoveY(0, 1f);
        }

        StageGoal.My.maxRoleLevel = 5;
    }

    public override void DoEnd()
    {
        
    }
}
