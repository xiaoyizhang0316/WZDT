using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;
using UnityEngine.EventSystems;

public class SZFSItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public SZFSType szfsType;

    public Text SZFSText;

    public Image statusImg;

    public Sprite isSelect;

    public Sprite onHover;

    public Sprite normal;

    public bool isLock;

    public void Init(SZFSType type)
    {
        szfsType = type;
        InitInfo();
    }

    public void InitInfo()
    {
        SZFSText.text = szfsType.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isLock)
        {
            SZFS.My.OnValueChange(szfsType);
            statusImg.sprite = isSelect;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isLock)
        {
            statusImg.sprite = onHover;
            CreateTradeManager.My.PopUpShow(szfsType.ToString(), Input.mousePosition);
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
        if (CreateTradeManager.My.selectSZFS == szfsType)
            statusImg.sprite = isSelect;
    }
}
