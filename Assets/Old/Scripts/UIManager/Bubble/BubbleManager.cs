using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class BubbleManager : MonoSingleton<BubbleManager>
{
    public GameObject moneyPrb;

    public GameObject consumerPrb;

    public List<Sprite> consumerSprite;

    public List<Sprite> moneySprite;

    public List<ConsumerSatisfySign> consumerSatisfySigns;

    public List<SellMoneySign> sellMoneySigns;

    public void CreateConsumerBubble(float satisfy,Vector3 pos,double id)
    {
        GameObject go = Instantiate(consumerPrb, pos, transform.rotation, transform);
        go.GetComponent<ConsumerSatisfySign>().Init(satisfy,id);
    }

    public void CreateMoneyBubble(int money, Vector3 pos,double id)
    {
        GameObject go = Instantiate(moneyPrb, pos, transform.rotation, transform);
        go.GetComponent<SellMoneySign>().Init(money,id);
    }

    public void BreakRoleBubble(double id)
    {
        for (int i = 0; i < consumerSatisfySigns.Count; i++)
        {
            if (Mathf.Abs((float)(consumerSatisfySigns[i].mapRoleId - id)) < 0.1f)
            {
                consumerSatisfySigns[i].CheckBreak();
                //print("find");
            }
        }
        for (int i = 0; i < sellMoneySigns.Count; i++)
        {
            if (Mathf.Abs((float)(sellMoneySigns[i].mapRoleId - id)) < 0.1f)
            {
                sellMoneySigns[i].CheckBreak();
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
