using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Youtuber : BaseServiceSkill
{
    public int buffId;

    public GameObject youtuberItemPrb;

    public override void Skill(TradeData data)
    {
        base.Skill(data);
        GameObject go = Instantiate(youtuberItemPrb, PlayerData.My.GetMapRoleById(double.Parse(data.targetRole)).transform);
        go.transform.position = PlayerData.My.GetMapRoleById(double.Parse(data.targetRole)).transform.position + new Vector3(0f,-1.2f,0f);
        go.GetComponent<YoutuberItem>().Init(buffId, data.ID);
    }

    public override void SkillOff(TradeData data)
    {
        base.SkillOff(data);
        YoutuberItem[] list = FindObjectsOfType<YoutuberItem>();
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].tradeId == data.ID)
            {
                Destroy(list[i].gameObject);
            }
        }
    }
}
