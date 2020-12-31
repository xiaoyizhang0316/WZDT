using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_1_5_OpenCG : MonoBehaviour
{
    public GameObject endPanel;
    public void PrologueOn()
    {
        //CameraPlay.WidescreenH_ON(Color.black, 1);
    }

    public void PrologueOff()
    {
        endPanel.GetComponent<Button>().onClick.AddListener(()=>
        {
            PlayerData.My.playerGears.Clear();
            PlayerData.My.playerWorkers.Clear();
            PlayerData.My.Reset();
            NetworkMgr.My.UpdatePlayerFTE("1.5", () =>
            {
                SceneManager.LoadScene("Map");
            });
        });
        endPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
