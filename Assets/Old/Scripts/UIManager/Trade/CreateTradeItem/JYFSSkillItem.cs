using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JYFSSkillItem : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{

    public string skillName;

    public Text skillNameText;

    public Sprite isSelect;

    public Sprite onHover;

    public Sprite normal;

    public Image statusImg;

    public int index;

    public void Init(string skill)
    {
        skillName = skill;
        SetInfo();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CreateTradeManager.My.currentTrade.isFirstSelect)
        {
            statusImg.sprite = isSelect;
            JYFS.My.OnValueChange(skillName);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        statusImg.sprite = onHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        statusImg.sprite = normal;
    }

    public void SetInfo()
    {
        skillNameText.text = skillName;
    }

    // Start is called before the first frame update
    void Start()
    {
        //isSelect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CreateTradeManager.My.selectJYFS.Equals(skillName))
            statusImg.sprite = isSelect;
    }
}
