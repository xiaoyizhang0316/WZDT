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
        
        //Debug.Log(CompressUtils.Uncompress("H4sIAAAAAAAEAOy9B2AcSZYlJi9tynt/SvVK1+B0oQiAYBMk2JBAEOzBiM3mkuwdaUcjKasqgcplVmVdZhZAzO2dvPfee++999577733ujudTif33/8/XGZkAWz2zkrayZ4hgKrIHz9+fB8/Iv7Lv/KP/8//+j/pP//T/4hf+HCHnke7v3B3hP/v4P+//3/+R/x5/8Xf9CfzV7uPdn7hzsj+//f/r/62v+u/+LP+VPPi3iN+Qf+/2/n2Xufd//rP/Tv+8z/tbzDf7nfeDb+976HUf/fTAGHq90/6y//rP+0P+s//8L/uP/8T/y5u8KDz+n/+x/3h/9Vf+of/53/CH4Jvd3c6iP2Xf9qf95//vX/Pf/nX85B3u0P+L/6mv+2/+Av+vP/87/zLzet73QZ/wZ/3X/zNf/B//sf8RV6b/U6b/+wf/PP/6z/7r3YY7t7vNPjP/5S/6r/6G/8SbwgHYYP/JwAA//9avHRxsgEAAA=="));
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
