using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoSingletonDontDestroy<GameMain>
{
    public string sceneName = "FTE_1";
    // Start is called before the first frame update
    IEnumerator Start()
    {
        PlayerPrefs.DeleteAll();
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
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string name)
    {
        sceneName = name;
        SceneManager.LoadScene("GameMain");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("退出游戏"))
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
