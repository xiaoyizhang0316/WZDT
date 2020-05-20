using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNpc : MonoBehaviour
{
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

    public GameObject lockModel;

    public GameObject trueModel;

    /// <summary>
    /// 解锁npc
    /// </summary>
    /// <returns></returns>
    public bool UnlockNPCRole()
    {
        if (StageGoal.My.playerGold < lockNumber)
            return false;
        StageGoal.My.CostPlayerGold(lockNumber);
        isLock = false;
        lockModel.SetActive(false);
        trueModel.SetActive(true);
        return true;
    }

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<BaseMapRole>().npcScript = this;
        GetComponent<BaseMapRole>().isNpc = true;
        GetComponent<BaseMapRole>().baseRoleData.isNpc = true;
        lockModel.SetActive(true);
        trueModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
