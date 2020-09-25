using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuffText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BuffData buff;

    public BaseMapRole role;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitBuff(BuffData baseBuff,BaseMapRole _role)
    {
        buff = baseBuff;
        role = _role;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(buff!=null)
            NPCListInfo.My.ShowBuffInfo(buff.GenerateBuffDesc(role));
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        NPCListInfo.My.HideBuffInfo();
    }

    public void Reset()
    {
        buff = null;
    }
}
