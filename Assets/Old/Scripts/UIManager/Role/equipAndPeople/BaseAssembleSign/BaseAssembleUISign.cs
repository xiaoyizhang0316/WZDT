using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 基础装配类-装备人物的基类
/// </summary>
public class BaseAssembleUISign : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Transform lastPlot;

    /// <summary>
    /// 需要吸附的距离
    /// </summary>
    public Vector3 lastpos;

    /// <summary>
    /// 可以释放
    /// </summary>
    public bool isRelease;

    
 
    // Start is called before the first frame update
    void Start()
    {
        isRelease = true;
        //  isOccupied.SetActive(false);
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector3 V = Input.mousePosition;
        Vector3 V2 = new Vector3(V.x - Screen.width / 2, V.y - Screen.height / 2);
        transform.parent.localPosition = V2;
        
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if(!transform.parent.GetComponent<DragUI>().CheckAllRight(true))
        {
            if (tag.Equals("Worker"))
            {
                WorkerListManager.My.UninstallWorker(int.Parse(transform.parent.name.Split('_')[1]));
            }
            else if (tag.Equals("equip"))
            {
                EquipListManager.My.UninstallEquip(int.Parse(transform.parent.name.Split('_')[1]));
            }
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.PutEquip);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        
        if (tag == other.tag)
        {
            isRelease = false; 
            return;
        } 
        if (other.tag == "Plot")
        {
       
      
        lastPlot = other.transform;
            lastpos = other.transform.position - transform.position;
        }
    }

 
    public void OnTriggerExit(Collider other)
    { 
        isRelease = true;
        lastPlot = null;
    }


    // Update is called once per frame
    void Update()
    {
    }

 

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 V = Input.mousePosition;
        Vector3 V2 = new Vector3(V.x - Screen.width / 2, V.y - Screen.height / 2);
        Tweener tweener = transform.parent.DOLocalMove(V2, 0.1f);
        tweener.SetUpdate(true);
        AudioManager.My.PlaySelectType(GameEnum.AudioClipType.GrabEquip);
    }
}