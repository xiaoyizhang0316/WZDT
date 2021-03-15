﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoleListInfoDealer : BaseRoleListInfo
{
    public Text fireTime;

    public Text efficiency;

    public Text roleRange;

    public Text Range;

    public Text tradCost;
    public Text risk;
    public Text montyCost;

    public Text technology;

    public GameObject efficiencyBar;
    public GameObject rangeBar;
    public Transform productTF;

    public bool isShowProduct;

    public GameObject productPrb;

    public GameObject tradText;

    private List<string> fteList = new List<string>() { "FTE_0.5", "FTE_1.5", "FTE_2.5" };

    public override void Init(Role role)
    {
        float efficiencyNum = 1f / (role.efficiency * -0.01f + 1.5f);
        float add = 1f;
        float encourage = PlayerData.My.GetMapRoleById(role.ID).encourageLevel;
        fireTime.color = Color.white;
        if (encourage > 0)
        {
            add += encourage * 0.05f;
            fireTime.color = Color.green;
        }
        else if (encourage < 0)
        {
            add += encourage * 0.1f;
            fireTime.color = Color.red;
        }
        efficiencyNum *= add;
        fireTime.text = (efficiencyNum).ToString("F2");
        efficiency.text = role.efficiency.ToString();
        roleRange.text = role.range.ToString();
        Range.text = (role.range / 14.5f).ToString("F2");
        tradCost.text  =  role.tradeCost.ToString();
        risk .text =  role.riskResistance.ToString();
        if (StageGoal.My.currentType == GameEnum.StageType.Normal && !fteList.Contains(SceneManager.GetActiveScene().name))
        {
            montyCost.text = (role.cost * 2).ToString();
        }
        else
        {
            montyCost.text = role.cost.ToString();
        }
        technology.text = role.techAdd.ToString();
        UpdateBar(role);
        if (isShowProduct)
        {
            ShowLastpruduct(role);
        }
    }
    public void UpdateBar(Role role)
    {
        efficiencyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(role.efficiency/ 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
        rangeBar.GetComponent<RectTransform>().DOSizeDelta(new Vector2(role.range / 120f * 150f,
            rangeBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();

    }

   // private List<ProductData> _datas  = new List<ProductData>();
    public void ShowLastpruduct(Role role)
    {
        BaseMapRole baseMapRole =    PlayerData.My.GetMapRoleById(role.ID);
        for (int i = 0; i <productTF.childCount; i++)
        {
            Destroy(productTF.GetChild(i).gameObject);
        }
        for (int i = 0; i <  baseMapRole.warehouse.Count; i++)
        {
          //  if (_datas.Contains(baseMapRole.warehouse[i]))
          //  {
          //      continue;
          //  }
            GameObject Pruductgame =  Instantiate(productPrb, productTF);
            Pruductgame.GetComponent<ProductSign>().currentProduct =
                baseMapRole.warehouse[i];
             
            switch (baseMapRole.warehouse[i].bulletType )
            {
                case BulletType.Bomb:
                    Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.AOE;
                    break;
                case BulletType.NormalPP:
                    Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.normallpp;
                    break;
                case BulletType.Lightning:
                    Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.lightning;
                    break;
                case BulletType.summon:
                    Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.tow;
                    break;
            }
            if (Pruductgame.GetComponent<ProductSign>().currentProduct.wasteBuffList.Count > 0)
            {
                Pruductgame.GetComponent<Image>().color = new Color(1, 0.6f, 0.6f, 1);
            }
            if (PlayerData.My.client != null)
            {
                Pruductgame.GetComponentInChildren<Text>().text =  baseMapRole.warehouse[i].RepeatBulletCount.ToString() ;
            }
            else
            {
                Pruductgame.GetComponentInChildren<Image>().gameObject.SetActive(false);
            }
        }
    
    }
}
