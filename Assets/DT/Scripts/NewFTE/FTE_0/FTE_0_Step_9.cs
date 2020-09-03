using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FTE_0_Step_9 : BaseGuideStep
{
    public GameObject property;
    public GameObject hand;

    public string sceneName = "";

    public override IEnumerator StepEnd()
    {
        Debug.Log("结束教学 " + currentStepIndex);
        // TODO 开启答题
        //SceneManager.LoadScene("FTE_0-2");
        AnsweringPanel.My.ShowPanel(false, () =>
        {
            PlayerData.My.Reset();
            SceneManager.LoadScene(sceneName);
        });
        yield break;
    }

    public override IEnumerator StepStart()
    {
        Debug.Log("开始教学 " + currentStepIndex);
        if (property != null)
        {
            property.SetActive(false);
        }
        NewCanvasUI.My.GamePause(false);
        afterEntry = HandMove;
        yield return new WaitForSeconds(0.5f);
        ShowInfos();
        yield break;
    }

    void HandMove()
    {
        hand.SetActive(true);
    }

    void ShowInfos()
    {
        for (int i = 0; i < contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }
    }
}
