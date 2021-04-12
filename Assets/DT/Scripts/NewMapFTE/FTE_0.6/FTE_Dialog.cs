using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class FTE_Dialog : BaseGuideStep
{
    public bool uploadProgress = false;
    public GameObject dialog_obj;
    public List<int> equipAward=new List<int>();
    public override IEnumerator StepStart()
    {
        BeforeDialog();
        NewCanvasUI.My.GamePause();
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
        NewCanvasUI.My.GameNormal();
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
            if (equipAward.Count > 0)
            {
                for (int i = 0; i < equipAward.Count; i++)
                {
                    NetworkMgr.My.AddEquip(equipAward[i], equipAward[i].ToString().StartsWith("1")?0:1, 1);
                }
            }
        }
    }
}
