using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;
using UnityEngine.UI;

public class RoleListInfoDealer : BaseRoleListInfo
{
    public Text fireTime;

    public Text efficiency;

    public Text Range;

    public Text tradCost;
    public Text risk;
    public Text montyCost;

    public Text technology;

    public GameObject efficiencyBar; 
    public Transform productTF;

    public bool isShowProduct;

    public GameObject productPrb;

    public override void Init(Role role)
    {
      
        fireTime.text = (role.efficiency * -0.01f + 1.5f  ).ToString("F2") +"s";
        efficiency.text =role.efficiency.ToString();
        Range.text = (role.range ).ToString() ;
        tradCost.text  =  role.tradeCost.ToString();
        risk .text =  role.riskResistance.ToString();
        montyCost.text =  role.cost.ToString();
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
        
    }
    
    public void ShowLastpruduct(Role role)
    {
        BaseMapRole baseMapRole =    PlayerData.My.GetMapRoleById(role.ID);
        for (int i = 0; i <productTF.childCount; i++)
        {
            Destroy(productTF.GetChild(i).gameObject);
        }
        for (int i = 0; i <  baseMapRole.warehouse.Count; i++)
        { 
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
