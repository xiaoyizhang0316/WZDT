using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
  

public class SkillActivityUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject content;

    public Text contentUI;
    // Start is called before the first frame update

    public Image SkillCD;
    public Button openButton;
    public Text skillName;
    public Text cost;
    public void OnPointerEnter(PointerEventData eventData)
    {
        content.gameObject.SetActive(true);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        content.gameObject.SetActive(false);
    }

    private void Start()
    {
        openButton = GetComponent< Button>();
    }
}
