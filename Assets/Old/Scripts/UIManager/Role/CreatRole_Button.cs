using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameEnum;

public class CreatRole_Button : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerExitHandler,
    IBeginDragHandler, IEndDragHandler, IPointerEnterHandler
{
    public Vector3 world;

    /// <summary>
    /// 角色预制件
    /// </summary>
    public GameObject RolePrb;

    public GameObject role;

    public RoleType type;

    public GameObject dustPrb;

    public Text techCost;

    private int costTech;

    private Image dragImg;

    /// <summary>
    /// 窗口开关控制
    /// </summary>
    private bool secondMenuStatus = false;


    // Start is called before the first frame update
    void Start()
    {
        if (BaseLevelController.My.stageSpecialTypes.Contains(StageSpecialType.RoleNoMega))
        {
            costTech = 0;
            techCost.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            costTech = GameDataMgr.My.GetModelData(type, 1).costTech;
            dragImg = transform.Find("Image").GetComponent<Image>();
        }

    }

    public void ReadCostTech(int cost = -1)
    {
        if (cost == -1)
        {
            costTech = GameDataMgr.My.GetModelData(type, 1).costTech;
        }
        else
        {
            costTech = cost;
            GameDataMgr.My.GetModelDataFTE(type, 1).costTech = cost;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (techCost != null)
        {
            techCost.text = costTech.ToString();


            if (StageGoal.My.playerTechPoint < costTech)
            {
                techCost.color = Color.red;
            }
            else
            {
                techCost.color = Color.white;
            }
        }

        if (Input.GetMouseButton(1))
        {
            Destroy(role, 0f);
            role = null;
            RoleFloatWindow.My.Hide();
            if (dragImg != null)
                dragImg.raycastTarget = true;
        }
    }

    private HexCell currentCell;
    public void OnDrag(PointerEventData eventData)
    {
        if (role == null)
            return;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            PlayerData.My.MapRole[i].transform.localScale = Vector3.one;
        }

     
        if (NewCanvasUI.My.isSetTrade)
            return;
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {

            if (currentCell != null)
            {
                currentCell.DisableHighlight();
            }

              currentCell = HexGrid.My.GetCell(hit.point);
            if (currentCell.TerrainTypeIndex == 1)
            {
                currentCell.EnableHighlight(Color.green);

             }
            else
            {
                currentCell.EnableHighlight(Color.red);
                
            }

            role.transform.position =currentCell.Position+ new Vector3(0, 0.3f, 0);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
  

        if (Input.GetMouseButton(1))
            return;
        if (NewCanvasUI.My.isSetTrade)
            return;
        Role tempRole = new Role();
        tempRole.baseRoleData = GameDataMgr.My.GetModelData(type, 1);
        tempRole.ID = CommonData.My.GetTimestamp(DateTime.Now);
        switch (type)
        {
            case RoleType.Seed:
                tempRole.baseRoleData.roleName =
                    StaticRoleName.SeedName[UnityEngine.Random.Range(0, StaticRoleName.SeedName.Length)];
                DataUploadManager.My.AddData(DataEnum.角色_放置种子商);

                break;
            case RoleType.Peasant:
                tempRole.baseRoleData.roleName =
                    StaticRoleName.PeasantName[UnityEngine.Random.Range(0, StaticRoleName.PeasantName.Length)];
                DataUploadManager.My.AddData(DataEnum.角色_放置农民);

                break;
            case RoleType.Merchant:
                tempRole.baseRoleData.roleName =
                    StaticRoleName.MerchantName[UnityEngine.Random.Range(0, StaticRoleName.MerchantName.Length)];
                DataUploadManager.My.AddData(DataEnum.角色_放置贸易商);

                break;
            case RoleType.Dealer:
                tempRole.baseRoleData.roleName =
                    StaticRoleName.DealerName[UnityEngine.Random.Range(0, StaticRoleName.DealerName.Length)];
                DataUploadManager.My.AddData(DataEnum.角色_放置零售商);

                break;
        }
        role = Instantiate(RolePrb, NewCanvasUI.My.RoleTF.transform);
        role.name = tempRole.ID.ToString();
        role.GetComponent<BaseMapRole>().baseRoleData = new Role();
        role.GetComponent<BaseMapRole>().baseRoleData = tempRole;
        role.GetComponent<BaseMapRole>().HideTradeButton(false);
        if (dragImg != null)
            dragImg.raycastTarget = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
         
        if (Input.GetMouseButton(1))
            return;
        if (role == null)
            return;
        //UIManager.My.LandCube.transform.DOMoveY(0, 0.5f).SetUpdate(true);
        bool isSuccess = false;

        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            HexCell currentCell = HexGrid.My.GetCell(hit.point);
           
            if (currentCell!=null)
            {
                currentCell.DisableHighlight();
                for (int i = 0; i < currentCell.neighbors.Length; i++)
                {
                    currentCell.neighbors[i].DisableHighlight();
                }
                int x =currentCell.GetComponent<MapSign>().x;
                int y = currentCell.GetComponent<MapSign>().y;
                if (MapManager.My.CheckLandAvailable(x, y) &&
                    StageGoal.My.CostTechPoint(costTech))
                {
                    print("true ");
                    role.GetComponent<BaseMapRole>().putTime = StageGoal.My.timeCount;
                    StageGoal.My.CostTp(costTech,
                        CostTpType.Build);
                    role.transform.position =currentCell.Position + new Vector3(0f, 2f, 0f);
                    role.transform.DOMove(currentCell.Position + new Vector3(0f, 0f, 0f), 0.2f).OnComplete(() =>
                        {
                            GameObject go = Instantiate(dustPrb, role.transform);
                            Destroy(go, 1f);
                            role.transform.DOScale(new Vector3(1.3f, 0.8f, 1.3f), 0.2f).OnComplete(() =>
                                {
                                    role.transform.DOScale(1f, 0.15f).Play().OnComplete(() =>
                                        {
                                            role = null;
                                            if (dragImg != null)
                                                dragImg.raycastTarget = true;
                                        }).timeScale = 1f / DOTween.timeScale;
                                }).Play().timeScale = 1f / DOTween.timeScale;
                        }).SetEase(Ease.Linear).Play().timeScale = 1f / DOTween.timeScale;
                    role.GetComponent<BaseMapRole>().costTechPoint = costTech;
                    role.GetComponent<BaseMapRole>().baseRoleData.inMap = true;
                    PlayerData.My.RoleData.Add(role.GetComponent<BaseMapRole>().baseRoleData);
                    PlayerData.My.MapRole.Add(role.GetComponent<BaseMapRole>());
                    isSuccess = true;
                    MapManager.My.SetLand(x, y, role.GetComponent<BaseMapRole>());
                    role.GetComponent<BaseMapRole>().posX = x;
                    role.GetComponent<BaseMapRole>().posY = y;
                    role.GetComponent<BaseMapRole>().MonthlyCost();
                    role.GetComponent<BaseMapRole>().AddTechPoint();
                    role.GetComponent<BaseMapRole>().HideTradeButton(NewCanvasUI.My.isTradeButtonActive);
                    if (!CommonParams.fteList.Contains(SceneManager.GetActiveScene().name)
                    )
                    {
                        BaseLevelController.My.CountPutRole(role.GetComponent<BaseMapRole>().baseRoleData);
                    }
                    PlayerData.My.RoleCountStatic(role.GetComponent<BaseMapRole>(), 1);
                    CreateRoleOperationRecord(role.GetComponent<BaseMapRole>());
                    if (!PlayerData.My.isSOLO)
                    {
                        string str = "CreateRole|";
                        str += role.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleType.ToString() + ",";
                        str += role.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleName + ",";
                        str += role.GetComponent<BaseMapRole>().baseRoleData.ID.ToString() + ",";
                        str += x.ToString() + "," + y.ToString();
                        if (PlayerData.My.isServer)
                        {
                            PlayerData.My.server.SendToClientMsg(str);
                        }
                        else
                        {
                            PlayerData.My.client.SendToServerMsg(str);
                        }
                    }
                }
                else
                {
                    print("false ");
                    Destroy(role, 0.01f);
                    RoleFloatWindow.My.Hide();
                    if (dragImg != null)
                        dragImg.raycastTarget = true;
                }

            
            }
        }


        if (!isSuccess)
        {
            if (dragImg != null)
                dragImg.raycastTarget = true;
            RoleFloatWindow.My.Hide();
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
        param.Add("FALSE");
        param.Add(mapRole.baseRoleData.baseRoleData.roleName);
        param.Add(mapRole.baseRoleData.baseRoleData.roleType.ToString());
        param.Add(mapRole.baseRoleData.baseRoleData.level.ToString());
        StageGoal.My.RecordOperation(OperationType.PutRole, param);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string desc = GameDataMgr.My.GetTranslateName(type.ToString());
        RoleFloatWindow.My.Init(transform, desc, RoleSkillType.Product, type);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RoleFloatWindow.My.Hide();
    }
}