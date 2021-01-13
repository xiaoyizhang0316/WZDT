using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_2_5_Dialog4 : BaseGuideStep
{
    public GameObject openCG;
    public List<GameObject> npcs;
    public List<GameObject> npcPlace;

    public override IEnumerator StepStart()
    {
        openCG.SetActive(true);
        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].SetActive(true);
            npcs[i].transform.DOMoveY(0.32f, 1f).Play().OnPause(() =>
            {
                npcs[i].transform.DOMoveY(0.32f, 1f).Play();
            });
        }

        for (int i = 0; i < npcPlace.Count; i++)
        {
            npcPlace[i].transform.DOMoveY(0, 1f).Play().OnPause(() =>
            {
                npcPlace[i].transform.DOMoveY(0, 1f).Play();
            });
        }

        StageGoal.My.maxRoleLevel = 5;
        yield return new WaitForSeconds(0.5f);
    }

    public override bool ChenkEnd()
    {
        return !openCG.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
