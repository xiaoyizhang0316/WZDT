using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TeamAcount
{
	public string teamID;
	public string playerIDs;
	public string playerNames;
	public string groupID;
	public string teamName;
	public bool isDisbanded;
	public int createTime;
}

[Serializable]
public class TeamAcounts
{
	public List<TeamAcount> teamAcounts;
}

[Serializable]
public class TeamConfiguration
{
	public string playerIDs;
	public string playerNames;
	public string captain;
	public string playersPosition;
	public string playersDuty;

	public TeamConfiguration(List<TeamRole> roles)
    {
		for(int i=0; i< roles.Count; i++)
        {

        }
    }
}

public class TeamRole
{
	public string playerID;
	public string playerName;
	public bool isCaptain;
	public string playerDuty;

	public TeamRole(string playerID, string playerName, bool captain, string duty)
    {
		this.playerID = playerID;
		this.playerName = playerName;
		this.isCaptain = captain;
		this.playerDuty = duty;
		//PlayerData.My.creatRole==PlayerData.My.playerDutyID
    }
}

public class TeamPlayers
{
	public string playerIDs;
	public string playerNames;

	public TeamPlayers(List<PlayerDatas> playerDatas)
    {
		playerDatas.Sort((x, y) => x.playerID.CompareTo(y.playerID));
		for(int i=0; i<playerDatas.Count; i++)
        {
			playerIDs += playerDatas[i].playerID+"_";
			playerNames += playerDatas[i].playerName + "_";
        }
		playerIDs = playerIDs.Substring(0, playerIDs.Length - 1);
		playerNames = playerNames.Substring(0, playerNames.Length - 1);
    }
}