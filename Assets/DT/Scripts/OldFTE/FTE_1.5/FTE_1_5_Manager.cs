using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_5_Manager : MonoSingleton<FTE_1_5_Manager>
{
    public int goal1FinalCost = 0;
    public bool needSkip = false;

    public GameObject qualityStation;
    public GameObject npcSeed;
    public GameObject npcPeasant;
    private void Start()
    {
        
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
        }).OnPause(()=>
        {
            role.transform.DOLocalMoveY(endPos, 1f).Play().OnComplete(() =>
            {
                doEnd?.Invoke();
            });
        });
    }
    
    public void DownRole(GameObject role, float endPos=-8f, Action doEnd = null)
    {
        role.transform.DOLocalMoveY(endPos, 1f).Play().OnComplete(() =>
        {
            doEnd?.Invoke();
        }).OnPause(()=>
        {
            role.transform.DOLocalMoveY(endPos, 1f).Play().OnComplete(() =>
            {
                doEnd?.Invoke();
            });
        });
    }
}
