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

    public List<Text> selectText;
    public List<GameObject> selectFlags;

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
        this.condition_1.text = skill.condition_1;
        this.condition_2.text = skill.condition_2;
        this.condition_3.text =skill.condition_3;
        if (skill.index == -1 && b.Threshold())
        {
            for (int i = 0; i <conditionButtons.Count; i++)
            {
                conditionButtons[i].interactable = true;
                selectText[i].text = rs.roleSkillSelect[i].checkDetails[0].checkContent;
                selectFlags[i].SetActive(false);
            }
            ConditionButtonClick();
        }
        else
        {
            for (int i = 0; i <conditionButtons.Count; i++)
            {
                conditionButtons[i].interactable = false;
                selectText[i].text = rs.roleSkillSelect[i].checkDetails[0].checkContent;
                if (skill.index == i)
                {
                    selectFlags[i].SetActive(true);
                }
                else
                {
                    selectFlags[i].SetActive(false);
                }
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
        selectFlags[index].SetActive(true);
    }
}
