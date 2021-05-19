using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResRing : MonoBehaviour
{
    public int  addMoney;

    public int addScore;
    
    Dictionary<ConsumeSign,int > consumeData = new Dictionary<ConsumeSign, int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
      if( other.CompareTag("Consumer")  ) 
          consumeData.Add(other.transform.GetComponent<ConsumeSign>(),StageGoal.My.timeCount); 
      
    }

    public void OnTriggerExit(Collider other)
    {
        int time = consumeData[other.transform.GetComponent<ConsumeSign>()] * addMoney;
       StageGoal.My.GetPlayerGold(time);
       StageGoal.My.Income(time, IncomeType.Npc, GetComponentInParent<BaseMapRole>());

       StageGoal.My.ScoreGet(ScoreType.消费者得分,consumeData[other.transform.GetComponent<ConsumeSign>()] * addScore);
       consumeData.Remove(other.transform.GetComponent<ConsumeSign>());

    }

// Update is called once per frame
    void Update()
    {
        
    }
}
