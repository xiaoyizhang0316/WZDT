using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_2_5_Dialog5 : BaseGuideStep
{
    public GameObject openCG;
    public GameObject soft;
    public GameObject crisp;
    public GameObject sweet;
    public GameObject softPlace;
    public GameObject crispPlace;
    public GameObject sweetPlace;

    public GameObject seed;
    public GameObject peasant;
    public GameObject merchant;

    public GameObject place1;
    public GameObject place2;
    public GameObject place3;

    public Transform roles;

    public override IEnumerator StepStart()
    {
        openCG.SetActive(true);
        soft.GetComponent<BaseMapRole>().encourageLevel = -3;
        soft.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        crisp.GetComponent<BaseMapRole>().encourageLevel = -3;
        crisp.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        sweet.GetComponent<BaseMapRole>().encourageLevel = -3;
        sweet.GetComponent<BaseMapRole>().startEncourageLevel = -3;

        sweet.transform.DOLocalMoveY(-8, 1f).Play().OnPause(() =>
        {
            sweet.transform.DOLocalMoveY(-8, 1f).Play().OnComplete(() =>
            {
                sweet.SetActive(false);
            });
        }).OnComplete(() =>
        {
            sweet.SetActive(false);
        });
        

        sweetPlace.transform.DOLocalMoveY(-8.32f, 1f).Play().OnPause(() =>
        {
            sweetPlace.transform.DOLocalMoveY(-8.32f, 1f).Play();
        });
        
        soft.transform.DOLocalMoveY(-8, 1f).Play().OnPause(() =>
        {
            soft.transform.DOLocalMoveY(-8, 1f).Play().OnComplete(() =>
            {
                soft.SetActive(false);
            });
        }).OnComplete(() =>
        {
            soft.SetActive(false);
        });
        

        softPlace.transform.DOLocalMoveY(-8.32f, 1f).Play().OnPause(() =>
        {
            softPlace.transform.DOLocalMoveY(-8.32f, 1f).Play();
        });
        
        crisp.transform.DOLocalMoveY(-8, 1f).Play().OnPause(() =>
        {
            crisp.transform.DOLocalMoveY(-8, 1f).Play().OnComplete(() =>
            {
                crisp.SetActive(false);
            });
        }).OnComplete(() =>
        {
            crisp.SetActive(false);
        });

        crispPlace.transform.DOLocalMoveY(-8.32f, 1f).Play().OnPause(() =>
        {
            crispPlace.transform.DOLocalMoveY(-8.32f, 1f).Play();
        });
        
        foreach (Transform role in roles)
        {
            if (!role.GetComponent<BaseMapRole>().isNpc && role.gameObject.activeInHierarchy)
            {
                PlayerData.My.DeleteRole(role.GetComponent<BaseMapRole>().baseRoleData.ID);
            }
        }
        PlayerData.My.ClearAllRoleWarehouse();
        yield return new WaitForSeconds(0.5f);
    }

    public override bool ChenkEnd()
    {
        return !openCG.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        seed.SetActive(true);
        peasant.SetActive(true);
        merchant.SetActive(true);

        seed.transform.DOLocalMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            seed.transform.DOLocalMoveY(0.32f, 1f).Play();
        });
        place1.transform.DOLocalMoveY(0f, 1f).Play().OnPause(() =>
        {
            place1.transform.DOLocalMoveY(0f, 1f).Play();
        });
        peasant.transform.DOLocalMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            peasant.transform.DOLocalMoveY(0.32f, 1f).Play();
        });
        place2.transform.DOLocalMoveY(0f, 1f).Play().OnPause(() =>
        {
            place2.transform.DOLocalMoveY(0f, 1f).Play();
        });
        merchant.transform.DOLocalMoveY(0.32f, 1f).Play().OnPause(() =>
        {
            merchant.transform.DOLocalMoveY(0.32f, 1f).Play();
        });
        place3.transform.DOLocalMoveY(0f, 1f).Play().OnPause(() =>
        {
            place3.transform.DOLocalMoveY(0f, 1f).Play();
        });
        yield return new WaitForSeconds(0.5f);
    }
}
