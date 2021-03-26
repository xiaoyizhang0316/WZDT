using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_2 : BaseGuideStep
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InitHighlightUI()
    {
     //   highLight2DObjList.Add(WaveCount.My.transform.GetChild(0).GetChild(3).GetChild(0).gameObject);
    }

    public override IEnumerator StepStart()
    {
        for (int i = 0; i <MapManager.My._mapSigns.Count; i++)
        {
            MapManager.My._mapSigns[i].isCanPlace = false;
        }

        MapManager.My.GetMapSignByXY(9, 24).isCanPlace = true;
        yield break;
         
    }

    public override IEnumerator StepEnd()
    {
     
 
        yield break;
    }

    public override bool ChenkEnd()
    {
        if (WaveCount.My.waveBg.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
