using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class SingleWaveEnemyInfo : MonoBehaviour
{ 
    public Text enemyNum;

    public ConsumerType Consumetype;

    public void Init(ConsumerType type,int number)
    {
        enemyNum.text = number.ToString();
        Consumetype = type;
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
