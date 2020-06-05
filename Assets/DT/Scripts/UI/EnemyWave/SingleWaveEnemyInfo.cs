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
        //string[] buffList = list[2].Split('|');
        List<int> buffList = new List<int>();
        ConsumerTypeData data = GameDataMgr.My.GetConsumerTypeDataByType(consumeType);
        buffList.AddRange(data.bornBuff);
        string[] strList = list[2].Split('|');
        for (int i = 0; i < strList.Length; i++)
        {
            if (int.Parse(strList[i]) != -1)
            {
                buffList.Add(int.Parse(strList[i]));
            }
        }
        for (int i = 0; i < buffList.Count; i++)
        {
            GameObject go = Instantiate(singleBuffPrb, buffListTF);
            go.GetComponent<WaveBuffSign>().Init(buffList[i]);
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
