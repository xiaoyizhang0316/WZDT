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

    public Text totalGold;

    public Text consumerName;

    public Text consumerGold;

    public Text consumerHealth;


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
        typeSprite.SetNativeSize();
        typeSprite.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        enemyNum.text = "x" + int.Parse(list[1]).ToString();
        //string[] buffList = list[2].Split('|');
        List<int> buffList = new List<int>();
        ConsumerTypeData data = GameDataMgr.My.GetConsumerTypeDataByType(consumeType);
        buffList.AddRange(data.bornBuff);
        totalGold.text = "$" + (data.killMoney * int.Parse(list[1])).ToString();
        WaveCount.My.totalGold += data.killMoney * int.Parse(list[1]);
        transform.Find("TypeSatisfy").GetComponent<Text>().text = data.killSatisfy.ToString();
        transform.Find("TypeGold").GetComponent<Text>().text = data.killMoney.ToString();
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
                    if (PlayerData.My.creatRole == PlayerData.My.playerDutyID)
                    {
                        go.GetComponent<WaveBuffSign>().Init(buffList[i]);
                    }
                    else
                    {
                        go.GetComponent<WaveBuffSign>().InitClient(buffList[i]);
                    }
                }
                else
                {
                    GameObject go = Instantiate(singleBuffPrb, buffListTF);
                    if (PlayerData.My.creatRole == PlayerData.My.playerDutyID)
                    {
                        go.GetComponent<WaveBuffSign>().Init(999);
                    }
                    else
                    {
                        go.GetComponent<WaveBuffSign>().InitClient(999);
                    }
                }
            }
            else
            {
                if (PlayerData.My.creatRole == PlayerData.My.playerDutyID)
                {
                    GameObject go = Instantiate(singleBuffPrb, buffListTF);
                    go.GetComponent<WaveBuffSign>().Init(buffList[i]);
                }
                else
                {
                    GameObject go1 = GetComponentsInChildren<WaveBuffSign>()[i].gameObject;
                    go1.GetComponent<WaveBuffSign>().InitClient(buffList[i]);
                }
            }
        }
        if (PlayerData.My.creatRole != PlayerData.My.playerDutyID)
        {
            consumerName.text = data.typeDesc;
            consumerGold.text = data.killMoney.ToString();
            consumerHealth.text = data.maxHealth.ToString();
            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, 160 + (buffList.Count - 2) * 51);
        }
    }

    public void FloatWindowShow()
    {
        ConsumerTypeData data = GameDataMgr.My.GetConsumerTypeDataByType(consumeType);
        FloatWindow.My.Init(data.typeDesc);
    }

    public void FLoatWindowHide()
    {
        FloatWindow.My.Hide();
    }
}
