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

    public void Init<T>(Action<T> _retry = null,Action<T> _quit = null)
    {
        //retry.onClick.RemoveAllListeners();
        //quit.onClick.RemoveAllListeners();
        //retry.onClick.AddListener(() =>
        //{
        //    _retry();
        //});
        //quit.onClick.AddListener(() =>
        //{
        //    _quit();
        //});
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
