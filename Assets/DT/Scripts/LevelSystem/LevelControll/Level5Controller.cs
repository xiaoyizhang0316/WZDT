using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class Level5Controller : BaseLevelController
{

    public override void CountKillNumber(ConsumeSign sign)
    {
        List<ConsumerType> list = new List<ConsumerType>() { ConsumerType.GoldencollarEpic, ConsumerType.GoldencollarEpic,
            ConsumerType.GoldencollarRare,ConsumerType.GoldencollarLegendary };
        if (list.Contains(sign.consumerType))
        {
            targetNumber++;
        }
    }

    public override void CheckStarTwo()
    {
        if (targetNumber >= 10)
        {
            starTwoStatus = true;
        }
    }

    public override void CheckStarThree()
    {
        TradeSign[] signs = FindObjectsOfType<TradeSign>();
        if (signs.Length > 10)
        {
            starThreeStatus = false;
            CancelInvoke("CheckStarThree");
        }
        else
        {
            starThreeStatus = true;
        }
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
