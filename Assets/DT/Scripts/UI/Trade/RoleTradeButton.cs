using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleTradeButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    public BaseMapRole currentRole;

    public GameObject lockImage;

    public GameObject unlockImage;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentRole.isNpc)
        {
            if (currentRole.npcScript.isLock)
                return;
        }
        NewCanvasUI.My.CreateTrade(currentRole);
        Debug.Log("开始拖拽");
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("结束拖拽");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        if (hit.transform != null)
        {

            if (hit.transform.CompareTag("MapRole"))
            {
                if (hit.transform.GetComponentInParent<BaseMapRole>().isNpc)
                {
                    NewCanvasUI.My.endRole = hit.transform.GetComponent<BaseMapRole>();
                    if (NewCanvasUI.My.endRole.baseRoleData.ID != NewCanvasUI.My.startRole.baseRoleData.ID)
                    {
                        if (TradeManager.My.CheckTradeCondition())
                        {
                            NewCanvasUI.My.InitCreateTradePanel();
                            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.EndTrade);
                        }
                    }
                }
                else
                {
                    NewCanvasUI.My.endRole = hit.transform.GetComponentInParent<BaseMapRole>();
                    if (NewCanvasUI.My.endRole.baseRoleData.ID != NewCanvasUI.My.startRole.baseRoleData.ID)
                    {
                        if (TradeManager.My.CheckTradeCondition())
                        {
                            print("配置交易成功");
                            NewCanvasUI.My.InitCreateTradePanel();
                            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.EndTrade);
                        }
                    }
                }
            }
        }
        NewCanvasUI.My.CreateTradeLineGo.SetActive(false);
        NewCanvasUI.My.CreateTradeLineGo.SetActive(false);
        NewCanvasUI.My.isSetTrade = false;
        if (NewCanvasUI.My.isChange)
        {
            NewCanvasUI.My.isChange = false;
            NewCanvasUI.My.HideAllTradeButton();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentRole.isNpc)
        {
            if (currentRole.npcScript.isLock)
                return;
        }
        StartTrade();
    }

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    print("结束拖拽");
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    Physics.Raycast(ray, out RaycastHit hit);
    //    if (hit.transform != null)
    //    {

    //        if (hit.transform.CompareTag("MapRole"))
    //        {
    //            if (hit.transform.GetComponent<BaseMapRole>().isNpc)
    //            {
    //                if (!hit.transform.GetComponent<BaseMapRole>().npcScript.isLock && hit.transform.GetComponent<BaseMapRole>().npcScript.isCanSee)
    //                {
    //                    NewCanvasUI.My.endRole = hit.transform.GetComponent<BaseMapRole>();
    //                    if (NewCanvasUI.My.endRole.baseRoleData.ID != NewCanvasUI.My.startRole.baseRoleData.ID)
    //                    {
    //                        if (TradeManager.My.CheckTradeCondition())
    //                        {
    //                            NewCanvasUI.My.InitCreateTradePanel();
    //                            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.EndTrade);
    //                        }
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                NewCanvasUI.My.endRole = hit.transform.GetComponent<BaseMapRole>();
    //                if (NewCanvasUI.My.endRole.baseRoleData.ID != NewCanvasUI.My.startRole.baseRoleData.ID)
    //                {
    //                    if (TradeManager.My.CheckTradeCondition())
    //                    {
    //                        print("配置交易成功");
    //                        NewCanvasUI.My.InitCreateTradePanel();
    //                        AudioManager.My.PlaySelectType(GameEnum.AudioClipType.EndTrade);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    NewCanvasUI.My.CreateTradeLineGo.SetActive(false);
    //    NewCanvasUI.My.CreateTradeLineGo.SetActive(false);
    //    NewCanvasUI.My.isSetTrade = false;
    //    if (NewCanvasUI.My.isChange)
    //    {
    //        NewCanvasUI.My.isChange = false;
    //        NewCanvasUI.My.HideAllTradeButton();
    //    }
    //}

    /// <summary>
    /// 发起交易
    /// </summary>
    public void StartTrade()
    {
        NewCanvasUI.My.CreateTrade(currentRole);

    }

    // Start is called before the first frame update
    void Start()
    {
        currentRole = transform.GetComponentInParent<BaseMapRole>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.LookAt(Camera.main.transform.position);
        GetComponent<Image>().fillAmount = currentRole.warehouse.Count / (float)currentRole.baseRoleData.bulletCapacity;
        if (currentRole.isNpc)
        {
            if (currentRole.npcScript.isLock)
            {
                lockImage.SetActive(true);
                unlockImage.SetActive(false);
            }
            else
            {
                lockImage.SetActive(false);
                unlockImage.SetActive(true);
            }
        }
    }
}
