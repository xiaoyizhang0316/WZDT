using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static StageGoal;
using System.Text.RegularExpressions;

public class LoginPanel : MonoBehaviour
{
    public Transform playerInfoPanel;
    public Transform threeWordsPanel;
    public GameObject setServerPanel;

    public InputField username_Input;
    public InputField password_Input;
    public InputField server_Input;
    public Text server_InputText;

    public Toggle savePassword_Toggle;
    public Toggle userServer_Toggle;

    public Button login_Btn;
    public Button serverConfirm;
    public Button serverCancel;

    public GameObject notice;

    string username = "";
    string password = "";
    string serverIP = "";
    string regx = "^(\\d{1,3}\\.){3}\\d{1,3}$";

    bool isLogin = false;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        login_Btn.onClick.AddListener(Login);
        username = PlayerPrefs.GetString("username", "0");
        password = PlayerPrefs.GetString("password", "0");
        serverIP = PlayerPrefs.GetString("server_IP", "0");
        UserServerOrNot();
        if (username != "0")
        {
            username_Input.text = username;
        }

        if(password != "0")
        {
            password_Input.text = password;
        }
        if (PlayerPrefs.GetInt("savePassword", 0) == 1)
        {
            savePassword_Toggle.isOn = true;
        }

        //username_Input.ActivateInputField();
        //username_Input.MoveTextEnd(true);
        Test();
    }

    private void UserServerOrNot()
    {
        server_Input.onValueChanged.AddListener((ip) => InputServerIP(ip));
        server_Input.onEndEdit.AddListener((ip) => InputServerEnd(ip));
        serverConfirm.onClick.AddListener(ServerConfirm);
        serverCancel.onClick.AddListener(ServerCancel);
        if (PlayerPrefs.GetInt("useServer", 0) == 1)
        {
            userServer_Toggle.isOn = true;
            //Url.SetIp(serverIP);
            NetworkMgr.My.PingTest(serverIP, () => { Debug.Log("有效的服务器地址"); }, () => {
                HttpManager.My.ShowClickTip("当前使用的自定义的服务器无效，请重新输入正确的IP地址！", ()=> {
                    setServerPanel.SetActive(true);
                });
            });
        }
        userServer_Toggle.onValueChanged.AddListener((isOn) => ServerToggleChange(isOn));
    }

    private void Login()
    {
        isLogin = true;
        username = username_Input.text.Replace(" ", "");
        password = password_Input.text.Replace(" ", "");

        if (username.Replace(" ", "") == "" || password.Replace(" ", "")=="")
        {
            Debug.LogWarning("用户名或密码不能为空!");
            HttpManager.My.ShowTip("用户名或密码不能为空!");
            username_Input.ActivateInputField();
            username_Input.MoveTextEnd(true);
            isLogin = false;
        }
        else
        {
            ForLogin();
        }
    }

    private void ForLogin()
    {
        NetworkMgr.My.Login(username, password, LoginSuccess, LoginFail);
    }

    private void LoginSuccess()
    {
        SavePasswordOrNot();
        UseServerOrNot();
        if (NetworkMgr.My.playerDatas.status == 0)
        {
            // 创建用户信息
            // TODO
            HttpManager.My.ShowTip("创建用户信息");
        }
        else
        {
            //if (NetworkMgr.My.playerDatas.playerIcon.Equals("0"))
            //{
            //    //TODO 创建个人信息
            //}else 
            if (NetworkMgr.My.playerDatas.threeWordsProgress == 0)
            {
                // 第一个问题
                threeWordsPanel.GetComponent<ThreeWordsPanel>().SetQuesion(Questions.questions[NetworkMgr.My.playerDatas.threeWordsProgress]);
                threeWordsPanel.GetChild(0).gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                if (NetworkMgr.My.playerDatas.fteProgress==0)
                {
                    SceneManager.LoadScene("FTE_0");
                }
                else
                {
                    //NetworkMgr.My.GetLevelProgress();
                    //NetworkMgr.My.GetPlayerEquips();
                    //NetworkMgr.My.GetAnswers();
                    SceneManager.LoadScene("Map");
                }
            }
        }
        isLogin = false;
    }

    private void LoginFail()
    {
        password_Input.text = "";
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
        HttpManager.My.ShowTip("用户名或密码错误，请重新输入!");
        username_Input.ActivateInputField();
        username_Input.MoveTextEnd(true);
        isLogin = false;
    }

    private void SavePasswordOrNot()
    {
        PlayerPrefs.SetString("username", username);
        if (savePassword_Toggle.isOn)
        {
            PlayerPrefs.SetString("password", password);
            PlayerPrefs.SetInt("savePassword", 1);
        }
        else
        {
            PlayerPrefs.SetString("password", "0");
            PlayerPrefs.SetInt("savePassword", 0);
        }
    }

    private void UseServerOrNot()
    {
        if (userServer_Toggle.isOn)
        {
            PlayerPrefs.SetInt("useServer", 1);
            PlayerPrefs.SetString("server_IP", serverIP);
        }
        else
        {
            PlayerPrefs.SetInt("useServer", 0);
            PlayerPrefs.SetString("server_IP", "0");
        }
    }

    private void Update()
    {
        //if (Input.anyKeyDown)
        //{
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (username_Input.isFocused)
                {
                    password_Input.ActivateInputField();
                    password_Input.MoveTextEnd(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.Return) && !isLogin)
            {
                if (setServerPanel.activeInHierarchy)
                {
                    if(server_InputText.color == Color.green)
                    {
                        ServerConfirm();
                    }
                }
                else
                {
                    Login();
                }
            }
        //}
    }

    private void ServerToggleChange(bool isOn)
    {
        if (isOn)
        {
            setServerPanel.SetActive(true);
        }
        else
        {
            Url.SetIp(null);
        }
    }

    private void InputServerIP(string ip)
    {
        //string ip = server_Input.text.Trim();
        if(!Regex.IsMatch(ip.Trim(), regx))
        {
            server_InputText.color = Color.red;
            notice.SetActive(true);
            serverConfirm.interactable = false;
        }
        else
        {
            server_InputText.color = Color.green;
            notice.SetActive(false);
            serverConfirm.interactable = true;
        }
    }

    private void InputServerEnd(string ip)
    {
        if (!Regex.IsMatch(ip.Trim(), regx))
        {
            server_InputText.color = Color.red;
            notice.SetActive(true);
            serverConfirm.interactable = false;
        }
        else
        {
            server_InputText.color = Color.green;
            notice.SetActive(false);
            serverConfirm.interactable = true;
        }
    }

    private void ServerConfirm()
    {
        serverIP = server_Input.text.Trim();
        NetworkMgr.My.PingTest(serverIP, () => {
            setServerPanel.SetActive(false);
        }, ()=> {
            HttpManager.My.ShowClickTip("无效的服务器地址，请重新输入正确的IP地址！");
        });
    }

    private void ServerCancel()
    {
        userServer_Toggle.isOn = false;
        Url.SetIp(null);
        setServerPanel.SetActive(false);
    }

    private void Test()
    {
        //TestGetReplayDatas();
        //TestLogin();
        //string json = "{\"playerEquips\":[{\"playerID\":\"999999\",\"equipType\":0,\"equipID\":22202,\"count\":1},{\"playerID\":\"999999\",\"equipType\":1,\"equipID\":10001,\"count\":1},{\"playerID\":\"999999\",\"equipType\":0,\"equipID\":22202,\"count\":1},{\"playerID\":\"999999\",\"equipType\":1,\"equipID\":10001,\"count\":1}]}";
        //Debug.Log(json);
        //json.Replace("[{","\"[{");
        //json.Replace("}]", "}]\"");
        //Debug.Log(json);
        //PlayerEquips pes = JsonUtility.FromJson<PlayerEquips>(json);
        //Debug.Log(pes.playerEquips.Count);
        //try
        //{
        //    ParseEquips pe = JsonUtility.FromJson<ParseEquips>(json);
        //    Debug.Log(pe.playerEquips.ToString());
        //}
        //catch (System.Exception ex)
        //{
        //    Debug.Log(ex.Message);
        //}
        //string ip = "192.168.1.32";
        //NetworkMgr.My.PintTest(ip,()=> {
        //    Debug.Log("pass");
        //});
        //string ip = "111.1.0.1t";
        //if(Regex.IsMatch(ip, regx))
        //{
        //    Debug.Log(true);
        //}
    }

    private void TestGetReplayDatas()
    {
        NetworkMgr.My.GetReplayDatas("B9izmneslOKhE14zpk4", null, null);
    }

    private void TestGetReplayList()
    {
        NetworkMgr.My.GetReplayLists("FTE_1",null, null);
    }

    private void TestUploadReplaydDatas()
    {
        List<PlayerOperation> playerOperations = new List<PlayerOperation>();
        List<DataStat> dataStats = new List<DataStat>();

        PlayerOperation pa = new PlayerOperation();

        DataStat dataStat;

        pa.operateTime = 0;
        pa.type = GameEnum.OperationType.ChangeRole;
        pa.operationParam = new List<string>() { "12", "13", "14" };
        dataStat = new DataStat(10, 10, 10, 10, 1000, 10, 10, 10);
        playerOperations.Add(pa);
        dataStats.Add(dataStat);

        pa.operateTime = 2;
        pa.type = GameEnum.OperationType.ChangeTrade;
        pa.operationParam = new List<string>() { "12", "13", "14" };
        dataStat = new DataStat(12, 10, 10, 10, 10000, 10, 10, 10);
        playerOperations.Add(pa);
        dataStats.Add(dataStat);

        pa.operateTime = 4;
        pa.type = GameEnum.OperationType.CreateTrade;
        pa.operationParam = new List<string>() { "12", "13", "14" };
        dataStat = new DataStat(14, 10, 10, 10, 1000, 10, 10, 10);
        playerOperations.Add(pa);
        dataStats.Add(dataStat);

        pa.operateTime = 6;
        pa.type = GameEnum.OperationType.DeleteRole;
        pa.operationParam = new List<string>() { "12", "13", "14" };
        dataStat = new DataStat(16, 10, 10, 10, 100000, 10, 10, 10);
        playerOperations.Add(pa);
        dataStats.Add(dataStat);

        pa.operateTime = 8;
        pa.type = GameEnum.OperationType.DeleteTrade;
        pa.operationParam = new List<string>() { "12", "13", "14" };
        dataStat = new DataStat(18, 10, 10, 10, 100000, 10, 10, 10);
        playerOperations.Add(pa);
        dataStats.Add(dataStat);

        //PlayerReplay playerReplay = new PlayerReplay();
        //Debug.Log(dataStats.Count);
        //playerReplay.dataStats = dataStats;
        //playerReplay.operations = playerOperations;
        //playerReplay.sceneName = "FTE_1";
        //playerReplay.score = 100;
        //playerReplay.stars = "101";
        //playerReplay.timeCount = 100;
        //playerReplay.win = true;

        //NetworkMgr.My.AddReplayData(playerReplay, null, null);
    }
}
