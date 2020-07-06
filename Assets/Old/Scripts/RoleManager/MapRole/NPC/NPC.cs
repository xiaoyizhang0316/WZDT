using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static GameEnum;

public class NPC : BaseNpc
{

    private Role currentRole;

    public List<string> autoTradeList;

    private void OnMouseEnter()
    {


        //Debug.Log(UIManager.My.Panel_POPInfo.gameObject.activeSelf);
        //if (UIManager.My.Panel_POPInfo.GetComponent<POPRoleManager>().InitPOPRole(gameObject.transform.parent.GetComponent<BaseMapRole>()))
        //{
        //    UIManager.My.Panel_POPInfo.gameObject.SetActive(true);
        //}
    }

    private void OnMouseExit()
    {


        // UIManager.My.Panel_POPInfo.gameObject.SetActive(false);
        //Debug.Log(UIManager.My.Panel_POPInfo.gameObject.activeSelf);

    }

    public void OnMouseUp()
    {
        if (NewCanvasUI.My.NeedRayCastPanel())
        {
            return;
        }
        if (NewCanvasUI.My.isSetTrade && !EventSystem.current.IsPointerOverGameObject())
        {
            NewCanvasUI.My.endRole = GetComponentInParent<BaseMapRole>();
            if (NewCanvasUI.My.endRole.baseRoleData.ID != NewCanvasUI.My.startRole.baseRoleData.ID)
            {
                if (TradeManager.My.CheckTradeCondition())
                {
                    NewCanvasUI.My.InitCreateTradePanel();
                    AudioManager.My.PlaySelectType(GameEnum.AudioClipType.EndTrade);
                }
            }
        }
        else if (!EventSystem.current.IsPointerOverGameObject())
        {
            //NewCanvasUI.My.Panel_RoleInfo.SetActive(true);
            //RoleListInfo.My.Init(currentRole);
            if (isCanSee)
            {
                if (isLock)
                {
                    NPCListInfo.My.ShowUnlckPop(transform);
                }
                else
                {
                    NPCListInfo.My.ShowNpcInfo(transform);
                }
            }
            else if (int.Parse(SceneManager.GetActiveScene().name.Split('_')[1]) > 3)
            {
                NPCListInfo.My.ShowHideTipPop("使用广角镜发现角色");
            }
        }
    }

    public void OnMouseDown()
    {
        if (NewCanvasUI.My.NeedRayCastPanel())
        {
            return;
        }

    }


    public void OnMouseDrag()
    {
        //  if (currentrole == null)
        //  {
        //      return;
        //  }

        //  Vector3 screenpos = Camera.main.WorldToScreenPoint(MoveTF.transform.position ); //物体的世界坐标转化成屏幕坐标  
        //  Vector3 e = Input.mousePosition; //鼠标的位置  
        //  e.z = screenpos.z; //1.因为鼠标的屏幕 Z 坐标的默认值是0，所以需要一个z坐标  
        //  world.x = Camera.main.ScreenToWorldPoint(e).x;
        //  world.z = Camera.main.ScreenToWorldPoint(e).z;
        //  world.y =0;
        //  speed = 1;
        //  MoveTF.position = world;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerData.My.RoleData.Add(GetComponent<BaseMapRole>().baseRoleData);
        PlayerData.My.MapRole.Add(GetComponent<BaseMapRole>());
        currentRole = GetComponentInParent<BaseMapRole>().baseRoleData;
        Invoke("AutoSetTrade",0.2f);
    }

    public void AutoSetTrade()
    {
        if (autoTradeList.Count > 0)
        {
            for (int i = 0; i < autoTradeList.Count; i++)
            {
                TradeManager.My.AutoCreateTrade(currentRole.ID.ToString(), autoTradeList[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
