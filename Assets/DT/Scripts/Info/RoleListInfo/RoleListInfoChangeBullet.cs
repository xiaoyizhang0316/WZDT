using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleListInfoChangeBullet : BaseRoleListInfo
{
   
    public Text productTime;

    public Text tradCost;
   
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
        float efficiencyNum = (role.efficiency / 20f);
        float add = 1f;
        float encourage = PlayerData.My.GetMapRoleById(role.ID).encourageLevel;
        productTime.color = Color.black;
        if (encourage > 0)
        {
            add += encourage * 0.05f;
            productTime.color = Color.green;
        }
        else if (encourage < 0)
        {
            add += encourage * 0.1f;
            productTime.color = Color.red;
        }
        efficiencyNum *= add;
       
        productTime.text = (efficiencyNum).ToString("F1");
        tradCost.text  =  role.tradeCost.ToString();
      
    }
}
