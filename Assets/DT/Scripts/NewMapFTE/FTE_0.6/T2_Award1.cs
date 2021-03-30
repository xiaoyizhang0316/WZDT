using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Award1 : BaseGuideStep
{
    public List<int> awards=new List<int>();
    
    public GameObject award_Panel;
    public override IEnumerator StepStart()
    {
        award_Panel.SetActive(true);
        for (int i = 0; i < awards.Count; i++)
        {
            PlayerData.My.GetNewGear(awards[i]);
        }
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        yield return null;
    }
}
