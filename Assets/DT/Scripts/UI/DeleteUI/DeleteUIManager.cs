using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteUIManager : MonoBehaviour
{
    public Text content;

    public Button concle;

    public Button delete;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(string content,Action delete,Action cancle)
    {
        this.content.text = content;
        this.delete.onClick.AddListener(() =>
        {
            delete();
            gameObject.SetActive(false);

        });
        this.concle.onClick.AddListener(() =>
        {
            cancle();
            gameObject.SetActive(false);
        });
    }
}
