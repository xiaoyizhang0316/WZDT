using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoleListInfoDealer : BaseRoleListInfo
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init(Role role)
    {
        fireTime.text = (role.efficiency  ).ToString()+"%";
        efficiency.text =role.efficiency.ToString();
        Range.text = (role.range ).ToString() ;
        tradCost.text  =  role.cost.ToString();
        risk .text =  role.riskResistance.ToString();
        montyCost.text =  role.cost.ToString();
        technology.text = role.techAdd.ToString();
        UpdateBar(role);
    }
    public void UpdateBar(Role role)
    {
        efficiencyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(role.efficiency/ 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
        
    }
}
