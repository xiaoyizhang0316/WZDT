using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if ((!isup&& NewCanvasUI.My.Panel_Update.gameObject.activeSelf)||(!isup&& NewCanvasUI.My.Panel_AssemblyRole.gameObject.activeSelf ) || (!isup &&CheckPanel()))
        {
            isup = true;
            GetComponent<RectTransform>().DOAnchorPosY(124, 0.5f).SetEase(Ease.Linear).Play().OnPause(() =>
                {
                    GetComponent<RectTransform>().DOAnchorPosY(124, 0.5f).SetEase(Ease.Linear).Play();
                });
        }
        if ( isup&& !NewCanvasUI.My.Panel_Update.gameObject.activeSelf  && !NewCanvasUI.My.Panel_AssemblyRole.gameObject.activeSelf && !CheckPanel() )
        {
            isup = false;
            GetComponent<RectTransform>().DOAnchorPosY(-249.3f, 0.5f).SetEase(Ease.Linear).Play().OnPause(() =>
                {
                    GetComponent<RectTransform>().DOAnchorPosY(-249.3f, 0.5f).SetEase(Ease.Linear).Play();
                });
        }
    }
    
    bool CheckPanel()
    {
        if (SceneManager.GetActiveScene().name == "FTE_0.5") {
            return false;
        }
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
