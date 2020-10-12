using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class NetManager
{

    public delegate void msgAction(string str);

    public static Dictionary<string, msgAction> listeners;

    public static void OnLoadScene(string str)
    {
        Debug.Log(str);
        SceneManager.LoadScene(str);
    }

    public static void Receivemsg(string str)
    {
        string methodName = str.Split('|')[0];
        if (listeners.ContainsKey(methodName))
        {
            Debug.Log(methodName);
            string args = str.Split('|')[1];
            listeners[methodName](args);
        }
    }

    public static string GetIP(ADDRESSFAM Addfam)
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

    public static void Init()
    {
        Debug.Log("construct");
        listeners = new Dictionary<string, msgAction>();
        listeners.Add("LoadScene", OnLoadScene);
    }

}
public enum ADDRESSFAM
{
    IPv4, IPv6
}

