using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AddRoleBuff : BaseSpawnItem
{
  

    public override void Init(int id)
    {
        base.Init(id);
        transform.DOScale(1f, GameDataMgr.My.GetBuffDataByID(id).duration).OnComplete(() => {
            Destroy(gameObject, 0f);
        }).Play();
    }
 

}
