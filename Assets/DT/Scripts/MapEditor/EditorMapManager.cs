using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static GameEnum;

public class EditorMapManager : MapManager
{
    private float interval;

    private int consumerSpotCount = 0;

    public string fteName;

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
        //JsonUtility.FromJson<LandsData>()
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

    private void Update()
    {
        interval += Time.deltaTime;
        if (interval >= 0.1f)
        {
            //平草地
            //if (Input.GetKey(KeyCode.Alpha1))
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit[] hit = Physics.RaycastAll(ray);
            //    for (int i = 0; i < hit.Length; i++)
            //    {
            //        //print(hit[i].transform);
            //        if (hit[i].transform.tag.Equals("MapLand"))
            //        {
            //            GameObject go = Instantiate(mapTypeList[0], transform);
            //            go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
            //            int tempX = hit[i].transform.GetComponent<MapSign>().x;
            //            int tempY = hit[i].transform.GetComponent<MapSign>().y;
            //            go.GetComponent<MapSign>().x = tempX;
            //            go.GetComponent<MapSign>().y = tempY;
            //            go.AddComponent<EditorLandItem>();
            //            go.GetComponent<EditorLandItem>().Init(hit[i].transform.GetComponent<MapSign>());
            //            Destroy(hit[i].transform.gameObject);
            //            break;
            //        }
            //    }
            //    interval = 0f;
            //}
            ////高草地
            //else if (Input.GetKey(KeyCode.Alpha2))
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit[] hit = Physics.RaycastAll(ray);
            //    for (int i = 0; i < hit.Length; i++)
            //    {
            //        //print(hit[i].transform);
            //        if (hit[i].transform.tag.Equals("MapLand"))
            //        {
            //            GameObject go = Instantiate(mapTypeList[1], transform);
            //            go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
            //            int tempX = hit[i].transform.GetComponent<MapSign>().x;
            //            int tempY = hit[i].transform.GetComponent<MapSign>().y;
            //            go.GetComponent<MapSign>().x = tempX;
            //            go.GetComponent<MapSign>().y = tempY;
            //            go.AddComponent<EditorLandItem>();
            //            go.GetComponent<EditorLandItem>().Init(hit[i].transform.GetComponent<MapSign>());
            //            Destroy(hit[i].transform.gameObject);
            //            break;
            //        }
            //    }
            //    interval = 0f;
            //}
            ////土地
            //else if (Input.GetKey(KeyCode.Alpha3))
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit[] hit = Physics.RaycastAll(ray);
            //    for (int i = 0; i < hit.Length; i++)
            //    {
            //        //print(hit[i].transform);
            //        if (hit[i].transform.tag.Equals("MapLand"))
            //        {
            //            GameObject go = Instantiate(mapTypeList[2], transform);
            //            go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
            //            int tempX = hit[i].transform.GetComponent<MapSign>().x;
            //            int tempY = hit[i].transform.GetComponent<MapSign>().y;
            //            go.GetComponent<MapSign>().x = tempX;
            //            go.GetComponent<MapSign>().y = tempY;
            //            Destroy(hit[i].transform.gameObject);
            //            break;
            //        }
            //    }
            //    interval = 0f;
            //}
            ////路
            //else if (Input.GetKey(KeyCode.Alpha4))
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit[] hit = Physics.RaycastAll(ray);
            //    for (int i = 0; i < hit.Length; i++)
            //    {
            //        //print(hit[i].transform);
            //        if (hit[i].transform.tag.Equals("MapLand"))
            //        {
            //            GameObject go = Instantiate(mapTypeList[3], transform);
            //            go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
            //            int tempX = hit[i].transform.GetComponent<MapSign>().x;
            //            int tempY = hit[i].transform.GetComponent<MapSign>().y;
            //            go.GetComponent<MapSign>().x = tempX;
            //            go.GetComponent<MapSign>().y = tempY;
            //            go.AddComponent<EditorLandItem>();
            //            go.GetComponent<EditorLandItem>().Init(hit[i].transform.GetComponent<MapSign>());
            //            Destroy(hit[i].transform.gameObject);
            //            break;
            //        }
            //    }
            //    interval = 0f;
            //}
            ////海
            //else if (Input.GetKey(KeyCode.Alpha5))
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit[] hit = Physics.RaycastAll(ray);
            //    for (int i = 0; i < hit.Length; i++)
            //    {
            //        //print(hit[i].transform);
            //        if (hit[i].transform.tag.Equals("MapLand"))
            //        {
            //            GameObject go = Instantiate(mapTypeList[4], transform);
            //            go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
            //            int tempX = hit[i].transform.GetComponent<MapSign>().x;
            //            int tempY = hit[i].transform.GetComponent<MapSign>().y;
            //            go.GetComponent<MapSign>().x = tempX;
            //            go.GetComponent<MapSign>().y = tempY;
            //            go.AddComponent<EditorLandItem>();
            //            go.GetComponent<EditorLandItem>().Init(hit[i].transform.GetComponent<MapSign>());
            //            Destroy(hit[i].transform.gameObject);
            //            break;
            //        }
            //    }
            //    interval = 0f;
            //}
            //消费者出生点
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        consumerSpotCount++;
                        string path = "Prefabs/Common/ConsumerSpot" + consumerSpotCount.ToString();
                        Vector3 pos = hit[i].transform.position + new Vector3(0f, 0.3f, 0f);
                        GameObject go = Instantiate(Resources.Load<GameObject>(path));
                        go.transform.position = pos;
                        go.AddComponent<EditorConsumerSpot>();
                    }
                    break;
                }
                interval = 0f;
            }
            //消费者终点
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        string path = "Prefabs/Common/EndPoint";
                        Vector3 pos = hit[i].transform.position + new Vector3(0f, 0.3f, 0f);
                        GameObject go = Instantiate(Resources.Load<GameObject>(path));
                        go.transform.position = pos;
                        go.AddComponent<EditorConsumerEnd>();
                    }
                    break;
                }
                interval = 0f;
            }
        }
    }
}
