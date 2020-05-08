using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BossAndConSliderUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public string ShowText;

    public Text numText;
    public Text showText;
    public Transform parentTF;

    public Transform selfTf;
    // Start is called before the first frame update

    private void Start( )
    {
        showText.gameObject.gameObject.SetActive(false);
        numText.gameObject.gameObject.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        showText.gameObject.gameObject.SetActive(true);
        numText.gameObject.gameObject.SetActive(true);
        float a = selfTf.GetComponent<RectTransform>().sizeDelta.x / parentTF.GetComponent<RectTransform>().sizeDelta.x;
        int b = (int)  (a * 100) ;
        numText.text = b.ToString()   +"%";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        showText.gameObject.gameObject.SetActive(false);
        numText.gameObject.gameObject.SetActive(false);
    }
}
