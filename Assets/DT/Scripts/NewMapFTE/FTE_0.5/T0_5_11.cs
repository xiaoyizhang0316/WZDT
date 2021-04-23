using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class T0_5_11 : BaseGuideStep
{


    public GameObject roleImage;

   
    public int count;
    public int time;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
 
       FTE_0_5Manager.My.consumerSpot.SetActive(true);
       FTE_0_5Manager.My.endPoint.SetActive(true);
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
        roleImage.SetActive(true);
        roleImage.SetActive(false);
      
            PlayerData.My.DeleteRole(PlayerData.My.RoleData[0].ID);
            PlayerData.My.DeleteRole(PlayerData.My.RoleData[0].ID);
            PlayerData.My.DeleteRole(PlayerData.My.RoleData[0].ID);
            PlayerData.My.DeleteRole(PlayerData.My.RoleData[0].ID);
    
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Map");

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
 