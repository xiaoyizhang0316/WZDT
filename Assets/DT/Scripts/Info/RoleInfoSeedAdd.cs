using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
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
    // Start is called before the first frame update
    void Start()
    {
        Init();
        InvokeRepeating("UpdateBar",0.1f,0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        Init();
    }

    public override void Init()
    {
        effect.text = CreatRoleManager.My.finalEffect.ToString();
        Damage.text = (CreatRoleManager.My.finalEffect * 10f).ToString();
        efficiency.text = CreatRoleManager.My.finalEfficiency.ToString();
        productTime.text = (CreatRoleManager.My.finalEfficiency / 10f).ToString()+" /s";
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
