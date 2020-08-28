using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TSJ : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public int costTechNumber;

    public GameObject goCopy;

    public GameObject effectPrb;

    public Text costNumber;

    public void OnBeginDrag(PointerEventData eventData)
    {
        goCopy = Instantiate(gameObject, transform.parent);
        goCopy.transform.DOScale(1f, 0f).Play();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(goCopy.GetComponent<RectTransform>(), eventData.position,
        Camera.main, out pos);
        //goCopy.transform.position = pos;
        goCopy.transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hit = Physics.RaycastAll(ray);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.CompareTag("Building"))
            {
                if (!hit[i].transform.GetComponent<Building>().isUseTSJ)
                {
                    if (StageGoal.My.CostTechPoint(costTechNumber))
                    {
                        StageGoal.My.CostTp(costTechNumber, CostTpType.Mirror);
                        AudioManager.My.PlaySelectType(GameEnum.AudioClipType.ThreeMirror);
                        hit[i].transform.GetComponent<Building>().UseTSJ();
                        GameObject effect = Instantiate(effectPrb, hit[i].transform);
                        effect.transform.localPosition = Vector3.zero;
                        Debug.Log("使用透视镜成功");
                        break;
                    }
                    else
                    {
                        NPCListInfo.My.ShowHideTipPop("科技点数不足！");
                        break;
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
