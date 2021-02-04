using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_2_5_Manager : MonoSingleton<FTE_2_5_Manager>
{
    /*public int sweetKillNum = 0;
    public int crispKillNum = 0;
    public int softKillNum = 0;*/
    public int packageKillNum = 0;
    public int saleKillNum = 0;
    public int nolikeKillNum = 0;

    public GameObject qualityStation;
    public GameObject dealer1;
    public GameObject dealer2;
    public GameObject dealer3;
    public GameObject sweetFactory;
    public GameObject softFactory;
    public GameObject crispFactory;
    public GameObject npcSeed;
    public GameObject npcPeasant;
    public GameObject npcMerchant;
    public GameObject taste1;
    public GameObject taste2;
    public GameObject taste3;
    

    //public bool isClearGoods = false;

    public bool needSkip = false;

    public void CheckTasteKill(int index)
    {
        switch (index)
        {
            case 0:
                packageKillNum += 1;
                break;
            case 1:
                saleKillNum += 1;
                break;
            case 2:
                nolikeKillNum += 1;
                break;
        }
    }
    
    public void UpRole(GameObject role, float endPos=0, Action doEnd = null)
    {
        if (!role.activeInHierarchy)
        {
            role.SetActive(true);
        }

        role.transform.DOLocalMoveY(endPos, 1f).Play().OnComplete(() =>
        {
            doEnd?.Invoke();
            role.transform.DOLocalMoveY(endPos, 0.02f).Play();
        }).OnPause(()=>
        {
            role.transform.DOLocalMoveY(endPos, 1f).Play().OnComplete(() =>
            {
                doEnd?.Invoke();
                role.transform.DOLocalMoveY(endPos, 0.02f).Play();
            });
        });
    }
    
    public void DownRole(GameObject role, float endPos=-8f, Action doEnd = null)
    {
        role.transform.DOLocalMoveY(endPos, 1f).Play().OnComplete(() =>
        {
            doEnd?.Invoke();
            role.transform.DOLocalMoveY(endPos, 0.02f).Play();
        }).OnPause(()=>
        {
            role.transform.DOLocalMoveY(endPos, 1f).Play().OnComplete(() =>
            {
                doEnd?.Invoke();
                role.transform.DOLocalMoveY(endPos, 0.02f).Play();
            });
        });
    }
}
