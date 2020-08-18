using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.Networking;

public class OriginalData : MonoSingleton<OriginalData>
{
    public JsonDatas jsonDatas;
    public BuffsData buffRawData;

    public ConsumablesData consumableRawData;

    public StagesData stageRawData;

    public GearsData gearRawData;

    public WorkersData workerRawData;

    public RoleTemplateModelsData roleTemplateRawData;

    public ConsumerTypesData consumerTypeRawData;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    public void InitDatas(string data)
    {
        jsonDatas = JsonUtility.FromJson<JsonDatas>(data);
        Debug.Log("---------"+jsonDatas.BuffData);
        //StartCoroutine(ReadBuffJson());
        //StartCoroutine(ReadConsumableJson());
        //StartCoroutine(ReadStageJson());
        //StartCoroutine(ReadRoleTemplateJson());
        //StartCoroutine(ReadConsumerTypeJson());
        ReadBuffJson();
        ReadConsumableJson();
        ReadStageJson();
        ReadRoleTemplateJson();
        ReadConsumerTypeJson();
        GameDataMgr.My.Init();
    }

    void ReadBuffJson()
    {
        //WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/BuffData.json");
        ////Debug.Log(@"file://" + Application.streamingAssetsPath + @"/Data/BuffData.json");
        //yield return www;
        //if (www.isDone)
        //{
        //    if (www.error != null)
        //    {
        //        Debug.Log(www.error);
        //        yield return null;
        //    }
        //    else
        //    {
        //        string json = www.text.ToString();
        //buffRawData = JsonUtility.FromJson<BuffsData>(json);
        buffRawData = JsonUtility.FromJson<BuffsData>(jsonDatas.BuffData);
                GameDataMgr.My.ParseBuffData(buffRawData);
        //    }
        //}
    }

    void ReadConsumableJson()
    {
        //WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/ConsumableData.json");
        ////Debug.Log(@"file://" + Application.streamingAssetsPath + @"/Data/BuffData.json");
        //yield return www;
        //if (www.isDone)
        //{
        //    if (www.error != null)
        //    {
        //        Debug.Log(www.error);
        //        yield return null;
        //    }
        //    else
        //    {
        //        string json = www.text.ToString();
        //        consumableRawData = JsonUtility.FromJson<ConsumablesData>(json);
        consumableRawData = JsonUtility.FromJson<ConsumablesData>(jsonDatas.ConsumableData);
                GameDataMgr.My.ParseConsumableData(consumableRawData);
        //    }
        //}
    }

    void ReadStageJson()
    {
        //WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/StageData.json");
        //Debug.Log(@"file://" + Application.streamingAssetsPath + @"/Data/BuffData.json");
        //yield return www;
        //yield return StartCoroutine(ReadEquipJson());
        //yield return StartCoroutine(ReadWorkerJson());
        //if (www.isDone)
        //{
        //    if (www.error != null)
        //    {
        //        Debug.Log(www.error);
        //        yield return null;
        //    }
        //    else
        //    {
        //        string json = www.text.ToString();
        //        stageRawData = JsonUtility.FromJson<StagesData>(json);
        stageRawData = JsonUtility.FromJson<StagesData>(jsonDatas.StageData);
        GameDataMgr.My.ParseStageData(stageRawData);
        //    }
        //}
    }

    void ReadEquipJson()
    {
        //WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/EquipData.json");
        //yield return www;
        //if (www.isDone)
        //{
        //    if (www.error != null)
        //    {
        //        Debug.Log(www.error);
        //        yield return null;
        //    }
        //    else
        //    {
        //        string json = www.text.ToString();
        //        gearRawData = JsonUtility.FromJson<GearsData>(json);
        //        GameDataMgr.My.ParseEquipData(gearRawData);
        //    }
        //}
        gearRawData = JsonUtility.FromJson<GearsData>(jsonDatas.EquipData);
        GameDataMgr.My.ParseEquipData(gearRawData);
    }

    void ReadWorkerJson()
    {
        //WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/WorkerData.json");
        //yield return www;
        //if (www.isDone)
        //{
        //    if (www.error != null)
        //    {
        //        Debug.Log(www.error);
        //        yield return null;
        //    }
        //    else
        //    {
        //        string json = www.text.ToString();
        //        workerRawData = JsonUtility.FromJson<WorkersData>(json);
        //        GameDataMgr.My.ParseWorkerData(workerRawData);
        //    }
        //}
        workerRawData = JsonUtility.FromJson<WorkersData>(jsonDatas.WorkerData);
        GameDataMgr.My.ParseWorkerData(workerRawData);
    }

    void ReadRoleTemplateJson()
    {
        //WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/RoleTemplateData.json");
        //yield return www;
        //if (www.isDone)
        //{
        //    if (www.error != null)
        //    {
        //        Debug.Log(www.error);
        //        yield return null;
        //    }
        //    else
        //    {
        //        string json = www.text.ToString();
        //        roleTemplateRawData = JsonUtility.FromJson<RoleTemplateModelsData>(json);
        //        GameDataMgr.My.ParseRoleTemplateData(roleTemplateRawData);
        //    }
        //}
        roleTemplateRawData = JsonUtility.FromJson<RoleTemplateModelsData>(jsonDatas.RoleTemplateData);
        GameDataMgr.My.ParseRoleTemplateData(roleTemplateRawData);
    }

    void ReadConsumerTypeJson()
    {
        //WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/ConsumerTypeData.json");
        //yield return www;
        //if (www.isDone)
        //{
        //    if (www.error != null)
        //    {
        //        Debug.Log(www.error);
        //        yield return null;
        //    }
        //    else
        //    {
        //        string json = www.text.ToString();
        //        consumerTypeRawData = JsonUtility.FromJson<ConsumerTypesData>(json);
        //        GameDataMgr.My.ParseConsumerTypeData(consumerTypeRawData);
        //    }
        //}
        consumerTypeRawData = JsonUtility.FromJson<ConsumerTypesData>(jsonDatas.ConsumerTypeData);
        GameDataMgr.My.ParseConsumerTypeData(consumerTypeRawData);
    }
}
