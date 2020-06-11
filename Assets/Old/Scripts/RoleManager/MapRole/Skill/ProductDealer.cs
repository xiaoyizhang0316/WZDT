﻿using System.Collections;
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
            Debug.Log("攻击" + role.baseRoleData.ID);
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

        if (role.warehouse.Count > 0)
        {
            isPlay = true;
            ProductData data = role.warehouse[0];
            float d = 1f / (role.baseRoleData.efficiency * 0.1f) * data.loadingSpeed;
            //Debug.Log("释放技能" + d);
            Skill();
            transform.DORotate(transform.eulerAngles, d).OnComplete(() =>
            {
                isPlay = false;
                //Debug.Log("释放技能" + d);
                if (IsOpen)
                {
                    UnleashSkills();
                }
            });
        }
        else
        {
            isPlay = true;
            float d = 1f / (role.baseRoleData.efficiency * 0.1f);
            transform.DORotate(transform.eulerAngles, d).OnComplete(() =>
            {
                isPlay = false;
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