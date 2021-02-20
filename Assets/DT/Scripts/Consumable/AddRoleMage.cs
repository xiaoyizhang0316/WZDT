using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AddRoleMage : BaseSpawnItem
{
  

    public override void Init(int id)
    {
        StageGoal.My.GetTechPoint(40);
        base.Init(id);
  
            Destroy(gameObject, 0.4f);
    
    }
 

}
