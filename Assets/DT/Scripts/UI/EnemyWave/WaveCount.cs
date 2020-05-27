using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WaveCount : MonoBehaviour
{
    public Text waveNumber;

    public void CountDown(int num,int waveNum)
    {
        GetComponent<Image>().fillAmount = 0;
        waveNumber.text = waveNum.ToString();
        GetComponent<Image>().DOFillAmount(1f, num).SetEase(Ease.Linear);
    }

    private void Awake()
    {
        StageGoal.My.waveCountItem = this;
        waveNumber = GetComponentInChildren<Text>();
        GetComponent<Image>().fillAmount = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
