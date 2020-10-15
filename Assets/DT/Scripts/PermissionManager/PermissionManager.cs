﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermissionManager : MonoSingleton<PermissionManager>
{

    public GameObject severCanvas;
    public GameObject clientCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        InitUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// /根据权限初始化UI
    /// </summary>
    public void InitUI()
    {
        if (PlayerData.My.creatRole ==PlayerData.My.playerDutyID)
        {
            severCanvas.SetActive(true);
        }
        else
        {
            clientCanvas.SetActive(true);
        }
    }
}
