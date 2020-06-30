using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        //Test();
    }

    private void Login()
    {
        username = username_Input.text.Replace(" ", "");
        password = password_Input.text.Replace(" ", "");

        if (username.Replace(" ", "") == "" || password.Replace(" ", "")=="")
        {
            Debug.LogWarning("用户名或密码不能为空!");
            HttpManager.My.ShowTip("用户名或密码不能为空!");
            username_Input.ActivateInputField();
            username_Input.MoveTextEnd(true);
        }
        else
        {
            NetworkMgr.My.Login(username, password, LoginSuccess, LoginFail);
        }
    }

    private void LoginSuccess()
    {
        SavePasswordOrNot();
        //NetworkMgr.My.loginRecordID = NetworkMgr.My.playerDatas.loginRecordID;
        //NetworkMgr.My.playerID = NetworkMgr.My.playerDatas.playerID;
        // TODO
        if (NetworkMgr.My.playerDatas.status == 0)
        {
            // 创建用户信息
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
    }

    private void LoginFail()
    {
        // TODO
        // 清除错误密码
        password_Input.text = "";
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
        HttpManager.My.ShowTip("用户名或密码错误，请重新输入!");
        username_Input.ActivateInputField();
        username_Input.MoveTextEnd(true);
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

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Login();
            }
        //}
    }

    private void Test()
    {
        PlayerReplay operations = new PlayerReplay();
        List<Operation> operation = new List<Operation>();

        operation.Add(new Operation(10, OpType.ChangeRole, new List<string>() { "12", "22" }));
        operation.Add(new Operation(12, OpType.ChangeTrade, new List<string>() { "12", "22" }));
        operation.Add(new Operation(14, OpType.CreateTrade, new List<string>() { "12", "22" }));
        operation.Add(new Operation(16, OpType.DeleteRole, new List<string>() { "12", "22" }));

        operations.operations = operation;
        operations.sceneName = "FTE_1";

        string json = JsonUtility.ToJson(operations);
        NetworkMgr.My.TestPost(json);

        Debug.Log(json);

        //PlayerReplay op = JsonUtility.FromJson<PlayerReplay>(json);
        //Debug.Log( op.operations[0].type.GetType());
        //Debug.Log( op.operations[0].operationParam.Count);
    }
}
