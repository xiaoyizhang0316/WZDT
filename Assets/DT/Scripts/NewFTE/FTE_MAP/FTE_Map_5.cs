using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_Map_5 : BaseGuideStep
{
    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        yield return new WaitForSeconds(0.3f);
        ShowInfos();
    }

    public override bool ChenkEnd()
    {
        return !MapGuideManager.My.GetComponent<MapObject>().isTalentPanelOpen;
    }

    void ShowInfos()
    {
        for (int i = 0; i < contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }

    }
}
