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

    public Text winkey1;
    public Text winkey2;
    public Text winkey3;
    public List<Text> otherWinKey = new List<Text>();
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
        InitWinKey();
    }

    public void InitWinKey()
    {
        winkey1.text = BaseLevelController.My.winkey1;
        winkey2.text = BaseLevelController.My.winkey2;
        winkey3.text = BaseLevelController.My.winkey3;
    }

    public void InitOtherKey()
    {
        int count = 0;
        if (StageGoal.My.playerGold >= 10000)
        {
            otherWinKey[count].gameObject.SetActive(true);
            otherWinKey[count].text = "-投入资金给角色升级可以满足更多的消费者";
            count++;
        }
        bool flag = false;
        foreach (BaseMapRole item in PlayerData.My.MapRole)
        {
            if (!item.isNpc && item.baseRoleData.EquipList.Count == 0 && item.baseRoleData.peoPleList.Count == 0)
            {
                flag = true;
                break;
            }
        }
        if (flag)
        {
            otherWinKey[count].gameObject.SetActive(true);
            otherWinKey[count].text = "-为自己的角色安装装备可以满足更多的消费者";
            count++;
        }
        if (StageGoal.My.currentWave <= 3 && StageGoal.My.playerGold <= -5000 && count < 2)
        {
            otherWinKey[count].gameObject.SetActive(true);
            otherWinKey[count].text = "-前期需要一定的现金流来保证所有角色的运转";
            count++;
        }
    }
}
