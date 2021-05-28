using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceStall : BaseServiceSkill
{
    public GameObject stallItem;

    public override void Skill(TradeData data)
    {
        base.Skill(data);
        GameObject go = Instantiate(stallItem, PlayerData.My.GetMapRoleById(double.Parse(data.targetRole)).transform);
        go.transform.position = PlayerData.My.GetMapRoleById(double.Parse(data.targetRole)).transform.position + new Vector3(0f,-1.2f,0f);
        go.GetComponent<StallItem>().Init(buffId, data.ID);
    }

    public override void SkillOff(TradeData data)
    {
        base.SkillOff(data);
        StallItem[] list = FindObjectsOfType<StallItem>();
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].tradeId == data.ID)
            {
                Destroy(list[i].gameObject);
            }
        }
    }
}
