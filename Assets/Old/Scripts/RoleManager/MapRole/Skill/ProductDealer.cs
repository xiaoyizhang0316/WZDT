using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;

public class ProductDealer : BaseSkill
{

 
    // Start is called before the first frame update
 
    public override void Skill()
    {
        if (role.warehouse.Count > 0)
        {
            if (role.shootTarget == null)
            {
                CancelSkill();
                return;
            }
            //Debug.Log("攻击");
            ProductData data = role.warehouse[0];
            role.warehouse.RemoveAt(0);
            switch (data.bulletType)
            {
                case  BulletType.NormalPP: 
                    GetComponent<BulletLaunch>().LanchNormal( data,role.shootTarget);
                    break;    
                case  BulletType.Bomb: 
                    GetComponent<BulletLaunch>().LanchBoom( data);
                    break;    
                case  BulletType.Lightning: 
                    GetComponent<BulletLaunch>().LanchLightning( data);
                    break;    
                case  BulletType.summon: 
                    GetComponent<BulletLaunch>().CreatSummonTow( data);
                    break;
                 
            } 
        }
    }



    public override void UnleashSkills()
    {
        isPlay = true;
        if (role.warehouse.Count > 0)
        {
            ProductData data = role.warehouse[0];
            float d = 1f / (role.baseRoleData.efficiency * 0.1f) * data.loadingSpeed;
            //Debug.Log("释放技能" + d);
            Skill();
            transform.DOScale(1, d).OnComplete(() =>
            {
                //Debug.Log("释放技能" + d);
           
                if (IsOpen)
                {
                    UnleashSkills();
                }
            });
        }
        else
        {
            float d = 1f / (role.baseRoleData.efficiency * 0.1f);
            transform.DOScale(1, d).OnComplete(() =>
            {
           
                if (IsOpen)
                {
                    UnleashSkills();
                    
                }
            });
        }
    }

    private void Update()
    {
        role.SetShootTarget();
    }
}
