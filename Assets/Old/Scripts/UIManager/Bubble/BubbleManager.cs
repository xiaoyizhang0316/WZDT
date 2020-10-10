using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class BubbleManager : MonoSingleton<BubbleManager>
{
    public GameObject costMoneyPrb;

    public void InitCostMoney(Transform t,int number)
    {
        GameObject go = Instantiate(costMoneyPrb, transform);
        go.GetComponent<Text>().text = number.ToString();
        go.transform.localPosition = Camera.main.WorldToViewportPoint(t.position);
    }
}
