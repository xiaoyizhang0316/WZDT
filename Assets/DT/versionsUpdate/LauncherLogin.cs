using System.Collections;
using System.Collections.Generic;
using MHLab.Patch.Launcher.Scripts;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class LauncherLogin : MonoBehaviour
{
    public InputField username_Input;
    public InputField password_Input;

    public Launcher launcher;
    public Button login;

    // Start is called before the first frame update
    void Start()
    {
        login.onClick.AddListener(() => { Login(); });
      LoadAccount();

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
        
        Login(username, password, () =>
        {
            
            GetVersion(null);// TODO 判断版本号
            SaveAccount(username, password);
        });
    }
    
    
    /// <summary>
    /// 登陆
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    private void Login(string userName, string password, Action doSuccess = null, Action<bool,string> doFail= null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("username", userName);
        keyValues.Add("password", password);
        keyValues.Add("DeviceId", Application.identifier);

        StartCoroutine(HttpManager.My.HttpSend(Url.NewLoginUrl, (www)=> {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip("账号或密码错误，登陆失败！");
                if(response.data.Contains("密码"))
                {
                    doFail?.Invoke(true,response.data);
                }
                else
                {
                    doFail?.Invoke(false,response.data);
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
    void GetVersion(Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();

        StartCoroutine(HttpManager.My.HttpSend(Url.NewLoginUrl, (www)=> {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            
                Debug.Log(response.data);
                try
                {
                    version = response.data;
                    doEnd?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            
        }, keyValues, HttpType.Post, 10002));
    }


    public void LoadAccount()
    {
        try
        {
            StreamReader streamReader = new StreamReader(Application.dataPath+"Account.json");
            string str = streamReader.ReadToEnd();
            string decode =   Decrypt(str);
            AccountJosn  json =  JsonUtility.FromJson<AccountJosn>(decode);
            InitInput(json);
        }
        catch (Exception e)
        {
            return;
        }
     
    }


    public void LoadVersionsIndex()
    {
        try
        {
            StreamReader streamReader = new StreamReader(Application.dataPath+"Build.json");
            string str = streamReader.ReadToEnd();
           
            BuildJson  json =  JsonUtility.FromJson<BuildJson>(str);
            
        }
        catch (Exception e)
        {
            return;
        }

    }

    /// <summary>
    /// 初始化用户名
    /// </summary>
    public void InitInput(AccountJosn  json )
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
       string accoutjson =  JsonUtility.ToJson(account);
   string encode =   Encrypt(accoutjson);
       FileStream file = new FileStream(Application.dataPath+"Account.json", FileMode.Create);  
       byte[] bts = System.Text.Encoding.UTF8.GetBytes(encode);  
       file.Write(bts,0,bts.Length);  
       if(file != null)
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