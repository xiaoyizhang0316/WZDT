using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Prologue : MonoBehaviour
{
    public void PrologueOn()
    {
        NewCanvasUI.My.GamePause(false);
        CameraPlay.WidescreenH_ON(Color.black,1);
        foreach (var item in NewCanvasUI.My.highLight)
        {
            item.SetActive(false);
        }
    }

    public void PrologueOff()
    {
        transform.Find("Stage").gameObject.SetActive(false);
        CameraPlay.WidescreenH_OFF();
        transform.DOScale(transform.localScale, 1).OnComplete(() =>
        {
            NewCanvasUI.My.GameNormal();
            GuideManager.My.Init();
        }).Play();
        foreach (var item in NewCanvasUI.My.highLight)
        {
            item.SetActive(true);
        }
    }



    
}
