using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;

public class ProductRestaurant : BaseProductSkill
{


    public float currentenergy;

    public int seedEnergy;
    public int molonEnergy;
    public int qiaguaEnergy;
    public int guozhiEnergy;
    public int guantouEnergy;
    public int yinliaoEnergy;

    /// <summary>
    /// 生产能源时间
    /// </summary>
    public float chiguaSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Skill()
    {
        if (role.tradeList.Count == 0)
        {
            return;
        }

        if (role.warehouse.Count == 0)
        {
            return;
        }
        AddEnergy(); 
       
        
        ///消耗瓜
      
    }

    public override void UnleashSkills()
    {
        transform.DORotate(transform.eulerAngles, chiguaSpeed).OnComplete(() =>
        {
          
            Skill();
            if (IsOpen)
            {
                UnleashSkills();
            }
        });

       
    }


    public void AddEnergy()
    {
        if (role.tradeList.Count == 0)
        {
            return;
        }

        if (role.warehouse.Count == 0)
        {
            return;
        }

        switch (role.warehouse[0].bulletType)
        {
            case  BulletType.Seed :
                currentenergy += seedEnergy + role.warehouse[0].damage * 1;
                break;
            case  BulletType.NormalPP :
                currentenergy += molonEnergy + role.warehouse[0].damage * 1;
                break;
            case  BulletType.Bomb :
                currentenergy += guozhiEnergy + role.warehouse[0].damage * 1;
                break;
            case  BulletType.Juice :
                currentenergy += yinliaoEnergy + role.warehouse[0].damage * 1;
                break;
            
            case  BulletType.summon :
                currentenergy += guantouEnergy + role.warehouse[0].damage * 1;
                break;
            case  BulletType.Lightning :
                currentenergy += qiaguaEnergy + role.warehouse[0].damage * 1;
                break;
        }
        for (int i = 0; i <role.warehouse[0].buffList.Count ; i++)
        {
            currentenergy += GameDataMgr.My.GetBuffDataByID(role.warehouse[0].buffList[i]).buffValue;
        }
        
    }

// Update is called once per frame
    void Update()
    {
        
    }
}
