using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ResRing : MonoBehaviour
{
    public int  addMoney;

    public int addScore;
    public bool isopen;
    public int energyconsumeValue;

    Dictionary<ConsumeSign,int > consumeData = new Dictionary<ConsumeSign, int>();
    // Start is called before the first frame update
    void Start()
    {
        consumeEnergy();
    }

    private void OnTriggerEnter(Collider other)
    {
        
      if( other.CompareTag("Consumer")  ) 
          consumeData.Add(other.transform.GetComponent<ConsumeSign>(),StageGoal.My.timeCount); 
      
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.tag.Equals("Consumer"))
        {
            return;
        }

        int time =(StageGoal.My.timeCount- consumeData[other.transform.GetComponent<ConsumeSign>()]) * addMoney; 
        int score = (StageGoal.My.timeCount- consumeData[other.transform.GetComponent<ConsumeSign>()]) * addScore;
        StageGoal.My.GetPlayerGold(time);
        StageGoal.My.Income(time, IncomeType.Npc, GetComponentInParent<BaseMapRole>()); 
        StageGoal.My.GetHealth(score * 60 / 100);
        StageGoal.My.ScoreGet(ScoreType.消费者得分,score);
        consumeData.Remove(other.transform.GetComponent<ConsumeSign>());

    }

    public void consumeEnergy()
    {
       
            transform.DOScale(1, 1).OnComplete(() =>
            {
         
                GetComponentInParent<ProductRestaurant>().currentenergy -= energyconsumeValue;

                if (GetComponentInParent<ProductRestaurant>().currentenergy <= 0)
                {
                    isopen = false;
                    GetComponentInParent<ProductRestaurant>().currentenergy = 0;
                }
                else
                {
                    isopen = true;
                }

                consumeEnergy();
            }); 
    }

// Update is called once per frame
    void Update()
    {
        
    }
}
