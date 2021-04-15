using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_2Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StageGoal.My.maxRoleLevel = 3;
    }

    // Update is called once per frame
    void Update()
    {
      TradeManager.My.HideAllIcon();
      
    }
}
