using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_0_OtherOp:MonoSingleton<FTE_0_OtherOp>
{
    public GameObject flyMoney;
    public Transform flyMoneyParent;

    public bool consumeDie=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateFlyMoney(Vector3 pos)
    {
        if (!consumeDie)
        {
            consumeDie = true;
        }
        GameObject gameObject = Instantiate(flyMoney);
        gameObject.transform.position = pos;
        gameObject.GetComponent<FlyMoney>().Flying(flyMoneyParent);
    }
}
