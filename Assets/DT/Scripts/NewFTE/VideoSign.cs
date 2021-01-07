using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class VideoSign : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool isup =false;
    // Update is called once per frame
    void Update()
    {
        if ((!isup&& NewCanvasUI.My.Panel_Update.gameObject.activeSelf)||(!isup&& NewCanvasUI.My.Panel_AssemblyRole.gameObject.activeSelf ))
        {
            isup = true;
            GetComponent<RectTransform>().DOAnchorPosY(124, 0.5f).SetEase(Ease.Linear).SetUpdate(true).Play();
        }
        if ( isup&& !NewCanvasUI.My.Panel_Update.gameObject.activeSelf  && !NewCanvasUI.My.Panel_AssemblyRole.gameObject.activeSelf  )
        {
            isup = false;
            GetComponent<RectTransform>().DOAnchorPosY(-249.3f, 0.5f).SetEase(Ease.Linear).SetUpdate(true).Play();
        }
    }
}
