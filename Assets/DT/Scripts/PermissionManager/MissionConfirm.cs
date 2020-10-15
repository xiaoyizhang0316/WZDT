﻿using System.Collections;
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

// Start is called before the first frame update
    void Start()
    {
        GetUserName();
        InitName();
        start.onClick.AddListener(() => { LevelInfoManager.My.loadScene(); });
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
                PlayerData.My.server.SendToClientMsg("UpdateDutyUI|1");
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
                   PlayerData.My.server.SendToClientMsg("UpdateDutyUI|1"); 
                   PlayerData.My.server.SendToClientMsg(str1);

                //    } 
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        MissionConfirm.My.InitName();
    }
}