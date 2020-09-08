using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;

public class SoftFTE : MonoSingleton<SoftFTE>
{

    public List<SoftFTE_Base> softList;

    public int currentIndex;

    public GameObject softFTEObj;

    public void Init(RoleType type)
    {
        currentIndex = 0;
        for (int i = 0; i < softList.Count; i++)
        {
            softList[i].Init(type);
        }
        SoftFTEStart();
    }

    public void PlayNext()
    {
        if (currentIndex == softList.Count)
        {
            SoftFTEEnd();
        }
        else
        {
            softList[currentIndex].gameObject.SetActive(true);
            softList[currentIndex].Play();
            currentIndex++;
        }
    }

    public void SoftFTEStart()
    {
        NewCanvasUI.My.GamePause(false);
        softFTEObj.SetActive(true);
        CameraPlay.WidescreenH_ON(Color.black, 1);
        Invoke("PlayNext", 1f);
    }

    public void SoftFTEEnd()
    {
        CameraPlay.WidescreenH_OFF();
        transform.DOScale(transform.localScale, 1).OnComplete(() =>
        {
            NewCanvasUI.My.GameNormal();
            softFTEObj.SetActive(false);
        }).Play();
    }

    public void Test()
    {
        Init(RoleType.Bank);
    }
}
