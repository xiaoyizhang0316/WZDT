using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSet : MonoBehaviour
{
    public RectTransform bar1;
    public RectTransform bar2;
    public RectTransform bar3;

    public void SetBar(List<int> stageDan)
    {
        if (stageDan == null || stageDan.Count < 3)
        {
            return;
        }
        //Debug.Log(stageDan[0]+"," + stageDan[1]+","+stageDan[2]);
        var height1 = bar1.sizeDelta.y;
        var height2 = bar2.sizeDelta.y;
        var height3 = bar3.sizeDelta.y;
        bar1.sizeDelta = new Vector2(stageDan[0] / (float)stageDan[2] * CommonParams.bloodTotalWidth, height1);
        bar2.sizeDelta = new Vector2((stageDan[1]-stageDan[0]) / (float)stageDan[2] * CommonParams.bloodTotalWidth, height2);
        bar3.sizeDelta = new Vector2((stageDan[2]-stageDan[1]) / (float)stageDan[2] * CommonParams.bloodTotalWidth, height3);
    }
}
