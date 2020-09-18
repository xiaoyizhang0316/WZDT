using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_8 : BaseGuideStep
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
        WorkerListManager.My.GetComponent<ScrollRect>().vertical = false;
        if (AnsweringPanel.My.isComplete)
        {
            GuideManager.My.baseGuideSteps[20].isOpen = false;
        }
        yield break;
         
    }

    public override IEnumerator StepEnd()
    {
        WorkerListManager.My.GetComponent<ScrollRect>().vertical = true;

        yield break;
        
    }


    public override bool ChenkEnd()
    {
        for (int i = 0; i < WorkerListManager.My._signs.Count; i++)
        {
            if (WorkerListManager.My._signs[i].isOccupation)
            {
                return true;
            }
           
          
        }

        return false;
    }
}
