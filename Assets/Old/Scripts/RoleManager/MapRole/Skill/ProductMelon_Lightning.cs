using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;

public class ProductMelon_Lightning : BaseSkill
{
    private int currentCount = 0;

    public Animator anim;

    public new void Start()
    {
        base.Start();
        if (PlayerData.My.dingWei[5])
        {
            var buff = GameDataMgr.My.GetBuffDataByID(10016);
            BaseBuff baseb = new BaseBuff();
            baseb.Init(buff);
            baseb.SetRoleBuff(null, role, role);
        }
    }

    public void Update()
    {
        if (IsOpen&&isAnimPlaying)
        {
            //anim.SetBool("isPlay", true);
            anim.speed = 1;
        }
        else
        {
            //anim.SetBool("isPlay", false);
            anim.speed = 0;
        }
        
        if (TradeManager.My.CheckProductNpcTradeWrong(GetComponent<BaseMapRole>().baseRoleData.ID.ToString()))
        {
            //Debug.LogWarning("该角色存在使用错误的情况");
            if (tip == null)
            {
                tip = Instantiate(PlayerData.My.useErrorTip,
                    GetComponentInChildren<RoleTradeButton>().transform.parent);
                tip.transform.localScale = new Vector3(0.01f, 0.01f);
                tip.transform.position =GetComponentInChildren<RoleTradeButton>().transform.parent.position+ new Vector3(0, 2, 0);
                tip.GetComponent<UseErrorTip>().SetTip("加工型角色须有产品输入");
            }
        }
        else
        {
            if (tip)
            {
                Destroy(tip);
            }
        }
    }

    public override void Skill()
    {
        if (role.tradeList.Count == 0)
        {
            return;
        }
        if (role.encourageLevel <= -3)
        {
            return;
        }
        int numberNeed = 3;
        if (PlayerData.My.dingWei[5])
        {
            numberNeed--;
        }
        if (role.warehouse.Count > numberNeed && role.warehouse[0].bulletType == BulletType.NormalPP)
        {
            //print("产闪电链");
            ProductData data = role.warehouse[0];
            for (int i = 0; i < numberNeed; i++)
            {
                role.warehouse.RemoveAt(0);
            }
            for (int i = 0; i < role.GetEquipBuffList().Count; i++)
            {
                data.AddBuff(role.GetEquipBuffList()[i]);
            }
            for (int i = 0; i < badBaseBuffs.Count; i++)
            {
                badBaseBuffs[i].OnProduct(ref data);
            }
            data.bulletType = BulletType.Lightning;
            data.loadingSpeed *= 1f - role.baseRoleData.effect / 100f;
            data.loadingSpeed += 1;
            data.buffMaxCount = 4;
            data.damage = (data.damage * 0.6f + role.baseRoleData.effect);
            try
            {


                GameObject game = Instantiate(GoodsManager.My.GoodPrb, role.tradeList[currentCount].transform);
                game.GetComponent<GoodsSign>().productData = data;
                game.GetComponent<GoodsSign>().path = role.tradeList[currentCount].GetDeliverProductPath();
                game.GetComponent<GoodsSign>().role = PlayerData.My.GetMapRoleById(Double.Parse(role.tradeList[currentCount].tradeData.targetRole));
                game.transform.position = transform.position;
                game.GetComponent<GoodsSign>().Move();
                currentCount++;
            }
            catch (Exception e)
            {
                Debug.Log("exception : " + e.Message);
                currentCount = 0;
            }
            if (currentCount >= role.tradeList.Count)
            {
                currentCount = 0;
            }

        }

    }

    private GameObject tip;
    

}
