using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveEnemySign : MonoBehaviour
{
    public GameObject singleConsumerTypePrb;

    public Image buildingNumber;

    public List<Sprite> buildingSprites;

    public void Init(int num,List<string> enemys)
    {
        buildingNumber.sprite = buildingSprites[num];
        Building building = BuildingManager.My.GetBuildingByIndex(num);
        for (int i = 0; i < enemys.Count; i++)
        {
            GameObject go = Instantiate(singleConsumerTypePrb, transform);
            go.GetComponent<SingleWaveEnemyInfo>().Init(enemys[i],building.isUseTSJ);
        }
    }
}
