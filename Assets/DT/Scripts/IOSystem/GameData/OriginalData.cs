using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.Networking;

public class OriginalData : MonoSingleton<OriginalData>
{
    public JsonDatas jsonDatas=new JsonDatas();
    public BuffsData buffRawData;

    public ConsumablesData consumableRawData;

    public StagesData stageRawData;

    public GearsData gearRawData;

    public WorkersData workerRawData;

    public RoleTemplateModelsData roleTemplateRawData;

    public ConsumerTypesData consumerTypeRawData;

    public TranslatesData translateRawData;

    public AnswerStage[] answerStages;

    public QuestionList questionList;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //StartCoroutine(ReadBuffJson());
        //StartCoroutine(ReadConsumableJson());
        //StartCoroutine(ReadStageJson());
        //StartCoroutine(ReadRoleTemplateJson());
        //StartCoroutine(ReadConsumerTypeJson());
        //StartCoroutine(ReadTranslateJson());
        StartCoroutine(ReadQuestionList());
        if (NetworkMgr.My.useLocalJson)
        {
            StartCoroutine(ReadStageData());
            StartCoroutine(ReadConsumerType());
        }
    }

    public void InitDatas(string data)
    {
        jsonDatas = JsonUtility.FromJson<JsonDatas>(data);
        Debug.Log("---------" + jsonDatas.BuffData);
        
        ReadBuffJson();
        ReadConsumableJson();
        if (!NetworkMgr.My.useLocalJson)
        {

            ReadStageJson();
            ReadConsumerTypeJson();
        }
        ReadRoleTemplateJson();
        ReadTranslateJson();
        ReadEquipJson();
        ReadWorkerJson();
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

    void ReadTranslateJson()
    {
        //WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/TranslateData.json");
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
        //        translateRawData = JsonUtility.FromJson<TranslatesData>(json);
        //        GameDataMgr.My.ParseTranslateData(translateRawData);
        //    }
        //}
        translateRawData = JsonUtility.FromJson<TranslatesData>(jsonDatas.TranslateData);
        GameDataMgr.My.ParseTranslateData(translateRawData);
    }

    IEnumerator ReadQuestionList()
    {
        //questionList = JsonUtility.FromJson<QuestionList>(jsonDatas.questions);

        WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/Questions.json");
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
                questionList = JsonUtility.FromJson<QuestionList>(json);
            }
        }
    }

    IEnumerator ReadStageData()
    {
        WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/StageData.json");
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
                stageRawData = JsonUtility.FromJson<StagesData>(json);
        //stageRawData = JsonUtility.FromJson<StagesData>(jsonDatas.StageData);
                GameDataMgr.My.ParseStageData(stageRawData);
            }
        }
    }

    IEnumerator ReadConsumerType()
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
        //consumerTypeRawData = JsonUtility.FromJson<ConsumerTypesData>(jsonDatas.ConsumerTypeData);
        //GameDataMgr.My.ParseConsumerTypeData(consumerTypeRawData);
    }
}
