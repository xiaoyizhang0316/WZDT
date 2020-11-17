using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TalentItem : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TalentItem prevItem;

    public TalentItem nextItem;

    public string talentTitle;

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
                MapGuideManager.My.GetComponent<MapObject>().clickTalentItem = true;
                if (nextItem == null)
                {
                    TalentPanel.My.usedPoint--;
                    TalentPanel.My.UpdateTalentPoint();
                    isSelect = false;
                    CheckStatus();
                }
                else
                {
                    TalentPanel.My.usedPoint--;
                    TalentPanel.My.UpdateTalentPoint();
                    isSelect = false;

                    CheckStatus();
                    nextItem.CancleTalent();
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
                else
                {
                    PlayerData.My.isOneFinish[index - 1] = true;
                    TalentPanel.My.CheckLabel(index);
                }
                CheckStatus();
                MapGuideManager.My.GetComponent<MapObject>().clickTalentItem = true;
            }
            else
            {
                TalentPanel.My.ShowNoTalentInfo();
            }
        }
    }

    public void CancleTalent()
    {
        if (isSelect)
        {
            TalentPanel.My.usedPoint--;
            TalentPanel.My.UpdateTalentPoint();
            isSelect = false;
            CheckStatus();
            if (nextItem != null)
            {
                nextItem.CancleTalent();
            }
        }
        else
        {
            CheckStatus();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TalentPanel.My.OpenTalentDesc(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //TalentPanel.My.CloseTalentDesc();
    }
}
