using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (SceneManager.GetActiveScene().name.Equals("FTE_-2"))
        {
            NewCanvasUI.My.GamePause();

        }

        dailog.SetActive(true);
        yield break;
        
    }

    public override IEnumerator StepEnd()
    {
        if (SceneManager.GetActiveScene().name.Equals("FTE_-2"))
        {
            NewCanvasUI.My.GameNormal();
        }

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
