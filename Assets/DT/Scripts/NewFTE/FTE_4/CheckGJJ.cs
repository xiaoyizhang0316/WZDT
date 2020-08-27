using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGJJ : BaseGuideStep
{
    public Transform targetObj;

    public override IEnumerator StepEnd()
    {
        highLight2DObjList[0].SetActive(true);
        yield break;
    }

    public override IEnumerator StepStart()
    {
        NPC[] npcs = FindObjectsOfType<NPC>();
        for (int i = 0; i < npcs.Length; i++)
        {
            npcs[i].GetComponent<BoxCollider>().enabled = false;
        }
        targetObj.GetComponent<BoxCollider>().enabled = true;
        highLight2DObjList[0].SetActive(false);
        yield break;
    }

    public override bool ChenkEnd()
    {
        if (targetObj.GetComponent<NPC>().isCanSee)
            return true;
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
