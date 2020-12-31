using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_2_5_Dialog3Do : FTE_DialogDoBase
{
    public GameObject endPanel;
    public override void DoStart()
    {
        
    }

    public override void DoEnd()
    {
        endPanel.GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayerData.My.playerGears.Clear();
            PlayerData.My.playerWorkers.Clear();
            NetworkMgr.My.UpdatePlayerFTE("2.5", ()=>SceneManager.LoadScene("Map"));
        });
       
        endPanel.SetActive(true);
    }
}
