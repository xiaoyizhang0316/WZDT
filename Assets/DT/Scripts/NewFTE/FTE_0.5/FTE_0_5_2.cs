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
       MissionManager.My.ChangeTital("");
        
        yield return new WaitForSeconds(1f);
        for (int i = 0; i <land.Count; i++)
        {
            land[i].transform.DOLocalMoveY(-5, 1f).Play();
        }
      
    }

    public override IEnumerator StepEnd()
    { 
        for (int i = 0; i <land.Count; i++)
        {
            land[i].transform.DOLocalMoveY(0, 1f).Play();
       
        }

        for (int i = 0; i < Seedtesting.Count; i++)
        {
            Seedtesting[i].SetActive(true);
            Seedtesting[i].transform.DOLocalMoveY(0.3f, 1f).Play();

        }
        yield return new WaitForSeconds(1f);
    }

    void Update()
    {
        
    }
}
