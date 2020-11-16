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

    private int startTime = 0;

    public void Init(BaseBuff buff)
    {
        buffsign = buff;
        buffId = buff.buffId;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
            BuffInfo.My.Init(buffsign);
        startTime = TimeStamp.GetCurrentTimeStamp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
            BuffInfo.My.MenuHide();
        if(TimeStamp.GetCurrentTimeStamp()-startTime>=2 && startTime != 0)
        {
            DataUploadManager.My.AddData(DataEnum.角色_查看角色Buff);
            //startTime = 0;
        }
        startTime = 0;
    }
}
