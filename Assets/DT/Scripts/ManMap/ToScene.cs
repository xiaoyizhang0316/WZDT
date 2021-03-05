using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToScene : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadScene);
    }


    public void SignOut()
    {
        StartCoroutine(Delete(() => {
            SceneManager.LoadScene("Login");
        }));
        //SceneManager.LoadScene("Login");

    }

    public IEnumerator Delete(Action doend)
    {
        string path = Directory.GetParent( Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName) + "/Account.json";
        if (File.Exists(path))
        {
            Debug.LogError("   存在 删除文件 ");

            // System.IO.Directory.Delete(@updateAssets.list[0].LocalUrl);
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                Debug.Log(" 删除文件时 出现错误 " + ex.Message);
            }
        }
        else
        {
            doend();
            yield break;
        }

        while (System.IO.File.Exists(path))
        {
            yield return null;
        }

        doend();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
