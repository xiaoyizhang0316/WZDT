using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RubishProcessItem : MonoBehaviour
{
    public int countDownNumber;

    public GameObject rangeObj;

    public bool isOpen = false;

    public int healNumber;

    // Start is called before the first frame update
    void Start()
    {
        rangeObj.SetActive(false);
        GetComponentInParent<RubishProcess>().item = this;
        CountDown();
    }

    public void CountDown()
    {
        transform.DOScale(1f, 2f).OnComplete(() =>
        {
            if (countDownNumber < 2)
            {
                isOpen = false;
                rangeObj.SetActive(false);
            }
            else
            {
                isOpen = true;
                rangeObj.SetActive(true);
                countDownNumber -= 2;
            }
            CountDown(); 
        });
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Consumer"))
        {
            StageGoal.My.GetSatisfy(healNumber);
        }
    }
}
