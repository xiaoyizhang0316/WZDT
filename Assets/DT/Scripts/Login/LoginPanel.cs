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
    }

    private void Login()
    {
        username = username_Input.text.Replace(" ", "");
        password = password_Input.text.Replace(" ", "");

        if (username.Replace(" ", "") == "" || password.Replace(" ", "")=="")
        {
            Debug.LogWarning("用户名或密码不能为空!");
            HttpManager.My.ShowTip("用户名或密码不能为空!");
        }
        else
        {
            NetworkMgr.My.Login(username, password, LoginSuccess, LoginFail);
        }
    }

    private void LoginSuccess()
    {
        SavePasswordOrNot();
        NetworkMgr.My.loginRecordID = NetworkMgr.My.playerDatas.loginRecordID;
        NetworkMgr.My.playerID = NetworkMgr.My.playerDatas.playerID;
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
                threeWordsPanel.gameObject.SetActive(true);
            }
            else
            {
                if (NetworkMgr.My.playerDatas.fteProgress==0)
                {
                    SceneManager.LoadScene("FTE_0");
                }
                else
                {
                    SceneManager.LoadScene("Map");
                }
            }
        }
    }

    private void LoginFail()
    {
        // TODO
        // 清楚错误密码
        password_Input.text = "";
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
        HttpManager.My.ShowTip("用户名或密码错误，请重新输入!");
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
}
