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

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            string desc = GameDataMgr.My.GetTranslateName(currentRole.baseRoleData.roleType.ToString());
            RoleFloatWindow.My.Init(transform, desc,currentRole.baseRoleData.roleSkillType,currentRole.baseRoleData.roleType);
        }
        //Cursor.SetCursor(cursorTexture,Vector2.zero,CursorMode.Auto);
        //Debug.Log(UIManager.My.Panel_POPInfo.gameObject.activeSelf);
        //if (UIManager.My.Panel_POPInfo.GetComponent<POPRoleManager>().InitPOPRole(gameObject.transform.parent.GetComponent<BaseMapRole>()))
        //{
        //    UIManager.My.Panel_POPInfo.gameObject.SetActive(true);
        //}
    }

    private void OnMouseExit()
    {
        RoleFloatWindow.My.Hide();
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
                    print("配置交易成功");
                    NewCanvasUI.My.InitCreateTradePanel();
                    AudioManager.My.PlaySelectType(GameEnum.AudioClipType.EndTrade);
                    DataUploadManager.My.AddData(DataEnum.交易_发起的内部交易);
                }
            }
        }
        else if (!EventSystem.current.IsPointerOverGameObject())
        {
            NewCanvasUI.My.Panel_Update.SetActive(true);
            RoleUpdateInfo.My.Init(currentRole);
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