using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class GJJ : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    public int costTechNumber;

    public GameObject goCopy;

    public void OnBeginDrag(PointerEventData eventData)
    {
        goCopy = Instantiate(gameObject,transform);
        goCopy.transform.DOScale(1f,0.3f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(goCopy.GetComponent<RectTransform>(), eventData.position,
        Camera.main, out pos);
        goCopy.transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hit = Physics.RaycastAll(ray);
        for (int i = 0; i < hit.Length; i++)
        {
            if(hit[i].transform.CompareTag("MapRole"))
            {
                if (hit[i].transform.GetComponent<BaseMapRole>().isNpc)
                {
                    if (!hit[i].transform.GetComponentInChildren<BaseNpc>().isCanSee)
                    {
                        if (StageGoal.My.CostTechPoint(costTechNumber))
                        {
                            hit[i].transform.GetComponentInChildren<BaseNpc>().DetectNPCRole();
                            Debug.Log("使用广角镜成功");
                            break;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
