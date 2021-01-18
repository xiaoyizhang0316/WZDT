using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoSign : MonoBehaviour
{
    public Button in_btn;

    public Button out_btn;
    // Start is called before the first frame update
    void Start()
    {
        in_btn.onClick.AddListener(VideoHide);
        out_btn.onClick.AddListener(VideoShow);
    }

    public bool isup =false;

    private bool isManualOut = false;
    // Update is called once per frame
    void Update()
    {
        if ((!isup&& NewCanvasUI.My.Panel_Update.gameObject.activeSelf)||(!isup&& NewCanvasUI.My.Panel_AssemblyRole.gameObject.activeSelf ) || (!isup &&CheckPanel()))
        {
            isup = true;
            if (!isManualOut)
            {
                GetComponent<RectTransform>().DOAnchorPosY(124, 0.5f).SetEase(Ease.Linear).Play().OnPause(() =>
                {
                    GetComponent<RectTransform>().DOAnchorPosY(124, 0.5f).SetEase(Ease.Linear).Play();
                });
                out_btn.gameObject.SetActive(true);
            }
        }
        if ( isup&& !NewCanvasUI.My.Panel_Update.gameObject.activeSelf  && !NewCanvasUI.My.Panel_AssemblyRole.gameObject.activeSelf && !CheckPanel() )
        {
            isup = false;
            isManualOut = false;
            GetComponent<RectTransform>().DOAnchorPosY(-249.3f, 0.5f).SetEase(Ease.Linear).Play().OnPause(() =>
                {
                    GetComponent<RectTransform>().DOAnchorPosY(-249.3f, 0.5f).SetEase(Ease.Linear).Play();
                });
            in_btn.gameObject.SetActive(false);
            out_btn.gameObject.SetActive(false);
        }
    }

    void VideoShow()
    {
        isManualOut = true;
        GetComponent<RectTransform>().DOAnchorPosY(-249.3f, 0.5f).SetEase(Ease.Linear).Play();
        out_btn.gameObject.SetActive(false);
        in_btn.gameObject.SetActive(true);
    }

    void VideoHide()
    {
        isManualOut = false;
        GetComponent<RectTransform>().DOAnchorPosY(124, 0.5f).SetEase(Ease.Linear).Play();
        out_btn.gameObject.SetActive(true);
        in_btn.gameObject.SetActive(false);
    }
    
    bool CheckPanel()
    {
        if (SceneManager.GetActiveScene().name == "FTE_0.5") {
            if (OptionsPanel.My.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return true;
            }
            else
            {
                return false;

            }

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
