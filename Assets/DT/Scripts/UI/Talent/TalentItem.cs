using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TalentItem : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TalentItem prevItem;

    public TalentItem nextItem;

    public string talentDesc;

    public bool isSelect;

    public bool isLock;

    public int index;

    public int number;

    public List<Sprite> statusSprites;

    /// <summary>
    /// 检测该天赋当前状态
    /// </summary>
    public void CheckStatus()
    {
        if (isSelect)
        {
            GetComponent<Image>().sprite = statusSprites[1];
            isLock = false;
        }
        else
        {
            if (prevItem == null)
            {
                GetComponent<Image>().sprite = statusSprites[0];
                isLock = false;
                return;
            }
            else if (prevItem.isSelect)
            {
                GetComponent<Image>().sprite = statusSprites[0];
                isLock = false;
                return;
            }
            else
            {
                GetComponent<Image>().sprite = statusSprites[2];
                isLock = true;
                return;
            }
        }
    }

    /// <summary>
    /// 左键点击开关天赋
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLock)
        {
            return;
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isSelect)
            {
                if (nextItem == null)
                {
                    TalentPanel.My.usedPoint--;
                    TalentPanel.My.UpdateTalentPoint();
                    isSelect = false;
                    CheckStatus();
                }
                else
                {
                    if (!nextItem.isSelect)
                    {
                        TalentPanel.My.usedPoint--;
                        TalentPanel.My.UpdateTalentPoint();
                        isSelect = false;
                        nextItem.CheckStatus();
                        CheckStatus();
                    }
                }
            }
            else if (TalentPanel.My.totalPoint - TalentPanel.My.usedPoint > 0)
            {
                TalentPanel.My.usedPoint++;
                TalentPanel.My.UpdateTalentPoint();
                isSelect = true;
                if (nextItem != null)
                {
                    nextItem.CheckStatus();
                }
                CheckStatus();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FloatWindow.My.Init2D(talentDesc,transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FloatWindow.My.Hide();
    }
}
