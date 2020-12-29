using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FTE_2_5_Dialog1Do : FTE_DialogDoBase
{
    public List<GameObject> npcs;
    public List<GameObject> npcPlace;

    public Button updateButton;
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

        updateButton.interactable = true;
        updateButton.GetComponent<UpdateRole>().enabled = true;
    }

    public override void DoEnd()
    {
        
    }
}
