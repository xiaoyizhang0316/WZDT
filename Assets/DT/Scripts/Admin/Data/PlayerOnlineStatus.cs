using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerOnlineStatus
{
	public int groupID;
	public bool isTeacher;
	public string playerID;
	public string playerName;
	public bool isOnTeam;
	public string playerIDs;
	public string playerNames;
	public string onSceneName;
	public int lastUpdateTime;
}

[Serializable]
public class PlayerOnlineStatuses
{
	public List<PlayerOnlineStatus> playerStatuses;
}
