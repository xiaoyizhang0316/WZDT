using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    public Button reviewButton;
    public Button retryButton;
    public Button returnButton;
    // Start is called before the first frame update
    void Start()
    {
        reviewButton.onClick.AddListener(()=> {
            NewCanvasUI.My.Panel_Review.SetActive(true);
            ReviewPanel.My.Init(StageGoal.My.playerOperations);
        });

        retryButton.onClick.AddListener(()=> {
            PlayerData.My.Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            if (!PlayerData.My.isSOLO)
            {
                string str = "LoadScene|";
                str += SceneManager.GetActiveScene().name;
                if (PlayerData.My.isServer)
                {
                    PlayerData.My.server.SendToClientMsg(str);
                    NetworkMgr.My.SetPlayerStatus(SceneManager.GetActiveScene().name, NetworkMgr.My.currentBattleTeamAcount.teamID);
                }
                else
                {
                    PlayerData.My.client.SendToServerMsg(str);
                }
            }
            else
            {
                NetworkMgr.My.SetPlayerStatus(SceneManager.GetActiveScene().name, "");
            }
        });

        returnButton.onClick.AddListener(()=> {
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
        });
    }
}
