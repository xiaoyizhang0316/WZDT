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
    public Vector3 startPos;
    public Vector3 world;
    float speed = 800; //物体的移动速度  
    public RolePosSign Role;
    public bool isSucceed;


    /// <summary>
    /// 当前碰撞
    /// </summary>
    public bool isCollision;
    public RolePosSign currentrole;

    private void Start()
    {

    }

    /// <summary>
    /// 检测发起者和承受者交易距离
    /// </summary>
    /// <returns></returns>
    public bool CheckTradeDistance()
    {
        BaseMapRole startRole = PlayerData.My.GetMapRoleById(UIManager.My.startRole.ID);
        BaseMapRole endRole = PlayerData.My.GetMapRoleById(UIManager.My.endRole.ID);
        int distance = Mathf.Max(startRole.baseRoleData.baseRoleData.tradeRange, endRole.baseRoleData.baseRoleData.tradeRange);
        if (Vector3.Distance(startRole.transform.position, endRole.transform.position) > distance)
            return false;
        else
            return true;
    }

    /// <summary>
    /// 检测发起者和承受者技能类型
    /// </summary>
    /// <returns></returns>
    public bool CheckStartAndEnd()
    {
        if (UIManager.My.startRole.baseRoleData.roleSkillType == RoleSkillType.Service &&
            UIManager.My.endRole.baseRoleData.roleSkillType == RoleSkillType.Service)
            return false;
        else
            return true;
    }

    private void OnMouseEnter()
    {
        if (UIManager.My.NeedRayCastPanel())
        {
            return;
        }

        //Debug.Log(UIManager.My.Panel_POPInfo.gameObject.activeSelf);
        if (UIManager.My.Panel_POPInfo.GetComponent<POPRoleManager>().InitPOPRole(gameObject.transform.parent.GetComponent<BaseMapRole>()))
        {
            UIManager.My.Panel_POPInfo.gameObject.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (UIManager.My.NeedRayCastPanel())
        {
            return;
        }

        UIManager.My.Panel_POPInfo.gameObject.SetActive(false);
        //Debug.Log(UIManager.My.Panel_POPInfo.gameObject.activeSelf);

    }

    public void OnMouseUp()
    {
        if (UIManager.My.NeedRayCastPanel())
        {
            return;
        }

        if (UIManager.My.isSetTrade)
        {
            UIManager.My.endRole = PlayerData.My.GetRoleById(Double.Parse(Role.transform.name));
            if (UIManager.My.endRole.ID != UIManager.My.startRole.ID)
            {
                if (CheckTradeDistance() && CheckStartAndEnd())
                {
                    UIManager.My.Panel_CreateTrade.SetActive(true);
                    UIManager.My.InitCreateTradePanel();
                }
            }
            UIManager.My.isSetTrade = false;
        }
        else
        {
            if (!Role.GetComponent<BaseMapRole>().isNpc)
                UIManager.My.UpdateUIPosition(Role.transform);
        }
        //UIManager.My.UpdateUIPosition(Role.transform);
    }

    public void OnMouseDown()
    {
        //if (UIManager.My.isSetTrade)
        //{
        //    UIManager.My.endRole = PlayerData.My.GetRoleById(Double.Parse(Role.transform.name));
        //    UIManager.My.InitCreateTradePanel();
        //}
        //else
        //{
        //    UIManager.My.UpdateUIPosition(Role.transform);
        //}
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

    public Transform MoveTF;


    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Role")
        {
            isCollision = true;
            // Debug.Log("当前碰撞"+name);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Role")
        {
            isCollision = false;
        }
    }
}