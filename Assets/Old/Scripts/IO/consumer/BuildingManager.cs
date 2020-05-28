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
       // print(buildings.Count);
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

    /// <summary>
    /// 根据ID查找building
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Building GetBuildingByIndex(int index)
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].buildingId == index)
                return buildings[i];
        }
        print("-----------查不到此building-----------");
        return null;
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
