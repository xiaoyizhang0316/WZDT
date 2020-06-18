using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;
using UnityEngine.UI;

public class RoleListInfoMerchant : BaseRoleListInfo
{
    public Text effect;
    public Text MoveSpeed;

    public Text efficiency;

    public Text TradDown;

    public Text tradCost;
    public Text risk;
    public Text montyCost;

    public Text technology;

    public GameObject efficiencyBar;
    public GameObject effectyBar; 
    public Transform productTF;

    public bool isShowProduct;

    public GameObject productPrb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init(Role role)
    {
        effect.text = role.effect.ToString();
        MoveSpeed.text =  role.efficiency  .ToString()+"%";
        efficiency.text = role.efficiency.ToString();
        TradDown.text = (((role.effect *0.3f)/100f)*  role.tradeCost).ToString();
        tradCost.text  =   role.tradeCost.ToString();
        risk .text =  role.riskResistance.ToString();
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
            new Vector2(role.efficiency / 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
        effectyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2( role.effect / 120f * 150f,
                effectyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
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
        
        }
    }
}
