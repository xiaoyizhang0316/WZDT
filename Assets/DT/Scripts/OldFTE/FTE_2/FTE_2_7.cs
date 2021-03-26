using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FTE_2_7 : BaseGuideStep
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InitHighlightUI()
    {
     //   highLight2DObjList.Add(WaveCount.My.transform.GetChild(0).GetChild(3).GetChild(0).gameObject);
    // highLight2DObjList.Add(EquipListManager.My._signs[0].gameObject);
    }

    public override IEnumerator StepStart()
    {
        EquipListManager.My.GetComponent<ScrollRect>().vertical = false;
        yield break;
         
    }

    public override IEnumerator StepEnd()
    {
        EquipListManager.My.GetComponent<ScrollRect>().vertical = true;
 
        yield break;
        
    }


    public override bool ChenkEnd()
    {
        for (int i = 0; i < EquipListManager.My._signs.Count; i++)
        {
            if (EquipListManager.My._signs[i].isOccupation)
            {
                return true;
            }
           
          
        }

        return false;
    }
}
