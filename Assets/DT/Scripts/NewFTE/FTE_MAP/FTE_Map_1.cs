using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_Map_1 : BaseGuideStep
{
    public override IEnumerator StepEnd()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator StepStart()
    {
        throw new System.NotImplementedException();
    }

    public override bool ChenkEnd()
    {
        if (GuideManager.My.GetComponent<MapObject>().levelInfo.activeInHierarchy)
        {
            if (LevelInfoManager.My.currentSceneName.Equals("FTE_2"))
            {
                LevelInfoManager.My.close.interactable = false;
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
}
