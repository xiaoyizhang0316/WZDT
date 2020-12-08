using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_Map_3 : BaseGuideStep
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
        
        return MapGuideManager.My.GetComponent<MapObject>().isTalentPanelOpen;
    }

    void ShowInfos()
    {
        for (int i = 0; i < contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }

    }
}
