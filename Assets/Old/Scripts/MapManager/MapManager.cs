using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using IOIntensiveFramework.MonoSingleton;
using static GameEnum;

public class MapManager : MonoSingleton<MapManager>
{

    public List<MapSign>_mapSigns = new List<MapSign>();

    public List<RoleLandCost> roleLandCosts;

    [System.Serializable]
    public struct RoleLandCost
    {
        public RoleType roleType;

        public MapType landType;

        public int cost;
        
    }

    /// <summary>
    /// 查找对应地块类型对应角色的成本
    /// </summary>
    /// <param name="roleType"></param>
    /// <param name="mapType"></param>
    /// <returns></returns>
    public int GetLandRoleCost(RoleType roleType,MapType mapType)
    {
        foreach (RoleLandCost r in roleLandCosts)
        {
            if (r.roleType == roleType && r.landType == mapType)
                return r.cost;
        }
        print("找不到地块对应的角色价格");
        return 0;
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
