using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoleInfoSeedAdd : BaseRoleInfoAdd
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

    public Transform buffTf;
    // Start is called before the first frame update
    void Start()
    {
        //Init();
        //UpdateBar();
        //InvokeRepeating("UpdateBar",0.1f,0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //Init();
        //UpdateBar();
    }


    public override void Init()
    {
        float efficiencyNum = (CreatRoleManager.My.finalEfficiency / 20f);
        float add = 1f;
        float encourage = PlayerData.My.GetMapRoleById(CreatRoleManager.My.CurrentRole.ID).encourageLevel + CreatRoleManager.My.finalEncourageAdd;
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
        effect.text = CreatRoleManager.My.finalEffect.ToString();
        Damage.text = (CreatRoleManager.My.finalEffect * 10f).ToString();
        efficiency.text = CreatRoleManager.My.finalEfficiency.ToString();
        productTime.text = (efficiencyNum).ToString("F2")+" /s";
        tradCost.text  =  CreatRoleManager.My.finalTradeCost.ToString();
        risk .text =  CreatRoleManager.My.finalRiskResistance.ToString();
        if (StageGoal.My.currentType == GameEnum.StageType.Normal && !CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
        {
            montyCost.text = (CreatRoleManager.My.finalCost * 2).ToString();
        }
        else
        {
            montyCost.text = (CreatRoleManager.My.finalCost).ToString();
        }
        technology.text = CreatRoleManager.My.finalTechAdd.ToString();
        ShowLastpruduct(CreatRoleManager.My.CurrentRole);
        //   if (int.Parse(SceneManager.GetActiveScene().name.Split('_')[1]) >3)
        //   {
        //       transform.Find("Image_TradCost").gameObject.SetActive(true);
        //       transform.Find("Text_TradCost").gameObject.SetActive(true);
        //       transform.Find("Text_Trad").gameObject.SetActive(true);
        //       transform.Find("Image_risk").gameObject.SetActive(true);
        //       transform.Find("Text_risk").gameObject.SetActive(true);
        //       transform.Find("Text_riskchi").gameObject.SetActive(true);
        //   }
        //   else
        //   {
        //       transform.Find("Image_TradCost").gameObject.SetActive(false);
        //       transform.Find("Text_TradCost").gameObject.SetActive(false);
        //       transform.Find("Text_Trad").gameObject.SetActive(false);
        //       transform.Find("Image_risk").gameObject.SetActive(false);
        //       transform.Find("Text_risk").gameObject.SetActive(false);
        //       transform.Find("Text_riskchi").gameObject.SetActive(false);
        //   }
    }

    public override void UpdateBar()
    {
        efficiencyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(CreatRoleManager.My.finalEfficiency / 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
        effectyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(CreatRoleManager.My.finalEffect / 120f * 150f,
                effectyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
    }

    public void ShowLastpruduct(Role role)
    {
        BaseMapRole baseMapRole = PlayerData.My.GetMapRoleById(role.ID);
        for (int i = 0; i < CreatRoleManager.My.goodsTF.childCount; i++)
        {
            Destroy(CreatRoleManager.My.goodsTF.GetChild(i).gameObject);
        }

        int count = 9;
        if (baseMapRole.GetComponent<ProductSeed>().productDatas.Count < 9)
        {
            count = baseMapRole.GetComponent<ProductSeed>().productDatas.Count;
        }


        for (int i = 1; i <= count; i++)
        {
            //Debug.Log(i + "||" + baseMapRole.GetComponent<ProductSeed>().productDatas.Count);
            GameObject Pruductgame = Instantiate(CreatRoleManager.My.goodsPrb, CreatRoleManager.My.goodsTF);
         
            Pruductgame.GetComponent<ProductSign>().Image.gameObject.SetActive(false);
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
    public override void UpdateBuff()
    {
        List<int> equipId = CreatRoleManager.My.EquipList.Keys.ToList();
        List<int> tempBuffList = new List<int>();
        for (int i = 0; i < equipId.Count; i++)
        {
            GearData data = GameDataMgr.My.GetGearData(equipId[i]);
            if (data.buffList[0] != -1)
            {
                tempBuffList.Add(data.buffList[0]);
            }
        }
        BaseMapRole role = PlayerData.My.GetMapRoleById(CreatRoleManager.My.CurrentRole.ID);
        tempBuffList.AddRange(role.tasteBuffList);
        for (int i = 0; i < 4; i++)
        {
            try
            {
                buffTf.GetChild(i).gameObject.SetActive(true);
                buffTf.GetChild(i).GetComponent<WaveBuffSign>().Init(tempBuffList[i]);
            }
            catch (Exception e)
            {
                buffTf.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
