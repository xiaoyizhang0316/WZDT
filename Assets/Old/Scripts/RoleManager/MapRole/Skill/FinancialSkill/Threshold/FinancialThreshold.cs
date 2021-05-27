using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinancialThreshold : BaseFinancialCompanyThreshold
{
    public int targetSeedEffect;
    public override bool Threshold()
    {
        return true;
    }

    public override string FailedTip()
    {
        return "场上未有种子商的效果值达到" + targetSeedEffect;
    }

    public override string ThresholdTip()
    {
        if (targetSeedEffect == 0)
        {
            return "";
        }

        return "至少存在一个种子商的效果值达到" + targetSeedEffect;
    }

 
}
