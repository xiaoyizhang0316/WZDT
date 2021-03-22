using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_01_Step_14 : BaseGuideStep
{
    public GameObject hand;
    public Transform editorParent;
    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        //Debug.Log("开始教学 " + currentStepIndex);
        RoleEditor.My.isDragEnd = true;
        RoleEditor.My.SetAllSlider(true);
        RoleEditor.My.transform.SetParent(editorParent);
        afterEntry = HandMove;
        yield return new WaitForSeconds(1);
    }

    void HandMove()
    {
        if (hand != null)
            hand.SetActive(true);
    }
}
