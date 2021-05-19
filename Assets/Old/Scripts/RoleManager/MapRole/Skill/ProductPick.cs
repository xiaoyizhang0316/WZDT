using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProductPick : BaseProductSkill
{
    public int buffId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Skill()
    {
        if (role.tradeList.Count == 0)
        {
            return;
        }

        if (role.warehouse.Count == 0)
        {
            return;
        }

        role.warehouse.Remove(role.warehouse[0]);

        for (int i = 0; i < role.shootTargetList.Count; i++)
        {
            BuffData data = GameDataMgr.My.GetBuffDataByID(buffId);
            BaseBuff baseBuff = new BaseBuff();
            baseBuff.Init(data);
            baseBuff.SetConsumerBuff(role.shootTargetList[i]);
            
        }
    }


    public override void UnleashSkills()
    {
        transform.DORotate(transform.eulerAngles, 1).OnComplete(() =>
        {
            Skill();
            if (IsOpen)
            {
                UnleashSkills();
            }
        });
    }

// Update is called once per frame
    void Update()
    {
        
    }
}
