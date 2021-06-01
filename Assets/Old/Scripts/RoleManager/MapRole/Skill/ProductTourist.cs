using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;
public class ProductTourist : BaseProductSkill
{
    public BaseMapRole air;


    public int waceCount = 0;

    public int peopleCount = 0;

    private bool isWait;

    public ConsumerType type = ConsumerType.OldpaoNormal;
    // Start is called before the first frame update
 
    public override void Skill()
    {
        if (StageGoal.My.timeCount - StageGoal.My.turnStartTime < 20)
        {
            return;
        }

        if ( 
            !GetComponent<NPC>().isLock && role.warehouse.Count > 0&& BuildingManager.My.extraConsumer.Count>0
        )
        {
            if (BuildingManager.My.extraConsumer.Count == 0)
            {
                return;
            }

            if (waceCount < BuildingManager.My.extraConsumer.Count )
            {
                if (BuildingManager.My.extraConsumer[waceCount].num == peopleCount)
                {
                    waceCount++;
                    peopleCount = 0;
                    if (waceCount == BuildingManager.My.extraConsumer.Count)
                    {
                        return;
                    }
                }


       
            }
        else if (waceCount == BuildingManager.My.extraConsumer.Count)
            {
                return;
            }

            role.warehouse.RemoveAt(0);
            role.GetComponent<Building>().SpawnWaveSingleConsumer(  BuildingManager.My.extraConsumer[waceCount],peopleCount);
            type = BuildingManager.My.extraConsumer[waceCount].consumerType;
            peopleCount++;
            
        }
        
        
    }


    public override void UnleashSkills()
    {
        transform.DORotate(transform.eulerAngles,GameDataMgr.My.consumerWaitTime[type]).OnComplete(() =>
        {
          
            Skill();
            if (IsOpen)
            {
                UnleashSkills();
            }
        });

       
    }

    public override void OnEndTurn()
    {
     waceCount = 0;

     peopleCount = 0;

    }
}