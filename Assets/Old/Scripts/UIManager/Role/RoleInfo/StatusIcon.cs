using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 三个指示图标的控制脚本
/// </summary>
public class StatusIcon : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    public string errorText;

    public string rightText;

    public bool status;

    public Text targetText;

    public GameObject targetPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (status)
        {
            GetComponent<Image>().color = Color.green;
            targetText.text = rightText;
            targetText.color = Color.green;
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            targetText.text = errorText;
            targetText.color = Color.red;
        }
    }
}
