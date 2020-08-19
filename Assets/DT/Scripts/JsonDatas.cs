using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class JsonDatas 
{
	public string BuffData;
	public string ConsumableData;
	public string ConsumerTypeData;
	public string EquipData;
	public string RoleTemplateData;
	public string SkillData;
	public string StageData;
	public string TradeSkillData;
	public string WorkerData;
	public string TranslateData;
    public string FTE_0;
    public string FTE_1;
    public string FTE_2;
    public string FTE_3;
    public string FTE_4;
    public string FTE_5;
    public string FTE_6;
    public string FTE_7;
    public string FTE_8;
    public string FTE_9;
    //public List<Level> level;

    //public string GetLevelDataByID(int id)
    //   {
    //	for(int i = 0; i < level.Count; i++)
    //       {
    //		if(level[i].index == id)
    //           {
    //			return level[i].levelData;
    //           }
    //       }
    //	return null;
    //   }
    public string GetLevelData(string sceneName)
    {
        switch (sceneName)
        {
            case "FTE_0":
                return FTE_0;
            case "FTE_1":
                return FTE_1;
            case "FTE_2":
                return FTE_2;
            case "FTE_3":
                return FTE_3;
            case "FTE_4":
                return FTE_4;
            case "FTE_5":
                return FTE_5;
            case "FTE_6":
                return FTE_6;
            case "FTE_7":
                return FTE_7;
            case "FTE_8":
                return FTE_8;
            case "FTE_9":
                return FTE_9;
        }
        return null;
    }
}

//[Serializable]
//public class Level
//{
//	public int index;
//	public string levelData;
//}