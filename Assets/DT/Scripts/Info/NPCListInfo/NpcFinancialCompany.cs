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
    }

    public void  ConditionButtonClick()
    {
        for (int i = 0; i <conditionButtons.Count; i++)
        {
            conditionButtons[i].onClick.AddListener(() => { InitButton(i); });
        }
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
