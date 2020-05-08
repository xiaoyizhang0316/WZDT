using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class BuffInfo : MonoSingleton<BuffInfo>
{
    public Text buffName;

    public Text buffDesc;

    public Image buffSprite;

    public float offsetX;

    public float offsetY;

    public void Init(BaseBuff buff)
    {
        buffName.text = buff.buffName;
        buffDesc.text = buff.buffData.BuffDesc;
        buffSprite.sprite = Resources.Load<Sprite>("Sprite/Buff/" + buff.buffId.ToString());
        MenuShow();
    }

    public void MenuShow()
    {
        transform.localPosition = new Vector3(Input.mousePosition.x - 960f, Input.mousePosition.y - 540f, 0f) + new Vector3(150f,0f,0f);
    }

    public void MenuHide()
    {
        transform.localPosition = new Vector3(-1187f, -20f, 0);
    }

    private void Start()
    {
        MenuHide();
    }
}
