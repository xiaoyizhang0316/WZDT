using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreatRole_Button : MonoBehaviour, IDragHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector3 world;

    /// <summary>
    /// 角色预制件
    /// </summary>
    public GameObject RolePrb;

    public GameObject role;

    ///// <summary>
    ///// 修改角色
    ///// </summary>
    //public Button modification; 
    ///// <summary>
    ///// 修改角色
    ///// </summary>
    //public Button deleteButton;

    /// <summary>
    /// 窗口开关控制
    /// </summary>
    private bool secondMenuStatus = false;


    // Start is called before the first frame update
    void Start()
    {
        // modification.gameObject.SetActive(secondMenuStatus);

        //deleteButton.gameObject.SetActive(secondMenuStatus); 
        //   modification.onClick.AddListener(() =>
        //   {
        //       Role tempRole = PlayerData.My.GetRoleById(double.Parse(name.Split('_')[1]));
        //       print(tempRole);
        //       UIManager.My.Panel_AssemblyRole.SetActive(true);
        //       CreatRoleManager.My.Open(tempRole);
        //   });

        // deleteButton.onClick.AddListener(() =>
        // {
        //     Role tempRole = PlayerData.My.GetRoleById(double.Parse(name.Split('_')[1]));
        //     UIManager.My.Panel_Confirm.SetActive(true);
        //     string str = "确定要删除该角色吗？";
        //     UIManager.My.Panel_Confirm.GetComponent<ConfirmPanel>().Init(PlayerData.My.DeleteRole, tempRole.ID,str);
        // });
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Vector3 screenpos = Camera.main.WorldToScreenPoint(role.transform.position); //物体的世界坐标转化成屏幕坐标  
        //Vector3 e = eventData.position; //鼠标的位置  
        //e.z = screenpos.z; //1.因为鼠标的屏幕 Z 坐标的默认值是0，所以需要一个z坐标  
        //world.x = Camera.main.ScreenToWorldPoint(e).x;
        //world.z = Camera.main.ScreenToWorldPoint(e).z;
        //world.y = 5; 
        //role.transform.position = new Vector3(world.x, world.y, world.z);
        //Vector3 fwd = role.transform.TransformDirection(Vector3.down);
        //RaycastHit[] hit = Physics.RaycastAll(role.transform.position, fwd, 100);
        //for (int j = 0; j < hit.Length; j++)
        //{
        //    if (hit[j].transform.tag.Equals("MapLand"))
        //    {
        //        role.transform.position = hit[j].transform.position;
        //        int x = hit[j].transform.GetComponent<MapSign>().x;
        //        int y = hit[j].transform.GetComponent<MapSign>().y;
        //        print("x" + x.ToString() + "y" + y.ToString());
        //    }
        //}
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
        //Vector3 screenpos = Camera.main.WorldToScreenPoint(role.transform.position); //物体的世界坐标转化成屏幕坐标  
        //Vector3 e = eventData.position; //鼠标的位置  
        //e.z = screenpos.z; //1.因为鼠标的屏幕 Z 坐标的默认值是0，所以需要一个z坐标  
        //world.x = Camera.main.ScreenToWorldPoint(e).x;
        //world.z = Camera.main.ScreenToWorldPoint(e).z;
        //world.y = 5;
        //role.transform.position = new Vector3(world.x, world.y, world.z);
        //        UIManager.My.LandCube.transform.DOMoveY(-0.2f, 0.5f).SetUpdate(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        secondMenuStatus = !secondMenuStatus;
        //modification.gameObject.SetActive(secondMenuStatus);
        //deleteButton.gameObject.SetActive(secondMenuStatus);
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
        //role.GetComponent<RolePosSign>().ReleaseRole(() =>
        //{


        //    role.GetComponent<BaseMapRole>().AI = true;
        //    GetComponent<Button>().interactable = false;
        //    GetComponent<Image>().raycastTarget = false;
        //    RoleListManager.My.UpdateRoleList();

        //}, () =>
        //{
        //    Debug.Log("失败");
        //    int index = PlayerData.My.MapRole.IndexOf(PlayerData.My.GetMapRoleById(double.Parse(role.name)));
        //    PlayerData.My.MapRole.RemoveAt(index);
        //    GetComponent<Button>().interactable = true;
        //    GetComponent<Image>().raycastTarget = true;
        //    PlayerData.My.GetRoleById(double.Parse(role.name)).inMap = false;
        //  RoleListManager.My.UpdateRoleList();
        //});
    }
}