using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_6 : BaseGuideStep
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
    // highLight2DObjList.Add(EquipListManager.My._signs[0].gameObject);
    }

    public override IEnumerator StepStart()
    {
      
        yield break;
         
    }

    public override IEnumerator StepEnd()
    {
        for (int i = 0; i <MapManager.My._mapSigns.Count; i++)
        {
            if (MapManager.My._mapSigns[i].mapType == GameEnum.MapType.Grass)
            {
                MapManager.My._mapSigns[i].isCanPlace = true;
            }
        }
        yield break;
        
    }

 
}
