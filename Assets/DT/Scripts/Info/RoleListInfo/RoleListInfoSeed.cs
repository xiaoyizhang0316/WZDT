using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoleListInfoSeed : BaseRoleListInfo
{
    public Text effect;
    public Text Damage;

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
        float efficiencyNum = (role.efficiency / 20f);
        float add = 1f;
        float encourage = PlayerData.My.GetMapRoleById(role.ID).encourageLevel;
        productTime.color = Color.white;
        if (encourage > 0)
        {
            add += encourage * 0.05f;
            productTime.color = Color.green;
        }
        else if (encourage < 0)
        {
            add += encourage * 0.1f;
            productTime.color = Color.red;
        }
        efficiencyNum *= add;
        effect.text = role.effect.ToString();
        Damage.text = (role.effect * 10f).ToString();
        efficiency.text = role.efficiency.ToString();
        productTime.text = (efficiencyNum).ToString("F2");
        tradCost.text = role.tradeCost.ToString();
        risk.text = role.riskResistance.ToString();
        if (StageGoal.My.currentType == GameEnum.StageType.Normal && !CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
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
            new Vector2(role.efficiency / 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
        effectyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(role.effect / 120f * 150f,
                effectyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
    }

    public void ShowLastpruduct(Role role)
    {
        BaseMapRole baseMapRole = PlayerData.My.GetMapRoleById(role.ID);
        for (int i = 0; i < productTF.childCount; i++)
        {
            Destroy(productTF.GetChild(i).gameObject);
        }

        int count = 9;
        if (baseMapRole.GetComponent<ProductSeed>().productDatas.Count < 9)
        {
            count = baseMapRole.GetComponent<ProductSeed>().productDatas.Count;
        }


        for (int i = 1; i <= count; i++)
        {
            //Debug.Log(i + "||" + baseMapRole.GetComponent<ProductSeed>().productDatas.Count);
            GameObject Pruductgame = Instantiate(productPrb, productTF);
         

            Pruductgame.GetComponent<ProductSign>().currentProduct =
                baseMapRole.GetComponent<ProductSeed>().productDatas[baseMapRole.GetComponent<ProductSeed>().productDatas.Count - i];
            Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.seedSpeed;
            if (Pruductgame.GetComponent<ProductSign>().currentProduct.wasteBuffList.Count > 0)
            {
                Pruductgame.GetComponent<Image>().color = new Color(1,0.6f,0.6f,1);
            }
         //   if (PlayerData.My.client != null)
         //   {
         //       Pruductgame.GetComponentInChildren<Text>().text = baseMapRole.GetComponent<ProductSeed>()
         //           .productDatas[baseMapRole.GetComponent<ProductSeed>().productDatas.Count - i].RepeatBulletCount.ToString();
         //   }
         //   else
         //   {
         //       Pruductgame.GetComponentInChildren<Text>().gameObject.SetActive(false);
         //   }

        }
    }
}
