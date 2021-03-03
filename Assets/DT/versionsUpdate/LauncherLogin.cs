﻿using System.Collections;
using System.Collections.Generic;
using MHLab.Patch.Launcher.Scripts;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DG.Tweening;
using Google.Protobuf.WellKnownTypes;
using UnityEditor;
using Debug = UnityEngine.Debug;

public class LauncherLogin : MonoBehaviour
{
    public InputField username_Input;
    public InputField password_Input;

    public Launcher launcher;
    public Button login;

    public GameObject logo;

    public List<GameObject> fadeManager;

    public List<GameObject> fadeManagerText;

    // Start is called before the first frame update
    void Start()
    {
        login.onClick.AddListener(() => { Login(); });
        StartCoroutine(LoadAccount(() => { Login(); }, () => { ShowLogo(); }));
    }

    public void ShowLogo()
    {
        logo.transform.localScale = Vector3.zero;
        logo.transform.DOScale(1, 1).SetEase(Ease.InCirc).OnComplete(() =>
        {
            logo.transform.DOLocalMoveX(-180, 0.8f);
            for (int i = 0; i < fadeManager.Count; i++)
            {
                fadeManager[i].GetComponent<Image>().DOFade(1, 0.8f);
            }

            for (int i = 2; i < fadeManagerText.Count; i++)
            {
                fadeManagerText[i].GetComponent<Text>().DOFade(1, 0.8f);
            }

            for (int i = 0; i < 2; i++)
            {
                fadeManagerText[i].GetComponent<Text>().DOFade(0.4f, 0.8f);
            }
        });
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username_Input.isFocused)
            {
                password_Input.ActivateInputField();
                password_Input.MoveTextEnd(true);
            }
        }
    }

    /// <summary>
    /// 点击登陆
    /// </summary>
    public void Login()
    {
        string username = username_Input.text.Replace(" ", "");
        string password = password_Input.text.Replace(" ", "");
        if (username.Equals("") || password.Equals(""))
        {
            HttpManager.My.ShowTip("用户名或密码不能为空!");
            return;
        }

        GetVersion((w) => { });

        Login(username, password, () =>
        {
            GetVersion((w)=>{
            });
            //Debug.Log(TimeStamp.GetCurrentTimeStamp());
            GetVersion((str) =>
            {
                
                /*StartCoroutine(LoadVersionsIndex((index) =>
                {
                    if (int.Parse(str.Split('.')[0]) > int.Parse(index))
                    {
                        Delete();
                    }
                    UpdateGame();
                }));*/
                VerifyVersion(str);
                SaveAccount(username, password);

            }); 
        });
    }

    private void VerifyVersion(string str)
    {
        StartCoroutine(LoadVersionsIndex((index) =>
        {
            if (int.Parse(str.Split('.')[0]) > int.Parse(index))
            {
                Delete();
            }
            UpdateGame();
        }));
        //SaveAccount(username, password);
    }


    /// <summary>
    /// 登陆
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    private void Login(string userName, string password, Action doSuccess = null, Action<bool, string> doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("username", userName);
        keyValues.Add("password", password);
        keyValues.Add("DeviceId", Application.identifier);

        StartCoroutine(HttpManager.My.HttpSend(Url.NewLoginUrl, (www) =>
        {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip("账号或密码错误，登陆失败！");
                if (response.data.Contains("密码"))
                {
                    doFail?.Invoke(true, response.data);
                }
                else
                {
                    doFail?.Invoke(false, response.data);
                }
            }
            else
            {
                Debug.Log(response.data);
                try
                {
                    PlayerDatas playerDatas = JsonUtility.FromJson<PlayerDatas>(response.data);
                    if (playerDatas.isOutDate)
                    {
                        HttpManager.My.ShowTip("账号已失效");
                        return;
                    }

                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
        }, keyValues, HttpType.Post, 10001));
    }

    private string version = "";

    /// <summary>
    /// 获取版本号
    /// </summary>
    /// <param name="doEnd"></param>
    void GetVersion(Action<string> doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();

        StartCoroutine(HttpManager.My.HttpSend(Url.GetVersion, (www) =>
        {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            Debug.Log(response.data);
            try
            {
                if (String.IsNullOrEmpty(response.data))
                {
                    HttpManager.My.ShowTip("获取不到服务器");
                    return;
                } 
                    version = response.data;
                    doEnd?.Invoke(version);
          
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }, keyValues, HttpType.Get, 10002));
    }


    public IEnumerator LoadAccount(Action canLogin, Action cantLogin)
    {
        if (!File.Exists(Application.dataPath + "Account.json"))
        {
            cantLogin();
        }
        else
        {
            StreamReader streamReader = new StreamReader(Application.dataPath + "Account.json");
            if (streamReader != null)
            {
                string str = streamReader.ReadToEnd();
                while (string.IsNullOrEmpty(str))
                {
                    yield return null;
                }

                string decode = Decrypt(str);
                AccountJosn json = JsonUtility.FromJson<AccountJosn>(decode);
                if (!string.IsNullOrEmpty(json.name))
                {
                    Debug.Log(json.name+"名字"+json.password+"密码");
                    InitInput(json);
                    canLogin();
                }
                else
                {
                    cantLogin();
                }
            }
            else
            {
                cantLogin();
            }
        }
    }

    public void Delete()
    {
        string fullPath = Application.dataPath + "/Game";
        FileUtil.DeleteFileOrDirectory(fullPath);
    }

    public IEnumerator LoadVersionsIndex(Action<string> doEnd)
    {
        Debug.Log("获取本地");
        if (!File.Exists(Application.dataPath + "Build.json"))
        {
            doEnd("0");
        }
        else
        {
            StreamReader streamReader = new StreamReader(Application.dataPath + "Build.json");
            if (streamReader == null)
            {
                doEnd("0");
            }
            else
            {
                string str = streamReader.ReadToEnd();
                while (string.IsNullOrEmpty(str))
                {
                    yield return null;
                }

                BuildJson json = JsonUtility.FromJson<BuildJson>(str);
                doEnd(json.versionsIndex);
            }
        }
    }

    /// <summary>
    /// 初始化用户名
    /// </summary>
    public void InitInput(AccountJosn json)
    {
        if (json != null)
        {
            username_Input.text = json.name;
            password_Input.text = json.password;
        }
        else
        {
            username_Input.text = "";
            password_Input.text = "";
        }
    }

    /// <summary>
    /// 保存账号密码
    /// </summary>
    public void SaveAccount(string name, string password)
    {
        AccountJosn account = new AccountJosn()
        {
            name = name,
            password = password
        };
        string accoutjson = JsonUtility.ToJson(account);
        string encode = Encrypt(accoutjson);
        FileStream file = new FileStream(Application.dataPath + "Account.json", FileMode.Create);
        byte[] bts = System.Text.Encoding.UTF8.GetBytes(encode);
        file.Write(bts, 0, bts.Length);
        if (file != null)
        {
            file.Close();
        }

        ///保存账号密码
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void UpdateGame()
    {
        launcher.Init();
    }

    /// <summary>
    /// 加密  返回加密后的结果
    /// </summary>
    /// <param name="toE">需要加密的数据内容</param>
    /// <returns></returns>
    private static string Encrypt(string toE)
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789012");
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateEncryptor();

        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    /// <summary>
    /// 解密  返回解密后的结果
    /// </summary>
    /// <param name="toD">加密的数据内容</param>
    /// <returns></returns>
    private static string Decrypt(string toD)
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789012");

        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateDecryptor();

        byte[] toEncryptArray = Convert.FromBase64String(toD);
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return UTF8Encoding.UTF8.GetString(resultArray);
    }
}