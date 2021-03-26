using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_18_3 : BaseGuideStep
{
    public GameObject UI;
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
        yield return new WaitForSeconds(0.2f); 
    }

    public override IEnumerator StepEnd()
    {
        yield return null;
    }

    public override void InitHighlight3d()
    {
        
        
    }

    public override bool ChenkEnd()
    {
        if (UI.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
