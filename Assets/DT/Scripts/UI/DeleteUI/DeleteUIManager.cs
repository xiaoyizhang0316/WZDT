using System;
using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class DeleteUIManager : MonoSingleton<DeleteUIManager>
{
    public Text content;

    public Button cancle;

    public Button delete;
    // Start is called before the first frame update
    void Start()
    {
        cancle.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(string content,Action delete)
    {
        this.content.text = content;
        this.delete.onClick.RemoveAllListeners();
        this.delete.onClick.AddListener(() =>
        {
            delete();
            gameObject.SetActive(false);
        });
    }
}
