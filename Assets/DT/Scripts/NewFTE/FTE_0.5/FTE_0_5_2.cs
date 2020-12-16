using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_0_5_2 : BaseGuideStep
{
    public List<GameObject> land;
    public List<GameObject> Seedtesting;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        
        
        yield return new WaitForSeconds(1f);
        for (int i = 0; i <land.Count; i++)
        {
            land[i].transform.DOLocalMoveY(-5, 1f).Play();
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i <land.Count; i++)
        {
            land[i].transform.DOLocalMoveY(0, 1f).Play();
       
        }

        for (int i = 0; i < Seedtesting.Count; i++)
        {
            Seedtesting[i].SetActive(true);
            Seedtesting[i].transform.DOLocalMoveY(0.3f, 1f).Play();

        }
      
    }

    public override IEnumerator StepEnd()
    {
        missiondatas.data[0].currentNum = 1; 
        missiondatas.data[0].isFinish= true; 
        
      yield break;
      ;
    }

    public override bool ChenkEnd()
    {
        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (PlayerData.My.RoleData[i].baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                return true;
            }
        }

        return false;
    }

 
}
