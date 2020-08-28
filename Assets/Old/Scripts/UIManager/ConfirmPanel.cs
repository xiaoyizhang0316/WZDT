using System;
using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class ConfirmPanel : MonoSingleton<ConfirmPanel>
{

    public Action<double> doubleAction;

    public Action<int> intAction;

    public string param;

    public Text titleText;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="action"></param>
    /// <param name="p"></param>
    public void Init(Action<double> action, double p,string str)
    {
        Reset();
        doubleAction = action;
        param = p.ToString();
        titleText.text = str;
    }

    public void Init(Action<int> action, int p, string str)
    {
        Reset();
        intAction = action;
        param = p.ToString();
        titleText.text = str;
    }

    /// <summary>
    /// 执行确定调用委托
    /// </summary>
    public void Comfirm()
    {
        if (intAction != null)
            intAction(int.Parse(param));
        else if (doubleAction != null)
            doubleAction(double.Parse(param));
        Close();
    }

    /// <summary>
    /// 取消时 
    /// </summary>
    public void Close()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 重置所有委托
    /// </summary>
    public void Reset()
    {
        doubleAction = null;
        intAction = null;
    }
}
