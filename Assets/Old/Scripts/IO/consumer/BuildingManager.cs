using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class BuildingManager : MonoSingleton<BuildingManager>
{

    public List<Building> buildings = new List<Building>();

    public int buildingIndex = 0;

    /// <summary>
    /// 初始化所有建筑
    /// </summary>
    public void InitAllBuilding(List<StageEnemyData> datas)
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            buildings[i].Init(datas);
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
