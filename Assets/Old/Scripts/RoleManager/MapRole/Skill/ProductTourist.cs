using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductTourist : BaseProductSkill
{
    public BaseMapRole air;


    public int waceCount = 0;

    public int peopleCount = 0;

    // Start is called before the first frame update
 
    public override void Skill()
    {
        if ( 
            !GetComponent<NPC>().isLock && role.warehouse.Count > 0&& BuildingManager.My.extraConsumer.Count>0
        )
        {
            if (waceCount < BuildingManager.My.extraConsumer.Count - 1)
            {
                if (BuildingManager.My.extraConsumer[waceCount].num == peopleCount)
                {
                    waceCount++;
                    peopleCount = 0;
                }


       
            }
            else if (waceCount == BuildingManager.My.extraConsumer.Count - 1 &&
                     BuildingManager.My.extraConsumer[waceCount].num == peopleCount)
            {
                return;
            }

            else
            {
            }
            
            role.warehouse.RemoveAt(0);
            role.GetComponent<Building>().SpawnWaveSingleConsumer(  BuildingManager.My.extraConsumer[waceCount],peopleCount);
            peopleCount++;
            
        }
        
        
    }

    public override void OnEndTurn()
    {
     waceCount = 0;

     peopleCount = 0;

    }
}