using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地块标志 用来检测当前是否镶嵌装备或者人物
/// </summary>
public class PlotSign : MonoBehaviour
{
    public bool isOccupied;
    
    private void OnTriggerStay(Collider other)
    {
       
        isOccupied = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        isOccupied = false;
    }
}
