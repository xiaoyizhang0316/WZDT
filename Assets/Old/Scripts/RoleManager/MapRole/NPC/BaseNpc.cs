using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameEnum;

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
    /// NPC提供的buffList(多棱镜)
    /// </summary>
    public List<int> NPCBuffList = new List<int>();

    public GameObject hideModel;

    public GameObject trueModel;

    /// <summary>
    /// 激活npc
    /// </summary>
    /// <returns></returns>
    public bool UnlockNPCRole()
    {
        if (StageGoal.My.CostTechPoint(lockNumber))
        {
            isLock = false;
            StageGoal.My.CostTp(lockNumber, CostTpType.Unlock);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 广角镜解锁NPC
    /// </summary>
    public void DetectNPCRole()
    {
        isCanSee = true;
        hideModel.SetActive(false);
        trueModel.SetActive(true);
        GetComponent<BaseMapRole>().CheckLevel();
    }

    /// <summary>
    /// 使用多棱镜
    /// </summary>
    public void UseDLJ()
    {
        float add = 1.77f;
        if (GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleSkillType != RoleSkillType.Product)
        {
            add *= 2;
        }
        for (int i = 0; i < NPCBuffList.Count; i++)
        {
            BuffData tempData = GameDataMgr.My.GetBuffDataByID(NPCBuffList[i]);
            GetComponent<BaseMapRole>().baseRoleData.tradeCost += (int)(tempData.buffValue * add);
        }
        GetComponent<BaseMapRole>().RecheckDLJBuff();
    }

    //public float h;

    //public float s;

    //public float v;

    //private Color color = Color.HSVToRGB(0.1736111f, 1f, 0.4433962f);

    /// <summary>
    /// 初始化设置地块占用
    /// </summary>
    public void InitSetLand()
    {
        if (SceneManager.GetActiveScene().name.Equals("FTE_0")|| SceneManager.GetActiveScene().name.Equals("FTE_0-1") || SceneManager.GetActiveScene().name.Equals("FTE_Record"))
            return;
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position + new Vector3(0f, 5f, 0f), Vector3.down);
        for (int j = 0; j < hit.Length; j++)
        {
            if (hit[j].transform.tag.Equals("MapLand"))
            {
                //print(hit[j].transform);
                MapManager.My.SetLand(hit[j].transform.GetComponent<MapSign>().x, hit[j].transform.GetComponent<MapSign>().y);
                transform.position = hit[j].transform.position + new Vector3(0f, 0.3f, 0f);
            }
        }
        //Color.RGBToHSV(color, out h, out s, out v);
        //GetComponentInChildren<MeshRenderer>().material.EnableKeyword("_EMISSION");
        //GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", Color.HSVToRGB(h,s,v));


    }

    // Start is called before the first frame update
    void Awake()
    {
        BaseInit();
        if(PlayerData.My.yeWuXiTong[0])
        {
            lockNumber = lockNumber * 90 / 100;
        }
    }

    public void BaseInit()
    {
        GetComponent<BaseMapRole>().npcScript = this;
        GetComponent<BaseMapRole>().isNpc = true;
        GetComponent<BaseMapRole>().baseRoleData.isNpc = true;
        GetComponent<BaseMapRole>().baseRoleData.inMap = true;
        if (isCanSee)
        {
            hideModel.SetActive(false);
            trueModel.SetActive(true);
            GetComponent<BaseMapRole>().CheckLevel();
        }
        else
        {
            hideModel.SetActive(true);
            trueModel.SetActive(false);
        }
        Invoke("InitSetLand", 0.5f);
        if (SceneManager.GetActiveScene().name.Equals("FTE_0") || SceneManager.GetActiveScene().name.Equals("FTE_0-1") || SceneManager.GetActiveScene().name.Equals("FTE_Record"))
            return;
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position + new Vector3(0f, 5f, 0f), Vector3.down);
        for (int j = 0; j < hit.Length; j++)
        {
            if (hit[j].transform.tag.Equals("MapLand"))
            {
                //print(hit[j].transform);
                transform.position = hit[j].transform.position + new Vector3(0f, 0.3f, 0f);
            }
        }
    }

}