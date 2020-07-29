using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameEnum;

public class MapSign : MonoBehaviour,IDragHandler
{
    public MapType mapType;

    public int x;

    public int y;

    public int height = 0;

    public bool isCanPlace = true;

    public bool lostEffect = false;

    public bool addCost = false;
    public int addCostBuffId;
    public BaseMapRole baseMapRole;
    // Start is called before the first frame update
    private void Awake()
    {
         MapManager.My._mapSigns.Add(this);
         isCanPlace = GetComponent<MeshRenderer>().enabled && isCanPlace;
    }

    public void LostEffect(int time)
    {
        lostEffect = true;
        transform.DOScale(100f, time).OnComplete(() =>
        {
            if (baseMapRole != null)
            {
                baseMapRole.transform.GetComponent<BaseSkill>().ReUnleashSkills();
                lostEffect = false;
            }

      
        });

    }

    public void AddCost(int id,int time )
    {
        
        addCost = true;

        addCostBuffId = id;
        transform.DOScale(100f, time).OnComplete(() => { addCost = false; });
    }

 
    public void OnDrag(PointerEventData eventData)
    {
  
    }
    void Start()
    {
        if (mapType == MapType.Road && MapManager.My.generatePath)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            go.transform.position = transform.position + new Vector3(0f,0.15f,0f);
            go.transform.SetParent(transform.parent.parent);
            //go.GetComponent<MeshCollider>().enabled = false;
        }
    }

    public void  GetRoleByLand()
    {
        baseMapRole = null;
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position + new Vector3(0f, -5f, 0f), Vector3.up);
        for (int j = 0; j < hit.Length; j++)
        {
            if (hit[j].transform.tag.Equals("MapRole"))
            {
                baseMapRole = hit[j].transform.GetComponent<BaseMapRole>();
            }
        }

    
    }

    // Update is called once per frame
    void Update()
    {
       GetRoleByLand();
       if (lostEffect)
       {
       
           if (lostEffect&&baseMapRole!=null)
           {
               baseMapRole.transform.GetComponent<BaseSkill>().CancelSkill();
           }
       }

       if (addCost)
       {
           if (baseMapRole != null)
           { 
               var buff = GameDataMgr.My.GetBuffDataByID(addCostBuffId );
               BaseBuff baseb = new BaseBuff();
               baseb.Init(buff);
               baseb.SetRoleBuff(baseMapRole, baseMapRole, baseMapRole);
               
           }
       }
    }
       
}
