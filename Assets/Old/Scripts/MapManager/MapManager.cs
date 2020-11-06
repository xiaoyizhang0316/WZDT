using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using IOIntensiveFramework.MonoSingleton;
using static GameEnum;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MapManager : MonoSingleton<MapManager>
{
    public List<MapSign>_mapSigns = new List<MapSign>();

    public List<GameObject> mapTypeList;

    private float interval;

    private Vector3 medium = Vector3.zero;

    private Vector3 high = new Vector3(0f, 0.6f, 0f);

    public bool generatePath;

    public GameObject skillOneEffect;
    public GameObject skillTwoEffect;
    public GameObject skillThreeEffect;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("CheckDuplicate", 1f);
        //Invoke("CheckDuplicateID", 1f);
        //Invoke("CheckGrassAvailable", 1f);
        //Invoke("TestMethod", 1f);
        InitStageNPCData();
    }


    /// <summary>
    /// 检测地块是否能放置建筑
    /// </summary>
    /// <param name="xList"></param>
    /// <param name="yList"></param>
    /// <returns></returns>
    public bool CheckLandAvailable(int x,int y)
    {
        MapSign temp = GetMapSignByXY(x, y);
        if (temp == null)
        {
            HttpManager.My.ShowTip("当前地块不能放置角色！");
            return false;
        }
        else
        {
            if (!temp.isCanPlace)
            {
                HttpManager.My.ShowTip("当前地块不能放置角色！");
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 根据X，Y坐标查找地块
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public MapSign GetMapSignByXY(int x,int y)
    {
        foreach(MapSign m in _mapSigns)
        {
            if (m.x == x && m.y == y)
            {
                return m;
            }
        }
        print("-----未找到地块-----" + x + "||||" + y);
        return null;
    }

    /// <summary>
    /// 设置地块占用
    /// </summary>
    /// <param name="xList"></param>
    /// <param name="yList"></param>
    public void SetLand(int x, int y)
    {
        //print("x:" + x + "  y:" + y);
            MapSign temp = GetMapSignByXY(x, y);
            if (temp != null)
            {
                temp.isCanPlace = false;
            }
    }

    public void SetLand(int x, int y,BaseMapRole role)
    {
        MapSign temp = GetMapSignByXY(x, y);
        if (temp != null)
        {
            temp.isCanPlace = false;
 
        }
    }

    /// <summary>
    /// 解除地块占用
    /// </summary>
    /// <param name="xList"></param>
    /// <param name="yList"></param>
    public void ReleaseLand(int x, int y)
    {
            MapSign temp = GetMapSignByXY(x, y);
            if (temp != null)
            {
                temp.isCanPlace = true;
            }
    }

    /// <summary>
    /// 根据配置表生成NPC并放置到地图上
    /// </summary>
    /// <param name="npc"></param>
    public void PutNPC(StageNPCData npc)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/NPC/" + npc.roleType));
        go.transform.SetParent(GameObject.Find("Role").transform);
        go.transform.position = GetMapSignByXY(npc.posX, npc.posY).transform.position + new Vector3(0f, 0.3f, 0f);
        SetNPCAttribute(go,npc);
        go.name = npc.npcName;
        go.GetComponent<NPC>().BaseInit();
        go.GetComponent<NPC>().Init();
    }

    /// <summary>
    /// 设置NPC的属性
    /// </summary>
    /// <param name="go"></param>
    /// <param name="npc"></param>
    public void SetNPCAttribute(GameObject go, StageNPCData npc)
    {
        BaseMapRole role = go.GetComponent<BaseMapRole>();
        role.baseRoleData.effect = npc.effect;
        role.baseRoleData.efficiency = npc.efficiency;
        role.baseRoleData.range = npc.range;
        role.baseRoleData.tradeCost = npc.tradeCost;
        role.baseRoleData.riskResistance = npc.risk;
        role.baseRoleData.baseRoleData.level = npc.level;
        role.baseRoleData.baseRoleData.roleName = npc.npcName;
        role.baseRoleData.bulletCapacity = npc.bulletCount;
        role.baseRoleData.ID = npc.npcID;
        role.startEncourageLevel = npc.startEncourageLevel;
        role.encourageLevel = npc.startEncourageLevel;
        NPC npcScript = go.GetComponent<NPC>();
        npcScript.isCanSee = npc.isCanSee;
        npcScript.isLock = npc.isLock;
        npcScript.lockNumber = npc.lockNumber;
        npcScript.isCanSeeEquip = npc.isCanSeeEquip;
        //go.GetComponent<BaseSkill>().skillDesc = npc.skillDesc;
        go.GetComponent<BaseSkill>().buffList.Clear();
        go.GetComponent<BaseSkill>().goodBaseBuffs.Clear();
        go.GetComponent<BaseSkill>().badBaseBuffs.Clear();
        go.GetComponent<BaseSkill>().buffList.AddRange(npc.initBuffList);
        go.GetComponent<NPC>().NPCBuffList.Clear();
        go.GetComponent<NPC>().NPCBuffList.AddRange(npc.hideBuffList);
        for (int i = 0; i < npc.goodBaseBuffList.Count; i++)
        {
            BaseBuff buff = new BaseBuff();
            BuffData data = GameDataMgr.My.GetBuffDataByID(npc.goodBaseBuffList[i]);
            buff.Init(data);
            buff.targetRole = role;
            buff.castRole = role;
            buff.buffRole = role;
            go.GetComponent<BaseSkill>().goodBaseBuffs.Add(buff);
        }
        for (int i = 0; i < npc.badBaseBuffList.Count; i++)
        {
            BaseBuff buff = new BaseBuff();
            BuffData data = GameDataMgr.My.GetBuffDataByID(npc.badBaseBuffList[i]);
            buff.Init(data);
            buff.targetRole = role;
            buff.castRole = role;
            buff.buffRole = role;
            go.GetComponent<BaseSkill>().badBaseBuffs.Add(buff);
        }
    }

    public void InitStageNPCData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Equals("FTE_0-1") || sceneName.Equals("FTE_0-2"))
        {
            return;
        }
        if (int.Parse(sceneName.Split('_')[1]) >4 )
        {
            ReadStageNPCData(sceneName);
        }
    }

    public void ReadStageNPCData(string sceneName)
    {
        //TODO
        string json = OriginalData.My.jsonDatas.GetLevelData(sceneName, true);
        //Debug.Log("+++++++++" + json);
        StageNPCsData stageNPCsData = JsonUtility.FromJson< StageNPCsData >(json );
        //Debug.Log("============" + stageNPCsData.stageNPCItems.Count);
        ParseStageNPCData(stageNPCsData);
    }


    public void ParseStageNPCData(StageNPCsData rawData)
    {
        foreach (StageNPCItem s in rawData.stageNPCItems)
        {
            StageNPCData npc = new StageNPCData(s);
            PutNPC(npc);
        }
    }

    /// <summary>
    /// 单元测试
    /// </summary>
    public void TestMethod()
    {
        StageNPCData npc = new StageNPCData();
        npc.roleType = "Seed";
        npc.level = 1;
        npc.npcName = "dd";
        npc.effect = 10;
        npc.efficiency = 10;
        npc.risk = 10;
        npc.npcID = 1;
        npc.tradeCost = 10;
        npc.range = 10;
        npc.bulletCount = 10;
        npc.posX = 15;
        npc.posY = 21;
        npc.isCanSee = false;
        npc.isCanSeeEquip = false;
        npc.lockNumber = 10;
        npc.isLock = true;
        PutNPC(npc);
    }


    // Update is called once per frame
    void Update()
    {
        //interval += Time.deltaTime;
        //if (interval >= 0.1f)
        //{
        //    //平草地
        //    if (Input.GetKey(KeyCode.Alpha1))
        //    {
        //        print("press 1");
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit[] hit = Physics.RaycastAll(ray);
        //        for (int i = 0; i < hit.Length; i++)
        //        {
        //            print(hit[i].transform);
        //            if (hit[i].transform.tag.Equals("MapLand"))
        //            {
        //                GameObject go = Instantiate(mapTypeList[0], transform);
        //                go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
        //                int tempX = hit[i].transform.GetComponent<MapSign>().x;
        //                int tempY = hit[i].transform.GetComponent<MapSign>().y;
        //                go.GetComponent<MapSign>().x = tempX;
        //                go.GetComponent<MapSign>().y = tempY;
        //                Destroy(hit[i].transform.gameObject);
        //                break;
        //            }
        //        }
        //        interval = 0f;
        //    }
        //    //高草地
        //    if (Input.GetKey(KeyCode.Alpha2))
        //    {
        //        print("press 2");
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit[] hit = Physics.RaycastAll(ray);
        //        for (int i = 0; i < hit.Length; i++)
        //        {
        //            print(hit[i].transform);
        //            if (hit[i].transform.tag.Equals("MapLand"))
        //            {
        //                GameObject go = Instantiate(mapTypeList[1], transform);
        //                go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
        //                int tempX = hit[i].transform.GetComponent<MapSign>().x;
        //                int tempY = hit[i].transform.GetComponent<MapSign>().y;
        //                go.GetComponent<MapSign>().x = tempX;
        //                go.GetComponent<MapSign>().y = tempY;
        //                Destroy(hit[i].transform.gameObject);
        //                break;
        //            }
        //        }
        //        interval = 0f;
        //    }
        //    if (Input.GetKey(KeyCode.Alpha3))
        //    {
        //        print("press 3");
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit[] hit = Physics.RaycastAll(ray);
        //        for (int i = 0; i < hit.Length; i++)
        //        {
        //            print(hit[i].transform);
        //            if (hit[i].transform.tag.Equals("MapLand"))
        //            {
        //                GameObject go = Instantiate(mapTypeList[2], transform);
        //                go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
        //                int tempX = hit[i].transform.GetComponent<MapSign>().x;
        //                int tempY = hit[i].transform.GetComponent<MapSign>().y;
        //                go.GetComponent<MapSign>().x = tempX;
        //                go.GetComponent<MapSign>().y = tempY;
        //                Destroy(hit[i].transform.gameObject);
        //                break;
        //            }
        //        }
        //        interval = 0f;
        //    }
        //    if (Input.GetKey(KeyCode.Alpha4))
        //    {
        //        print("press 3");
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit[] hit = Physics.RaycastAll(ray);
        //        for (int i = 0; i < hit.Length; i++)
        //        {
        //            print(hit[i].transform);
        //            if (hit[i].transform.tag.Equals("MapLand"))
        //            {
        //                GameObject go = Instantiate(mapTypeList[3], transform);
        //                go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
        //                int tempX = hit[i].transform.GetComponent<MapSign>().x;
        //                int tempY = hit[i].transform.GetComponent<MapSign>().y;
        //                go.GetComponent<MapSign>().x = tempX;
        //                go.GetComponent<MapSign>().y = tempY;
        //                Destroy(hit[i].transform.gameObject);
        //                break;
        //            }
        //        }
        //        interval = 0f;
        //    }

        //    //地块升高
        //    if (Input.GetKeyDown(KeyCode.UpArrow))
        //    {
        //        print("press up");
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit[] hit = Physics.RaycastAll(ray);
        //        for (int i = 0; i < hit.Length; i++)
        //        {
        //            if (hit[i].transform.tag.Equals("MapLand"))
        //            {
        //                hit[i].transform.localPosition += new Vector3(0f, 0.3f, 0f);
        //                hit[i].transform.GetComponent<MapSign>().height += 1;
        //            }
        //            break;
        //        }
        //        interval = 0f;
        //    }
        //    //地块降低
        //    if (Input.GetKeyDown(KeyCode.DownArrow))
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit[] hit = Physics.RaycastAll(ray);
        //        for (int i = 0; i < hit.Length; i++)
        //        {
        //            if (hit[i].transform.tag.Equals("MapLand"))
        //            {
        //                hit[i].transform.localPosition += new Vector3(0f, -0.3f, 0f);
        //                hit[i].transform.GetComponent<MapSign>().height -= 1;
        //            }
        //            break;
        //        }
        //        interval = 0f;
        //    }

        //    if (Input.GetKeyDown(KeyCode.Q))
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit[] hit = Physics.RaycastAll(ray);
        //        for (int i = 0; i < hit.Length; i++)
        //        {
        //            if (hit[i].transform.tag.Equals("MapLand"))
        //            {
        //                hit[i].transform.GetComponent<MeshRenderer>().enabled = true;
        //            }
        //            break;
        //        }
        //        interval = 0f;
        //    }

        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit[] hit = Physics.RaycastAll(ray);
        //        for (int i = 0; i < hit.Length; i++)
        //        {
        //            if (hit[i].transform.tag.Equals("MapLand"))
        //            {
        //                hit[i].transform.GetComponent<MeshRenderer>().enabled = false;
        //            }
        //            break;
        //        }
        //        interval = 0f;
        //    }
        //}
    }

    #region 辅助函数

    private List<double> idList = new List<double>();

    /// <summary>
    /// 检测是否有重复角色ID
    /// </summary>
    public void CheckDuplicateID()
    {
        foreach (BaseMapRole role in PlayerData.My.MapRole)
        {
            if (idList.Contains(role.baseRoleData.ID))
            {
                print("-----------重复角色ID------------" + role.baseRoleData.ID.ToString() + role.baseRoleData.baseRoleData.roleName);
            }
            else
            {
                idList.Add(role.baseRoleData.ID);
            }
        }
    }
    
    private List<string> xyList = new List<string>();

    /// <summary>
    /// 检测是否有重复坐标地块
    /// </summary>
    public void CheckDuplicate()
    {
        foreach (MapSign sign in _mapSigns)
        {
            string str = sign.x.ToString() + " " + sign.y.ToString();
            if (xyList.Contains(str))
            {
                print(str + "------------Duplicate X&Y");
                sign.transform.DOScale(70f, 0f);
                GetMapSignByXY(sign.x, sign.y).transform.DOScale(70f, 0f);
            }
            else
            {
                xyList.Add(str);
            }
        }
    }

    public void CheckGrassAvailable()
    {
        foreach (MapSign sign in _mapSigns)
        {
            if (sign.baseMapRole == null && !sign.isCanPlace && sign.mapType == MapType.Grass)
            {
                print("---------empty grass not available!-----" + sign.x +"  " + sign.y);
                sign.transform.DOScale(70f, 0f);
            }
        }
    }

    #endregion
}
