using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using UnityEngine.UI;

public class HttpManager : MonoSingleton<HttpManager>
{
    public UnityWebRequestAgentHelper unityWebRequest;

    public GameObject mask;
    public Text tip;
    public Transform clickTip;
    public Transform selectTip;
    public Text selectTipText;
    public Button selectCancel;
    public Button selectRetry;

    private bool timer = false;
    private float time = 0;

    bool isTipShow = false;

    public Action retry;
    public bool retryAppend = false;


    void Start()
    {
        timer = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            time += Time.deltaTime;
        }
    }

    public IEnumerator HttpSend(string webRequestUrl, Action<UnityWebRequest> action, SortedDictionary<string, string> userData = null, HttpType httpType = HttpType.Get)
    {
        mask.SetActive(true);
        bool isNetworkSlow = false;
        time = 0;
        //Debug.Log("http send");

        UnityWebRequest uwr = new UnityWebRequest();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("网络不可用");
            ShowClickTip("网络不可用！", () => SceneManager.LoadScene("Login"));
            yield break;
        }
        //if (Application.internetReachability == NetworkReachability.NotReachable && SceneManager.GetActiveScene().name != "GameMain")
        //{
        //    yield break;
        //}
        //else
        //{
        //if (SceneManager.GetActiveScene().name != "GameMain" && SceneManager.GetActiveScene().name != "Start")
        //{
        //    if (deviceDatas == null)
        //    {
        //        deviceDatas = new SortedDictionary<string, string>();
        //    }
        //    //deviceDatas.Clear();
        //    //deviceDatas.Add("playerID", JsonRead.My.playerID);
        //    //deviceDatas.Add("GUID", GlobalVariable.deviceNumber);
        //    //checkUwr = unityWebRequest.Request(JsonPath.checkDeviceNumber, deviceDatas, HttpType.Post);
        //}
        uwr = unityWebRequest.Request(webRequestUrl, userData, httpType);
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
                if(SceneManager.GetActiveScene().name =="Login" || SceneManager.GetActiveScene().name == "FTE_0")
                {
                    ShowTwoClickTip("网络较慢，请重新登录或重试");
                }
                else
                {
                    ShowTwoClickTip("网络缓慢，请重试或返回主界面", () => { SceneManager.LoadScene("Map"); });
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
            if (!string.IsNullOrEmpty(uwr.error) || uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error+uwr.responseCode);
                //ShowNetworkStatus(uwr.responseCode);
                ShowClickTip("网络错误！",()=>SceneManager.LoadScene("Login"));
                mask.SetActive(false);
                yield break;
            }
            else
            {
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
                ShowNetworkStatus(uwr.responseCode);


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

    public void ShowTip(string tipStr, Action doEnd = null)
    {
        tip.text = tipStr;
        if (isTipShow)
        {

            DOTween.Kill("httpTip");
            tip.DOFade(1, 0.02f).Play();
        }
        tip.gameObject.SetActive(true);
        isTipShow = true;
        tip.DOFade(0, 2f).SetId("httpTip").Play().OnComplete(()=> {
            tip.gameObject.SetActive(false);
            tip.DOFade(1, 0).Play();
            isTipShow = false;
        });
    }

    public void ShowClickTip(string tip,Action doEnd=null)
    {
        clickTip.GetChild(0).GetComponent<Text>().text = tip;
        clickTip.gameObject.SetActive(true);
        mask.SetActive(false);
        clickTip.GetComponent<Button>().onClick.RemoveAllListeners();
        clickTip.GetComponent<Button>().onClick.AddListener(()=> { doEnd?.Invoke(); });
    }

    public void ShowTwoClickTip(string tip, Action cancel=null)
    {
        selectTipText.text = tip;

        selectCancel.onClick.RemoveAllListeners();
        selectCancel.onClick.AddListener(() => GotoLogin());
        
        if (cancel != null)
        {
            selectCancel.onClick.RemoveAllListeners();
            selectCancel.onClick.AddListener(() => cancel());
        }
        selectRetry.onClick.RemoveAllListeners();
        selectRetry.onClick.AddListener(() => retry());
        selectTip.gameObject.SetActive(true);
        mask.SetActive(false);
    }

    private void GotoLogin()
    {
        SceneManager.LoadScene("Login");
    }

    public void Retry(Action add)
    {
        retry = add;
    }
}

