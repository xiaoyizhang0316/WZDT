using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetalDataSkillInfo : MonoBehaviour
{
    public Text faqi;

    public Text chengshou;

    public Text chengben;

    public Text jiaoyichengben;

    public Text SkillName;

    public Text income;

    public Image JYFS;

    public Image SZLY;

    public Image SZFS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitUI(string skillName,string faqi ,string chengshou ,string chengben ,string jiaoyichengben )
    {
        this.faqi.text = faqi;
        this.chengshou.text = chengshou;
        this.SkillName.text = skillName;
        this.chengben.text = chengben;
        this.jiaoyichengben.text = jiaoyichengben;
    }

    public void InitUI(BaseMapRole.TradeRecordData data)
    {
        faqi.text = data.startRole;
        chengshou.text = data.endRole;
        SkillName.text = data.selectSkill;
        chengben.text = data.skillCost.ToString();
        jiaoyichengben.text = data.transactionCost.ToString();
        income.text = data.income.ToString();
    }
}
