using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject tradText;


    public override void Init(Role role)
    {
        float efficiencyNum = (role.efficiency);
        float add = 1f;
        float encourage = PlayerData.My.GetMapRoleById(role.ID).encourageLevel;
        MoveSpeed.color = Color.black;
        if (encourage > 0)
        {
            add += encourage * 0.05f;
            MoveSpeed.color = Color.green;
        }
        else if (encourage < 0)
        {
            add += encourage * 0.1f;
            MoveSpeed.color = Color.red;
        }
        efficiencyNum *= add;
        effect.text = role.effect.ToString();
        MoveSpeed.text = efficiencyNum.ToString()+"%";
        efficiency.text = role.efficiency.ToString();
        TradDown.text = (role.effect * 0.3f + 24f).ToString() + "%";
        tradCost.text  =   role.tradeCost.ToString();
        risk .text =  role.riskResistance.ToString();
        if (StageGoal.My.currentType == GameEnum.StageType.Normal && !CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
        {
            montyCost.text = (role.cost * 2).ToString();
        }
        else
        {
            montyCost.text = role.cost.ToString();
        }
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
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
        effectyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2( role.effect / 120f * 150f,
                effectyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
    }
    
    private List<ProductData> _datas  = new List<ProductData>();
    public void ShowLastpruduct(Role role)
    {
        for (int i = 0; i < productTF.childCount; i++)
        {
            Destroy(productTF.GetChild(0).gameObject);
        }
        BaseMapRole baseMapRole = PlayerData.My.GetMapRoleById(role.ID);
      
        for (int i = 0; i < baseMapRole.warehouse.Count; i++)
        {
            if (baseMapRole.warehouse[i].bulletType == BulletType.NormalPP)
            {
                GameObject Pruductgame = Instantiate(productPrb, productTF);
                Pruductgame.GetComponent<InitDanzhongPrb>().Init( RoleUpdateInfo.My.normallpp,  baseMapRole.warehouse[i].damage.ToString(),  baseMapRole.warehouse[i].loadingSpeed.ToString(),  baseMapRole.warehouse[i]);
             break;
             
            }
        }
        for (int i = 0; i < baseMapRole.warehouse.Count; i++)
        {
            if (baseMapRole.warehouse[i].bulletType == BulletType.Bomb)
            {
                GameObject Pruductgame = Instantiate(productPrb, productTF);
                Pruductgame.GetComponent<InitDanzhongPrb>().Init( RoleUpdateInfo.My.AOE,  baseMapRole.warehouse[i].damage.ToString(),  baseMapRole.warehouse[i].loadingSpeed.ToString(),  baseMapRole.warehouse[i]);
                break;
             
            }
        }
        for (int i = 0; i < baseMapRole.warehouse.Count; i++)
        {
            if (baseMapRole.warehouse[i].bulletType == BulletType.Lightning)
            {
                GameObject Pruductgame = Instantiate(productPrb, productTF);
                Pruductgame.GetComponent<InitDanzhongPrb>().Init( RoleUpdateInfo.My.lightning,  baseMapRole.warehouse[i].damage.ToString(),  baseMapRole.warehouse[i].loadingSpeed.ToString(),  baseMapRole.warehouse[i]);
                break;
             
            }
        }
        for (int i = 0; i < baseMapRole.warehouse.Count; i++)
        {
            if (baseMapRole.warehouse[i].bulletType == BulletType.summon)
            {
                GameObject Pruductgame = Instantiate(productPrb, productTF);
                Pruductgame.GetComponent<InitDanzhongPrb>().Init( RoleUpdateInfo.My.tow,  baseMapRole.warehouse[i].damage.ToString(),  baseMapRole.warehouse[i].loadingSpeed.ToString(),  baseMapRole.warehouse[i]);
                break;
             
            }
        }
          }
  
    public void ShowLastpruduct1(Role role)
    {
        BaseMapRole baseMapRole =    PlayerData.My.GetMapRoleById(role.ID);
        for (int i = 0; i <productTF.childCount; i++)
        {
            Destroy(productTF.GetChild(i).gameObject);
        }
        _datas.Clear();
        for (int i = 0; i <baseMapRole.warehouse.Count; i++)
        {
            baseMapRole.warehouse[i].RepeatBulletCount = 0;
        }

        for (int i = 0; i <  baseMapRole.warehouse.Count; i++)
        {
            int  isSameCount = 0;
            int count = 0;
            for (int j = 0; j <_datas.Count; j++)
            {
            
                if (_datas[j].CheckSame(baseMapRole.warehouse[i]))
                {
                    isSameCount++;
                    _datas[j].RepeatBulletCount++;
                    count++;
                }
            }

            if (isSameCount>0)
            {
  
                continue;
            }
            _datas.Add(baseMapRole.warehouse[i]);
            
        }

        for (int i = 0; i < _datas.Count; i++)
        {
            

         //   baseMapRole.warehouse[i].RepeatBulletCount = isSameCount;
            GameObject Pruductgame =  Instantiate(productPrb, productTF);
            Pruductgame.GetComponent<ProductSign>().currentProduct =
                _datas[i];

            Pruductgame.GetComponent<ProductSign>().conut.text =_datas[i].RepeatBulletCount.ToString();
      
       
          
        
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
        
         //  if (PlayerData.My.client != null)
         //  {
         //      Pruductgame.GetComponentInChildren<Text>().text = baseMapRole.warehouse[i].RepeatBulletCount.ToString();
         //  }
         //  else
         //  {
         //      Pruductgame.GetComponentInChildren<Text>().gameObject.SetActive(false);
         //  }
        }
    }
}
