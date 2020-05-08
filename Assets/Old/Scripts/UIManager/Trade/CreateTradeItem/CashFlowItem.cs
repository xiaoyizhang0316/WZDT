using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;
using UnityEngine.EventSystems;

public class CashFlowItem : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public CashFlowType cashFlowType;

    public Text cashFlowText;

    public Image statusImg;

    public Sprite isSelect; 

    public Sprite onHover;

    public Sprite normal;

    public bool isLock;

    public void Init(CashFlowType type)
    {
        cashFlowType = type;
        SetInfo();
    }

    public void SetInfo()
    {
        cashFlowText.text = cashFlowType.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isLock)
        {
            statusImg.sprite = isSelect;
            CashFlow.My.OnValueChange(cashFlowType);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isLock)
        {
            statusImg.sprite = onHover;
            CreateTradeManager.My.PopUpShow(cashFlowType.ToString(), Input.mousePosition);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isLock)
        {
            statusImg.sprite = normal;
            CreateTradeManager.My.PopUpHide();
        }
    }

    public void SetLock(bool lockStatus)
    {
        isLock = lockStatus;
        if (isLock)
        {
            GetComponent<Image>().color = Color.gray;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CreateTradeManager.My.selectCashFlow == cashFlowType)
            statusImg.sprite = isSelect;
    }
}
