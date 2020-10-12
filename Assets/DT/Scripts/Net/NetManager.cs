using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameEnum;

public class NetManager : MonoSingleton<NetManager>
{
    struct NoDelayedQueueItem
    {
        public Action<string> action;
        public string param;
    }

    List<NoDelayedQueueItem> listNoDelayActions = new List<NoDelayedQueueItem>();

    public Action<string> msgAction;

    public Dictionary<string, Action<string>> listeners;

    public void OnLoadScene(string str)
    {
        loadSceneName = str;
        SceneManager.LoadScene(str);
    }

    public void OnCreateRole(string str)
    {
        string[] args = str.Split(',');
        RoleType type = (RoleType)Enum.Parse(typeof(RoleType), args[0]);
        Role tempRole = new Role();
        int x = int.Parse(args[3]);
        int y = int.Parse(args[4]);
        tempRole.baseRoleData = GameDataMgr.My.GetModelData(type, 1);
        tempRole.ID = double.Parse(args[2]);
        tempRole.baseRoleData.roleName = args[1];

        GameObject role = Instantiate(Resources.Load<GameObject>("Prefabs/Role/" + args[0] + "_1"), NewCanvasUI.My.RoleTF.transform);
        role.name = tempRole.ID.ToString();
        role.GetComponent<BaseMapRole>().baseRoleData = new Role();
        role.GetComponent<BaseMapRole>().baseRoleData = tempRole;
        role.transform.position = MapManager.My.GetMapSignByXY(x, y).transform.position + new Vector3(0f, 0.3f, 0f);
        PlayerData.My.RoleData.Add(role.GetComponent<BaseMapRole>().baseRoleData);
        PlayerData.My.MapRole.Add(role.GetComponent<BaseMapRole>());
        MapManager.My.SetLand(x, y, role.GetComponent<BaseMapRole>());
        role.GetComponent<BaseMapRole>().baseRoleData.inMap = true;
    }

    public void OnChangeTimeScale(string str)
    {
        int select = int.Parse(str);
        switch(select)
        {
            case 0:
                DOTween.PauseAll();
                DOTween.defaultAutoPlay = AutoPlay.None;
                break;
            case 1:
                DOTween.PlayAll();
                DOTween.timeScale = 1f;
                DOTween.defaultAutoPlay = AutoPlay.All;
                break;
            case 2:
                DOTween.PlayAll();
                DOTween.timeScale = 2f;
                DOTween.defaultAutoPlay = AutoPlay.All;
                break;
            default:
                DOTween.PlayAll();
                DOTween.timeScale = 1f;
                DOTween.defaultAutoPlay = AutoPlay.All;
                break;
        }
    }

    public void Receivemsg(string str)
    {
        string methodName = str.Split('|')[0];
        if (listeners.ContainsKey(methodName))
        {
            Debug.Log(methodName);
            string args = str.Split('|')[1];
            //actionActions.Add(listeners[methodName]);
            listNoDelayActions.Add(new NoDelayedQueueItem { action = listeners[methodName], param = args });
        }
    }

    public string GetIP(ADDRESSFAM Addfam)
    {
        //Return null if ADDRESSFAM is Ipv6 but Os does not support it
        if (Addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
        {
            return null;
        }

        string output = "";

        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif 
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    //IPv4
                    if (Addfam == ADDRESSFAM.IPv4)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }

                    //IPv6
                    else if (Addfam == ADDRESSFAM.IPv6)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
        }
        return output;
    }

    public void Init()
    {
        Debug.Log("construct");
        listeners = new Dictionary<string, Action<string>>();
        listeners.Add("LoadScene", OnLoadScene);
        listeners.Add("CreateRole", OnCreateRole);
        listeners.Add("ChangeTimeScale", OnChangeTimeScale);
    }

    public string loadSceneName;

    public bool isLoadScene = false;

    public bool isCreateRole = false;

    private void Update()
    {
        if (listNoDelayActions.Count > 0)
        {
            try
            {
                listNoDelayActions[0].action(listNoDelayActions[0].param);
                listNoDelayActions.RemoveAt(0);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

        }
    }

    //void OnGUI()
    //{
    //    if (GUILayout.Button("load"))
    //    {
    //        listeners["LoadScene"]("FTE_1");
    //    }
    //}
}


public enum ADDRESSFAM
{
    IPv4, IPv6
}

