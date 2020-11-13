using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGuideManager : GuideManager
{
    public override void Init()
    {
        if (!PlayerData.My.isSOLO && PlayerData.My.creatRole != PlayerData.My.playerDutyID)
        {
            FindObjectOfType<CloseGuide>().gameObject.SetActive(false);
            return;
        }
        //foreach (var item in NewCanvasUI.My.highLight)
        //{
        //    item.SetActive(false);
        //}

        //if (SceneManager.GetActiveScene().name == "FTE_0-1" || SceneManager.GetActiveScene().name == "FTE_0-2")
        //{
        //    currentGuideIndex = 0;
        //    PlayerPrefs.SetInt("isUseGuide", 1);
        //}
        //else
        if (currentGuideIndex >= 0 && PlayerPrefs.GetInt("isUseGuide") == 1)
        {
            //currentGuideIndex = 0;

            //NewCanvasUI.My.GamePause(false);
            //guideClose.Init();


        }
        else
        {
            currentGuideIndex = -1;
            CloseFTE();

            //guideClose.Init();

        }
        PlayCurrentIndexGuide();
    }

    public override void CloseFTE()
    {
        ftegob.SetActive(false);
        currentGuideIndex = -1;
        //foreach (var item in NewCanvasUI.My.highLight)
        //{
        //    item.SetActive(true);
        //}

        foreach (var VARIABLE in darkEffect._items)
        {
            VARIABLE.radius = 0;
        }
        darkEffect._darkColor = new Color(1, 1, 1, 0);
        //foreach (var VARIABLE in MapManager.My._mapSigns)
        //{
        //    if (VARIABLE.mapType == GameEnum.MapType.Grass && VARIABLE.baseMapRole == null)
        //    {
        //        VARIABLE.isCanPlace = true;
        //    }
        //}
        if (!PlayerData.My.isSOLO)
        {
            string str1 = "OpenGuide|false";
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str1);
            }
            else
            {
                PlayerData.My.client.SendToServerMsg(str1);
            }
        }
        //NewCanvasUI.My.Panel_Update.transform.localPosition = Vector3.one;
        if (guideClose != null)
            guideClose.gameObject.SetActive(false);
    }
}
