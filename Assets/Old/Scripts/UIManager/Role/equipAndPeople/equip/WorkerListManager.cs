using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class WorkerListManager : MonoSingleton<WorkerListManager>
{
    public  List<WorkerSign> _signs= new List<WorkerSign>();

    public Transform tempPos;

    public Transform infoPos;
    public Transform content;
    /// <summary>
    /// 初始化，从角色物品栏中赋值
    /// </summary>
    public void Init()
    {
        
        for (int i = 0; i <_signs.Count; i++)
        {
            Destroy(_signs[0].gameObject);
        }
        foreach (PlayerWorker p in PlayerData.My.playerWorkers)
        {
            //CreatRoleManager.My.equipPrb
            GameObject go = Instantiate(workerPrb, transform.Find("Viewport/Content").position, transform.Find("Viewport/Content").rotation, transform.Find("Viewport/Content"));
            //go.transform.SetParent(transform.Find("Viewport/Content"));
            go.transform.GetComponent<WorkerSign>().Init(p.WorkerId,p.isEquiped);
        }
        foreach (WorkerSign w in _signs)
        {
            if (w.isOccupation)
                w.CheckOccupyStatus();
        }
    //    if (GuideManager.My.ftegob.activeSelf)
    //    {
    //        for (int i = 0; i < transform.Find("Viewport/Content").childCount; i++)
    //        {
    //            if (     transform.Find("Viewport/Content").GetChild(i).GetComponent<WorkerSign>().ID !=10001 )
    //            {
    //                transform.Find("Viewport/Content").GetChild(i).gameObject.SetActive(false);
    //            }
    //        }
    //    }
    }

    public void ReInit()
    {
        for (int i = 0; i <_signs.Count; i++)
        {
            _signs[i].transform.SetParent(tempPos);
        }

        for (int i = 0; i <_signs.Count; i++)
        {
            _signs[i].transform.SetParent(transform.Find("Viewport/Content"));
        }

        transform.Find("Viewport/Content").DOLocalMoveY(0, 0.5f).Play();
    }
    /// <summary>
    /// 将所有工人的占用情况更新到角色物品栏中
    /// </summary>
    public void QuitAndSave()
    {
        foreach (WorkerSign w in _signs)
        {
            PlayerData.My.SetWorkerStatus(w.ID, w.isOccupation);
        }
    }

    /// <summary>
    /// 将角色身上的工人脱下
    /// </summary>
    /// <param name="id"></param>
    public void UninstallWorker(int id)
    {
        foreach (WorkerSign w in _signs)
        {
            if (w.ID == id)
                w.SetOccupyStatus(false);
        }
    }

    public Transform workerPos;

    public GameObject workerPrb;
}
