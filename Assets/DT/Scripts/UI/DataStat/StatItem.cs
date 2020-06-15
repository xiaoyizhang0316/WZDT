using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatItem : MonoBehaviour
{
    public Text itemName;
    public Text itemPerMinAndTotal;
    //public Text itemTotal;

    public void Setup(string name, int perMin, int total)
    {
        itemName.text = name;
        itemPerMinAndTotal.text = string.Format($"{ perMin.ToString()}\t\t{total.ToString()}");
        //itemTotal.text = total.ToString();

        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }
}
