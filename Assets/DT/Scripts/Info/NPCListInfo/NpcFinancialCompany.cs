﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcFinancialCompany : MonoBehaviour
{

    public Text condition_1;
    public Text condition_2;
    public Text condition_3;

    public FinancialCompanySkill skill;

    public List<Button> conditionButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void Init( FinancialCompanySkill skill)
    {
        
        this.skill =  skill ;
        this.condition_1.text = skill.condition_1;
        this.condition_2.text = skill.condition_2;
        this.condition_3.text =skill. condition_3;
        if (skill.index == -1)
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
        skill.SkillOn(null);
        for (int i = 0; i <conditionButtons.Count; i++)
        {
            conditionButtons[i].interactable = false;
        }
    }
}
