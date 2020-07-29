using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9Controller : BaseLevelController
{

    public List<MapSign> lockLandList;

    public override void CheckStarOne()
    {
        if (StageGoal.My.playerSatisfy >= 10000)
        {
            starOneStatus = true;
        }
        else
        {
            starOneStatus = false;
        }
        starOneCondition = "累计得分大于10000,当前：" + StageGoal.My.playerSatisfy.ToString();
    }

    public override void CheckStarTwo()
    {
        if (StageGoal.My.playerSatisfy >= 50000)
        {
            starTwoStatus = true;
        }
        else
        {
            starTwoStatus = false;
        }
        starTwoCondition = "累计得分大于50000,当前：" + StageGoal.My.playerSatisfy.ToString();
    }

    public override void CheckStarThree()
    {
        if (StageGoal.My.playerSatisfy >= 100000)
        {
            starThreeStatus = true;
        }
        else
        {
            starThreeStatus = false;
        }
        starThreeCondition = "累计得分大于100000,当前：" + StageGoal.My.playerSatisfy.ToString();
        CheckCheat();
    }
}
