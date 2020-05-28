using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;

public class WaveSwim : MonoBehaviour
{
    public GameObject singleWaveInfoPrb;

    public GameObject singleEnemyInfoPrb;

    public void Init(int buildingId, List<StageEnemyData> datas)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            GameObject go = Instantiate(singleWaveInfoPrb, transform);
            switch (buildingId)
            {
                case 0:
                    InitEachEnemy(go, datas[i].point1);
                    break;
                case 1:
                    InitEachEnemy(go, datas[i].point2);
                    break;
                case 2:
                    InitEachEnemy(go, datas[i].point3);
                    break;
                case 3:
                    InitEachEnemy(go, datas[i].point4);
                    break;
                case 4:
                    InitEachEnemy(go, datas[i].point5);
                    break;
                case 5:
                    InitEachEnemy(go, datas[i].point6);
                    break;
            }
            int waitNumber = StageGoal.My.waitTimeList[i];
            print(waitNumber);
            go.transform.localPosition = new Vector3(go.transform.localPosition.x, -StageGoal.My.waitTimeList[i] * 20, go.transform.localPosition.z);
            go.transform.DOLocalMoveY(500f, StageGoal.My.waitTimeList[i]).SetEase(Ease.Linear).OnComplete(()=> {
                Destroy(go);
            });
        }
    }

    public void InitEachEnemy(GameObject game,List<string> enemyInfo)
    {
        for (int i = 0; i < enemyInfo.Count; i++)
        {
            GameObject go = Instantiate(singleEnemyInfoPrb, game.transform);
            ConsumerType type = (ConsumerType)Enum.Parse(typeof(ConsumerType),enemyInfo[i].Split('_')[0]);
            int number = int.Parse(enemyInfo[i].Split('_')[1]);
            go.GetComponent<SingleWaveEnemyInfo>().Init(type,number);
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
