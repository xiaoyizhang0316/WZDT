using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DLJ : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{


    public int consumeTechNumber;

    public GameObject goCopy;

    public void OnBeginDrag(PointerEventData eventData)
    {
        goCopy = Instantiate(gameObject, transform);
        goCopy.transform.DOScale(1f, 0.3f);
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
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        List<RaycastResult> hit = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, hit);
        for (int i = 0; i < hit.Count; i++)
        {
            print(hit[i].gameObject.name);
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
