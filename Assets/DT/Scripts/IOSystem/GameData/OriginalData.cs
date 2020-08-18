using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.Networking;

public class OriginalData : MonoSingleton<OriginalData>
{
    public BuffsData buffRawData;

    public ConsumablesData consumableRawData;

    public StagesData stageRawData;

    public GearsData gearRawData;

    public WorkersData workerRawData;

    public RoleTemplateModelsData roleTemplateRawData;

    public ConsumerTypesData consumerTypeRawData;

    public TranslatesData translateRawData;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(ReadBuffJson());
        StartCoroutine(ReadConsumableJson());
        StartCoroutine(ReadStageJson());
        StartCoroutine(ReadRoleTemplateJson());
        StartCoroutine(ReadConsumerTypeJson());
        StartCoroutine(ReadTranslateJson());
    }

    IEnumerator ReadBuffJson()
    {
        WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/BuffData.json");
        //Debug.Log(@"file://" + Application.streamingAssetsPath + @"/Data/BuffData.json");
        yield return www;
        if (www.isDone)
        {
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield return null;
            }
            else
            {
                string json = www.text.ToString();
                buffRawData = JsonUtility.FromJson<BuffsData>(json);
                GameDataMgr.My.ParseBuffData(buffRawData);
            }
        }
    }

    IEnumerator ReadConsumableJson()
    {
        WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/ConsumableData.json");
        //Debug.Log(@"file://" + Application.streamingAssetsPath + @"/Data/BuffData.json");
        yield return www;
        if (www.isDone)
        {
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield return null;
            }
            else
            {
                string json = www.text.ToString();
                consumableRawData = JsonUtility.FromJson<ConsumablesData>(json);
                GameDataMgr.My.ParseConsumableData(consumableRawData);
            }
        }
    }

    IEnumerator ReadStageJson()
    {
        WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/StageData.json");
        Debug.Log(@"file://" + Application.streamingAssetsPath + @"/Data/BuffData.json");
        yield return www;
        yield return StartCoroutine(ReadEquipJson());
        yield return StartCoroutine(ReadWorkerJson());
        if (www.isDone)
        {
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield return null;
            }
            else
            {
                string json = www.text.ToString();
                stageRawData = JsonUtility.FromJson<StagesData>(json);
                GameDataMgr.My.ParseStageData(stageRawData);
            }
        }
    }

    IEnumerator ReadEquipJson()
    {
        WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/EquipData.json");
        yield return www;
        if (www.isDone)
        {
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield return null;
            }
            else
            {
                string json = www.text.ToString();
                gearRawData = JsonUtility.FromJson<GearsData>(json);
                GameDataMgr.My.ParseEquipData(gearRawData);
            }
        }
    }

    IEnumerator ReadWorkerJson()
    {
        WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/WorkerData.json");
        yield return www;
        if (www.isDone)
        {
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield return null;
            }
            else
            {
                string json = www.text.ToString();
                workerRawData = JsonUtility.FromJson<WorkersData>(json);
                GameDataMgr.My.ParseWorkerData(workerRawData);
            }
        }
    }

    IEnumerator ReadRoleTemplateJson()
    {
        WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/RoleTemplateData.json");
        yield return www;
        if (www.isDone)
        {
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield return null;
            }
            else
            {
                string json = www.text.ToString();
                roleTemplateRawData = JsonUtility.FromJson<RoleTemplateModelsData>(json);
                GameDataMgr.My.ParseRoleTemplateData(roleTemplateRawData);
            }
        }
    }

    IEnumerator ReadConsumerTypeJson()
    {
        WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/ConsumerTypeData.json");
        yield return www;
        if (www.isDone)
        {
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield return null;
            }
            else
            {
                string json = www.text.ToString();
                consumerTypeRawData = JsonUtility.FromJson<ConsumerTypesData>(json);
                GameDataMgr.My.ParseConsumerTypeData(consumerTypeRawData);
            }
        }
    }

    IEnumerator ReadTranslateJson()
    {
        WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/TranslateData.json");
        yield return www;
        if (www.isDone)
        {
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield return null;
            }
            else
            {
                string json = www.text.ToString();
                translateRawData = JsonUtility.FromJson<TranslatesData>(json);
                GameDataMgr.My.ParseTranslateData(translateRawData);
            }
        }
    }
}
