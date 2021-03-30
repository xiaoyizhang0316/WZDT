using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class FTE_Dialog : BaseGuideStep
{
    public GameObject dialog_obj;
    public override IEnumerator StepStart()
    {
        BeforeDialog();
        dialog_obj.SetActive(true);
        yield return null;
    }

    public override bool ChenkEnd()
    {
        return !dialog_obj.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        AfterDialog();
        yield return null;
    }

    public virtual void BeforeDialog()
    {
    }

    public virtual void AfterDialog()
    {
        
    }
}
