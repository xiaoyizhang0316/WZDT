using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T5_Task1 : BaseGuideStep
{
    public GameObject checkBorder;
    public GameObject checkBorder1;
    public GameObject checkImage;
    public GameObject wave;
    public override IEnumerator StepStart()
    {
        checkBorder.SetActive(true);
        wave.SetActive(true);
        NewCanvasUI.My.GameNormal();
        Check();
        yield return null;
    }

    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.3f);
    }

    private bool showRect = false;
    void CheckGoal()
    {
        if (!missiondatas.data[0].isFinish)
        {
            if (checkImage.activeInHierarchy)
            {
                if (!showRect)
                {
                    checkBorder.SetActive(false);
                    checkBorder1.SetActive(true);
                    showRect = true;
                }
            }
            else
            {
                if (showRect)
                {
                    checkBorder.SetActive(true);
                    checkBorder1.SetActive(false);
                    showRect = false;
                }
            }

            if (showRect)
            {
                if (!checkBorder1.activeInHierarchy && checkImage.activeInHierarchy)
                {
                    missiondatas.data[0].isFinish = true;
                }
            }
        }
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(3);
        Destroy(wave);
        FloatWindow.My.Hide();
    }
}
