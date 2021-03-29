using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoleInfoDealer : BaseRoleInfoAdd
{ 
    
    public Text fireTime;

    public Text efficiency;

    public Text text_Range;

    public Text Range;

    public Text tradCost;
    public Text risk;
    public Text montyCost;

    public Text technology;

    public GameObject efficiencyBar;

    public GameObject rangeBar;

    public Transform buffTf;
    // Start is called before the first frame update
    void Start()
    {
    
        //InvokeRepeating("UpdateBar",0.1f,0.2f);
    }


    public override void Init()
    {
        float efficiencyNum =1f / (CreatRoleManager.My.finalEfficiency * -0.01f + 1.5f);
        float add = 1f;
        float encourage = PlayerData.My.GetMapRoleById(CreatRoleManager.My.CurrentRole.ID).encourageLevel + CreatRoleManager.My.finalEncourageAdd;
        fireTime.color = Color.white;
        if (encourage > 0)
        {
            add += encourage * 0.05f;
            fireTime.color = Color.green;
        }
        else if (encourage < 0)
        {
            add += encourage * 0.1f;
            fireTime.color = Color.red;
        }
        efficiencyNum *= add;
        fireTime.text = efficiencyNum.ToString("F2") + "/s";
        efficiency.text = CreatRoleManager.My.finalEfficiency.ToString();
        Range.text = (CreatRoleManager.My.finalRange / 14.5f).ToString("F2") ;
        tradCost.text  =  CreatRoleManager.My.finalTradeCost.ToString();
        risk .text =  CreatRoleManager.My.finalRiskResistance.ToString();
        if (StageGoal.My.currentType == GameEnum.StageType.Normal && !CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
        {
            montyCost.text = (CreatRoleManager.My.finalCost * 2).ToString();
        }
        else
        {
            montyCost.text = (CreatRoleManager.My.finalCost).ToString();
        }
        technology.text = CreatRoleManager.My.finalTechAdd.ToString();
        text_Range.text = (CreatRoleManager.My.finalRange).ToString();
    }

    public override void UpdateBar()
    {
        efficiencyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(CreatRoleManager.My.finalEfficiency / 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
        rangeBar.GetComponent<RectTransform>().DOSizeDelta(
           new Vector2(CreatRoleManager.My.finalRange / 120f * 150f,
               rangeBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
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
        BaseMapRole role = PlayerData.My.GetMapRoleById(CreatRoleManager.My.CurrentRole.ID);
        tempBuffList.AddRange(role.tasteBuffList);
        for (int i = 0; i < 4; i++)
        {
            try
            {
                buffTf.GetChild(i).gameObject.SetActive(true);
                buffTf.GetChild(i).GetComponent<WaveBuffSign>().Init(tempBuffList[i]);
            }
            catch (Exception e)
            {
                buffTf.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
