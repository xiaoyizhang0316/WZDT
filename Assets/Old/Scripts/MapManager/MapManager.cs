using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using IOIntensiveFramework.MonoSingleton;
using static GameEnum;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Linq;

public class MapManager : MonoSingleton<MapManager>
{
    public List<MapSign>_mapSigns = new List<MapSign>();

    public List<GameObject> mapTypeList;

    private float interval;

    private Vector3 medium = Vector3.zero;

    private Vector3 high = new Vector3(0f, 0.6f, 0f);

    public bool generatePath;
    public SaveLoadMenu SaveLoadMenu;
    public GameObject skillOneEffect;
    public GameObject skillTwoEffect;
    public GameObject skillThreeEffect;

    public Transform buildTF;

    public Dictionary<int, List<GameObject>> diveList = new Dictionary<int, List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("CheckDuplicate", 1f);
        //Invoke("CheckDuplicateID", 1f);
        //Invoke("CheckGrassAvailable", 1f);
        //Invoke("TestMethod", 1f);
        Shader.EnableKeyword("HEX_MAP_EDIT_MODE");
        SaveLoadMenu.LoadActualScene(Application.streamingAssetsPath+"/"+SceneManager.GetActiveScene().name  + ".map");
        //buildTF = transform.root
        Debug.Log(transform.root);
        Invoke("InitStageNPCData",0.6f);
        //InitStageNPCData();
    }

    public virtual void SaveJSON(string name)
    {

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
        go.transform.position = GetMapSignByXY(npc.posX, npc.posY).transform.position + new Vector3(0f, 0f, 0f);
        go.GetComponent<BaseMapRole>().posX = npc.posX;
        go.GetComponent<BaseMapRole>().posY = npc.posY;
        SetNPCAttribute(go,npc);
        go.name = npc.npcName;
        go.GetComponent<NPC>().BaseInit();
        go.GetComponent<NPC>().Init();
    }

    /// <summary>
    /// 随机摆放NPC位置
    /// </summary>
    /// <param name="npc"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void PutNPCRandom(StageNPCData npc,int x,int y)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/NPC/" + npc.roleType));
        go.transform.SetParent(GameObject.Find("Role").transform);
        go.transform.position = GetMapSignByXY(x, y).transform.position + new Vector3(0f, 0.3f, 0f);
        SetNPCAttribute(go, npc);
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
        if (PlayerData.My.yingLiMoShi[4])
        {
            role.startEncourageLevel += 2;
        }
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
        if (sceneName.Equals("FTE_0-1") || sceneName.Equals("FTE_0-2")|| sceneName.Equals("FTE_0.5")
            || sceneName.Equals("FTE_1.5")|| sceneName.Equals("FTE_2.5"))
        {
            return;
        }
        if (int.Parse(sceneName.Split('_')[1]) > 4)
        {
            ReadStageNPCData(sceneName);
        }
    }

    /// <summary>
    /// 读取关卡NPC配置表
    /// </summary>
    /// <param name="sceneName"></param>
    public void ReadStageNPCData(string sceneName)
    {
        //TODO
        string json;
        if (!NetworkMgr.My.useLocalJson)
        {
            json = OriginalData.My.jsonDatas.GetLevelData(sceneName, true);
        }
        else
        {
            WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/StageNPC/" + sceneName + ".json");
            while(true)
            {
                if (www.isDone)
                {
                    json = www.text.ToString();
                    break;
                }
            }
        }
        //Debug.Log("+++++++++" + json);
        StageNPCsData stageNPCsData = JsonUtility.FromJson< StageNPCsData >(json );
        //Debug.Log("============" + stageNPCsData.stageNPCItems.Count);
        ParseStageNPCData(stageNPCsData);
    }

    /// <summary>
    /// 根据配置表生成实际NPC
    /// </summary>
    /// <param name="rawData"></param>
    public void ParseStageNPCData(StageNPCsData rawData)
    {
        //npc随机位置
        if (SceneManager.GetActiveScene().name.Equals("FTE_7"))
        {
            List<StageNPCData> npcs = new List<StageNPCData>();
            List<string> pos = new List<string>();
            foreach (StageNPCItem s in rawData.stageNPCItems)
            {
                StageNPCData npc = new StageNPCData(s);
                if (npc.roleType.Equals("Dealer"))
                {
                    PutNPC(npc);
                }
                else
                {
                    string tempPos = s.posX.ToString() + "," + s.posY.ToString();
                    pos.Add(tempPos);
                    npcs.Add(npc);
                }
            }
            for (int i = 0; i < npcs.Count; i++)
            {
                int index = UnityEngine.Random.Range(0, pos.Count);
                int x = int.Parse(pos[index].Split(',')[0]);
                int y = int.Parse(pos[index].Split(',')[1]);
                pos.RemoveAt(index);
                PutNPCRandom(npcs[i], x, y);
            }

        }
        //npc非随机位置
        else
        {
            foreach (StageNPCItem s in rawData.stageNPCItems)
            {
                StageNPCData npc = new StageNPCData(s);
                PutNPC(npc);
            }
        }
    }

    /// <summary>
    /// 将特殊操作JSON文件导入到实际游戏场景中
    /// </summary>
    public virtual void LoadJSON(string fteName)
    {
        string name = SceneManager.GetActiveScene().name;
        StreamReader streamReader = null;
        try
        {
#if UNITY_STANDALONE_WIN
            streamReader = new StreamReader(Application.streamingAssetsPath + "/FTEConfig/" + name + ".json");
#elif UNITY_STANDALONE_OSX
            streamReader = new StreamReader(Application.streamingAssetsPath + "/FTEConfig/" + name + ".json");
#endif
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        //StreamReader streamReader = new StreamReader( Directory.GetParent(Directory.GetParent(Application.dataPath)+"") + "\\StartGame_Data\\Account.json");
        if (streamReader != null)
        {
            string str = streamReader.ReadToEnd();
            while (string.IsNullOrEmpty(str))
            {

            }
            streamReader.Close();
            Debug.Log(str);
            //string decode = CompressUtils.Decrypt(str);
            LandsDatas json = JsonUtility.FromJson<LandsDatas>(str);
            for (int i = 0; i < json.landItems.Count; i++)
            {
                LandItem item = JsonUtility.FromJson<LandItem>(json.landItems[i]);
                InitJSONItem(item);
                Debug.Log(item.specialOption);
            }
            BuildingManager.My.InitAllBuilding(StageGoal.My.enemyDatas);
        }
    }

    /// <summary>
    /// 将每一个特殊操作导入到实际游戏场景中
    /// </summary>
    /// <param name="item"></param>
    public virtual void InitJSONItem(LandItem item)
    {
        List<string> options = item.specialOption.Split(',').ToList();
        int x = int.Parse(item.x);
        int y = int.Parse(item.y);
        bool isItemMoveDown = false;
        int moveTime = 0;
        for (int i = 0; i < options.Count; i++)
        {
            LandOptionType type = (LandOptionType)Enum.Parse(typeof(LandOptionType), options[i].Split('_')[0]);
            switch (type)
            {
                case LandOptionType.MoveDown:
                    {
                        //地块下沉
                        if (options.Count <= 1)
                        {
                            //TODO 地块下沉处理
                            MapSign cell = GetMapSignByXY(x, y);
                        }
                        //出生点&终点下沉
                        else
                        {
                            isItemMoveDown = true;
                            moveTime = int.Parse(options[i].Split('_')[1]);

                        }
                        break;
                    }
                case LandOptionType.ConsumerSpot:
                    {
                        int index = int.Parse(options[i].Split('_')[1]);
                        MapSign cell = GetMapSignByXY(x, y);
                        string path = "Prefabs/Common/ConsumerSpot" + (index + 1).ToString();
                        Vector3 pos = cell.transform.position + new Vector3(0f, 0.1f, 0f);
                        GameObject go = Instantiate(Resources.Load<GameObject>(path),buildTF);
                        go.transform.position = pos;
                        go.GetComponent<Building>().ParsePathList(options[i].Split('_')[2]);
                        go.GetComponent<Building>().buildingId = index;
                        if (isItemMoveDown)
                        {
                            //TODO 出生点下沉处理
                        }
                        break;
                    }
                case LandOptionType.End:
                    {
                        MapSign cell = GetMapSignByXY(x, y);
                        string path = "Prefabs/Common/EndPoint";
                        Vector3 pos = cell.transform.position + new Vector3(0f, -0.3f, 0f);
                        GameObject go = Instantiate(Resources.Load<GameObject>(path), buildTF);
                        go.transform.position = pos;
                        if (isItemMoveDown)
                        {
                            //TODO 终点下沉处理
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 将下沉物体注册进下沉监测字典中
    /// </summary>
    /// <param name="time"></param>
    /// <param name="go"></param>
    public void AddDive(int time,GameObject go)
    {
        if (diveList.ContainsKey(time))
        {
            diveList[time].Add(go);
        }
        else
        {
            List<GameObject> list = new List<GameObject>();
            list.Add(go);
            diveList.Add(time, list);
        }
        go.transform.position += new Vector3(0f, -2f, 0f);
        go.SetActive(false);
    }


    public void CheckDive(int time)
    {
        if (diveList.ContainsKey(time))
        {
            for (int i = 0; i < diveList[time].Count; i++)
            {
                diveList[time][i].SetActive(true);
            }
            diveList.Remove(time);
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
        interval += Time.deltaTime;
        if (interval >= 0.1f)
        {
            //平草地
            if (Input.GetKey(KeyCode.Alpha1))
            {
                //print("press 1");
                Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(inputRay, out hit))
                {
                    HexCell currentCell = HexGrid.My.GetCell(hit.point);

                    if (currentCell != null)
                    {
                        Debug.Log("X:" + currentCell.coordinates.X.ToString() + "-----Y:" + currentCell.coordinates.Z.ToString());
                    }
                }
                interval = 0f;
            }
        }
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
