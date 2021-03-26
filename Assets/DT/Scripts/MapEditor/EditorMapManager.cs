
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class EditorMapManager : MapManager
{

    private int consumerSpotCount = 0;

    public string fteName;

    public GameObject consumerSpotPrb;

    public GameObject consumerEnd;

    public string sceneName;

    /// <summary>
    /// 从编辑器场景导出保存一个特殊操作的JSON文件
    /// </summary>
    public override void SaveJSON(string fteName)
    {
        EditorLandItem[] total = FindObjectsOfType<EditorLandItem>();
        Debug.Log(total.Length);
        LandsDatas data = new LandsDatas();
        for (int i = 0; i < total.Length; i++)
        {
            LandItem item = new LandItem();
            item.x = total[i].x.ToString();
            item.y = total[i].y.ToString();
            item.specialOption = total[i].GenerateSpecialOptionString();

            string str = JsonUtility.ToJson(item);
            Debug.Log(str);
            data.landItems.Add(str);
        }
        string result = JsonUtility.ToJson(data,false);
        Debug.Log(result);
        string encode = result;
#if UNITY_STANDALONE_WIN
                    FileStream file =new FileStream(Application.streamingAssetsPath
                                                    + "/FTEConfig/" + fteName + ".json", FileMode.Create);
#elif UNITY_STANDALONE_OSX
        FileStream file = new FileStream(Application.streamingAssetsPath
                                     + "/FTEConfig/" + fteName + ".json", FileMode.Create);
#endif
        byte[] bts = System.Text.Encoding.UTF8.GetBytes(encode);
        file.Write(bts, 0, bts.Length);
        if (file != null)
        {
            file.Close();
        }
    }

    private void Start()
    {
        //LoadJSON();

    }

    /// <summary>
    /// 将JSON文件导入到场景编辑器中
    /// </summary>
    public override void LoadJSON(string fteName)
    {
        StreamReader streamReader = null;
        try
        {
#if UNITY_STANDALONE_WIN
            streamReader = new StreamReader(Application.streamingAssetsPath + "/FTEConfig/" + fteName + ".json");
#elif UNITY_STANDALONE_OSX
            streamReader = new StreamReader(Application.streamingAssetsPath + "/FTEConfig/" + fteName + ".json");
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
                //Debug.Log(item.specialOption);
            }
        }
    }

    /// <summary>
    /// 将每一个特殊操作导入到编辑器场景中
    /// </summary>
    /// <param name="item"></param>
    public override void InitJSONItem(LandItem item)
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
                        if(options.Count <= 1)
                        {
                            MapSign cell = GetMapSignByXY(x, y);
                            int time = int.Parse(options[i].Split('_')[1]);
                            cell.gameObject.AddComponent<EditorLandItem>();
                            cell.gameObject.GetComponent<EditorLandItem>().x = x;
                            cell.gameObject.GetComponent<EditorLandItem>().y = y;
                            cell.gameObject.GetComponent<EditorLandItem>().isUnder = true;
                            cell.gameObject.GetComponent<EditorLandItem>().underTime =moveTime;
                        }
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
                        MapSign cell = GetMapSignByXY(x,y);
                        GameObject go = Instantiate(consumerSpotPrb);
                        go.transform.position = cell.transform.position;
                        go.GetComponent<EditorConsumerSpot>().x = x;
                        go.GetComponent<EditorConsumerSpot>().y = y;
                        go.GetComponent<EditorConsumerSpot>().ParsePathItem(options[i].Split('_')[2]);
                        go.GetComponent<EditorConsumerSpot>().index = index;
                        if (isItemMoveDown)
                        {
                            go.GetComponent<EditorConsumerSpot>().isUnder = true;
                            go.GetComponent<EditorConsumerSpot>().underTime = moveTime;
                        }
                        CreatePrb(go, index);
                        break;
                    }
                case LandOptionType.End:
                    {
                        MapSign cell = GetMapSignByXY(x, y);
                        GameObject go = Instantiate(consumerEnd);
                        go.transform.position = cell.transform.position;
                        go.GetComponent<EditorConsumerEnd>().x = x;
                        go.GetComponent<EditorConsumerEnd>().y = y;
                        if (isItemMoveDown)
                        {
                            go.GetComponent<EditorConsumerEnd>().isUnder = true;
                            go.GetComponent<EditorConsumerEnd>().underTime = moveTime;
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }

    public Transform PlayerStartPointTF;
    public GameObject PlayerStartPointUIPrb;

    public GameObject currentPort;

    public int count = 0;

    /// <summary>
    /// 测试六边形快下降
    /// </summary>
    /// <param name="hexCell"></param>
    /// <param name="targetLevel"></param>
    /// <param name="time"></param>
    /// <param name="end"></param>
    public void TestHexDown(HexCell hexCell  )
    {
        hexCell.Elevation -= 0.2f;

    }  
    public void TestHexUp(HexCell hexCell  )
    {
        hexCell.Elevation += 0.2f;

    }

    public void OnGUI()
    {
        if (GUILayout.Button("1") )
        {
            StartCoroutine(HexGrid.My.GetCell(0).ChangeElevationLerpUp(5,0.1f, () =>
            {
                Debug.Log("wancheng");
                StartCoroutine(HexGrid.My.GetCell(0).ChangeElevationLerpDown(0,0.1f, () =>
                {
                    Debug.Log("wancheng");
                })) ;
            })) ;
        }
        if (GUILayout.RepeatButton("2"))
        {
           HexGrid.My.GetCell(0) ;
        }
    }

    public void CreatPrb(GameObject game)
    {
        game.GetComponent<EditorConsumerSpot>().index = count;
        count++;
        GameObject gameobj = Instantiate(PlayerStartPointUIPrb, PlayerStartPointTF);
        gameobj.GetComponent<PlayStartSign>().port = game;
        gameobj.GetComponentInChildren<Text>().text = count.ToString();
    }

    public void CreatePrb(GameObject game,int index)
    {
        GameObject gameobj = Instantiate(PlayerStartPointUIPrb, PlayerStartPointTF);
        gameobj.GetComponent<PlayStartSign>().port = game;
        gameobj.GetComponentInChildren<Text>().text = index.ToString();
    }
    
}
