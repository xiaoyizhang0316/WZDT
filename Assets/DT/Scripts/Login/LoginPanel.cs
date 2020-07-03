using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static StageGoal;

public class LoginPanel : MonoBehaviour
{
    public Transform playerInfoPanel;
    public Transform threeWordsPanel;

    public InputField username_Input;
    public InputField password_Input;

    public Toggle savePassword_Toggle;

    public Button login_Btn;

    string username = "";
    string password = "";

    bool isLogin = false;

    // Start is called before the first frame update
    void Start()
    {
        login_Btn.onClick.AddListener(Login);
        username = PlayerPrefs.GetString("username", "0");
        password = PlayerPrefs.GetString("password", "0");
        if (username != "0")
        {
            username_Input.text = username;
        }

        if(password != "0")
        {
            password_Input.text = password;
        }
        username_Input.ActivateInputField();
        username_Input.MoveTextEnd(true);
        Test();
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
            NetworkMgr.My.Login(username, password, LoginSuccess, LoginFail);
        }
    }

    private void LoginSuccess()
    {
        SavePasswordOrNot();
        
        if (NetworkMgr.My.playerDatas.status == 0)
        {
            // 创建用户信息
            // TODO
            HttpManager.My.ShowTip("创建用户信息");
        }
        else
        {
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
                    NetworkMgr.My.GetLevelProgress();
                    NetworkMgr.My.GetPlayerEquips();
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
        }
        else
        {
            PlayerPrefs.SetString("password", "0");
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
                Login();
            }
        //}
    }

    private void Test()
    {
        //TestGetReplayDatas();
        
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
