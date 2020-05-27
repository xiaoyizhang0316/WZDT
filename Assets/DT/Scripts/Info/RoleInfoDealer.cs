using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoleInfoDealer : BaseRoleInfoAdd
{ 
   
    public Text fireTime;

    public Text efficiency;

    public Text Range;

    public Text tradCost;
    public Text risk;
    public Text montyCost;

    public Text technology;

    public GameObject efficiencyBar; 
    // Start is called before the first frame update
    void Start()
    {
   
        InvokeRepeating("UpdateBar",0.1f,0.2f);
    }
 

    public override void Init()
    {
       
        fireTime.text = (CreatRoleManager.My.finalEfficiency  ).ToString()+"%";
        efficiency.text = CreatRoleManager.My.finalEfficiency.ToString();
        Range.text = (CreatRoleManager.My.finalRange ).ToString() ;
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
        
    }
}
