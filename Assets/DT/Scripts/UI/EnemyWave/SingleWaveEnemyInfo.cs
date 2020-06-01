using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;
using System;

public class SingleWaveEnemyInfo : MonoBehaviour
{ 
    public Text enemyNum;

    public ConsumerType consumeType;

    public Image typeSprite;

    public GameObject singleBuffPrb;

    public Transform buffListTF;

    public void Init(string str)
    {
        string[] list = str.Split('_');
        consumeType = (ConsumerType)Enum.Parse(typeof(ConsumerType), list[0]);
        string path = "Sprite/ConsumerType/" + consumeType.ToString();
        print(path);
        typeSprite.sprite = Resources.Load<Sprite>(path);
        enemyNum.text = int.Parse(list[1]).ToString();
        string[] buffList = list[2].Split('|');
        for (int i = 0; i < buffList.Length; i++)
        {
            if (int.Parse(buffList[i]) != -1)
            {
                GameObject go = Instantiate(singleBuffPrb, buffListTF);
            }
        }
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
