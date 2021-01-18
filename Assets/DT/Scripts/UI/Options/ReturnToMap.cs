using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMap : MonoBehaviour
{
    public Button confirm;
    public Button cancel;

    private static string[] sceneName = {"FTE_0.5", "FTE_1.5", "FTE_2.5"};
    // Start is called before the first frame update
    void Start()
    {
        confirm.onClick.AddListener(Confirm);
        cancel.onClick.AddListener(Cancel);
    }


    void Confirm()
    {
        if (!sceneName.Contains(SceneManager.GetActiveScene().name))
        {
            StageGoal.My.CommitLose();
        }
        PlayerData.My.Reset();
        SceneManager.LoadScene("Map");
        if (!PlayerData.My.isSOLO)
        {
            string str = "LoadScene|Map";
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str);
                NetworkMgr.My.SetPlayerStatus("Map", NetworkMgr.My.currentBattleTeamAcount.teamID);
            }
            else
            {
                PlayerData.My.client.SendToServerMsg(str);
            }
        }
        else
        {
            NetworkMgr.My.SetPlayerStatus("Map", "");
        }
    }

    void Cancel()
    {
        gameObject.SetActive(false);
    }
    
}
