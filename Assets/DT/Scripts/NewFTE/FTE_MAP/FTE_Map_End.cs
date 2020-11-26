using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_Map_End : BaseGuideStep
{
    public GameObject hand;

    public List<GameObject> image;

    public override IEnumerator StepEnd()
    {
        Debug.Log("结束教学 " + currentStepIndex);
        yield break;
    }

    public override IEnumerator StepStart()
    {
        Debug.Log("开始教学 " + currentStepIndex);
        afterEntry = HandMove;
        yield return new WaitForSeconds(1);
        ShowInfos();
        EndInvoke();
    }

    void HandMove()
    {
        if (hand != null)
            hand.SetActive(true);
    }

    void ShowInfos()
    {
        for (int i = 0; i < contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < image.Count; i++)
        {
            image[i].SetActive(true);
        }
    }

    void EndInvoke()
    {
        string[] arr = NetworkMgr.My.playerDatas.unlockStatus.Split('_');
        arr[3 ]= "1";
        string newStatus = "1";
        for (int i = 1; i < arr.Length; i++)
        {
            newStatus += "_" + arr[i];
        }
        NetworkMgr.My.UpdateUnlockStatus(newStatus);
    }
}
