using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class BuildingManager : MonoSingleton<BuildingManager>
{

    public List<Building> buildings = new List<Building>();

    /// <summary>
    /// 初始化所有建筑
    /// </summary>
    public void InitAllBuilding(List<StageEnemyData> datas)
    {
        print(buildings.Count);
        for (int i = 0; i < buildings.Count; i++)
        {
            buildings[i].Init(datas);
        }
    }

    /// <summary>
    /// 根据波数让所有刷新点刷消费者
    /// </summary>
    /// <param name="waveNumber"></param>
    public void WaveSpawnConsumer(int waveNumber)
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            buildings[i].SpawnConsumer(waveNumber);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        print(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
