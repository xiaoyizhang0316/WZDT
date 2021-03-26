using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_28 : BaseGuideStep
{

    public Image image;
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
     PlayAnim();
        yield return new WaitForSeconds(0.2f); 
    }

    public override IEnumerator StepEnd()
    {
     
        yield break;
    }

    public override bool ChenkEnd()
    {
        if (image.sprite .name .Equals("000"))
        {
            return false;
        }

        else
        {
            return true;

        }

    }
}
