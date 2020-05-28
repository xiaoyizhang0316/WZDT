using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreatRole_Button : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerExitHandler,IBeginDragHandler, IEndDragHandler,IPointerEnterHandler
{
    public Vector3 world;

    /// <summary>
    /// 角色预制件
    /// </summary>
    public GameObject RolePrb;

    public GameObject role;

   

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
        role = Instantiate(RolePrb, NewCanvasUI.My.RoleTF.transform);
        role.name = name.Split('_')[1];
      
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        secondMenuStatus = !secondMenuStatus; 
        RoleListInfo.My.roleInfo.transform.position =  transform.position+new Vector3(0,4f) ;
        RoleListInfo.My.Init( PlayerData.My.GetRoleById(double.Parse(name.Split('_')[1])),this);
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
                    print("x" + x.ToString() + "y" + y.ToString());
                    print(hit[j].transform.position);
                    PlayerData.My.GetRoleById(double.Parse(name.Split('_')[1])).inMap = true;
                    PlayerData.My.MapRole.Add(role.GetComponent<BaseMapRole>());
                    RoleListManager.My.UpdateRoleList();
                    isSuccess = true;
                    GetComponent<Image>().raycastTarget = false;
                    MapManager.My.SetLand(tempXList, tempYList);
                }
                else
                {
                    print("false    ");
                    GetComponent<Image>().raycastTarget = true;
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