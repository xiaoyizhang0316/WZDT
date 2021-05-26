using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentPanel : MonoBehaviour
{
    public GameObject val_prefab;

    public GameObject buff_prefab;

    public Transform buff_content;
    public Transform val_content;
    
    public Button out_btn;
    public Button in_btn;

    private bool isPanelShow = false;
    private IEnumerator ie;

     void Start()
    {
        out_btn.onClick.AddListener(ShowEnvPanel);
        in_btn.onClick.AddListener(Close);
        stopShow = false;
        StartCoroutine(ShowEnvOnClose());
    }

    void ShowEnvPanel()
    {
        Clear();
        isPanelShow = true;
        ShowEnvBuffs();
        ShowEnvValues();
        transform.GetComponent<RectTransform>().DOAnchorPosY(-220, 0.5f).Play().OnComplete(() =>
        {
            in_btn.gameObject.SetActive(true);
            out_btn.gameObject.SetActive(false);
        });
        mask.SetActive(false);
        envItem.GetChild(0).GetComponent<Text>().text = "";
        envItem.GetChild(1).GetComponent<Text>().text = "";
        stopShow = true;
        InvokeRepeating("RefreshPanel", 1,2);
    }

    void Close()
    {
        isPanelShow = false;
        transform.GetComponent<RectTransform>().DOAnchorPosY(140, 0.5f).Play().OnComplete(() =>
        {
            CancelInvoke();
            Clear();
            stopShow = false;
            StartCoroutine(ShowEnvOnClose());
            in_btn.gameObject.SetActive(false);
            out_btn.gameObject.SetActive(true);
        });
    }

    void RefreshPanel()
    {
        if (isPanelShow)
        {
            Clear();
            ShowEnvBuffs();
            ShowEnvValues();
        }
        else
        {
            CancelInvoke();
        }
    }

    public GameObject mask;
    public Transform envItem;
    private int showIndex = 0;
    [SerializeField]
    private bool stopShow = false;
    public List<string> envNames = new List<string>
    {
        "风险防控等级","交易成本等级", "距离等级","固定成本等级","交付因素等级","获得满意度倍率","口味满足倍率"
    };
    IEnumerator ShowEnvOnClose()
    {
        if (stopShow)
        {
            mask.SetActive(false);
            envItem.GetChild(0).GetComponent<Text>().text = "";
            envItem.GetChild(1).GetComponent<Text>().text = "";
            yield break;
        }
        int switchCount = 0;
        mask.SetActive(true);

        ReSwitch:
        switchCount++;
        if (switchCount > 11)
        {
            envItem.GetChild(0).GetComponent<Text>().text = "";
            envItem.GetChild(1).GetComponent<Text>().text = "";
            goto SwitchEnd;
        }
        switch (showIndex)
        {
            case 0:
                showIndex++;
                if (BaseLevelController.My.riskControlLevel > 0)
                {
                    ShowEnvClosed(envNames[0],BaseLevelController.My.riskControlLevel.ToString() );
                }
                else
                {
                    goto ReSwitch;
                }
                break;
            case 1:
                showIndex++;
                if (BaseLevelController.My.tradeCostLevel > 0)
                {
                    ShowEnvClosed(envNames[1],BaseLevelController.My.tradeCostLevel.ToString() );
                }
                else
                {
                    goto ReSwitch;
                }
                break;
            case 2:
                showIndex++;
                if (BaseLevelController.My.distanceLevel > 0)
                {
                    ShowEnvClosed(envNames[2],BaseLevelController.My.distanceLevel.ToString() );
                }
                else
                {
                    goto ReSwitch;
                }
                break;
            case 3:
                showIndex++;
                if (BaseLevelController.My.monthCostLevel > 0)
                {
                    ShowEnvClosed(envNames[3],BaseLevelController.My.monthCostLevel.ToString() );
                }
                else
                {
                    goto ReSwitch;
                }
                break;
            case 4:
                showIndex++;
                if (BaseLevelController.My.encourageLevel > 0)
                {
                    ShowEnvClosed(envNames[4],BaseLevelController.My.encourageLevel.ToString() );
                }
                else
                {
                    goto ReSwitch;
                }
                break;
            case 5:
                showIndex++;
                if (BaseLevelController.My.satisfyRate > 0)
                {
                    ShowEnvClosed(envNames[5],BaseLevelController.My.satisfyRate.ToString("F2") );
                }
                else
                {
                    goto ReSwitch;
                }
                break;
            case 6:
                showIndex++;
                if (BaseLevelController.My.tasteDamageRate > 0)
                {
                    ShowEnvClosed(envNames[6],BaseLevelController.My.tasteDamageRate.ToString("F2") );
                }
                else
                {
                    goto ReSwitch;
                }
                break;
            case 7:
                showIndex++;
                if (BaseLevelController.My.environmentLevel1 > 0)
                {
                    ShowEnvClosed(BaseLevelController.My.environmentLevel1Name,BaseLevelController.My.environmentLevel1.ToString() );
                }
                else
                {
                    goto ReSwitch;
                }
                break;
            case 8:
                showIndex++;
                if (BaseLevelController.My.environmentLevel2 > 0)
                {
                    ShowEnvClosed(BaseLevelController.My.environmentLevel2Name,BaseLevelController.My.environmentLevel2.ToString() );
                }
                else
                {
                    goto ReSwitch;
                }
                break;
            case 9:
                showIndex++;
                if (BaseLevelController.My.environmentLevel3 > 0)
                {
                    ShowEnvClosed(BaseLevelController.My.environmentLevel3Name,BaseLevelController.My.environmentLevel3.ToString() );
                }
                else
                {
                    goto ReSwitch;
                }
                break;
            case 10:
                showIndex = 0;
                int extNum = BuildingManager.My.GetExtraConsumerNumber("100");
                if (extNum > 0)
                {
                    ShowEnvClosed("额外的消费者",extNum.ToString() );
                }
                else
                {
                    goto ReSwitch;
                }
                break;
        }
        SwitchEnd:
        yield return new WaitForSeconds(3);
        StartCoroutine(ShowEnvOnClose());
    }

    void ShowEnvClosed(string envName, string envVal)
    {
        string oldName = envItem.GetChild(0).GetComponent<Text>().text;
        if (oldName.Equals(envName))
        {
            return;
        }
        Tween te=null;
        te=envItem.DOLocalMoveY(35, 0.5f).OnComplete(() =>
            {
                envItem.GetChild(0).GetComponent<Text>().text = "";
                envItem.GetChild(1).GetComponent<Text>().text = "";
                envItem.transform.localPosition = Vector3.zero;
                envItem.GetChild(0).GetComponent<Text>().text = envName;
                envItem.GetChild(1).GetComponent<Text>().text = envVal;
            }).Play().OnPause(()=>te?.TogglePause());
        //te = envItem.DOLocalMoveY(35, 2.5f).SetEase(Ease.InOutBounce).SetEase(Ease.InExpo).Play().OnPause(()=>te?.TogglePause());
    }

    void ShowEnvValues()
    {
        if (BaseLevelController.My.riskControlLevel > 0)
        {
            GameObject go = Instantiate(val_prefab, val_content);
            go.GetComponent<EnvValItem>().Setup("风险防控等级", BaseLevelController.My.riskControlLevel.ToString());
        }
        if (BaseLevelController.My.tradeCostLevel > 0)
        {
            GameObject go = Instantiate(val_prefab, val_content);
            go.GetComponent<EnvValItem>().Setup("交易成本等级", BaseLevelController.My.tradeCostLevel.ToString());
        }
        if (BaseLevelController.My.distanceLevel > 0)
        {
            GameObject go = Instantiate(val_prefab, val_content);
            go.GetComponent<EnvValItem>().Setup("距离等级", BaseLevelController.My.distanceLevel.ToString());
        }
        if (BaseLevelController.My.monthCostLevel > 0)
        {
            GameObject go = Instantiate(val_prefab, val_content);
            go.GetComponent<EnvValItem>().Setup("固定成本等级", BaseLevelController.My.monthCostLevel.ToString());
        }
        if (BaseLevelController.My.encourageLevel > 0)
        {
            GameObject go = Instantiate(val_prefab, val_content);
            go.GetComponent<EnvValItem>().Setup("交付因素等级", BaseLevelController.My.encourageLevel.ToString());
        }
        if (BaseLevelController.My.environmentLevel1 > 0)
        {
            GameObject go = Instantiate(val_prefab, val_content);
            go.GetComponent<EnvValItem>().Setup(BaseLevelController.My.environmentLevel1Name, BaseLevelController.My.environmentLevel1.ToString());
        }
        if (BaseLevelController.My.environmentLevel2 > 0)
        {
            GameObject go = Instantiate(val_prefab, val_content);
            go.GetComponent<EnvValItem>().Setup(BaseLevelController.My.environmentLevel2Name, BaseLevelController.My.environmentLevel2.ToString());
        }
        if (BaseLevelController.My.environmentLevel3 > 0)
        {
            GameObject go = Instantiate(val_prefab, val_content);
            go.GetComponent<EnvValItem>().Setup(BaseLevelController.My.environmentLevel3Name, BaseLevelController.My.environmentLevel3.ToString());
        }
        if (BaseLevelController.My.satisfyRate > 0)
        {
            GameObject go = Instantiate(val_prefab, val_content);
            go.GetComponent<EnvValItem>().Setup("获得满意度倍率", BaseLevelController.My.satisfyRate.ToString("F2"));
        }
        if (BaseLevelController.My.tasteDamageRate > 0)
        {
            GameObject go = Instantiate(val_prefab, val_content);
            go.GetComponent<EnvValItem>().Setup("口味满足倍率", BaseLevelController.My.tasteDamageRate.ToString("F2"));
        }

        int extNum = BuildingManager.My.GetExtraConsumerNumber("100");
        if (extNum > 0)
        {
            GameObject go = Instantiate(val_prefab, val_content);
            go.GetComponent<EnvValItem>().Setup("额外的消费者", extNum.ToString());
        }
    }

    void ShowEnvBuffs()
    {
        List<BaseBuff> buffs = new List<BaseBuff>();
        buffs.AddRange(BaseLevelController.My.consumerBuffList);
        buffs.AddRange(BaseLevelController.My.playerStaticList);
        buffs.AddRange(BaseLevelController.My.stageStaticList);

        for (int i = 0; i < buffs.Count; i++)
        {
            GameObject go = Instantiate(buff_prefab, buff_content);
            go.GetComponent<EnvBuffItem>().Setup(buffs[i]);
        }
    }

    void Clear()
    {
        foreach (Transform child in buff_content)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in val_content)
        {
            Destroy(child.gameObject);
        }
    }
}
