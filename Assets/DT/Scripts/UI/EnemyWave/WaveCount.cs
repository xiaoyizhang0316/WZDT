using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WaveCount : MonoBehaviour
{
    public Text waveNumber;

    private Tweener twe;

    private int countDownNumber;

    public GameObject singleWavePrb;

    public void Init(List<StageEnemyData> datas)
    {
        for (int i = 0; i < BuildingManager.My.buildings.Count; i++)
        {
            GameObject go = Instantiate(singleWavePrb, transform);
            go.GetComponent<WaveSwim>().Init(BuildingManager.My.buildings[i].buildingId,datas);
        }
    }

    public void CountDown(int num,int waveNum)
    {
        if (twe != null)
            twe.Kill();
        countDownNumber = num;
        CountDownNumber();
        GetComponent<Image>().fillAmount = 0;
        GetComponent<Image>().DOFillAmount(1f, num).SetEase(Ease.Linear);
    }

    public void CountDownNumber()
    {
        if (countDownNumber == 0)
            return;
        waveNumber.text = countDownNumber.ToString();
        twe = transform.DOScale(1f, 1f).OnComplete(() =>
        {
            countDownNumber--;
            waveNumber.text = countDownNumber.ToString();
            CountDownNumber();
        });
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
