using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class T0_5_12 : BaseGuideStep
{


 

   
    public int count;
    public int time;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        StageGoal.My.killNumber = 0;


   HexCell cell =    HexGrid.My.GetCell(new HexCoordinates(4,17));
   cell.TerrainTypeIndex = 1;
        var list = FindObjectsOfType<ConsumeSign>();
        for (int i = 0; i <list.Length ; i++)
        {
            Destroy(list[i].gameObject);
        }
        StageGoal.My.killNumber = 0;
         transform.DOScale(1, 1).OnComplete(() =>
        {
            StartCoroutine(BuildingManager.My.buildings[0]
                .BornSingleTypeConsumer(GameEnum.ConsumerType.ConsumerModel1, count));
            Addxiaofei();
        }).Play(); 
        yield return new WaitForSeconds(1f);

    }

    private Tween t;
   
    public void Addxiaofei()
    {
          t= transform.DOScale(1, time).OnComplete(() =>
        {
            StartCoroutine(BuildingManager.My.buildings[0]
                .BornSingleTypeConsumer(GameEnum.ConsumerType.ConsumerModel1, count));
            Addxiaofei();
        }).Play();
          
        }

    public override IEnumerator StepEnd()
    { 
        t.Kill();  

        yield return new WaitForSeconds(2);  
    }

    public override bool ChenkEnd()
    {
       
        missiondatas.data[0].currentNum = StageGoal.My.killNumber;
        if (StageGoal.My.killNumber >= missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
        }
        if (  missiondatas.data[0].isFinish)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
   
}
 