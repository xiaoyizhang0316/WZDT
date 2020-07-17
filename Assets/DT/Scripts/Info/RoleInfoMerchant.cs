using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public Transform buffTf;
    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("UpdateBar",0.1f,0.2f);
    }

   
    public override void Init()
    {
        effect.text = CreatRoleManager.My.finalEffect.ToString();
        MoveSpeed.text =  CreatRoleManager.My.finalEfficiency.ToString()+"%";
        efficiency.text = CreatRoleManager.My.finalEfficiency.ToString();
        TradDown.text = (CreatRoleManager.My.finalEffect *0.3f + 24f).ToString()+"%";
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
        for (int i = 0; i < 4; i++)
        {
            try
            {
                buffTf.GetChild(i).gameObject.SetActive(true);
                buffTf.GetChild(i).GetComponent<WaveBuffSign>().Init(tempBuffList[i]);
            }
            catch (System.Exception e)
            {
                buffTf.GetChild(i).gameObject.SetActive(false);
            }
        }

    }
}
