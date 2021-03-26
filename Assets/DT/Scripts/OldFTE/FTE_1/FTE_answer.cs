using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_answer : BaseGuideStep
{
    public Button ensureButton;
    // Start is called before the first frame update
    void Start()
    {
        ensureButton.onClick.AddListener(() =>
        {
            //开始答题
            AnsweringPanel.My.ShowPanel(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator StepStart()
    {
         
        
        yield break;

    }

    public override IEnumerator StepEnd()
    { 
        yield break; 
    }

    public override bool ChenkEnd()
    {
        return AnsweringPanel.My.isComplete;
    }
}
