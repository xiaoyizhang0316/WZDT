using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoleBuffSign : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public int buffId;

    public BaseBuff buffsign;

    public Image maskImage;

    public void Init(BaseBuff buff)
    {
        buffsign = buff;
        buffId = buff.buffId;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BuffInfo.My.Init(buffsign);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BuffInfo.My.MenuHide();
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
