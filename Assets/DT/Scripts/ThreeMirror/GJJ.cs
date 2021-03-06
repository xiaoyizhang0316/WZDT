using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GJJ : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public int costTechNumber;

    public GameObject goCopy;

    public GameObject effectPrb;

    public Text costNumber;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(1))
            return;
        goCopy = Instantiate(gameObject,transform.parent);
        goCopy.transform.DOScale(1f,0f).Play();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(1))
            return;
        if (goCopy == null)
            return;
        Vector3 pos = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(goCopy.GetComponent<RectTransform>(), eventData.position,
        Camera.main, out pos);
        //print(eventData.position);
        goCopy.transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (goCopy==null)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray,out RaycastHit hit);
        if (hit.transform != null)
        {
            if (hit.transform.CompareTag("MapRole"))
            {
                if (hit.transform.GetComponentInParent<BaseMapRole>().isNpc)
                {
                    if (!hit.transform.GetComponentInParent<BaseMapRole>().npcScript.isCanSee)
                    {
                        if (StageGoal.My.CostTechPoint(costTechNumber))
                        {
                            StageGoal.My.CostTp(costTechNumber, CostTpType.Mirror);
                            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.ThreeMirror);
                            hit.transform.GetComponentInParent<BaseMapRole>().npcScript.DetectNPCRole();
                            GameObject effect = Instantiate(effectPrb, hit.transform);
                            effect.transform.localPosition = Vector3.zero;
                            hit.transform.GetComponentInParent<BaseMapRole>().HideTradeButton(NewCanvasUI.My.isTradeButtonActive);
                            SoftFTE.My.CheckUnlockNewRole(hit.transform.GetComponentInParent<BaseMapRole>().baseRoleData.baseRoleData.roleType);
                            Destroy(effect, 1f);
                            Debug.Log("使用广角镜成功");
                            DataUploadManager.My.AddData(DataEnum.使用广角镜);
                            if (!PlayerData.My.isSOLO)
                            {
                                string str1 = "UseThreeMirror|";
                                str1 += "0";
                                str1 += "," + hit.transform.GetComponentInParent<BaseMapRole>().baseRoleData.ID.ToString();
                                str1 += "," + costTechNumber.ToString();
                                if (PlayerData.My.isServer)
                                {
                                    PlayerData.My.server.SendToClientMsg(str1);
                                }
                                else
                                {
                                    PlayerData.My.client.SendToServerMsg(str1);
                                }
                            }
                        }
                        else
                        {
                            //NPCListInfo.My.ShowHideTipPop("科技点数不足！");
                        }
                    }
                }
            }
        }
        Destroy(goCopy);
    }

    public void AutoUseGJJ()
    {
        transform.DORotateQuaternion(transform.rotation, 30f).OnComplete(() =>
        {
            List<BaseMapRole> roleList = new List<BaseMapRole>();
            for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].isNpc && !PlayerData.My.MapRole[i].npcScript.isCanSee)
                {
                    roleList.Add(PlayerData.My.MapRole[i]);
                }
            }
            if (roleList.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, roleList.Count);
                AudioManager.My.PlaySelectType(GameEnum.AudioClipType.ThreeMirror);
                roleList[index].npcScript.DetectNPCRole();
                GameObject effect = Instantiate(effectPrb, roleList[index].transform);
                effect.transform.localPosition = Vector3.zero;
                roleList[index].transform.GetComponentInParent<BaseMapRole>().HideTradeButton(NewCanvasUI.My.isTradeButtonActive);
                SoftFTE.My.CheckUnlockNewRole(roleList[index].transform.GetComponentInParent<BaseMapRole>().baseRoleData.baseRoleData.roleType);
                Destroy(effect, 1f);
                Debug.Log("使用广角镜成功");
                DataUploadManager.My.AddData(DataEnum.使用广角镜);
                if (!PlayerData.My.isSOLO)
                {
                    string str1 = "UseThreeMirror|";
                    str1 += "0";
                    str1 += "," + roleList[index].baseRoleData.ID.ToString();
                    str1 += ",0";
                    if (PlayerData.My.isServer)
                    {
                        PlayerData.My.server.SendToClientMsg(str1);
                    }
                    else
                    {
                        PlayerData.My.client.SendToServerMsg(str1);
                    }
                }
                AutoUseGJJ();
            }
        }).Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        costNumber.text = costTechNumber.ToString();
        if (PlayerData.My.guanJianZiYuanNengLi[4])
        {
            transform.DORotateQuaternion(transform.rotation, 180f).OnComplete(() => {
                AutoUseGJJ();
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        costNumber.color = costTechNumber <= StageGoal.My.playerTechPoint ? Color.white : Color.red;
    }
}
