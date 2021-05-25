using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMaga : BaseFinancialCompanyThreshold
{
    public int mageAdd = 0 ;

    public override bool Threshold()
    {
        return StageGoal.My.techCost > mageAdd;
    }

    public override string FailedTip()
    {
        return "累计消耗Mega值未达标";
    }

    public override string ThresholdTip()
    {
        return "累计消耗Mega值需达到"+mageAdd+"点";
    }
}
