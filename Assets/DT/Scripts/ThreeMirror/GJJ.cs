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
                    if (!hit.transform.GetComponentInChildren<BaseNpc>().isCanSee)
                    {
                        if (StageGoal.My.CostTechPoint(costTechNumber))
                        {
                            StageGoal.My.CostTp(costTechNumber, CostTpType.Mirror);
                            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.ThreeMirror);
                            hit.transform.GetComponentInChildren<BaseNpc>().DetectNPCRole();
                            GameObject effect = Instantiate(effectPrb, hit.transform);
                            effect.transform.localPosition = Vector3.zero;
                            hit.transform.GetComponentInParent<BaseMapRole>().HideTradeButton(NewCanvasUI.My.isTradeButtonActive);
                            SoftFTE.My.CheckUnlockNewRole(hit.transform.GetComponentInParent<BaseMapRole>().baseRoleData.baseRoleData.roleType);
                            Destroy(effect, 1f);
                            Debug.Log("使用广角镜成功");
                            DataUploadManager.My.AddData(DataEnum.使用广角镜);
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

    // Start is called before the first frame update
    void Start()
    {
        costNumber.text = costTechNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        costNumber.color = costTechNumber <= StageGoal.My.playerTechPoint ? Color.white : Color.red;
    }
}
