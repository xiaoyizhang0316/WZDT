using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameEnum;

public class MapSign : MonoBehaviour, IDragHandler
{
    /// <summary>
    /// 地块类型
    /// </summary>
    public MapType mapType;

    /// <summary>
    /// 横坐标
    /// </summary>
    public int x;

    /// <summary>
    /// 纵坐标
    /// </summary>
    public int y;

    /// <summary>
    /// 高度
    /// </summary>
    public int height = 0;

    /// <summary>
    /// 是否可以释放
    /// </summary>
    public bool isCanPlace = true;

    /// <summary>
    /// 停止工作变量（boss技能专用）
    /// </summary>
    public bool lostEffect = false;

    /// <summary>
    /// 增加成本变量（Boss技能专用）
    /// </summary>
    public bool addCost = false;
    
    /// <summary>
    /// 增加成本BuffID
    /// </summary>
    private int addCostBuffId = 998;
  
    /// <summary>
    /// 停止工作BuffID
    /// </summary>
    private int addRangeBuffId = 996;
    
    /// <summary>
    /// 在这个地块上的角色
    /// </summary>
    public BaseMapRole baseMapRole;


    public GameObject lockImage;
    
    public int weighting;
 
    // Start is called before the first frame update
    private void Awake()
    {
        MapManager.My._mapSigns.Add(this);
      //  isCanPlace = GetComponent<MeshRenderer>().enabled && isCanPlace;
   //  x =(int) transform.localPosition.x ;
   //  y =(int) transform.localPosition.y ;
      if (!isCanPlace)
      {
          GetComponent<MeshRenderer>().material.color = new Color(0.2f,0.2f,0.2f,1);
      }
      else
      {
          GetComponent<MeshRenderer>().material.color = Color.white;

            
      }
    }

    /// <summary>
    /// 角色失效
    /// </summary>
    /// <param name="time"></param>
    public void LostEffect(int time)
    {
        lostEffect = true;
        GameObject eff = null;
        transform.DOScale(100f, 3).OnComplete(() =>
        {
            eff = Instantiate(MapManager.My.skillOneEffect, this.transform);
        }).Play();
        transform.DOScale(100f, time).OnComplete(() =>
        {
            Destroy(eff, 0.1f);
            if (baseMapRole != null)
            {
                baseMapRole.transform.GetComponent<BaseSkill>().ReUnleashSkills();
            }
            lostEffect = false;
        }).Play();
    }

    /// <summary>
    /// 增加交易成本
    /// </summary>
    /// <param name="id"></param>
    /// <param name="time"></param>
    public void AddCost(int id, int time)
    {
        addCost = true;
        GameObject eff = null;
        
        transform.DOScale(100f, 3).OnComplete(() =>
        {
            eff = Instantiate(MapManager.My.skillTwoEffect, this.transform);
        }).Play();
        addCostBuffId = id;
        transform.DOScale(100f, time).OnComplete(() =>
        {
            Destroy(eff, 0.1f);
            addCost = false;
        });

    }


    public void OnDrag(PointerEventData eventData)
    {

    }
    void Start()
    {
   
       
        }
 

    public void Unlock()
    {
 
        isCanPlace = true;
        GetComponent<MeshRenderer>().material.color = Color.white;
    }

    /// <summary>
    /// 获取在此地块上的角色
    /// </summary>
    public void GetRoleByLand()
    {
        weighting = 20;
        baseMapRole = null;
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position + new Vector3(0f, 5f, 0f), Vector3.down);
        for (int j = 0; j < hit.Length; j++)
        {
            if (hit[j].transform.CompareTag("MapRole"))
            {
                baseMapRole = hit[j].transform.GetComponentInParent<BaseMapRole>();
                weighting = baseMapRole.baseRoleData.riskResistance;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
     //   GetRoleByLand();
        if (lostEffect)
        {
            if (lostEffect && baseMapRole != null)
            {
                baseMapRole.transform.GetComponent<BaseSkill>().CancelSkill();

            }
        }
        if (addCost)
        {
            if (baseMapRole != null)
            {
                var buff = GameDataMgr.My.GetBuffDataByID(addCostBuffId);
                BaseBuff baseb = new BaseBuff();
                baseb.Init(buff);
                baseb.SetRoleBuff(baseMapRole, baseMapRole, baseMapRole);
            }
        }
        if (height >= 1)
        {
            if (baseMapRole != null && baseMapRole.baseRoleData.inMap)
            {
                var buff = GameDataMgr.My.GetBuffDataByID(addRangeBuffId);
                BaseBuff baseb = new BaseBuff();
                baseb.Init(buff);
                baseb.SetRoleBuff(baseMapRole, baseMapRole, baseMapRole);
            }
        }
    }
}
