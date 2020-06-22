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
        string permin = "";
        if (perMin != 0)
        {
            permin = perMin.ToString();
        }
        itemPerMinAndTotal.text = string.Format($"{ permin}\t\t{total}");
        //itemTotal.text = total.ToString();

        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }
}
