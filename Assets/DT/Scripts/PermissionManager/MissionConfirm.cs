using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionConfirm : MonoSingleton<MissionConfirm>
{
    /// <summary>
    /// 玩家一名称
    /// </summary>
    public Text playerMainName;

    /// <summary>
    /// 玩家二名称
    /// </summary>
    public Text playerBubName;


    public Button mainButton;
    public Button subButton;
    public string subName;
    /// <summary>
    /// 开始按钮
    /// </summary>
    public Button start;

    public Button close;
    
    public bool Ready;

    public bool subReady;
    public Sprite red;
    public Sprite redReady;
    public Sprite blue;
    /// <summary>
    /// 修改职责
    /// </summary>
    public void ChangeDuty()
    {
        
    }

    public void GetUserName()
    {
     
            ///客户端同步名称
            ///

            string str1 = "GetUserName|";
            str1 +=  NetworkMgr.My.playerDatas.playerName; 
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str1);
            }
            else
            {
                PlayerData.My.client.SendToServerMsg(str1);
            }
     
    }


    public void InitName()
    {
        if (PlayerData.My.playerDutyID ==PlayerData.My.creatRole)
        {
            playerMainName.text  = NetworkMgr.My.playerDatas.playerName;
            playerBubName.text = subName;
        }
        else
        {
            playerBubName.text = NetworkMgr.My.playerDatas.playerName;
            playerMainName.text = subName;
        }
    }

    public void InitReady()
    {
        if (Ready)
        {
            start.transform.GetChild(0).GetComponent<Text>().text = "取消";
            start.GetComponent<Image>().sprite = redReady;
        }

        else
        {
            start.transform.GetChild(0).GetComponent<Text>().text = "准备"; 
            start.GetComponent<Image>().sprite = red;
        }

        if (Ready && subReady&& PlayerData.My.isServer)
        {
            start.transform.GetChild(0).GetComponent<Text>().text = "开始"; 
            start.GetComponent<Image>().sprite = blue;
        }
    }

// Start is called before the first frame update
    void Start()
    {
        GetUserName();
        InitName();
        start.onClick.AddListener(() =>
        {
            if (Ready && subReady&& PlayerData.My.isServer)
            {
                LevelInfoManager.My.loadScene();
            }

            else 
            {
                Ready = !Ready;
                if (!PlayerData.My.isServer)
                {
                    string str1 = "OnReady|";
                    if (Ready)
                    {
                        str1 += 1.ToString();

                    }

                    else
                    {
                        str1 += 0.ToString();
                        
                    }
                    PlayerData.My.client.SendToServerMsg(str1); 
                }
            }
 
        });
        mainButton.onClick.AddListener(() =>
        {
            if (PlayerData.My.isServer)
            {
                string str1 = "ConfirmDuty|";
                //  if (conduty.isOn)
                //  {
                str1 += "1,0,0,1,1,1,1,1"; 
                NetManager.My.ConfirmDuty("1,0,0,1,1,1,1,1");
                //    }
                //    else
                //    {
                //        str1 += "0,1,1,0,0,0,0,0"; 
                //        NetManager.My.ConfirmDuty( "0,1,1,0,0,0,0,0");

                //    }  
                PlayerData.My.server.SendToClientMsg(str1); 
            }

        });
        subButton.onClick.AddListener(() =>
        {
            if (PlayerData.My.isServer)
            {
                string str1 = "ConfirmDuty|";
                //  if (conduty.isOn)
                //  {
              
                //    }
                //    else
                //    {
                   str1 += "0,1,1,0,0,0,0,0"; 
                   NetManager.My.ConfirmDuty( "0,1,1,0,0,0,0,0"); 
                   PlayerData.My.server.SendToClientMsg(str1);

                //    } 
            }

        });
        
        close.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        MissionConfirm.My.InitName();
        InitReady();
    }
}