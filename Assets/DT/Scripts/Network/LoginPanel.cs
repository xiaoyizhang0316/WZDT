using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
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
            NetworkMgr.My.Login(username, password, LoginSuccess, LoginFail, "登录失败: ");
        }
    }

    private void LoginSuccess()
    {
        SavePasswordOrNot();
        // TODO
    }

    private void LoginFail()
    {
        // TODO
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
