using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_0_Step_4 : BaseGuideStep
{
    bool isOver = false;
    public List<GameObject> hideGameObject;
    public List<GameObject> showGameObject;
    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        isOver = false;
        yield return new WaitForSeconds(0.5f);
        MethodDefine();
        yield return new WaitForSeconds(0.5f);
        isOver = true;
    }

    public override bool ChenkEnd()
    {
        return isOver;
    }

    void MethodDefine()
    {
        for(int i=0; i<hideGameObject.Count; i++)
        {
            hideGameObject[i].SetActive(false);
            showGameObject[i].SetActive(true);
        }
        //isOver = true;
    }
}
