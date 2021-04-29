using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAssetChange : MonoBehaviour
{
    public GameObject changeObj;
    public Transform parent;

    private Sequence dt_sq;
    private bool isExe = false;

    private Queue<int> cg_q;

    private void Start()
    {
        if (cg_q == null)
        {
            cg_q = new Queue<int>();
        }
        InvokeRepeating("CheckExe", 1, 0.5f);
    }

    void CheckExe()
    {
        if (!isExe && cg_q.Count>0)
        {
            isExe = true;
            QueueExe();
        }
    }

    public void SetChange(int change)
    {
        
        cg_q.Enqueue(change);
    }

    void QueueExe()
    {
        if (cg_q.Count > 0)
        {
            isExe = true;
            SetChange1(cg_q.Dequeue());
        }
        else
        {
            isExe = false;
        }
    }

    private void SetChange1(int change)
    {
        GameObject go = Instantiate(changeObj, parent);
        //go.SetActive(false);
        if (change > 0)
        {
            go.GetComponent<Text>().text = "<color=#93CC24>+" + change + "</color>";
        }
        else
        {
            go.GetComponent<Text>().text = "<color=#D05637>" + change + "</color>";
        }

        Tween te=null;
        te = go.transform.DOLocalMoveY((change / Mathf.Abs(change)) * 30, 0.5f).SetEase(Ease.InExpo).Play().OnComplete(() =>
        {
            Destroy(go);
            QueueExe();
        }).OnPause(()=>te?.TogglePause());
    }
}
