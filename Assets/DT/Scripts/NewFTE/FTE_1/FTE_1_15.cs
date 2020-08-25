using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_15 : BaseGuideStep
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
    
    public override IEnumerator StepStart()
    {
  
      
       yield return new WaitForSeconds(0.4f);
      
    }

    public override IEnumerator StepEnd()
    {

        NewCanvasUI.My.GameNormal(); 
     
        foreach (var VARIABLE in MapManager.My._mapSigns)
        {
            if (VARIABLE.mapType == GameEnum.MapType.Grass && VARIABLE.baseMapRole == null)
            {
                VARIABLE.isCanPlace = true;

            }

        }
        yield break;
    }

 
}
