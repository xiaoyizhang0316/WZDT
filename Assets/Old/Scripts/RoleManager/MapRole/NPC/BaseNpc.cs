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

    /// <summary>
    /// NPC装备
    /// </summary>
    public List<int> NPCgear = new List<int>();

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
        StageGoal.My.Expend(lockNumber, ExpendType.AdditionalCosts, null, "激活NPC");
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

    /// <summary>
    /// 初始化设置地块占用
    /// </summary>
    public void InitSetLand()
    {
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position + new Vector3(0f, 5f, 0f), Vector3.down);
        for (int j = 0; j < hit.Length; j++)
        {
            if (hit[j].transform.tag.Equals("MapLand"))
            {
                print(hit[j].transform);
                List<int> xlist = new List<int>();
                List<int> ylist = new List<int>();
                xlist.Add(hit[j].transform.GetComponent<MapSign>().x);
                ylist.Add(hit[j].transform.GetComponent<MapSign>().y);
                MapManager.My.SetLand(xlist, ylist);
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<BaseMapRole>().npcScript = this;
        GetComponent<BaseMapRole>().isNpc = true;
        GetComponent<BaseMapRole>().baseRoleData.isNpc = true;
        GetComponent<BaseMapRole>().baseRoleData.inMap = true;
        for (int i = 0; i < NPCgear.Count; i++)
        {
            GetComponent<BaseMapRole>().baseRoleData.EquipList.Add(NPCgear[i], Vector3.zero);
        }
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
