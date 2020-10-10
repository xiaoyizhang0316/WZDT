using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WaveBuffSign : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// buffID
    /// </summary>
    public int buffId;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="id"></param>
    public void Init(int id)
    {
        buffId = id;
        InitSprite();
    }

    /// <summary>
    /// 初始化buff图标
    /// </summary>
    public void InitSprite()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Buff/" + buffId.ToString());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buffId > 0)
        {
            BuffData data = GameDataMgr.My.GetBuffDataByID(buffId);
            FloatWindow.My.Init(data.BuffDesc);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FloatWindow.My.Hide();
    }
}
