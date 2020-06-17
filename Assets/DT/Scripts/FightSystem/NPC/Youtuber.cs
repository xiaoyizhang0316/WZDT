using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Youtuber : BaseExtraSkill
{
    public int buffId;

    public GameObject youtuberItemPrb;

    public override void SkillOn(TradeSign sign)
    {
        base.SkillOn(sign);
        GameObject go = Instantiate(youtuberItemPrb, PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.targetRole)).transform);
        go.transform.position = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.targetRole)).transform.position;
        go.GetComponent<YoutuberItem>().Init(buffId, sign.tradeData.ID);
    }

    public override void SkillOff(TradeSign sign)
    {
        base.SkillOff(sign);
        YoutuberItem[] list = FindObjectsOfType<YoutuberItem>();
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].tradeId == sign.tradeData.ID)
            {
                Destroy(list[i].gameObject);
            }
        }
    }

    private void Update()
    {
        if (isOpen)
        {

        }
    }
}
