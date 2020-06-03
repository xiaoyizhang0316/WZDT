using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNpc : MonoBehaviour
{
    /// <summary>
    /// 是否激活
    /// </summary>
    public bool isLock;
    
    /// <summary>
    /// 广角镜检测
    /// </summary>
    public bool isCanSee;

    /// <summary>
    /// 多棱镜检测
    /// </summary>
    public bool isCanSeeEquip;

    /// <summary>
    /// 解锁花费金钱
    /// </summary>
    public int lockNumber;

    public GameObject hideModel;

    public GameObject trueModel;

    /// <summary>
    /// 激活npc
    /// </summary>
    /// <returns></returns>
    public bool UnlockNPCRole()
    {
        if (StageGoal.My.playerGold < lockNumber)
            return false;
        StageGoal.My.CostPlayerGold(lockNumber);
        isLock = false;
        return true;
    }

    /// <summary>
    /// 广角镜解锁NPC
    /// </summary>
    public void DetectNPCRole()
    {
        isCanSee = true;
        hideModel.SetActive(false);
        trueModel.SetActive(true);
    }

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<BaseMapRole>().npcScript = this;
        GetComponent<BaseMapRole>().isNpc = true;
        GetComponent<BaseMapRole>().baseRoleData.isNpc = true;
        GetComponent<BaseMapRole>().baseRoleData.inMap = true;
        if (isCanSee)
        {
            hideModel.SetActive(false);
            trueModel.SetActive(true);
        }
        else
        {
            hideModel.SetActive(true);
            trueModel.SetActive(false);
        }
        gameObject.name = GetComponent<BaseMapRole>().baseRoleData.ID.ToString();
        //Invoke("UnlockNPCRole", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
