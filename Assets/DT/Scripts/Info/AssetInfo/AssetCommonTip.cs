using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AssetCommonTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text tip_text;

    public string tip;
    // Start is called before the first frame update
    void Start()
    {
        if (tip_text == null)
            tip_text = GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FloatWindow.My.Init(tip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FloatWindow.My.Hide();
    }
}
