using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_16 : BaseGuideStep
{
    public float waitTime;

    public List<Button> LockButton;

    public GameObject closeButton;

    public List<GameObject> closePanel;
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
        GuideManager.My.guideClose.gameObject.SetActive(false);
        NewCanvasUI.My.Panel_Update.gameObject.SetActive(false);
        NewCanvasUI.My.Panel_Update.transform.localPosition = Vector3.zero; 
        closeButton.SetActive(false);

        for (int i = 0; i <LockButton.Count; i++)
        {
            LockButton[i].interactable = false;
        }
        yield return new WaitForSeconds(0.2f);
       isover = true;
    }

    public override IEnumerator StepEnd()
    {
 
        NewCanvasUI.My.Panel_Update.gameObject.SetActive(false);
        closeButton.SetActive(true);

        for (int i = 0; i <closePanel.Count; i++)
        {
            closePanel[i].gameObject.SetActive(false);
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
