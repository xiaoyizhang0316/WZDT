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

    public Color addColor=new Color32((byte)147,(byte)204,(byte)36,(byte)255);
    public Color lostColor=new Color32((byte)208,(byte)86,(byte)55, (byte)255);

    private Sequence dt_sq;
    private bool isExe = false;
    private static readonly object lockRoot = new object();

    //private Queue<int> cg_q;

    private Queue<QueueData> q_data;

    private void Start()
    {
        if (q_data == null)
        {
            q_data = new Queue<QueueData>();
        }
        InvokeRepeating("CheckExe", 1, 0.2f);
    }

    void CheckExe()
    {
        if (!isExe && q_data.Count>0)
        {
            lock (lockRoot)
            {
                if (!isExe && q_data.Count > 0)
                {
                    isExe = true;
                    QueueExe();
                }
            }
        }
    }

    public void SetChange(int change)
    {
        //cg_q.Enqueue(change);
        q_data.Enqueue(new QueueData(change, TimeStamp.GetCurrentTimeStamp()));
    }

    void QueueExe()
    {
        CancelInvoke("CheckExe");
        if (q_data.Count > 0)
        {
            isExe = true;
            //SetChange1(cg_q.Dequeue());
            QueueData qd = q_data.Dequeue();
            int time = qd.enqueue_time;
            int total_change = qd.queue_val;
            while (q_data.Count>0&& q_data.Peek().enqueue_time==time)
            {
                total_change += q_data.Dequeue().queue_val;
            }
            SetChange1(total_change);
        }
        else
        {
            isExe = false;
            InvokeRepeating("CheckExe", 0.2f, 0.2f);
        }
    }

    private void SetChange1(int change)
    {
        GameObject go = Instantiate(changeObj, parent);
        if (change == 0)
        {
            go.SetActive(false);
        }
        //go.SetActive(false);
        if (change > 0)
        {
            go.GetComponent<Text>().text = "<color=#93CC24>+" + change + "</color>";
            go.GetComponent<Text>().color = addColor;
        }
        else
        {
            go.GetComponent<Text>().text = "<color=#D05637>" + change + "</color>";
            go.GetComponent<Text>().color = lostColor;
        }

        Tween te=null;
        te = go.transform.DOLocalMoveY((change / (Mathf.Abs(change)==0?1:Mathf.Abs(change))) * 30, 0.5f).SetEase(Ease.InExpo).Play().OnComplete(() =>
        {
            Destroy(go);
            QueueExe();
        }).OnPause(()=>te?.TogglePause());
    }
}

public class QueueData
{
    public int queue_val;
    public int enqueue_time;

    public QueueData(int queueVal, int enqueueTime)
    {
        queue_val = queueVal;
        enqueue_time = enqueueTime;
    }
}
