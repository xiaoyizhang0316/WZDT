﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoleListInfoService : BaseRoleListInfo
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

    public Image mainBuff;
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
        if (role.isNpc)
        {
            montyCost.gameObject.SetActive(false);
        }
        else
        {
            montyCost.gameObject.SetActive(true);

        }

        float efficiencyNum = (role.efficiency / 20f);
        float add = 1f;
        float encourage = PlayerData.My.GetMapRoleById(role.ID).encourageLevel;
        productTime.color = Color.black;
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
        mainBuff.GetComponent<ShowBuffText>().role =  PlayerData.My.GetMapRoleById(role.ID);
        mainBuff.sprite = Resources.Load<Sprite>("Sprite/Buff/" + PlayerData.My.GetMapRoleById(role.ID).buffList[0]);
        effect.text =role.effect.ToString();
        reload.text = role.effect  .ToString()+"%";
        efficiency.text = role.efficiency.ToString();
        productTime.text = (efficiencyNum).ToString("F1");
        tradCost.text  =  role.tradeCost.ToString();
        risk .text = role.riskResistance.ToString();
        if (StageGoal.My.currentType == GameEnum.StageType.Normal && !CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
        {
            montyCost.text = (role.cost * 2).ToString();
        }
        else
        {
            montyCost.text = role.cost.ToString();
        }
        technology.text =  role.techAdd.ToString(); 
       
    }
    
}