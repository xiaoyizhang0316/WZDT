using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_2_5_Dialog6 : BaseGuideStep
{
    public GameObject openCG;
    public GameObject endPanel;

    public override IEnumerator StepStart()
    {
        openCG.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }

    public override bool ChenkEnd()
    {
        return !openCG.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        
        yield return new WaitForSeconds(0.5f);
        endPanel.GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayerData.My.playerGears.Clear();
            PlayerData.My.playerWorkers.Clear();
            PlayerData.My.Reset();
            NetworkMgr.My.AddTeachLevel(TimeStamp.GetCurrentTimeStamp()-StageGoal.My.startTime, SceneManager.GetActiveScene().name, 1);
            NetworkMgr.My.UpdatePlayerFTE("2.5", ()=>SceneManager.LoadScene("Map"));
        });
       
        endPanel.SetActive(true);
    }
}
