using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameEnum;

public class CreatRole_Button : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerExitHandler,IBeginDragHandler, IEndDragHandler,IPointerEnterHandler
{
    public Vector3 world;

    /// <summary>
    /// 角色预制件
    /// </summary>
    public GameObject RolePrb;

    public GameObject role;

    public RoleType type;

    /// <summary>
    /// 窗口开关控制
    /// </summary>
    private bool secondMenuStatus = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
         
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit[] hit = Physics.RaycastAll(ray);
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.tag.Equals("MapLand"))
                {
                    role.transform.position = hit[i].transform.position + new Vector3(0f, 0f, 0f);
                    break;
                }
            }
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Role tempRole = new Role();
        tempRole.baseRoleData = GameDataMgr.My.GetModelData(type, 1);
        tempRole.ID = CommonData.My.GetTimestamp(DateTime.Now);
        switch (type)
        {
            case RoleType.Seed:
                tempRole.baseRoleData.roleName = StaticRoleName.SeedName[UnityEngine.Random.Range(0, StaticRoleName.SeedName.Length)];
                break;
            case RoleType.Peasant:
                tempRole.baseRoleData.roleName = StaticRoleName.PeasantName[UnityEngine.Random.Range(0, StaticRoleName.PeasantName.Length)];
                break;
            case RoleType.Merchant:
                tempRole.baseRoleData.roleName = StaticRoleName.MerchantName[UnityEngine.Random.Range(0, StaticRoleName.MerchantName.Length)];
                break;
            case RoleType.Dealer:
                tempRole.baseRoleData.roleName = StaticRoleName.DealerName[UnityEngine.Random.Range(0, StaticRoleName.DealerName.Length)];
                break;
        }
        role = Instantiate(RolePrb, NewCanvasUI.My.RoleTF.transform);
        role.name = tempRole.ID.ToString();
        role.GetComponent<BaseMapRole>().baseRoleData = new Role();
        role.GetComponent<BaseMapRole>().baseRoleData = tempRole;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //secondMenuStatus = !secondMenuStatus;
        //NewCanvasUI.My.Panel_Update.SetActive(true);
        //RoleListInfo.My.roleInfo.transform.position =  transform.position+new Vector3(0,4f) ;
        //RoleListInfo.My.Init( PlayerData.My.GetRoleById(double.Parse(name.Split('_')[1])),this);
        //RoleUpdateInfo.My.Init(PlayerData.My.GetRoleById(double.Parse(name.Split('_')[1])));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //UIManager.My.LandCube.transform.DOMoveY(0, 0.5f).SetUpdate(true);
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit[] hit = Physics.RaycastAll(ray);
        bool isSuccess = false;
        for (int j = 0; j < hit.Length; j++)
        {
            if (hit[j].transform.tag.Equals("MapLand"))
            {
                List<int> tempXList = new List<int>();
                List<int> tempYList = new List<int>();
                int x = hit[j].transform.GetComponent<MapSign>().x;
                int y = hit[j].transform.GetComponent<MapSign>().y;
                for (int i = 0; i < role.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.xList.Count; i++)
                {
                    tempXList.Add(x + role.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.xList[i]);
                    tempYList.Add(y + role.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.yList[i]);
                }
                if (MapManager.My.CheckLandAvailable(tempXList, tempYList))
                {
                    print("true ");
                    role.transform.position = hit[j].transform.position;
                    role.GetComponent<BaseMapRole>().baseRoleData.inMap = true;
                    PlayerData.My.RoleData.Add(role.GetComponent<BaseMapRole>().baseRoleData);
                    PlayerData.My.MapRole.Add(role.GetComponent<BaseMapRole>());
                    isSuccess = true;
                    MapManager.My.SetLand(tempXList, tempYList);
                    role.GetComponent<BaseMapRole>().xList = tempXList;
                    role.GetComponent<BaseMapRole>().yList = tempYList;
                }
                else
                {
                    print("false    ");
                    Destroy(role, 0.01f);
                }
                break;
            }
        }
        if (!isSuccess)
        {
            Destroy(role, 0.01f);
        }
         
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
   
       
    }

    public void OnPointerExit(PointerEventData eventData)
    { 
     
    }
}