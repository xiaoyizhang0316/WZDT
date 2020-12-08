using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminLogin : MonoBehaviour
{
    public InputField username_input;
    public InputField password_input;

    public Button login_btn;

    string username = "";
    string password = "";
    // Start is called before the first frame update
    void Start()
    {
        username = PlayerPrefs.GetString("adminUsername", "");
        password = PlayerPrefs.GetString("adminPassword", "");
        username_input.text = username;
        password_input.text = password;
        login_btn.onClick.AddListener(Login);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Login()
    {
        username = username_input.text.Replace(" ", "");
        password = password_input.text.Replace(" ", "");

        if(username.Equals("")||password.Equals(""))
        {
            HttpManager.My.ShowTip("账户或用户名不能为空");
            return;
        }
        else
        {
            AdminManager.My.Login(username, password, ()=> {
                PlayerPrefs.SetString("adminUsername", username);
                PlayerPrefs.SetString("adminPassword", password);

                if (AdminManager.My.playerDatas.levelID == 12)
                {
                    AdminManager.My.ShowGroupInTeacherPrivilage();
                }else if(AdminManager.My.playerDatas.levelID == 13)
                {
                    AdminManager.My.ShowAdmin();
                }
                else
                {
                    HttpManager.My.ShowTip("无登录权限！");
                }
            }, ()=> {
                password_input.text = "";
            });
        }
    }
}
