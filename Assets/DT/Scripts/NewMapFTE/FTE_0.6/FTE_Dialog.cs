using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class FTE_Dialog : BaseGuideStep
{
    public bool uploadProgress = false;
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
        if (uploadProgress)
        {
            NetworkMgr.My.UpdatePlayerFTE(SceneManager.GetActiveScene().name.Split('_')[1], () =>
            {
                SceneManager.LoadScene("Map");
            });
            NetworkMgr.My.AddTeachLevel(TimeStamp.GetCurrentTimeStamp()-StageGoal.My.startTime, SceneManager.GetActiveScene().name, 1);
        }
    }
}
