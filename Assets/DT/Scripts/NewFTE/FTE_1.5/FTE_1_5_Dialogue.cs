using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_5_Dialogue : BaseGuideStep
{
    public GameObject img;
    public GameObject txt;
    public override IEnumerator StepStart()
    {
        yield return new WaitForSeconds(0.5f);
        ShowImgAndTxt();
    }

    public override IEnumerator StepEnd()
    {
        yield break;
    }

    void ShowImgAndTxt()
    {
        if (img != null)
        {
            img.SetActive(true);
        }

        if (txt != null)
        {
            txt.SetActive(true);
        }
    }

    public override IEnumerator PlayEnd()
    {
        if (txt.GetComponent<FTE_TypeWord>())
        {
            if (txt.GetComponent<FTE_TypeWord>().isTyping)
            {
                txt.GetComponent<FTE_TypeWord>().StopType();
                yield break;
            }
        }
        Debug.Log("结束当前步骤"+GuideManager.My.currentGuideIndex);
        yield return StepEnd();
        for (int i = 0; i < highLightCopyObj.Count; i++)
        {
            //Destroy(highLightCopyObj[i], 0f);
            highLightCopyObj[i].SetActive(false);
        }
        CloseHighLight();

        GuideManager.My.PlayNextIndexGuide();
    }
}
