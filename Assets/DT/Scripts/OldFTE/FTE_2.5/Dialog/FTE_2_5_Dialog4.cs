using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_2_5_Dialog4 : BaseGuideStep
{
    public GameObject openCG;
    /*public List<GameObject> npcs;
    public List<GameObject> npcPlace;*/

    //public Transform soft;
    //public Transform crisp;
    //public Transform sweet;
    public Transform softPlace;
    public Transform crispPlace;
    public Transform sweetPlace;

    public override IEnumerator StepStart()
    {
        openCG.SetActive(true);
        /*for (int i = 0; i < npcs.Count; i++)
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
        }*/
        StageGoal.My.maxRoleLevel = 5;
        yield return null;
        FTE_2_5_Manager.My.sweetFactory.SetActive(true);

        /*sweet.DOMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            sweet.DOMoveY(0.32f, 1f).Play();
        });*/
        FTE_2_5_Manager.My.UpRole(FTE_2_5_Manager.My.sweetFactory, 0.32f);
        sweetPlace.DOMoveY(0f, 1f).Play().OnPause(() =>
        {
            sweetPlace.DOMoveY(0f, 1f).Play();
        });
        FTE_2_5_Manager.My.crispFactory.SetActive(true);

        /*crisp.DOMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            crisp.DOMoveY(0.32f, 1f).Play();
        });*/
        FTE_2_5_Manager.My.UpRole(FTE_2_5_Manager.My.crispFactory, 0.32f);
        
        crispPlace.DOMoveY(0f, 1f).Play().OnPause(() =>
        {
            crispPlace.DOMoveY(0f, 1f).Play();
        });
        FTE_2_5_Manager.My.softFactory.SetActive(true);

        /*soft.DOMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            soft.DOMoveY(0.32f, 1f).Play();
        });*/
        FTE_2_5_Manager.My.UpRole(FTE_2_5_Manager.My.softFactory, 0.32f);
        
        softPlace.DOMoveY(0f, 1f).Play().OnPause(() =>
        {
            softPlace.DOMoveY(0f, 1f).Play();
        });
    }

    public override bool ChenkEnd()
    {
        return !openCG.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        /*sweet.gameObject.SetActive(true);

        sweet.DOLocalMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            sweet.DOLocalMoveY(0.32f, 1f).Play();
        });
        sweetPlace.DOLocalMoveY(0f, 1f).Play().OnPause(() =>
        {
            sweetPlace.DOLocalMoveY(0f, 1f).Play();
        });
        crisp.gameObject.SetActive(true);

        crisp.DOLocalMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            crisp.DOLocalMoveY(0.32f, 1f).Play();
        });
        
        crispPlace.DOLocalMoveY(0f, 1f).Play().OnPause(() =>
        {
            crispPlace.DOLocalMoveY(0f, 1f).Play();
        });
        soft.gameObject.SetActive(true);

        soft.DOLocalMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            soft.DOLocalMoveY(0.32f, 1f).Play();
        });
        
        softPlace.DOLocalMoveY(0f, 1f).Play().OnPause(() =>
        {
            softPlace.DOLocalMoveY(0f, 1f).Play();
        });*/
        yield return new WaitForSeconds(0.5f);
    }
}
