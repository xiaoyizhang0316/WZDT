using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class Level2Controller : BaseLevelController
{

    public override void CountKillNumber(ConsumeSign sign)
    {
        List<ConsumerType> list = new List<ConsumerType>() { ConsumerType.BluecollarNormal,
            ConsumerType.BluecollarRare, ConsumerType.BluecollarEpic, ConsumerType.BluecollarLegendary };
        if (list.Contains(sign.consumerType))
        {
            targetNumber++;
        }
    }

    public override void CheckStarTwo()
    {
        if (targetNumber >= 18)
        {
            starTwoStatus = true;
        }
    }

    public override void CheckStarThree()
    {
        BaseMapRole[] baseMapRoles = FindObjectsOfType<BaseMapRole>();
        int count = 0;
        for (int i = 0; i < baseMapRoles.Length; i++)
        {
            if (!baseMapRoles[i].isNpc)
            {
                count++;
                if (count > 4)
                {
                    starThreeStatus = false;
                    CancelInvoke("CheckStarThree");
                }
            }
        }
        starThreeStatus = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
