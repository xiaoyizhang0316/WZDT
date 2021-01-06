using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_Video : MonoBehaviour
{
    public bool isup =false;
    // Update is called once per frame
    void Update()
    {
        if ((!isup&& NewCanvasUI.My.Panel_Update.gameObject.activeSelf)||(!isup&& NewCanvasUI.My.Panel_AssemblyRole.gameObject.activeSelf ))
        {
            isup = true;
            GetComponent<RectTransform>().DOAnchorPosY(300, 0.5f).SetEase(Ease.Linear).SetUpdate(true).Play();
        }
        if ( isup&& !NewCanvasUI.My.Panel_Update.gameObject.activeSelf  && !NewCanvasUI.My.Panel_AssemblyRole.gameObject.activeSelf  )
        {
            isup = false;
            GetComponent<RectTransform>().DOAnchorPosY(-145, 0.5f).SetEase(Ease.Linear).SetUpdate(true).Play();
        }
    }
}
