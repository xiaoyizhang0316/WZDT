<<<<<<< HEAD
﻿using System.Collections;
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
=======
﻿using System.Collections;
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
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
