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
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="str"></param>
    public void Init(string str,bool isCanSeeBuff)
    {
        string[] list = str.Split('_');
        consumeType = (ConsumerType)Enum.Parse(typeof(ConsumerType), list[0]);
        string path = "Sprite/ConsumerType/" + consumeType.ToString();
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
            if (i > 1)
            {
                if (isCanSeeBuff)
                {
                    GameObject go = Instantiate(singleBuffPrb, buffListTF);
                    go.GetComponent<WaveBuffSign>().Init(buffList[i]);
                }
                else
                {
                    GameObject go = Instantiate(singleBuffPrb, buffListTF);
                    go.GetComponent<WaveBuffSign>().Init(999);
                }
            }
            else
            {
                GameObject go = Instantiate(singleBuffPrb, buffListTF);
                go.GetComponent<WaveBuffSign>().Init(buffList[i]);
            }
        }
    }

    public void FloatWindowShow()
    {
        ConsumerTypeData data = GameDataMgr.My.GetConsumerTypeDataByType(consumeType);
        FloatWindow.My.Init(typeSprite.transform, data.typeDesc);
    }

    public void FLoatWindowHide()
    {
        FloatWindow.My.Hide();
    }
}
