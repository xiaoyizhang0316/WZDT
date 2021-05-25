using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcFinancialCompany : MonoBehaviour
{

    public Text condition_1;
    public Text condition_2;
    public Text condition_3;
    public Text f_name_text;
    public Text threshold_text;

    public BaseFinancialSkill skill;
    public BaseFinancialCompanyThreshold b;

    public List<Button> conditionButtons;

    // Start is called before the first frame update
    void Start()
    {
        //b = skill.GetComponent<BaseFinancialCompanyThreshold>();
    }
    
    public void Init( BaseMapRole role)
    {
        RoleSkillSelect rs = SkillCheckManager.My.GetRoleCheckDetailByType(role.baseRoleData);
        this.skill =  role.GetComponent<BaseFinancialSkill>() ;
        b = role.GetComponent<BaseFinancialCompanyThreshold>();
        threshold_text.text = b.ThresholdTip();
        threshold_text.color = b.Threshold() ? Color.green : Color.red;
        f_name_text.text = role.baseRoleData.baseRoleData.roleName;
        this.condition_1.text = skill.condition_1+"\n"+rs.roleSkillSelect[0].checkDetails[0].checkContent;
        this.condition_2.text = skill.condition_2+"\n"+rs.roleSkillSelect[1].checkDetails[0].checkContent;
        this.condition_3.text =skill.condition_3+"\n"+rs.roleSkillSelect[2].checkDetails[0].checkContent;
        if (skill.index == -1 && b.Threshold())
        {
            for (int i = 0; i <conditionButtons.Count; i++)
            {
                conditionButtons[i].interactable = true;
            }
            ConditionButtonClick();
        }
        else
        {
            for (int i = 0; i <conditionButtons.Count; i++)
            {
                conditionButtons[i].interactable = false;
            }
        }
        skill.GetComponent<BaseMapRole>().HideTradeButton(false);
    }

    public void  ConditionButtonClick()
    {
       
        conditionButtons[0].onClick.RemoveAllListeners();
        conditionButtons[1].onClick.RemoveAllListeners();
        conditionButtons[2].onClick.RemoveAllListeners();
        
            conditionButtons[0].onClick.AddListener(() =>
            {
                InitButton(0); 
                
            });
            conditionButtons[1].onClick.AddListener(() =>
            {
                InitButton(1); 
                
            });
            conditionButtons[2].onClick.AddListener(() =>
            {
                InitButton(2); 
                
            });
    }

    public void  InitButton(int index)
    {
        skill.index = index;
        //skill.isActive = true;
        skill.Skill();
        for (int i = 0; i <conditionButtons.Count; i++)
        {
            conditionButtons[i].interactable = false;
        }
    }
}
