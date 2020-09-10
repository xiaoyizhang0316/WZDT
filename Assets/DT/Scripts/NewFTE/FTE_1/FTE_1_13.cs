using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_13 : BaseGuideStep
{
    public GameObject trade;
    public bool check = false;
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

        if (check&&AnsweringPanel.My.isComplete)
        {
            GuideManager.My.baseGuideSteps[15].isOpen = false;
        }

        yield break;
    }

    public override IEnumerator StepEnd()
    {
      
        trade.SetActive(false);
        yield break;
    }

  
}
