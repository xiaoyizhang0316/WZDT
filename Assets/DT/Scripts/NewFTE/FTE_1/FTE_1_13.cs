using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_13 : BaseGuideStep
{
    public GameObject trade;
    public bool check = false;
    public bool showDetail = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator StepStart()
    {
        trade.SetActive(false);
        if (check&&AnsweringPanel.My.isComplete)
        {
            GuideManager.My.baseGuideSteps[15].isOpen = false;
        }
        if (showDetail)
        {
            WaveCount.My.showDetail = true;
        }
        yield break;
    }

    public override IEnumerator StepEnd()
    {
      
 
        WaveCount.My.showDetail = false ;
        yield break;
    }

  
}
