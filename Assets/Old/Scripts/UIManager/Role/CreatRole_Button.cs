using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class CreatRole_Button : MonoBehaviour, IDragHandler, IPointerClickHandler,IBeginDragHandler,IEndDragHandler
{
    public Vector3 world;

    /// <summary>
    /// 角色预制件
    /// </summary>
    public GameObject RolePrb;

    private GameObject role;

    /// <summary>
    /// 修改角色
    /// </summary>
    public Button modification; 
    /// <summary>
    /// 修改角色
    /// </summary>
    public Button deleteButton;

    /// <summary>
    /// 窗口开关控制
    /// </summary>
    private bool secondMenuStatus = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        modification.gameObject.SetActive(secondMenuStatus);
        
        deleteButton.gameObject.SetActive(secondMenuStatus); 
        modification.onClick.AddListener(() =>
        {
            Role tempRole = PlayerData.My.GetRoleById(double.Parse(name.Split('_')[1]));
            print(tempRole);
            UIManager.My.Panel_AssemblyRole.SetActive(true);
            CreatRoleManager.My.Open(tempRole);
        });
        
        deleteButton.onClick.AddListener(() =>
        {
            Role tempRole = PlayerData.My.GetRoleById(double.Parse(name.Split('_')[1]));
            UIManager.My.Panel_Confirm.SetActive(true);
            string str = "确定要删除该角色吗？";
            UIManager.My.Panel_Confirm.GetComponent<ConfirmPanel>().Init(PlayerData.My.DeleteRole, tempRole.ID,str);
        });
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 screenpos = Camera.main.WorldToScreenPoint(role.transform.position); //物体的世界坐标转化成屏幕坐标  
        Vector3 e = eventData.position; //鼠标的位置  
        e.z = screenpos.z; //1.因为鼠标的屏幕 Z 坐标的默认值是0，所以需要一个z坐标  
        world.x = Camera.main.ScreenToWorldPoint(e).x;
        world.z = Camera.main.ScreenToWorldPoint(e).z;
        world.y = 0; 
        role.transform.position = new Vector3(world.x, world.y, world.z);
    }



   

    public void OnBeginDrag(PointerEventData eventData)
    {
        role = Instantiate(RolePrb, CommonData.My.RoleTF.transform);
        role.name = name.Split('_')[1];
     
        PlayerData.My.GetRoleById(  double.Parse(name.Split('_')[1])   ) .inMap = true;
        PlayerData.My.MapRole.Add(role.GetComponent<BaseMapRole>());

        UIManager.My.LandCube.transform.DOMoveY(-0.2f, 0.5f).SetUpdate(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        secondMenuStatus = !secondMenuStatus;
        modification.gameObject.SetActive(secondMenuStatus);
        
        deleteButton.gameObject.SetActive(secondMenuStatus);


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIManager.My.LandCube.transform.DOMoveY(0, 0.5f).SetUpdate(true);

        role.GetComponent<RolePosSign>().ReleaseRole(() =>
        {
          
          
            role.GetComponent<BaseMapRole>().AI = true;
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().raycastTarget = false;
            UIManager.My.UpdateRoleList();

        }, () =>
        {
            Debug.Log("失败");
            int index = PlayerData.My.MapRole.IndexOf(PlayerData.My.GetMapRoleById(double.Parse(role.name)));
            PlayerData.My.MapRole.RemoveAt(index);
            GetComponent<Button>().interactable = true;
            GetComponent<Image>().raycastTarget = true;
            PlayerData.My.GetRoleById(double.Parse(role.name)).inMap = false;
            UIManager.My.UpdateRoleList();
        });
    }
}