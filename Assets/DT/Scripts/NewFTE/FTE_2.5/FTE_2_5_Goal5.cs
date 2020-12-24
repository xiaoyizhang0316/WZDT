using System.Collections;
using UnityEngine;

public class FTE_2_5_Goal5 : BaseGuideStep
{
    
    public override IEnumerator StepStart()
    {
        
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2f);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish&&missiondatas.data[3].isFinish;
    }

    void CheckGoal()
    {
        
    }
}
