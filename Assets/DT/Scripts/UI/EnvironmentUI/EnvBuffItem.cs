using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnvBuffItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Image buff_image;
    public BaseBuff buff;

    public void Setup(BaseBuff buff)
    {
        this.buff = buff;
        buff_image.sprite = Resources.Load<Sprite>("Sprite/Buff/" + buff.buffId);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FloatWindow.My.Init(buff.buffDesc);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FloatWindow.My.Hide();
    }
}
