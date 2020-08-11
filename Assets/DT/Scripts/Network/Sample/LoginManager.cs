using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class LoginManager : MonoBehaviour
{
    public Transform loginPanel;
    public Transform registerPanel;
    public Transform loginPassPanel;

    public InputField loginUsername;
    public InputField loginPassword;
    public InputField registerUsername;
    public InputField registerPassword;
    public InputField confirmPassword;
    public InputField keycode;

    public Button loginButton;
    public Button registerButton;
    public Button login;
    public Button register;

    public Text tipText;

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(GotoLogin);
        registerButton.onClick.AddListener(GotoRegister);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GotoLogin()
    {
        loginPanel.gameObject.SetActive(true);
        login.onClick.RemoveAllListeners();
        login.onClick.AddListener(Login);
    }

    private void GotoRegister()
    {
        registerPanel.gameObject.SetActive(true);
        register.onClick.RemoveAllListeners();
        register.onClick.AddListener(Register);
    }

    public void Login()
    {
        string name = loginUsername.text;
        string password = loginPassword.text;

        if(name.Replace(" ", "").Equals("")||password.Replace(" ", "").Equals(""))
        {
            Debug.LogError("用户名或密码不能为空！");
            SetTipText("用户名或密码不能为空!");
            return;
        }
        else
        {
            SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
            keyValues.Add("username", name);
            keyValues.Add("password", password);
            //Debug.LogError(HttpManager.My);
            StartCoroutine(HttpManager.My.HttpSend(Url.LoginUrl, (www) => {
                ResponseTest responseTest = JsonUtility.FromJson<ResponseTest>(www.downloadHandler.text);
                Debug.Log(responseTest.status);
                if (responseTest.status == 0)
                {
                    SetTipText("用户名或密码错误！");
                    return;
                }
                else if(responseTest.status == 1)
                {
                    SetTipText("登录成功！");
                    loginPassPanel.gameObject.SetActive(true);
                    loginPanel.gameObject.SetActive(false);
                }
            }, keyValues, HttpType.Post));
        }
    }

    public void Register()
    {
        if(registerUsername.text.Replace(" ", "").Equals("")|| 
            registerPassword.text.Replace(" ", "").Equals("")||
            confirmPassword.text.Replace(" ", "").Equals("")||
            keycode.text.Replace(" ", "").Equals(""))
        {
            Debug.LogError("用户名或密码不能为空!");
            SetTipText("用户名或密码或激活码不能为空!");
            return;
        }
        else
        {
            if (registerPassword.text.Equals(confirmPassword.text))
            {
                SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
                keyValues.Add("username", registerUsername.text);
                keyValues.Add("password", registerPassword.text);
                keyValues.Add("keycode", keycode.text);
                StartCoroutine(HttpManager.My.HttpSend(Url.RegisterUrl, (www)=>{
                    ResponseJson rj = JsonUtility.FromJson<ResponseJson>(www.downloadHandler.text);
                    if(rj.status == 1)
                    {
                        SetTipText("注册成功，请继续登录！");
                        GotoLogin();
                        registerPanel.gameObject.SetActive(false);
                    }else
                    {
                        SetTipText(rj.data);
                        return;
                    }
                }, keyValues, HttpType.Post));
                
            }
            else
            {
                Debug.LogError("密码输入不匹配!");
                SetTipText("密码输入不匹配!");
                return;
            }
        }
    }

    void SetTipText(string tip)
    {
        tipText.text = tip;
        if (tipText.gameObject.activeInHierarchy)
        {
            //int dk = DOTween.KillAll();
            DOTween.Kill("tip");
            //Debug.LogError("stop "+dk);
            tipText.DOFade(1, 0.02f);
        }
        tipText.gameObject.SetActive(true);
        tipText.DOFade(0, 2).SetId("tip").OnComplete(()=> {
            tipText.gameObject.SetActive(false);
            tipText.DOFade(1, 0.02f);
        });
    }
}

[Serializable]
public class ResponseTest
{
    public int status;
    public string response;
}