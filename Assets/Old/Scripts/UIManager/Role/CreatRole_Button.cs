using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameEnum;

public class CreatRole_Button : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler
{
    public Vector3 world;

    /// <summary>
    /// 角色预制件
    /// </summary>
    public GameObject RolePrb;

    public GameObject role;

    public RoleType type;

    public GameObject portalPrb;

    public GameObject dustPrb;

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
                    role.transform.position = hit[i].transform.position + new Vector3(0f, 0.3f, 0f);
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
                int x = hit[j].transform.GetComponent<MapSign>().x;
                int y = hit[j].transform.GetComponent<MapSign>().y;
                if (MapManager.My.CheckLandAvailable(x, y) && StageGoal.My.CostTechPoint(role.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.costTech))
                {
                    print("true ");
                    StageGoal.My.CostTp(role.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.costTech, CostTpType.Build);
                    role.transform.position = hit[j].transform.position + new Vector3(0f, 2f, 0f);
                    role.transform.DOMove(hit[j].transform.position + new Vector3(0f,0.3f,0f), 0.2f).OnComplete(() =>
                    {
                        GameObject go = Instantiate(dustPrb, role.transform);
                        Destroy(go, 1f);
                        role.transform.DOScale(new Vector3(1.3f, 0.8f, 1.3f), 0.2f).OnComplete(() =>
                        {
                            role.transform.DOScale(1f, 0.15f).Play();
                        }).Play();
                    }).SetEase(Ease.Linear).Play();
                    role.GetComponent<BaseMapRole>().baseRoleData.inMap = true;
                    PlayerData.My.RoleData.Add(role.GetComponent<BaseMapRole>().baseRoleData);
                    PlayerData.My.MapRole.Add(role.GetComponent<BaseMapRole>());
                    isSuccess = true;
                    MapManager.My.SetLand(x, y);
                    role.GetComponent<BaseMapRole>().posX = x;
                    role.GetComponent<BaseMapRole>().posY = y;
                    role.GetComponent<BaseMapRole>().MonthlyCost();
                    role.GetComponent<BaseMapRole>().AddTechPoint();
                    CreateRoleOperationRecord(role.GetComponent<BaseMapRole>());
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

    /// <summary>
    /// 记录创建角色的操作
    /// </summary>
    /// <param name="mapRole"></param>
    public void CreateRoleOperationRecord(BaseMapRole mapRole)
    {
        List<string> param = new List<string>();
        param.Add(mapRole.baseRoleData.ID.ToString());
        param.Add(mapRole.baseRoleData.baseRoleData.roleName);
        param.Add(mapRole.baseRoleData.baseRoleData.roleType.ToString());
        StageGoal.My.RecordOperation(OperationType.PutRole, param);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {


    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}