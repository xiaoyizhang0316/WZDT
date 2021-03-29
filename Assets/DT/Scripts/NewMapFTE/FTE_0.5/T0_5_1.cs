using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T0_5_1 : BaseGuideStep
{

    public GameObject dailog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        dailog.SetActive(true);
        yield break;
        
    }

    public override IEnumerator StepEnd()
    {
        dailog.SetActive(false);
        yield break;

    }

    public override bool ChenkEnd()
    {

        if (dailog.activeSelf)
        {
            return false;
        }

        else{
            return true;

        }

    }
}
