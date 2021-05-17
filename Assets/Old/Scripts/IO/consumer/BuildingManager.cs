using System;
using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class BuildingManager : MonoSingleton<BuildingManager>
{

    /// <summary>
    /// 建筑集合
    /// </summary>
    public List<Building> buildings = new List<Building>();

    /// <summary>
    /// 波数集合
    /// </summary>
    public List<WaveConfig> extraConsumer = new List<WaveConfig>();
    
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

    /// <summary>
    /// 计算本波所有消费者总数
    /// </summary>
    /// <param name="waveNumber"></param>
    /// <returns></returns>
    public int CalculateConsumerNumber(int waveNumber)
    {
        int result = 0;
        for (int i = 0; i < buildings.Count; i++)
        {
            result += buildings[i].CalculateConsumerNumber(waveNumber);
        }
        return result;
    }

    /// <summary>
    /// 获取指定类型的额外消费者的总数（100 = 所有类型的额外消费者）
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetExtraConsumerNumber(string type)
    {
        int result = 0;
        if (type.Equals("100"))
        {
            for (int i = 0; i < extraConsumer.Count; i++)
            {
                result += extraConsumer[i].num;
            }
        }
        else
        {
            GameEnum.ConsumerType consumerType =
                (GameEnum.ConsumerType) Enum.Parse(typeof(GameEnum.ConsumerType), type);
            for (int i = 0; i < extraConsumer.Count; i++)
            {
                if (extraConsumer[i].consumerType.Equals(consumerType))
                {
                    result += extraConsumer[i].num;
                }
            }
        }
        return result;
    }
    

    /// <summary>
    /// 重置所有建筑的spawn状态
    /// </summary>
    public void RestartAllBuilding()
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            buildings[i].isFinishSpawn = false;
        }
    }

    /// <summary>
    /// 隐藏所有建筑路线
    /// </summary>
    public void HideAllPath()
    {
        foreach (Building item in buildings)
        {
            item.StopShowPathLine();
        }
    }
    
    [Serializable]
    public class WaveConfig
    {
        public GameEnum.ConsumerType consumerType;

        public int num;

        public List<int> buffList;
    }
}
