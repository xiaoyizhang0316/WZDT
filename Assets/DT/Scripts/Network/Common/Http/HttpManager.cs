using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Net;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using UnityEngine.UI;

public class HttpManager : MonoSingleton<HttpManager>
{
    public UnityWebRequestAgentHelper unityWebRequest;

    public GameObject mask;
    public Text tip;
    public Transform clickTip;
    public Text clickTipText;
    public Button clickTipButton;
    public Transform selectTip;
    public Text selectTipText;
    public Button selectCancel;
    public Button selectRetry;

    public Transform reConnTip;
    public Text reConnTipText;
    public Button reConnConfirm;
    public Button reConnCancel;

    private bool timer = false;
    private float time = 0;

    bool isTipShow = false;

    float frameCount = 0;
    float frameTime = 0;
    float fps = 0;
    public Text FpsText;
    //public bool showFps = false;

    Dictionary<int, HttpData> retryDic = new Dictionary<int, HttpData>();

    void Start()
    {
        timer = false;
        if (NetworkMgr.My.isShowFPS && SceneManager.GetActiveScene().name.StartsWith("FTE") && SceneManager.GetActiveScene().name != "FTE_0")
        {
            InvokeRepeating("ShowFPS", 1, 1f);
        }
    }
    private float deltaTime;

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            time += Time.deltaTime;
        }

        if(NetworkMgr.My.isShowFPS && SceneManager.GetActiveScene().name.StartsWith("FTE")&&SceneManager.GetActiveScene().name != "FTE_0")
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            frameCount = 1f / deltaTime;
        }
    }

    void ShowFPS()
    {
        fps = frameCount;
        frameCount = 0;
        //FpsText.text = "FPS "+Mathf.Ceil(fps).ToString();
        FpsText.text = NetworkMgr.My.playerDatas.playerName;
        //FpsText.color = fps >= 60 ? Color.green : (fps >= 30 ? Color.white : Color.red);
    }


    public IEnumerator HttpSend(string webRequestUrl, Action<UnityWebRequest> action, SortedDictionary<string, string> userData = null,  HttpType httpType = HttpType.Get, int retryID=0)
    {
        mask.SetActive(true);
        bool isNetworkSlow = false;
        time = 0;

        if (retryID != 0 && !retryDic.ContainsKey(retryID))
            retryDic.Add(retryID, new HttpData(retryID, webRequestUrl, action, userData, httpType, true));
            

        UnityWebRequest uwr = new UnityWebRequest();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("网络不可用");
            SetNeedRetryById(retryID, true);
            if (SceneManager.GetActiveScene().name == "Login" || SceneManager.GetActiveScene().name == "FTE_0")
            {
                
                ShowTwoClickTip("网络不可用！请点击取消重新登录或检查网后重试！",  ()=> {
                    retryDic.Remove(retryID);
                    if (SceneManager.GetActiveScene().name == "FTE_0"|| SceneManager.GetActiveScene().name == "Map")
                    {
                        SceneManager.LoadScene("Login");    
                    }
                });
                mask.SetActive(false);
            }
            else
            {
                ShowTwoClickTip("网络不可用，请检查网络后重试或返回主界面！", () => {
                    retryDic.Remove(retryID);
                    SceneManager.LoadScene("Map");
                });
                mask.SetActive(false);
            }
            
            yield break;
        }

        uwr = unityWebRequest.Request(webRequestUrl, userData, httpType);
        //uwr.timeout = 6;
        //}
        //Debug.Log(uwr.responseCode);
        while (true)
        {
            //Debug.Log("code----------------" + uwr.responseCode);
            timer = true;
            if (time > 2 && time < 6)
            {
                //wait.transform.localEulerAngles = new Vector3(0, 0, wait.transform.localEulerAngles.z + 2f);
                //wait.SetActive(true);
            }
            if (time >= 6)
            {
                Debug.Log("long Delay----------" + uwr.responseCode);

                //ShowNetworkStatus(uwr.responseCode);
                SetNeedRetryById(retryID, true);
                if (SceneManager.GetActiveScene().name =="Login" || SceneManager.GetActiveScene().name == "FTE_0")
                {
                    ShowTwoClickTip("网络较慢，请重新登录或重试", () => {
                        retryDic.Remove(retryID);
                        if(SceneManager.GetActiveScene().name == "FTE_0"|| SceneManager.GetActiveScene().name == "Map")
                        {
                            SceneManager.LoadScene("Login");
                        }
                    });
                    mask.SetActive(false);
                }
                else
                {
                    ShowTwoClickTip("网络缓慢，请重试或返回主界面", () => {
                        retryDic.Remove(retryID);
                        SceneManager.LoadScene("Map");
                    });
                    mask.SetActive(false);
                }
                

                isNetworkSlow = true;
                timer = false;
                break;
            }
            
            
            if (uwr.isDone)
            {
                
                time = 0;
                timer = false;
                break;
            }

            yield return null;
        }
        
        if (isNetworkSlow)
        {
            Debug.Log("network broken!");
            isNetworkSlow = false;
        }
        else
        {
            SetNeedRetryById(retryID, true);
            if (!string.IsNullOrEmpty(uwr.error) || uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error+uwr.responseCode);
                //ShowNetworkStatus(uwr.responseCode);
                //SetNeedRetryById(retryID, false);
                
                if (retryID == HttpId.pingTestID)
                {
                    action(uwr);
                }
                else
                {
                    if(SceneManager.GetActiveScene().name == "Login" || SceneManager.GetActiveScene().name == "FTE_0"|| SceneManager.GetActiveScene().name == "Map")
                    {

                        ShowTwoClickTip("网络错误！请检查网络后重试或点击取消返回登录界面！",()=>{
                            retryDic.Remove(retryID);
                            if (SceneManager.GetActiveScene().name == "FTE_0"|| SceneManager.GetActiveScene().name == "Map")
                            {
                                SceneManager.LoadScene("Login");
                            }
                        });
                        mask.SetActive(false);
                    }
                    else
                    {
                        ShowTwoClickTip("网络错误！请检查网络后重试或点击取消返回主界面！", () => {
                            retryDic.Remove(retryID);
                            SceneManager.LoadScene("Map");
                        });
                        mask.SetActive(false);
                    }

                }
                yield break;
            }
            else
            {
                //SetNeedRetryById(retryID,false);
                retryDic.Remove(retryID);
                action(uwr);
            }
        }
    }

    public IEnumerator HttpsSend(string webRequestUrl, Action<UnityWebRequest> action, string headerkey, string headervalue, SortedDictionary<string, string> userData = null, HttpType httpType = HttpType.Get)
    {
        bool isNetworkSlow = false;
        time = 0;

        if (unityWebRequest == null)
        {

        }
        UnityWebRequest uwr = new UnityWebRequest();
        if (Application.internetReachability == NetworkReachability.NotReachable && SceneManager.GetActiveScene().name != "GameMain")
        {



            yield break;
        }
        else
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            uwr = UnityWebRequest.Get(webRequestUrl);
            uwr.SetRequestHeader(headerkey, headervalue);
            uwr.SendWebRequest();
        }
        while (true)
        {
            timer = true;
            if (time > 2 && time < 6)
            {
                
            }

            if (time >= 6)
            {
                //ShowNetworkStatus(uwr.responseCode);


                isNetworkSlow = true;
                timer = false;
                break;
            }
            if (uwr.isDone)
            {
                Debug.Log("network time:" + time);
                //wait.SetActive(false);
                time = 0;
                timer = false;
                break;
            }

            yield return null;
        }
        Debug.Log(uwr.responseCode);
        if (isNetworkSlow)
        {
            Debug.Log("network broken!");
            isNetworkSlow = false;
        }
        else
        {
            if (!string.IsNullOrEmpty(uwr.error) || uwr.isNetworkError || uwr.isHttpError)
            {

            }
            else
            {
                Debug.Log(uwr.responseCode);
                action(uwr);
                //wait.transform.localEulerAngles = Vector3.zero;
            }
        }
    }

    public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    {
        return true;
    }


    private void ShowNetworkStatus(long code)
    {
        string tip = "网络故障";
        switch (code)
        {
            case 201:
            case 202:
            case 203:
            case 204:
            case 205:
            case 206:
                tip = "当前网络不稳定，点击确定重试或稍后再试~";
                break;
            case 400:
            case 401:
            case 402:
            case 403:
            case 404:
            case 405:
            case 406:
            case 407:
            case 408:
            case 409:
            case 410:
            case 411:
            case 412:
            case 413:
            case 414:
            case 415:
            case 416:
            case 417:
                tip = "请求错误";
                break;
            case 500:
            case 501:
            case 502:
            case 503:
            case 504:
            case 505:
                tip = "服务器暂不可用，请稍后再试~";
                break;
        }
        ShowTip(tip);
    }

    public void ShowTip(string tipStr, Action doEnd = null,float time=2f)
    {
        tip.text = tipStr;
        if (isTipShow)
        {

            DOTween.Kill("httpTip");
            tip.DOFade(1, 0.02f).Play();
        }
        tip.gameObject.SetActive(true);
        isTipShow = true;
        tip.DOFade(0, time).SetId("httpTip").Play().OnComplete(()=> {
            tip.gameObject.SetActive(false);
            tip.DOFade(1, 0).Play();
            isTipShow = false;
        }).SetEase(Ease.InExpo);
    }
    
    public void ShowLongTip(string tipStr, Action doEnd = null)
    {
        tip.text = tipStr;
        if (isTipShow)
        {
            DOTween.Kill("httpLongTip");
            tip.DOFade(1, 0.02f).Play();
        }
        tip.gameObject.SetActive(true);
        isTipShow = true;
        tip.transform.DOScale(1, 5f).SetId("httpLongTip").Play().OnComplete(()=>{
            tip.DOFade(0, 2f).Play().OnComplete(()=> {
                tip.gameObject.SetActive(false);
                tip.DOFade(1, 0).Play();
                isTipShow = false;
            });
        });
    }

    public void ShowClickTip(string tip,Action doEnd=null)
    {
        clickTipText.text = tip;
        clickTip.gameObject.SetActive(true);
        mask.SetActive(false);
        clickTipButton.onClick.RemoveAllListeners();
        clickTipButton.onClick.AddListener(()=> {
            clickTip.gameObject.SetActive(false);
            doEnd?.Invoke();
        });
    }

    public void ShowTwoClickTip(string tip, Action cancel=null)
    {
        selectTipText.text = tip;
        //selectCancel.transform.GetChild(0).GetComponent<Text>().text = ""
        selectCancel.onClick.RemoveAllListeners();
        selectCancel.onClick.AddListener(() => GotoLogin());
        
        if (cancel != null)
        {
            selectCancel.onClick.RemoveAllListeners();
            selectCancel.onClick.AddListener(() => {
                cancel();
                selectTip.gameObject.SetActive(false);
            });
        }
        selectRetry.onClick.RemoveAllListeners();
        selectRetry.onClick.AddListener(() => {
            foreach(var v in retryDic.Values)
            {
                if(v.needRetry)
                    StartCoroutine( HttpSend(v.url, v.action, v.keyValues, v.httpType, v.httpId));
            }
            selectTip.gameObject.SetActive(false);
        });
        selectTip.gameObject.SetActive(true);
        mask.SetActive(false);
    }

    public void ShowTwoClickTip(string tip, Action confirm = null, Action cancel = null)
    {
        selectTipText.text = tip;
        selectCancel.transform.GetChild(0).GetComponent<Text>().text = "继续";
        selectCancel.onClick.RemoveAllListeners();
        selectCancel.onClick.AddListener(() => {
            cancel?.Invoke();
            selectTip.gameObject.SetActive(false);
        });
        selectRetry.transform.GetChild(0).GetComponent<Text>().text = "返回确认";
        selectRetry.onClick.RemoveAllListeners();
        selectRetry.onClick.AddListener(() => {
            confirm?.Invoke();
            selectTip.gameObject.SetActive(false);
        });
        selectTip.gameObject.SetActive(true);
        mask.SetActive(false);
    }

    public void ShowReConn(string str =null)
    {
        if (str != null)
        {
            reConnTipText.text = str;
        }
        else
        {
            reConnTipText.text = "与服务器断开连接";
        }
        reConnCancel.onClick.RemoveAllListeners();
        reConnCancel.onClick.AddListener(() => {
            reConnTip.gameObject.SetActive(false);
            SceneManager.LoadScene("Login");
        });

        if (SceneManager.GetActiveScene().name.Equals("Map"))
        {
            reConnConfirm.transform.GetChild(0).GetComponent<Text>().text = "重新登录";
            reConnConfirm.onClick.RemoveAllListeners();
            reConnConfirm.onClick.AddListener(()=> {
                reConnTip.gameObject.SetActive(false);
                SceneManager.LoadScene("Login");
            });
            
        }
        else
        {
            reConnConfirm.transform.GetChild(0).GetComponent<Text>().text = "重新连接";
            reConnConfirm.onClick.RemoveAllListeners();
            reConnConfirm.onClick.AddListener(()=> {
                reConnTip.gameObject.SetActive(false);
                NetworkMgr.My.ReConnect();
            });
        }
        
        reConnTip.gameObject.SetActive(true);
    }

    

    private void SetNeedRetryById(int id, bool retry)
    {
        HttpData httpData;
        retryDic.TryGetValue(id, out httpData);
        if (httpData != null)
        {
            httpData.SetNeedRetry(retry);
            retryDic.Remove(id);
            retryDic.Add(id, httpData);
        }
    }

    private void GotoLogin()
    {
        SceneManager.LoadScene("Login");
    }
    
    
    
}

[Serializable]
public class HttpResponse
{
    public int status;
    public string data;
    public string errMsg;
}

