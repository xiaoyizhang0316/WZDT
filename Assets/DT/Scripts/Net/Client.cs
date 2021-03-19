using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    private string staInfo = "NULL";             //状态信息
    private string inputIp = "192.168.1.22";   //输入ip地址
    private string inputPort = "6000";           //输入端口号
    public string inputMes = "NULL";             //发送的消息
    private int recTimes = 0;                    //接收到信息的次数
    private string recMes = "NULL";              //接收到的消息
    private Socket socketSend;                   //客户端套接字，用来链接远端服务器
    private bool clickSend = false;              //是否点击发送按钮

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //建立链接
    public string ConnectToServer(string ipAddr, int portNum)
    {
        try
        {
            PlayerData.My.isSOLO = false; 

            //int _port = Convert.ToInt32(inputPort);             //获取端口号
            //string _ip = inputIp;                               //获取ip地址

            //创建客户端Socket，获得远程ip和端口号
            socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(ipAddr);
            IPEndPoint point = new IPEndPoint(ip, portNum);

            socketSend.Connect(point);
            //Debug.Log("连接成功 , " + " ip = " + ip + " port = " + portNum);
            staInfo = ip + ":" + portNum + "  连接成功";
            Thread r_thread = new Thread(Received);             //开启新的线程，不停的接收服务器发来的消息
            r_thread.IsBackground = true;
            r_thread.Start();
            PlayerData.My.playerDutyID = 1;
            Thread s_thread = new Thread(SendMessage);          //开启新的线程，不停的给服务器发送消息
            s_thread.IsBackground = true;
            s_thread.Start();
            PlayerData.My.client = this;
            PlayerData.My.isServer = false;
            string str = "SetUpGroup|";
            string playerDatas = JsonUtility.ToJson(NetworkMgr.My.playerDatas).ToString();
            str += playerDatas;
            str += "%" + NetworkMgr.My.levelProgressList.Count;
            ///发送队员的信息  
            SendToServerMsg(str);
            return "true";
        }
        catch (Exception)
        {
            Debug.Log("IP或者端口号错误......");
            staInfo = "IP或者端口号错误......";
            return staInfo;
        }
    }

    private Stack<char> msg = new Stack<char>();

    private bool isStart = false;

    private string orderMsg = "";

    /// <summary>
    /// 接收服务端返回的消息
    /// </summary>
    void Received()
    {
        while (true)
        {
            try
            {
                byte[] buffer = new byte[1024 * 6];
                //实际接收到的有效字节数
                int len = socketSend.Receive(buffer);
                if (len == 0)
                {
                    break;
                }
                recMes = Encoding.UTF8.GetString(buffer, 0, len);
                char[] tempList = recMes.ToCharArray();
                for (int i = tempList.Length - 1; i >= 0; i--)
                {
                    msg.Push(tempList[i]);
                }
                while (msg.Count > 0)
                {
                    if (msg.Peek() == '(')
                    {
                        msg.Pop();
                        isStart = true;
                    }
                    else if (msg.Peek() == ')')
                    {
                        msg.Pop();
                        isStart = false;
                        NetManager.My.Receivemsg(orderMsg);
                        orderMsg = "";
                    }
                    else if (isStart)
                    {
                        orderMsg += msg.Pop();
                    }
                }
                //Debug.Log("客户端接收到的数据 ： " + recMes);
                
                recTimes ++;
                staInfo = "接收到一次数据，接收次数为 ：" + recTimes;
                //Debug.Log("接收次数为：" + recTimes);
            }
            catch { }
        }
    }

    /// <summary>
    /// 向服务器发送消息
    /// </summary>
    /// <param name="str"></param>
    public void SendToServerMsg(string str)
    {
        inputMes = str;
        clickSend = true;
    }

    /// <summary>
    /// 向服务器发送消息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void SendMessage()
    {
        try
        {
            while (true)
            {
                if (clickSend)                              //如果点击了发送按钮
                {
                    clickSend = false;
                    string msg = inputMes;
                    byte[] buffer = new byte[1024 * 6];
                    buffer = Encoding.UTF8.GetBytes("(" + msg + ")");
                    socketSend.Send(buffer);
                    //Debug.Log("发送的数据为：" + msg);
                }
            }
        }
        catch { }
    }


    private void OnDisable()
    {
        //Debug.Log("begin OnDisable()");

        string str = "GroupDismiss|1";
        SendToServerMsg(str);

        if (socketSend.Connected)
        {
            try
            {
                socketSend.Shutdown(SocketShutdown.Both);    //禁用Socket的发送和接收功能
                socketSend.Close();                          //关闭Socket连接并释放所有相关资源
                PlayerData.My.isSOLO = true;
                PlayerData.My.client = null;
            }
            catch (Exception e)
            {
                print(e.Message);
            }
        }
        //Debug.Log("end OnDisable()");
    }

    public void Disconect()
    {
        if (socketSend.Connected)
        {
            try
            {
                socketSend.Shutdown(SocketShutdown.Both);    //禁用Socket的发送和接收功能
                socketSend.Close();                          //关闭Socket连接并释放所有相关资源
                PlayerData.My.isSOLO = true;
                PlayerData.My.client = null;
            }
            catch (Exception e)
            {
                print(e.Message);
            }
        }
    }

    //用户界面
    void OnGUI()
    {
        //GUI.color = Color.white;

        //GUI.Label(new Rect(650, 10, 60, 20), "状态信息");

        //GUI.Label(new Rect(710, 10, 80, 60), staInfo);

        //GUI.Label(new Rect(650, 70, 50, 20), "服务器ip地址");

        //inputIp = GUI.TextField(new Rect(710, 70, 100, 20), inputIp, 20);

        //GUI.Label(new Rect(650, 110, 50, 20), "服务器端口");

        //inputPort = GUI.TextField(new Rect(800, 110, 100, 20), inputPort, 20);

        //GUI.Label(new Rect(650, 150, 80, 20), "接收到消息：");

        //GUI.Label(new Rect(800, 150, 80, 20), recMes);

        //GUI.Label(new Rect(650, 190, 80, 20), "发送的消息：");

        //inputMes = GUI.TextField(new Rect(850, 190, 100, 20), inputMes, 20);

        //if (GUI.Button(new Rect(650, 230, 60, 20), "开始连接"))
        //{
        //    //ConnectToServer();
        //}

        //if (GUI.Button(new Rect(650, 270, 60, 20), "发送信息"))
        //{
        //    clickSend = true;
        //}
    }
}