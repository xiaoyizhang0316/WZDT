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
        if ((!isup&& NewCanvasUI.My.Panel_Update.gameObject.activeInHierarchy)||(!isup&& NewCanvasUI.My.Panel_AssemblyRole.gameObject.activeInHierarchy ) || (!isup && CheckPanel()))
        {
            isup = true;
            GetComponent<RectTransform>().DOAnchorPosY(124, 0.5f).SetEase(Ease.Linear).SetUpdate( UpdateType.Normal, false).Play();
        }
        if ( isup&& !NewCanvasUI.My.Panel_Update.gameObject.activeInHierarchy  && !NewCanvasUI.My.Panel_AssemblyRole.gameObject.activeInHierarchy && !CheckPanel() )
        {
            isup = false;
            GetComponent<RectTransform>().DOAnchorPosY(-249.3f, 0.5f).SetEase(Ease.Linear).SetUpdate(true).Play();
        }
    }

    bool CheckPanel()
    {
        if (NewCanvasUI.My.Panel_NPC.transform.Find("InfoList").gameObject.activeInHierarchy)
        {
            return true;
        }

        if (DataStatPanel.My.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            return true;
        }

        if (OptionsPanel.My.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            return true;
        }

        return false;
    }
}
