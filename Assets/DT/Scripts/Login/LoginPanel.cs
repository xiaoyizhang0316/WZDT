using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static StageGoal;
using System.Text.RegularExpressions;
using DG.Tweening;

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

    public List<Transform> left = new List<Transform>();

    public List<Transform> right = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        /*PlayerPrefs.SetInt("BackGroundVolume",-80);
        PlayerPrefs.SetInt("SoundEffectVolume",-80);
        StartCoroutine(PeopleEffect());
        //PlayerPrefs.DeleteAll();
        login_Btn.onClick.AddListener(Login);
        transform.localPosition = new Vector3(0, 500, 0);
        transform.DOLocalMoveY(-35f, 0.8f).Play();
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
        }*/

        //username_Input.ActivateInputField();
        //username_Input.MoveTextEnd(true);
        //Test();
        NewInit();
        //Debug.Log(Directory.GetParent(Application.dataPath) + "\\Account.json");
    }

    void NewInit()
    {
        PlayerPrefs.SetInt("BackGroundVolume",-80);
        PlayerPrefs.SetInt("SoundEffectVolume",-80);
        StartCoroutine(PeopleEffect());
        //PlayerPrefs.DeleteAll();
        
        StartCoroutine(LoadAccount(() =>
        {
            login_Btn.interactable = false;
            Login();
        }, () =>
        {
            login_Btn.onClick.AddListener(Login);
            transform.localPosition = new Vector3(0, 500, 0);
            transform.DOLocalMoveY(-35f, 0.8f).Play();
            login_Btn.interactable = true;
        }));
        HideGame();
    }
    
    public IEnumerator LoadAccount(Action canLogin,Action cantLogin)
    {
#if UNITY_STANDALONE_WIN
            StreamReader streamReader = new StreamReader( Directory.GetParent(Directory.GetParent(Application.dataPath)+"") + "\\StartGame_Data\\Account.json");
#elif UNITY_STANDALONE_OSX
        StreamReader streamReader = new StreamReader(Directory.GetParent( Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName) + "/Account.json");
#endif
        //StreamReader streamReader = new StreamReader( Directory.GetParent(Directory.GetParent(Application.dataPath)+"") + "\\StartGame_Data\\Account.json");
        if (streamReader != null)
        {
            string str = streamReader.ReadToEnd();
            while (string.IsNullOrEmpty(str))
            {
                yield return null;
            }
            streamReader.Close();

            string decode = CompressUtils.Decrypt(str);
            AccountJosn json = JsonUtility.FromJson<AccountJosn>(decode);
            if (!string.IsNullOrEmpty(json.name))
            {
                InitInput(json);
                canLogin();
            }
            else
            {
                cantLogin();
            }

        }
    }
    
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
    public void SaveAccount(string name, string password)
    {
        AccountJosn account = new AccountJosn()
        {
            name = name,
            password = password
        };
        string accoutjson = JsonUtility.ToJson(account);
        string encode = CompressUtils.Encrypt(accoutjson);
        string path = "";
#if UNITY_STANDALONE_WIN
        path = Directory.GetParent(Directory.GetParent(Application.dataPath).FullName) + "\\StartGame_Data\\Account.json";
#elif UNITY_STANDALONE_OSX
        path = Directory.GetParent(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName) + "/Account.json";
#endif
/*#if UNITY_STANDALONE_WIN
        FileStream file = new FileStream(Directory.GetParent(Directory.GetParent(Application.dataPath)+"") + "\\StartGame_Data\\Account.json", FileMode.Create);
#elif UNITY_STANDALONE_OSX
        FileStream file = new FileStream(Directory.GetParent(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName) + "/Account.json", FileMode.Create);
#endif*/
        FileStream file = new FileStream(path, FileMode.Create);

        byte[] bts = System.Text.Encoding.UTF8.GetBytes(encode);
        file.Write(bts, 0, bts.Length);
        FileAttributes attributes = File.GetAttributes(path);
        File.SetAttributes(path,attributes| FileAttributes.Hidden);
        if (file != null)
        {
            file.Close();
        }

        ///保存账号密码
    }

    void HideGame()
    {
        #if UNITY_STANDALONE_WIN
        string path = Directory.GetParent(Directory.GetParent(Application.dataPath).FullName) + "\\Game";
        #elif UNITY_STANDALONE_OSX
        string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName) + "/Game";
#endif
        if (Directory.Exists(path))
        {
            Debug.Log("Hide Game Folder");
            FileAttributes attributes = File.GetAttributes(path);
            File.SetAttributes(path, attributes | FileAttributes.Hidden);
        }
    }

    void OldInit()
    {
        PlayerPrefs.SetInt("BackGroundVolume",-80);
        PlayerPrefs.SetInt("SoundEffectVolume",-80);
        StartCoroutine(PeopleEffect());
        //PlayerPrefs.DeleteAll();
        login_Btn.onClick.AddListener(Login);
        transform.localPosition = new Vector3(0, 500, 0);
        transform.DOLocalMoveY(-35f, 0.8f).Play();
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
        // for new version login
        /*SavePasswordOrNot();
        UseServerOrNot();*/
        NetworkMgr.My.GetJsonDatas((data) => {
            OriginalData.My.InitDatas(data);
            
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
                    /*if(NetworkMgr.My.playerDatas.fteProgress == -2)
                    {
                        NetworkMgr.My.SetPlayerStatus("FTE_0-1", "");
                        SceneManager.LoadScene("FTE_0-1");
                    }else 
                    if (NetworkMgr.My.playerDatas.fteProgress==-1)
                    {
                        NetworkMgr.My.SetPlayerStatus("FTE_0-2", "");
                        SceneManager.LoadScene("FTE_0-2");
                    }
                    else
                    {
                        //NetworkMgr.My.GetLevelProgress();
                        //NetworkMgr.My.GetPlayerEquips();
                        //NetworkMgr.My.GetAnswers();
                        NetworkMgr.My.SetPlayerStatus("Map", "");
                        SceneManager.LoadScene("Map");
                    }*/

                    if (NetworkMgr.My.playerDatas.fte.Equals("0"))
                    {
                        if (NetworkMgr.My.playerDatas.fteProgress > 0)
                        {
                            NetworkMgr.My.SetPlayerStatus("Map", "");
                            SceneManager.LoadScene("Map");
                        }
                        else
                        {
                            NetworkMgr.My.SetPlayerStatus("FTE_0.5", "");
                            SceneManager.LoadScene("FTE_0.5");
                        }
                        
                    }else
                    {
                        NetworkMgr.My.SetPlayerStatus("Map", "");
                        SceneManager.LoadScene("Map");
                    }
                }
                
                SaveAccount(username, password);
            }
        });
        isLogin = false;
    }

    private void LoginFail(bool deletePwd, string errMsg)
    {
        HttpManager.My.ShowTip(errMsg);
        if (deletePwd)
        {
            password_Input.text = "";
            PlayerPrefs.DeleteKey("username");
            PlayerPrefs.DeleteKey("password");
            username_Input.ActivateInputField();
            username_Input.MoveTextEnd(true);
        }
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

            /*if (Input.GetKeyDown(KeyCode.Return) && !isLogin)
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
            }*/
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
        //GetJson();
        //string t = "_111_1_";
        //string[] tt = t.Split('_');
        //Debug.Log(tt.Length);
        //Debug.Log(tt[0]);
        //Debug.Log(tt[tt.Length - 1]);
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

    private void GetJson()
    {
        NetworkMgr.My.TestGet((data) => {
            Debug.Log(data);
            data = CompressUtils.Uncompress(data);
            JsonDatas json = JsonUtility.FromJson<JsonDatas>(data);
            Debug.Log(json.BuffData);
            BuffsData buffsData = JsonUtility.FromJson<BuffsData>(json.BuffData);
            Debug.Log(buffsData.buffSigns.Count);
        });
    }



    public IEnumerator PeopleEffect()
    {
        for (int i = 0; i < left.Count; i++)
        {
            left[i].transform.DOLocalMove(left[i].transform.localPosition - new Vector3(3000,3000),0.01f);
        }
        for (int i = 0; i < right.Count; i++)
        {
            right[i].transform.DOLocalMove(right[i].transform.localPosition + new Vector3( 3000, -3000),0.01f);
        }
      yield return new WaitForSeconds(0.4f);
        for (int i = 0; i <  left.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
                        left[i].transform.DOLocalMove(left[i].transform.localPosition +new Vector3(+3000,+3000),0.8f);
                        yield return new WaitForSeconds(0.1f);

                        right[i].transform.DOLocalMove(right[i].transform.localPosition - new Vector3( 3000, -3000),0.8f);

        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i <  left.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
            left[i].transform.DOLocalJump(    left[i].transform.localPosition+new Vector3(0,10,0), 10,1,0.3f).SetLoops(100);
            yield return new WaitForSeconds(0.1f);

            right[i].transform.DOLocalJump(right[i].transform.localPosition+new Vector3(0,10,0),10,1,0.3f).SetLoops(100);
        }
    }
}
