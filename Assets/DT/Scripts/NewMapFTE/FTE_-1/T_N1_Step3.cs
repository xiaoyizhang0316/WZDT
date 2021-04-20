﻿using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class T_N1_Step3 : BaseGuideStep
{
    public GameObject hand;
    private Canvas ui_Canvas;
    private Camera UI_Camera;
    public override IEnumerator StepStart()
    {
        GameObject dealer = GetDealer();
        UI_Camera = Camera.main;
        ui_Canvas = NewCanvasUI.My.gameObject.GetComponent<Canvas>();
        UpdateNamePosition(dealer);
        yield return null;
    }

    GameObject GetDealer()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
            {
                return PlayerData.My.MapRole[i].gameObject;
            }
        }

        return null;
    }
    
    void UpdateNamePosition(GameObject obj)
    {
        Vector2 mouseDown = Camera.main.WorldToScreenPoint(obj.transform.position);
        Vector2 mouseUGUIPos = new Vector2();
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(ui_Canvas.transform as RectTransform,
            mouseDown, UI_Camera, out mouseUGUIPos);
        if (isRect)
        {
            hand.GetComponent<RectTransform>().anchoredPosition = mouseUGUIPos+new Vector2(2,-1);
            hand.SetActive(true);
        }
    }
    
    public override bool ChenkEnd()
    {
        return NewCanvasUI.My.Panel_Update.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        
        yield return null;
    }
}
