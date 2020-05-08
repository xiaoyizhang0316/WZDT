using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ThirdParty : MonoBehaviour
{

    public void OnValueChange()
    {
        Dropdown temp = GetComponent<Dropdown>();
        CreateTradeManager.My.thirdPartyRole = temp.options[temp.value].text;
    }


    public void Init()
    {
        Dropdown temp = GetComponent<Dropdown>();
        temp.options.Clear();
        SkillData data = GameDataMgr.My.GetSkillDataByName(CreateTradeManager.My.selectJYFS);
        if (!data.supportThird)
        {
            temp.options.Add(new Dropdown.OptionData("当前技能不支持第三方技能目标",null));
            temp.interactable = false;
            temp.value = 0;
            temp.captionText.text = temp.options[0].text;
        }
        else
        {
            temp.interactable = true;
            List<string> excludeList = new List<string>();
            excludeList.Add(CreateTradeManager.My.castRole);
            excludeList.Add(CreateTradeManager.My.targetRole);
            for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
            {
                if (PlayerData.My.RoleData[i].inMap && !excludeList.Contains(PlayerData.My.RoleData[i].ID.ToString()))
                {
                    temp.options.Add(new Dropdown.OptionData(PlayerData.My.RoleData[i].ID.ToString(),null));
                }
            }
            if(CreateTradeManager.My.currentTrade.isFirstSelect)
            {
                if (temp.options.Count > 0)
                {
                    temp.value = 0;
                    CreateTradeManager.My.thirdPartyRole = temp.options[0].text;
                    temp.captionText.text = CreateTradeManager.My.thirdPartyRole;
                }
                else
                {
                    temp.options.Add(new Dropdown.OptionData("无第三方目标可选", null));
                    temp.interactable = false;
                    temp.value = 0;
                    temp.captionText.text = temp.options[0].text;
                }
            }
            else
            {
                for (int i = 0; i < temp.options.Count; i++)
                {
                    if (CreateTradeManager.My.thirdPartyRole.Equals(temp.options[i].text))
                    {
                        temp.value = i;
                    }
                }
            }
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
