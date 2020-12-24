using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_1_5_Dialog3Do : FTE_DialogDoBase
{
    public GameObject complete;

    public override void DoStart()
    {
        
    }

    public override void DoEnd()
    {
        complete.GetComponent<Button>().onClick.AddListener(()=>
        {
            PlayerData.My.playerGears.Clear();
            PlayerData.My.playerWorkers.Clear();
            SceneManager.LoadScene("Map");
        });
        complete.SetActive(true);
    }
}
