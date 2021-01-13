using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class MissionManager : IOIntensiveFramework.MonoSingleton.MonoSingleton<MissionManager>
{
    public Text missionText;

    public Transform contentTF;

    public GameObject missionPrb;
    public GameObject mainMissionPrb;

    public Button in_btn;
    public Button out_btn;
    public Text tip;

    public List<MissionSign> signs=new List<MissionSign>();

    public void AddMission(MissionData data)
    {
        GameObject sign;
        if (data.isMainmission)
        {
            sign = Instantiate(mainMissionPrb, contentTF);
        }
        else
        {
            sign = Instantiate(missionPrb, contentTF);
        }

        sign.GetComponent<MissionSign>().Init( data);
        
    }

    public void ChangeTital(string content)
    {
        missionText.text = content;
        tip.gameObject.SetActive(false);
        isTipShow = false;
    }


    public void ClearSigns()
    {
        for (int i = 0; i < signs.Count; i++)
        {
            signs[i].transform.DOLocalMoveX(signs[i].transform.localPosition.x - 100, 0.3f);
            Destroy(signs[i].gameObject,0.3f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        out_btn.onClick.AddListener(PanelShow);
        in_btn.onClick.AddListener(PanelHide);
    }

    void PanelHide()
    {
        isOut = false;
        missions.GetComponent<RectTransform>().DOAnchorPosX(-411.3f, 0.5f).Play();
        titles.GetComponent<RectTransform>().DOAnchorPosX(-421.95f, 0.5f).Play();
        out_btn.gameObject.SetActive(true);
        in_btn.gameObject.SetActive(false);
    }

    void PanelShow()
    {
        isOut = true;
        missions.GetComponent<RectTransform>().DOAnchorPosX(-11.3f, 0.5f).Play();
        titles.GetComponent<RectTransform>().DOAnchorPosX(-21.95f, 0.5f).Play();
        out_btn.gameObject.SetActive(false);
        in_btn.gameObject.SetActive(true);
    }

    private bool isOut = false;
    private bool isTipShow = false;

    public Transform missions;//-11.3
    public Transform titles;//-21.95
    // Update is called once per frame
    void Update()
    {
        if (NewCanvasUI.My.Panel_AssemblyRole.activeInHierarchy)
        {
            if (!isOut)
            {
                missions.GetComponent<RectTransform>().DOAnchorPosX(-411.3f, 0.5f).Play();
                titles.GetComponent<RectTransform>().DOAnchorPosX(-421.95f, 0.5f).Play();
                out_btn.gameObject.SetActive(true);
            }

            if(isTipShow)
                tip.gameObject.SetActive(false);
        }
        else
        {
            isOut = false;
            missions.GetComponent<RectTransform>().DOAnchorPosX(-11.3f, 0.5f).Play();
            titles.GetComponent<RectTransform>().DOAnchorPosX(-21.95f, 0.5f).Play();
            out_btn.gameObject.SetActive(false);
            in_btn.gameObject.SetActive(false);
            if(isTipShow)
                tip.gameObject.SetActive(true);
        }
    }

    public void ShowTipText(string tipContent, Color showColor)
    {
        tip.color = showColor;
        tip.text = tipContent;
        tip.gameObject.SetActive(true);
        isTipShow = true;
    }

    public void HideTip()
    {
        tip.gameObject.SetActive(false);
        tip.text = "";
        isTipShow = false;
    }
}
