using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HotelRing : MonoBehaviour
{
    public int addMoney;

    public int addScore;

    public int energyconsumeValue;
    Dictionary<ConsumeSign, int> consumeData = new Dictionary<ConsumeSign, int>();

    public bool isopen;

    // Start is called before the first frame update
    void Start()
    {
        consumeEnergy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isopen)
        {
            if (other.CompareTag("Consumer"))
                consumeData.Add(other.transform.GetComponent<ConsumeSign>(), StageGoal.My.timeCount);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (isopen)
        {
            int time = (StageGoal.My.timeCount-consumeData[other.transform.GetComponent<ConsumeSign>()] )* addMoney;
            StageGoal.My.GetPlayerGold(time);
            StageGoal.My.Income(time, IncomeType.Npc, GetComponentInParent<BaseMapRole>());

            StageGoal.My.ScoreGet(ScoreType.消费者得分, consumeData[other.transform.GetComponent<ConsumeSign>()] * addScore);
            consumeData.Remove(other.transform.GetComponent<ConsumeSign>());
        }
    }

    public void consumeEnergy()
    {
        transform.DOScale(1, 1).OnComplete(() =>
        {
         
                GetComponentInParent<ProductHotel>().currentenergy -= energyconsumeValue;

                if (GetComponentInParent<ProductHotel>().currentenergy <= 0)
                {
                    isopen = false;
                    GetComponentInParent<ProductHotel>().currentenergy = 0;
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