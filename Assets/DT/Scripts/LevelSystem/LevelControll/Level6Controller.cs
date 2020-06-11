using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Level6Controller : BaseLevelController
{
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

}
