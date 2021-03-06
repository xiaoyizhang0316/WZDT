using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ConsumerBuffRange : MonoBehaviour
{

    public List<int> buffList = new List<int>();

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Consumer"))
        {
            for (int i = 0; i < buffList.Count; i++)
            {
                BuffData data = GameDataMgr.My.GetBuffDataByID(buffList[i]);
                BaseBuff buff = new BaseBuff();
                buff.Init(data);
                buff.SetConsumerBuff(other.GetComponent<ConsumeSign>());
            }
        }
    }
}
