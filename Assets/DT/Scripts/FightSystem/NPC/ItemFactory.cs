using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : BaseExtraSkill
{
    /// <summary>
    /// 消耗品列表
    /// </summary>
    public List<int> consumableList = new List<int>();

    /// <summary>
    /// 每回合给的消耗品数量
    /// </summary>
    public int giveNumber;


    public override void OnEndTurn()
    {
        if (!isOpen)
        {
            return;
        }
        for (int i = 0; i < giveNumber; i++)
        {
            int index = Random.Range(0, consumableList.Count);
            PlayerData.My.GetNewConsumalbe(consumableList[index]);
        }
        base.OnEndTurn();
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }
}
