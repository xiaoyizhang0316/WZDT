using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMData
{
    public static bool IsTrueContent(string jsonContent, JsonTye jsonTye, out string err)
    {
        switch (jsonTye)
        {
            case JsonTye.StageEnemy:
                return IsTrueType<StageEnemysData>(jsonContent, out err);
            case JsonTye.StageNpc:
                return IsTrueType<StageNPCsData>(jsonContent, out err);
            case JsonTye.BuffData:
                return IsTrueType<BuffsData>(jsonContent, out err);
            case JsonTye.ConsumableData:
                return IsTrueType<ConsumablesData>(jsonContent, out err);
            case JsonTye.ConsumerTypeData:
                return IsTrueType<ConsumerTypesData>(jsonContent, out err);
            case JsonTye.EquipData:
                return IsTrueType<GearsData>(jsonContent, out err);
            case JsonTye.RoleTemplateData:
                return IsTrueType<RoleTemplateModelsData>(jsonContent, out err);
            case JsonTye.SkillData:
                err = "SkillData not exist";
                return false ;
            case JsonTye.StageData:
                return IsTrueType<StagesData>(jsonContent, out err);
            case JsonTye.TradeSkillData:
                err = "trade skill not exist";
                return false;
            case JsonTye.TranslateData:
                return IsTrueType<TranslatesData>(jsonContent, out err);
            case JsonTye.WorkerData:
                return IsTrueType<WorkersData>(jsonContent, out err);
        }
        err = "";
        return false;
    }

    private static bool IsTrueType<T>(string content, out string error)
    {
        try
        {
            T tt = JsonUtility.FromJson<T>(content);
            if (tt != null)
            {
                error = "解析通过";
                return true;
            }
        }
        catch(Exception e)
        {
            error = "数据与类型不匹配";
            return false;
        }
        error = "数据与类型不匹配";
        return false;
    }
}


public enum JsonTye
{
    StageEnemy,
    StageNpc,
    BuffData,
    ConsumableData,
    ConsumerTypeData,
    EquipData,
    RoleTemplateData,
    SkillData,
    StageData,
    TradeSkillData,
    TranslateData,
    WorkerData
}
