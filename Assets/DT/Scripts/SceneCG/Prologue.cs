using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    public void PrologueOn()
    {

        NewCanvasUI.My.GamePause(false);
        if (!SceneManager.GetActiveScene().name.Equals("FTE_6"))
        {
            GuideManager.My.guideClose.gameObject.SetActive(false);
        }

        CameraPlay.WidescreenH_ON(Color.black, 1);
    }

    public void PrologueOff()
    {
        transform.Find("Stage").gameObject.SetActive(false);
        CameraPlay.WidescreenH_OFF();
        transform.DOScale(transform.localScale, 1).OnComplete(() =>
        {
            //NewCanvasUI.My.GameNormal();
            if (!SceneManager.GetActiveScene().name.Equals("FTE_6"))
            {
                GuideManager.My.guideClose.gameObject.SetActive(true);
                StartCoroutine(GuideManager.My.Init());
            }
        }).Play();
    } 
}
