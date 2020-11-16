using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_Map_1 : BaseGuideStep
{
    public GameObject hand;
    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        yield return new WaitForSeconds(0.5f);
        hand.SetActive(true);
        ShowInfos();
    }

    public override bool ChenkEnd()
    {
        if (GuideManager.My.GetComponent<MapObject>().levelInfo.activeInHierarchy)
        {
            if (LevelInfoManager.My.currentSceneName.Equals("FTE_2"))
            {
                //LevelInfoManager.My.close.interactable = false;
                return true;
            }
            else
            {
                LevelInfoManager.My.panel.SetActive(false);
                LevelInfoManager.My.listScript.gameObject.SetActive(false);
                LevelInfoManager.My.rankPanel.SetActive(false);
                return false;
            }
        }
        return false;
    }
    void ShowInfos()
    {
        for (int i = 0; i < contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }

    }
}
