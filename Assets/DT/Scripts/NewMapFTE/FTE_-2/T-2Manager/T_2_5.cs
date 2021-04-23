using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_2_5 : BaseGuideStep
{

    public GameObject hand;

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        StageGoal.My.killNumber = 0;
        yield return new WaitForSeconds(1);

    }

    public override IEnumerator StepEnd()
    {
       NewCanvasUI.My.GamePause();
      yield return new WaitForSeconds(1); 
    }

    public override bool ChenkEnd()
    {
        if (StageGoal.My.isTurnStart)
        {
            hand.SetActive(false);
        }

        return StageGoal.My.killNumber>1;
    }
}
