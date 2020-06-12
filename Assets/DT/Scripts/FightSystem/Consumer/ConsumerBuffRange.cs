using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
