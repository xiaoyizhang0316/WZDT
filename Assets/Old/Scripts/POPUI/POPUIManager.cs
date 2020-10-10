using System;
using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class POPUIManager : MonoSingleton<POPUIManager>
{
    public GameObject normalPOPUIPrb;

    public GameObject tipPOPUIPrb;
    /// <summary>
    /// 普通弹窗
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="Yse"></param>
    /// <param name="No"></param>
    public void POPNormalUI(string title, string content, Action Yse, Action No)
    {
        UIManager.My.Panel_POPUI.SetActive(true);
        GameObject gameObject = Instantiate(normalPOPUIPrb, UIManager.My.Panel_POPUI.transform);
        gameObject.GetComponent<POPNormalUI>().InitPOPUI(title, content, () =>
        {
            Yse();
            Destroy(gameObject,0.01f);
            UIManager.My.Panel_POPUI.SetActive(false);
        }
       , () =>
        {
            No();
            Destroy(gameObject,0.01f);
            UIManager.My.Panel_POPUI.SetActive(false);

        });
    }

    /// <summary>
    /// tishi弹窗
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="Yse"></param>
    /// <param name="No"></param>
    public void POPTipUI(string title, string content, Action Yse )
    {
        UIManager.My.Panel_POPUI.SetActive(true);
        GameObject gameObject = Instantiate(tipPOPUIPrb, UIManager.My.Panel_POPUI.transform);
        gameObject.GetComponent<POPNormalUI>().InitPOPUI(title, content, () =>
            {
                Yse();
                Destroy(gameObject,0.01f);
                UIManager.My.Panel_POPUI.SetActive(false);
            }
            , () =>
            {
                
                Destroy(gameObject,0.01f);
                UIManager.My.Panel_POPUI.SetActive(false);

            });
    }

}