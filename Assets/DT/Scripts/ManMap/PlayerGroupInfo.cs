using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerGroupInfo
{
    public int groupID;
    public string groupName;
    public string groupDesc;
    public int catchLevel;
    public int joinRank;
	public bool isOpenLastLevel;
    public bool isLoginLimit;
    public int levelLimit;
    public int outdateTime;
    public bool isOpenMatch;
    public int openLevel;
    public bool isOpenLimitLevel;
}
