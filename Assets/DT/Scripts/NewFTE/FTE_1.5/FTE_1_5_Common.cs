using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_5_Common : BaseGuideStep
{
    public GameObject imgs;
    public List<Text> txts;
    public override IEnumerator StepStart()
    {
        yield return new WaitForSeconds(0.5f);
        ShowImgsAndTxts();
    }

    public override IEnumerator StepEnd()
    {
        yield break;
    }

    void ShowImgsAndTxts()
    {
        if (imgs != null)
        {
            imgs.SetActive(true);
        }

        if (txts.Count > 0)
        {
            for (int i = 0; i < txts.Count; i++)
            {
                txts[i].gameObject.SetActive(true);
            }
        }
    }
}
