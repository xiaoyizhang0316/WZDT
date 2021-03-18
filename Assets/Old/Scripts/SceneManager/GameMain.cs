using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoSingletonDontDestroy<GameMain>
{
    public bool useNetWork = false;
    public string sceneName = "FTE_1";
    public bool showFPS = false;
    public bool useLocalIp = false;

    public bool useLocalJson = false;

    public static int mainThreadId;

    public string localIP = "";
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("isUseGuide",1);
        //   Debug.Log("FTE_1"+PlayerPrefs.GetInt("FTE_1|1",true));
        //   Debug.Log("FTE_2"+PlayerPrefs.GetInt("FTE_2"));
        //   Debug.Log("FTE_3"+PlayerPrefs.GetInt("FTE_3"));
        //   Debug.Log("FTE_4"+PlayerPrefs.GetInt("FTE_4"));
        //   for (int i = 1; i < 5; i++)
        //   {
        //       Debug.Log("当前"+PlayerPrefs.GetInt("FTE_" + i.ToString()));
        //       if (PlayerPrefs.GetInt("FTE_" + i.ToString(), 0) == 0)
        //       {
        //           SceneManager.LoadScene("FTE_" + i.ToString());
        //           return;
        //       }
        //   }
        
        //Debug.Log(SystemInfo.deviceUniqueIdentifier);
        mainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
        /*foreach (string str in "sdasdsads*".Split('*'))
        {
            Debug.Log("字符: " +str);
        }*/
        if (useNetWork)
        {
            NetworkMgr.My.isUsingHttp = true;
            sceneName = "Login";
        }
        else
        {
            //if (PlayerPrefs.GetInt("FTE0End", 0) == 0)
            //{
            //    sceneName = "Map";
            //}
            //else
            //{
            //    sceneName = "Map";
            //}
        }
        if (showFPS)
        {
            NetworkMgr.My.isShowFPS = true;
        }

        if (useLocalIp)
        {
            if (localIP == null || localIP.Replace(" ", "" ).Equals(""))
            {
                Url.SetIp(useLocalIp);
            }
            else
            {
                Url.SetIp(localIP);
            }
            
        }

        if (useLocalJson)
        {
            NetworkMgr.My.useLocalJson = true;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string name)
    {
        sceneName = name;
        SceneManager.LoadScene(name);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("退出游戏"))
        {
            Application.Quit();
        }
    }


}
