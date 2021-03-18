
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

    public Transform buildTF;

    public void SaveJSON()
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
                    FileStream file = new FileStream( Directory.GetParent(Directory.GetParent(Application.dataPath)+"")
                                 + "\\Build.json", FileMode.Create);
#elif UNITY_STANDALONE_OSX
        FileStream file = new FileStream(Directory.GetParent(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName)
                                     + "/Temp.json", FileMode.Create);
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


    public void LoadJSON()
    {
        StreamReader streamReader = null;
        try
        {
#if UNITY_STANDALONE_WIN
            streamReader = new StreamReader( Directory.GetParent(Directory.GetParent(Application.dataPath)+"") + "\\Bu.M_Data\\Account.json");
#elif UNITY_STANDALONE_OSX
            streamReader = new StreamReader(Directory.GetParent(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName) + "/Temp.json");
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
                Debug.Log(item.specialOption);
            }
        }
    }

    public void InitJSONItem(LandItem item)
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
                            //TODO 地块下沉处理
                            HexGrid.My.GetCell(x, y);
                        }
                        //出生点&终点下沉
                        else
                        {
                            isItemMoveDown = true;
                            moveTime = int.Parse(options[i].Split('_')[1]);
                            //TODO 出生点&终点 下沉处理
                        }
                        break;
                    }
                case LandOptionType.ConsumerSpot:
                    {
                        int index = int.Parse(options[i].Split('_')[1]);
                        HexCell cell = HexGrid.My.GetCell(x, y);

                        break;
                    }
                case LandOptionType.End:
                    {
                        HexCell cell = HexGrid.My.GetCell(x, y);

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

    public void CreatPrb(GameObject game)
    {
        count++;
        GameObject gameobj = Instantiate(PlayerStartPointUIPrb, PlayerStartPointTF);
        gameobj.GetComponent<PlayStartSign>().port = game;
        gameobj.GetComponentInChildren<Text>().text = count.ToString();
    }
    
}
