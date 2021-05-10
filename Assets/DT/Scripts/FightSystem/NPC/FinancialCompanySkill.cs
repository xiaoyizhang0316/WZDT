using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinancialCompanySkill : BaseExtraSkill
{
    public string condition_1;
    public string condition_2;
    public string condition_3;

    public int index =-1;
    // Start is called before the first frame update
    void Start()
    {
       GetComponent<BaseMapRole>().HideTradeButton(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SkillOn(TradeSign sign)
    {
        
    }
}
