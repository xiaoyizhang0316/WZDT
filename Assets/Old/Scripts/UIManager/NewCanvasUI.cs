using System;
using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class NewCanvasUI : MonoSingleton<NewCanvasUI>
{
 
    public GameObject Panel_ChoseRole;
    public Role CurrentClickRole;
    public BaseMapRole currentMapRole;
    public GameObject Panel_AssemblyRole; 
    public Transform RoleTF;
    /// <summary>
    /// 需要遮挡的UI
    /// </summary>
    public List<GameObject> needReycastTargetPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

    /// <summary>
    /// 检测当前界面是否可以穿透panel
    /// </summary>
    public bool NeedRayCastPanel()
    {

        for (int i = 0; i <needReycastTargetPanel.Count; i++)
        {
            if (needReycastTargetPanel[i].activeSelf)
            {
               
                return true;
            }
        }

        return false;
    }

}
