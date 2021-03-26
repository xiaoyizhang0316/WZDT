using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_TipManager : MonoSingleton<FTE_TipManager>
{
    public GameObject equipPanel;
    public GameObject tipPanel;

    public Text tip_text;

    private bool needShow=false;

    private Color _color;

    private Color hide;

    private bool isShow = false;
    // Update is called once per frame
    private void Start()
    {
        _color = tipPanel.GetComponent<Image>().color;
        hide = new Color(_color.r, _color.g, _color.b, 0.5f);
        //tipPanel.SetActive(false);
    }

    void Update()
    {
        if (equipPanel.activeInHierarchy)
        {
            if (isShow)
            {
                tipPanel.GetComponent<Image>().color = hide;
                needShow = true;
            }
        }
        else
        {
            if (needShow)
            {
                tipPanel.GetComponent<Image>().color = _color;
                needShow = false;
            }
        }
    }

    public void ShowTip(string tips)
    {
        //tipPanel.SetActive(true);
        tip_text.text = tips;
        isShow = true;
        tipPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(202, 182), 0.5f).SetUpdate(true).Play();
    }

    public void HideTips()
    {
        
        tipPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-250, 182), 0.5f).SetUpdate(true).Play();
        isShow = false;
        //tipPanel.SetActive(false);
    }
}
