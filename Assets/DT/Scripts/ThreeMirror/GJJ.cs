using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GJJ : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    public int costTechNumber;

    public GameObject goCopy;

    public GameObject effectPrb;

    public Text costNumber;

    public void OnBeginDrag(PointerEventData eventData)
    {
        goCopy = Instantiate(gameObject,transform.parent);
        goCopy.transform.DOScale(1f,0f).Play();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(goCopy.GetComponent<RectTransform>(), eventData.position,
        Camera.main, out pos);
        //print(eventData.position);
        goCopy.transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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
                            //effect.transform.eulerAngles = new Vector3(-90f, 0f, 0f);
                            Destroy(effect, 1f);
                            Debug.Log("使用广角镜成功");
                        }
                        else
                        {
                            NPCListInfo.My.ShowHideTipPop("科技点数不足！");
                        }
                    }
                }
            }
        }
        Destroy(goCopy);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

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
