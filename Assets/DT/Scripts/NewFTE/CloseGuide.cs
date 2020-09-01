﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseGuide : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GuideManager.My.guideClose = this;
    }

    public void Init()
    {
        if (!AnsweringPanel.My.isComplete || PlayerPrefs.GetInt("isUseGuide") == 0)
        {
            gameObject.SetActive(false);
        }

        else
        {
            gameObject.SetActive(true);
        }

        GetComponent<Button>().onClick.AddListener(() =>
        {
            GuideManager.My.CloseFTE();
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
