using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_2_5_Dialog5 : BaseGuideStep
{
    public GameObject openCG;
    //public GameObject soft;
    //public GameObject crisp;
    //public GameObject sweet;
    public GameObject softPlace;
    public GameObject crispPlace;
    public GameObject sweetPlace;

    //public GameObject seed;
    //public GameObject peasant;
    //public GameObject merchant;

    public GameObject place1;
    public GameObject place2;
    public GameObject place3;

    public Transform roles;

    public override IEnumerator StepStart()
    {
        openCG.SetActive(true);
        FTE_2_5_Manager.My.softFactory.GetComponent<BaseMapRole>().encourageLevel = -3;
        FTE_2_5_Manager.My.softFactory.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        FTE_2_5_Manager.My.crispFactory.GetComponent<BaseMapRole>().encourageLevel = -3;
        FTE_2_5_Manager.My.crispFactory.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        FTE_2_5_Manager.My.sweetFactory.GetComponent<BaseMapRole>().encourageLevel = -3;
        FTE_2_5_Manager.My.sweetFactory.GetComponent<BaseMapRole>().startEncourageLevel = -3;

        /*sweet.transform.DOLocalMoveY(-8, 1f).Play().OnPause(() =>
        {
            sweet.transform.DOLocalMoveY(-8, 1f).Play().OnComplete(() =>
            {
                sweet.SetActive(false);
            });
        }).OnComplete(() =>
        {
            sweet.SetActive(false);
        });*/
        

        
        /*soft.transform.DOLocalMoveY(-8, 1f).Play().OnPause(() =>
        {
            soft.transform.DOLocalMoveY(-8, 1f).Play().OnComplete(() =>
            {
                soft.SetActive(false);
            });
        }).OnComplete(() =>
        {
            soft.SetActive(false);
        });*/
        

        
        
        /*crisp.transform.DOLocalMoveY(-8, 1f).Play().OnPause(() =>
        {
            crisp.transform.DOLocalMoveY(-8, 1f).Play().OnComplete(() =>
            {
                crisp.SetActive(false);
            });
        }).OnComplete(() =>
        {
            crisp.SetActive(false);
        });*/

        
        
        foreach (Transform role in roles)
        {
            if (!role.GetComponent<BaseMapRole>().isNpc && role.gameObject.activeInHierarchy)
            {
                PlayerData.My.DeleteRole(role.GetComponent<BaseMapRole>().baseRoleData.ID);
            }
        }
        PlayerData.My.ClearAllRoleWarehouse();
        yield return null;
        FTE_2_5_Manager.My.DownRole(FTE_2_5_Manager.My.softFactory, -8, ()=>FTE_2_5_Manager.My.softFactory.SetActive(false));
        FTE_2_5_Manager.My.DownRole(FTE_2_5_Manager.My.crispFactory, -8, ()=>FTE_2_5_Manager.My.crispFactory.SetActive(false));
        FTE_2_5_Manager.My.DownRole(FTE_2_5_Manager.My.sweetFactory, -8, ()=>FTE_2_5_Manager.My.sweetFactory.SetActive(false));
        crispPlace.transform.DOLocalMoveY(-8.32f, 1f).Play().OnPause(() =>
        {
            crispPlace.transform.DOLocalMoveY(-8.32f, 1f).Play();
        });
        softPlace.transform.DOLocalMoveY(-8.32f, 1f).Play().OnPause(() =>
        {
            softPlace.transform.DOLocalMoveY(-8.32f, 1f).Play();
        });
        sweetPlace.transform.DOLocalMoveY(-8.32f, 1f).Play().OnPause(() =>
        {
            sweetPlace.transform.DOLocalMoveY(-8.32f, 1f).Play();
        });

    }

    public override bool ChenkEnd()
    {
        return !openCG.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        
        yield return new WaitForSeconds(0.5f);
        FTE_2_5_Manager.My.npcSeed.SetActive(true);
        FTE_2_5_Manager.My.npcPeasant.SetActive(true);
        FTE_2_5_Manager.My.npcPeasant.SetActive(true);

        /*seed.transform.DOLocalMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            seed.transform.DOLocalMoveY(0.32f, 1f).Play();
        });*/
        FTE_2_5_Manager.My.UpRole(FTE_2_5_Manager.My.npcSeed, 0.32f);
        FTE_2_5_Manager.My.UpRole(FTE_2_5_Manager.My.npcPeasant, 0.32f);
        FTE_2_5_Manager.My.UpRole(FTE_2_5_Manager.My.npcMerchant, 0.32f);
        place1.transform.DOLocalMoveY(0f, 1f).Play().OnPause(() =>
        {
            place1.transform.DOLocalMoveY(0f, 1f).Play();
        });
        /*peasant.transform.DOLocalMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            peasant.transform.DOLocalMoveY(0.32f, 1f).Play();
        });*/
        place2.transform.DOLocalMoveY(0f, 1f).Play().OnPause(() =>
        {
            place2.transform.DOLocalMoveY(0f, 1f).Play();
        });
        /*merchant.transform.DOLocalMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            merchant.transform.DOLocalMoveY(0.32f, 1f).Play();
        });*/
        place3.transform.DOLocalMoveY(0f, 1f).Play().OnPause(() =>
        {
            place3.transform.DOLocalMoveY(0f, 1f).Play();
        });
    }
}
