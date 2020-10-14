using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMap : MonoBehaviour
{
    public Button confirm;
    public Button cancel;
    // Start is called before the first frame update
    void Start()
    {
        confirm.onClick.AddListener(Confirm);
        cancel.onClick.AddListener(Cancel);
    }


    void Confirm()
    {
        StageGoal.My.CommitLose();
        PlayerData.My.Reset();
        SceneManager.LoadScene("Map");
        if (!PlayerData.My.isSOLO)
        {
            string str = "LoadScene|Map";
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str);
            }
            else
            {
                PlayerData.My.client.SendToServerMsg(str);
            }
        }
    }

    void Cancel()
    {
        gameObject.SetActive(false);
    }
    
}
