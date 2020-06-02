using System;
using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine.EventSystems;
using static GameEnum;

//通过射线控制物体贴地面移动
public class RoleDrag : MonoBehaviour
{
    private Role currentRole;

    private void Start()
    {
        currentRole = GetComponentInParent<BaseMapRole>().baseRoleData;
    }

    /// <summary>
    /// 检测发起者和承受者技能类型
    /// </summary>
    /// <returns></returns>
    public bool CheckStartAndEnd()
    {
        if (NewCanvasUI.My.startRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service &&
            NewCanvasUI.My.endRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
            return false;
        else
            return true;
    }

    /// <summary>
    /// 检测npc是否激活
    /// </summary>
    /// <returns></returns>
    public bool CheckNpcActive()
    {
        BaseMapRole start = PlayerData.My.GetMapRoleById(NewCanvasUI.My.startRole.baseRoleData.ID);
        BaseMapRole end = PlayerData.My.GetMapRoleById(NewCanvasUI.My.endRole.baseRoleData.ID);
        if (start.isNpc)
        {
            if (start.GetComponent<BaseNpc>().isLock)
                return false;
        }
        if (end.isNpc)
        {
            if (end.GetComponent<BaseNpc>().isLock)
                return false;
        }
        return true;
    }

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
        if (NewCanvasUI.My.isSetTrade)
        {
            NewCanvasUI.My.endRole = GetComponentInParent<BaseMapRole>();
            if (NewCanvasUI.My.endRole.baseRoleData.ID != NewCanvasUI.My.startRole.baseRoleData.ID)
            {
                if (CheckStartAndEnd() && CheckNpcActive())
                {
                    //UIManager.My.Panel_CreateTrade.SetActive(true);
                    NewCanvasUI.My.InitCreateTradePanel();
                }
            }
        }
        else
        {
            NewCanvasUI.My.Panel_RoleInfo.SetActive(true);
            RoleListInfo.My.Init(currentRole);
        }
        //if (NewCanvasUI.My.isSetTrade)
        //{
        //    if (NewCanvasUI.My.endRole.baseRoleData.ID != NewCanvasUI.My.startRole.baseRoleData.ID)
        //    {
        //        if (CheckStartAndEnd() && CheckNpcActive())
        //        {
        //            //UIManager.My.Panel_CreateTrade.SetActive(true);
        //            NewCanvasUI.My.InitCreateTradePanel();
        //        }
        //    }
        //    NewCanvasUI.My.isSetTrade = false;
        //}
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

    //public void OnMouseUp()
    //{
    //    if (currentrole == null)
    //    {
    //        return;
    //    }

    //   // currentrole.ReleaseRole(() => { }, () => { });
    //}
    //  public float minDestence = 2;

    //  public MapSign minMapSign;
}