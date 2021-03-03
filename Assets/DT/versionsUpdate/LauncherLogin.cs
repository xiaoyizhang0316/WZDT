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

    public void Login()
    {
        //wj
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
           
            AccountJosn  json =  JsonUtility.FromJson<AccountJosn>(str);
            
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