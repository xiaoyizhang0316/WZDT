using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

     void Start()
    {
        out_btn.onClick.AddListener(ShowEnvPanel);
        in_btn.onClick.AddListener(Close);
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
        InvokeRepeating("RefreshPanel", 1,2);
    }

    void Close()
    {
        isPanelShow = false;
        transform.GetComponent<RectTransform>().DOAnchorPosY(230, 0.5f).Play().OnComplete(() =>
        {
            CancelInvoke();
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
            go.GetComponent<EnvValItem>().Setup(BaseLevelController.My.environmentLevel2Name, BaseLevelController.My.environmentLevel3.ToString());
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
