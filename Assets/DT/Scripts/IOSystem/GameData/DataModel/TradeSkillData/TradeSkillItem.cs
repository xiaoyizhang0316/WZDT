using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace WZDT.GameData
{
	[Serializable]
	public class TradeSkillItem
	{
		/// <summary>
		/// 
		/// </summary>
		public string ID;
		/// <summary>
		/// 
		/// </summary>
		public string startRole;
		/// <summary>
		/// 
		/// </summary>
		public string endRole;
		/// <summary>
		/// 
		/// </summary>
		public string conductRole;
		/// <summary>
		/// 
		/// </summary>
		public string anotherRole;
		/// <summary>
		/// 送货
		/// </summary>
		public string skillId;

		/// <summary>
		/// 
		/// </summary>
		public string isLock;
		/// <summary>
		/// 
		/// </summary>
		public string searchIn;
		/// <summary>
		/// 
		/// </summary>
		public string bargainIn;
		/// <summary>
		/// 
		/// </summary>
		public string deliverIn;
		/// <summary>
		/// 
		/// </summary>
		public string riskIn;
		/// <summary>
		/// 
		/// </summary>
		public string searchOut;
		/// <summary>
		/// 
		/// </summary>
		public string bargainOut;
		/// <summary>
		/// 
		/// </summary>
		public string deliverOut;
		/// <summary>
		/// 
		/// </summary>
		public string riskOut;
	}

	[Serializable]
	public class TradeSkillsData
	{
		public List<TradeSkillItem> tradeSkillSigns = new List<TradeSkillItem>();
	}
}