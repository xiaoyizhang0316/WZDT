﻿using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_1_5_OpenCG : MonoBehaviour
{
    public GameObject endPanel;
    public void PrologueOn()
    {
        //CameraPlay.WidescreenH_ON(Color.black, 1);
        SaveMenu.instance.Show();
    }

    public void PrologueOff()
    {
        endPanel.GetComponent<Button>().onClick.AddListener(()=>
        {
            PlayerData.My.playerGears.Clear();
            PlayerData.My.playerWorkers.Clear();
            PlayerData.My.Reset();
            NetworkMgr.My.AddTeachLevel(TimeStamp.GetCurrentTimeStamp()-StageGoal.My.startTime, SceneManager.GetActiveScene().name, 1);
            NetworkMgr.My.UpdatePlayerFTE("1.5", () =>
            {
                SceneManager.LoadScene("Map");
            });
        });
        endPanel.SetActive(true);
        gameObject.SetActive(false);
        SaveMenu.instance.Hide();
    }
}