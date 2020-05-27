using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoleInfoMerchant : BaseRoleInfoAdd
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
    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("UpdateBar",0.1f,0.2f);
    }

   
    public override void Init()
    {
        effect.text = CreatRoleManager.My.finalEffect.ToString();
        MoveSpeed.text =  CreatRoleManager.My.finalEffect  .ToString()+"%";
        efficiency.text = CreatRoleManager.My.finalEfficiency.ToString();
        TradDown.text = (((CreatRoleManager.My.finalEfficiency *0.3f)/100f)*  CreatRoleManager.My.finalTradeCost).ToString()+" /s";
        tradCost.text  =  CreatRoleManager.My.finalTradeCost.ToString();
        risk .text =  CreatRoleManager.My.finalRiskResistance.ToString();
        montyCost.text =  CreatRoleManager.My.finalCost.ToString();
        technology.text = CreatRoleManager.My.finalTechAdd.ToString();

    }

    public void UpdateBar()
    {
        efficiencyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(CreatRoleManager.My.finalEfficiency / 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
        effectyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(CreatRoleManager.My.finalEffect / 120f * 150f,
                effectyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
    }
}
