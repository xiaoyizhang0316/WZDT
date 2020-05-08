using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RoleText1 : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
  

    public void OnGUI()
    {
      
    }

    public string  Seedband()
    {
        
      float band = ((1+(float)(UIManager.My.currentMapRole.baseRoleData.brand *0.1)/100)* ( UIManager.My.currentMapRole.baseRoleData.brand * 0.7f) );
      int b = (int) band; 
      string a = "影响生产种子的" + "<color=#F87C02>" + "品牌值" + "</color>" + "，当前产品" + "<color=#F87C02>" + "品牌值" + "</color>" +
                 "约为 " + b.ToString()  +"\n";
      a+= "影响研发出种子的" + "<color=#F87C02>" + "品牌值" + "</color>" + "，当前新产品" + "<color=#F87C02>" + "品牌值" + "</color>" +
          "约为 " +(UIManager.My.currentMapRole.baseRoleData.brand * 0.7f).ToString()  +"\n";
      a += "影响零售时所吸引消费者的范围";
      return a;
    }
    public string  SeedQulity()
    {
        int random;
        if (UIManager.My.currentMapRole.baseRoleData.quality >=70)
        {
            random = Random.Range(50,  70);
        }
        else if (UIManager.My.currentMapRole.baseRoleData.quality  >=40)
        {
            random = Random.Range(15,  50);
        }
        else if (UIManager.My.currentMapRole.baseRoleData.quality  >= 15)
        {
            random = Random.Range(5,  25); 
        }
        else
        {
            random = Random.Range(1,  10);  
        }

       int Quality =(int)( UIManager.My.currentMapRole.baseRoleData.quality*0.6f+random);
        float band = ((1+(float)( UIManager.My.currentMapRole.baseRoleData.quality *0.2)/100)* Quality );
        int b = (int) band;
        string a = "影响生产种子的" + "<color=#F87C02>" + "质量" + "</color>" + "，当前产品" + "<color=#F87C02>" + "质量" + "</color>" +
                   "约为 " + b.ToString()  +"\n";
        a+= "影响研发出种子的" + "<color=#F87C02>" + "质量" + "</color>" + "，当前新产品" + "<color=#F87C02>" + "质量" + "</color>" +
            "约为 " +Quality  +"\n"; 
        return a;
    }

    public string Seedcapacity()
    {
        return "暂无";
    }

    public string Seedefficiency()
    {
        string a = "影响生产种子的时间，当前约为"+(int)(12 / (1 +UIManager.My.currentMapRole.baseRoleData.efficiency / 100f))+"秒"+"\n";
        a += "影响研发种子的时间，当前约为" + (int)(10 / (1 +UIManager.My.currentMapRole.baseRoleData.efficiency / 100f)) + "秒";
        return a;
    }

    public string PeasantBand()
    {
        string a = "影响种植出产品的" + "<color=#F87C02>" + "品牌值" + "</color>" + "，处理" + "<color=#F87C02>" + "品牌值" + "</color>" + "小于"+UIManager.My.currentMapRole.baseRoleData.brand/0.8+"的原材料时对产品品牌值有加成";
        a += "影响零售时所吸引消费者的范围";
        return a;
    }

    public string PeasantQulity()
    {
        string a = "影响种植出产品的" + "<color=#F87C02>" + "质量" + "</color>" + "，处理" + "<color=#F87C02>" + "质量" + "</color>" + "小于"+UIManager.My.currentMapRole.baseRoleData.quality/0.8+"的原材料时对产品质量有加成";
        return a;
    }

    public string Peasantcapacity()
    {
        string a = "影响产量，当前产量约为"+500+"";
        return a;
    }

    public string Peasantefficiency()
    {
        string a = "影响生产种子的时间，当前约为"+(int)(12 / (1 +UIManager.My.currentMapRole.baseRoleData.efficiency / 100f))+"秒"+"\n";
        return a;
    }

    public string MerchantBand()
    {
        return "暂无";
    }
    public string MerchantQulity()
    {
        return "影响贸易商运输载具数量（暂未实装）";
    }   
    public string Merchantcapacity()
    {
        return "影响贸易商对运输加速的范围";
    }
    public string Merchantefficiency()
    {
        return "影响贸易商对运输加速的效果";
    }
    public string DealerBand()
    {
        string a =  "影响零售时所吸引消费者的范围";
        a += "影响服务、加工类产品的" + "<color=#F87C02>" + "品牌值" + "</color>" + "，处理" + "<color=#F87C02>" + "质量" + "</color>" + "小于"+UIManager.My.currentMapRole.baseRoleData.brand/0.8+"的原材料时对产品品牌有加成";
        return a;
    }
    public string DealerQulity()
    {
        return "影响零售时产品价格，当前增加约为" + UIManager.My.currentMapRole.baseRoleData.quality*0.2 ;
    }   
    public string Dealercapacity()
    {
        return "影响零售商对消费者加速的范围";
    }
    public string Dealerefficiency()
    {
        return "影响零售商对消费者加速的效果";
    }
}
