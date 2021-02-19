using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_7 : BaseGuideStep
{
  
    
    
    public override IEnumerator StepStart()
    {
        for (int i = 0; i < MapManager.My._mapSigns.Count; i++)
        {
            if (MapManager.My._mapSigns[i].mapType == GameEnum.MapType.Grass)
            {
                MapManager.My._mapSigns[i].isCanPlace = true;
            }
        }
        yield return  new WaitForSeconds(1);
    }

    public override IEnumerator StepEnd()
    {
        yield return  new WaitForSeconds(1);

    }

    public override bool ChenkEnd()
    {
        Debug.Log(StageGoal.My.isEndTurn);
        return StageGoal.My.isEndTurn;
    }
}
