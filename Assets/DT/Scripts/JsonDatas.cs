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
    public string EncourageSkillData;
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
    public string NPC_1;
    public string NPC_2;
    public string NPC_3;
    public string NPC_4;
    public string NPC_5;
    public string NPC_6;
    public string NPC_7;
    public string NPC_8;
    public string NPC_9;
    public string FTE_4_5;
    public string FTE_F2;
    public string FTE_F1;

    public string questions;
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
    public string GetLevelData(string sceneName, bool isNPC = false)
    {
        switch (sceneName)
        {
            case "FTE_-2":
                if (isNPC)
                {
                    return null;
                }
                return FTE_F2;
            case "FTE_-1":
                if (isNPC)
                {
                    return null;
                }
                return FTE_F1;
            case "FTE_0":
                if (isNPC)
                    return null;
                return FTE_0;
            case "FTE_1":
                if (isNPC)
                    return NPC_1;
                return FTE_1;
            case "FTE_2":
                if (isNPC)
                    return NPC_2;
                return FTE_2;
            case "FTE_3":
                if (isNPC)
                    return NPC_3;
                return FTE_3;
            case "FTE_4":
                if (isNPC)
                    return NPC_4;
                return FTE_4;
            case "FTE_4.5":
                
                return FTE_4_5;
            case "FTE_5":
                if (isNPC)
                    return NPC_5;
                return FTE_5;
            case "FTE_6":
                if (isNPC)
                    return NPC_6;
                return FTE_6;
            case "FTE_7":
                if (isNPC)
                    return NPC_7;
                return FTE_7;
            case "FTE_8":
                if (isNPC)
                    return NPC_8;
                return FTE_8;
            case "FTE_9":
                if (isNPC)
                    return NPC_9;
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