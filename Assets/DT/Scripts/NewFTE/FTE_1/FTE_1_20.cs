using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_20 : BaseGuideStep
{
    public float waitTime;

    public List<Button> unlockButton;

    public List<Button> afterUnlockButton;

    public List<GameObject> panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isover;
 
    
    public override IEnumerator StepStart()
    {
        isover = false;
        NewCanvasUI.My.GameNormal();
        
        NewCanvasUI.My.Panel_Update.gameObject.SetActive(false);

        foreach (var VARIABLE in unlockButton)
        {
            VARIABLE.interactable = true;
        }
        yield return new WaitForSeconds(0.2f);
       isover = true;
    }

    public override IEnumerator StepEnd()
    {
        
        NewCanvasUI.My.GamePause();
        RoleListManager.My.OutButton();
        foreach (var VARIABLE in afterUnlockButton)
        {
            VARIABLE.interactable = true;
        }
        foreach (var VARIABLE in panel)
        {
            VARIABLE.gameObject.SetActive(false); 
        }
        
        yield break;
    }


    public override bool ChenkEnd()
    {
        
        if (StageGoal.My.timeCount > waitTime)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
