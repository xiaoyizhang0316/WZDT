using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MessageManager : NetworkBehaviour
{
    #region Singleton

    public static MessageManager my;

    private void Awake()
    {
        if (my != null)
        {
            return;
        }
        else
        {
            my = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [ClientRpc]
    public void RpcGamePause(bool isCount = true)
    {
        DOTween.PauseAll();
        DOTween.defaultAutoPlay = AutoPlay.None;
    }

    [ClientRpc]
    public void RpcGameNormal()
    {
        DOTween.PlayAll();
        DOTween.timeScale = 1f;
        DOTween.defaultAutoPlay = AutoPlay.All;
    }

    [ClientRpc]
    public void RpcGameAccerlarate()
    {
        DOTween.PlayAll();
        DOTween.timeScale = 2f;
        DOTween.defaultAutoPlay = AutoPlay.All;
    }

    [ClientRpc]
    public void RpcLoadTargetScene(string loadScene)
    {
        Debug.Log("sadasdasdsad");
        SceneManager.LoadScene(loadScene);
    }

    [ClientRpc]
    public void RpcTest()
    {
        Debug.Log("dasds213124eadfadfdsf34321e2");
    }
}
