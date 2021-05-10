using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T8_New_KW : BaseGuideStep
{
    public BaseMapRole role;
    // Start is called before the first frame update

    public int currentCount = 0 ;

    public void OnWareHouseMoveOn(ProductData data)
    {
        if (data.buffList.Count >= 1)
        {
            currentCount++;
        }
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        role.OnMoved += OnWareHouseMoveOn;

        yield return new WaitForSeconds(1);
    }

    public override IEnumerator StepEnd()
    {  
        yield return new WaitForSeconds(1); 
    }
                  
    public override bool ChenkEnd()
    {
        missiondatas.data[0].currentNum = currentCount;
        return   missiondatas.data[0].currentNum>=   missiondatas.data[0].maxNum;
    }
 
}
