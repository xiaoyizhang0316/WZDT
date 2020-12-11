using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class RTDatas
{
    
}

[Serializable]
public class PlayerRTScore
{
	public string playerID;
	public string playerName;
	public int groupID;
	public int bossLevel;
	public int score;
	public bool isGameEnd;
}

[Serializable]
public class PlayerRTScoreList
{
	public List<PlayerRTScore> prts;
}