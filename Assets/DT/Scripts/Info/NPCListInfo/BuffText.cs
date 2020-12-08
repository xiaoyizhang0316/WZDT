using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuffText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BuffData buff;

    public BaseMapRole role;

    private int startTime = 0;

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
        if (buff != null)
        {
            startTime = TimeStamp.GetCurrentTimeStamp() ;
            NPCListInfo.My.ShowBuffInfo(buff.GenerateBuffDesc(role));
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        NPCListInfo.My.HideBuffInfo();
        if (TimeStamp.GetCurrentTimeStamp() - startTime >= 2 && startTime!=0)
        {
            // 记录操作
            DataUploadManager.My.AddData(DataEnum.角色_查看角色Buff);
            //startTime = 0;
        }
        startTime = 0;
    }

    public void Reset()
    {
        buff = null;
    }
}
