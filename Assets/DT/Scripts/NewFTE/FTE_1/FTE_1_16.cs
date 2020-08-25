using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_16 : BaseGuideStep
{
    public float waitTime;

    public List<Button> LockButton;
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
        
        NewCanvasUI.My.Panel_Update.gameObject.SetActive(false);
        NewCanvasUI.My.Panel_Update.transform.localPosition = Vector3.zero; 
        
        for (int i = 0; i <LockButton.Count; i++)
        {
            LockButton[i].interactable = false;
        }
        yield return new WaitForSeconds(waitTime);
       isover = true;
    }

    public override IEnumerator StepEnd()
    {
 
        
        yield break;
    }


    public override bool ChenkEnd()
    {
        return isover;
    }
}
