using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoleListInfoPeasant : BaseRoleListInfo
{
    public Text effect;
    public Text reload;

    public Text efficiency;

    public Text productTime;

    public Text tradCost;
    public Text risk;
    public Text montyCost;

    public Text technology;

    public GameObject efficiencyBar;
    public GameObject effectyBar; 
    
    public Transform productTF;

    public bool isShowProduct;

    public GameObject productPrb;
    public GameObject tradText;

    public override void Init(Role role)
    {
        effect.text =role.effect.ToString();
        reload.text = role.effect  .ToString()+"%";
        efficiency.text = role.efficiency.ToString();
        productTime.text = (role.efficiency / 20f).ToString()+" /s";
        tradCost.text  =  role.tradeCost.ToString();
        risk .text = role.riskResistance.ToString();
        montyCost.text =   role.cost.ToString();
        technology.text =  role.techAdd.ToString();
        UpdateBar(role);
        if (isShowProduct)
        {
            ShowLastpruduct(role);
        }
    }
    public void UpdateBar(Role role)
    {
        efficiencyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2( role.efficiency/ 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
        effectyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2( role.effect / 120f * 150f,
                effectyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
    }
    
    public void ShowLastpruduct(Role role)
    {
        BaseMapRole baseMapRole =    PlayerData.My.GetMapRoleById(role.ID);
        for (int i = 0; i <productTF.childCount; i++)
        {
            Destroy(productTF.GetChild(i).gameObject);
        }

        int count = 9;
        if (baseMapRole.GetComponent<ProductMelon>().productDatas.Count <9)
        {
            count = baseMapRole.GetComponent<ProductMelon>().productDatas.Count ;
        }
     

        for (int i = 1; i <=count; i++)
        { 
            //Debug.Log(i+"||"+ baseMapRole.GetComponent<ProductMelon>().productDatas.Count);
            GameObject Pruductgame =  Instantiate(productPrb, productTF);
            Pruductgame.GetComponent<ProductSign>().currentProduct =
                baseMapRole.GetComponent<ProductMelon>().productDatas[    baseMapRole.GetComponent<ProductMelon>().productDatas.Count-i];
            Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.normallpp;
            if (PlayerData.My.client != null)
            {
                Pruductgame.GetComponentInChildren<Text>().text = baseMapRole.GetComponent<ProductSeed>()
                    .productDatas[baseMapRole.GetComponent<ProductSeed>().productDatas.Count - i].RepeatBulletCount.ToString();
            }
            else
            {
                Pruductgame.GetComponentInChildren<Text>().gameObject.SetActive(false);
            }
        }
    }
}
