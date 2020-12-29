using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;

public class SoftFTE : MonoSingleton<SoftFTE>
{

    public List<SoftFTE_Base> softList;

    public int currentIndex;

    public GameObject softFTEObj;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="type"></param>
    public void Init(RoleType type)
    {
        currentIndex = 0;
        for (int i = 0; i < softList.Count; i++)
        {
            softList[i].Init(type);
        }
        SoftFTEStart();
    }

    public void InitMap(RoleType type)
    {
        currentIndex = 0;
        for (int i = 0; i < softList.Count; i++)
        {
            softList[i].Init(type);
        }
        SoftFTEMapStart();
    }

    /// <summary>
    /// 执行下一步
    /// </summary>
    public void PlayNext()
    {
        if (currentIndex == softList.Count)
        {
            SoftFTEEnd();
        }
        else
        {
            softList[currentIndex].gameObject.SetActive(true);
            softList[currentIndex].Play();
            currentIndex++;
        }
    }

    /// <summary>
    /// 软引导开始
    /// </summary>
    public void SoftFTEStart()
    {
        NewCanvasUI.My.GamePause(false);
        softFTEObj.SetActive(true);
        CameraPlay.WidescreenH_ON(Color.black, 1);
        Invoke("PlayNext", 1f);
    }

    public void SoftFTEMapStart()
    {
        softFTEObj.SetActive(true);
        CameraPlay.WidescreenH_ON(Color.black, 1);
        Invoke("PlayNext", 1f);
    }

    /// <summary>
    /// 软引导结束
    /// </summary>
    public void SoftFTEEnd()
    {
        CameraPlay.WidescreenH_OFF();
        transform.DOScale(transform.localScale, 1).OnComplete(() =>
        {
            //NewCanvasUI.My.GameNormal();
            softFTEObj.SetActive(false);
        }).Play();
        NetworkMgr.My.UpdateRoleFound();
    }

    /// <summary>
    /// 检测是否是第一次解开角色
    /// </summary>
    /// <param name="type"></param>
    public void CheckUnlockNewRole(RoleType type)
    {
        if (NetworkMgr.My.roleFoundDic.ContainsKey(type))
        {
            print("find type: " + type);
            if (NetworkMgr.My.roleFoundDic[type] == 0)
            {
                Init(type);
                NetworkMgr.My.roleFoundDic[type] = 1;
            }
        }
    }

    /// <summary>
    /// 测试用例
    /// </summary>
    public void Test()
    {
        Init(RoleType.Bank);
    }
}
