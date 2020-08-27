using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NetErrorPanel : MonoSingleton<NetErrorPanel>
{
    public Button retry;

    public Button quit;

    public void Open()
    {
        transform.localPosition = new Vector3(0f, 0f, 0f);
    }
    
    public void Close()
    {
        transform.localPosition = new Vector3(100000f, 0f, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        Close();
        quit.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Login");
        });
    }
}
