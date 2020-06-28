using System.Collections;
using System.Collections.Generic;
using DT.Fight.Bullet;
using UnityEngine;
using UnityEngine.UI;

public class NpcSpecialInfo : MonoBehaviour
{
    public GameObject seedProp;
    public GameObject peasantProp;
    public GameObject merchantProp;
    public GameObject dealerProp;

    public Text npcName;
    public Text skillDes;
    public Text efficiency;
    public Text effect;
    public Text cost;
    public Text risk;

    public List<Image> buffs;

    public Text level;

    public GameObject bulletWarehourse;

    public Transform bulletContent;

    public GameObject bulletPrefab;

    public Sprite AOE;
    public Sprite normallpp;
    public Sprite lightning;
    public Sprite tow;

    public void SetInfo(BaseMapRole npc, BaseSkill baseSkill, Transform npcTF)
    {
        npcName.text = npc.baseRoleData.baseRoleData.roleName;
        skillDes.text = baseSkill.skillDesc;
        efficiency.text = npc.baseRoleData.efficiency.ToString();
        effect.text = npc.baseRoleData.effect.ToString() ;
        cost.text = npc.baseRoleData.cost.ToString();
        risk.text = npc.baseRoleData.riskResistance.ToString();
        level.text = npc.baseRoleData.baseRoleData.level.ToString();
        HideAll();
        //ClearBulletContent();
        switch (npc.baseRoleData.baseRoleData.roleType)
        {
            case GameEnum.RoleType.Seed:
                seedProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.efficiency / 20f).ToString("#.#") + "/s";
                seedProp.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.effect*10).ToString();
                seedProp.SetActive(true);
                ShowSeedBullet(npcTF);
                bulletWarehourse.SetActive(true);
                break;
            case GameEnum.RoleType.Peasant:
                peasantProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.efficiency / 10f).ToString("#.#") + "/s";
                peasantProp.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.effect).ToString()+"%";
                peasantProp.SetActive(true);
                ShowPeasantBullet(npcTF);
                bulletWarehourse.SetActive(true);
                break;
            case GameEnum.RoleType.Merchant:
                merchantProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (((npc.baseRoleData.efficiency * 0.3f) / 100f) * npc.baseRoleData.tradeCost).ToString("#.#");
                merchantProp.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.effect).ToString() + "%";
                merchantProp.SetActive(true);
                ShowMerchantBullet(npcTF);
                bulletWarehourse.SetActive(true);
                break;
            case GameEnum.RoleType.Dealer:
                dealerProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.efficiency).ToString() + "%";
                dealerProp.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.range).ToString();

                dealerProp.SetActive(true);
                ShowDealerBullet(npcTF);
                bulletWarehourse.SetActive(true);
                break;
        }
        int i = 0;
        foreach(var sp in buffs)
        {
            if (i < baseSkill.buffList.Count)
            {
                sp.sprite = Resources.Load<Sprite>("Sprite/Buff/" + baseSkill.buffList[i]);
                sp.GetComponent<BuffText>().InitBuff(GameDataMgr.My.GetBuffDataByID(baseSkill.buffList[i]));
            }
            else
            {
                sp.sprite = Resources.Load<Sprite>("Sprite/Buff/999");
                sp.GetComponent<BuffText>().Reset();
            }
        }
        
        if (npc.GetComponent<NPC>().isCanSeeEquip)
        {
            int a = 0;
            for (int j = baseSkill.buffList.Count; j < buffs.Count; j++)
            {
                if(a< npc.GetComponent<NPC>().NPCBuffList.Count)
                {
                    buffs[j].sprite = Resources.Load<Sprite>("Sprite/Buff/" + npc.GetComponent<NPC>().NPCBuffList[a]);
                    buffs[j].GetComponent<BuffText>().InitBuff(GameDataMgr.My.GetBuffDataByID(npc.GetComponent<NPC>().NPCBuffList[a]));
                }
                else
                {
                    buffs[j].sprite = NPCListInfo.My.buff;
                    buffs[j].GetComponent<BuffText>().Reset();
                }
                a++;
            }
        }
        
        
    }

    void ShowSeedBullet(Transform npc)
    {
        int count = 9;
        if (npc.GetComponent<ProductSeed>().productDatas.Count < 9)
        {
            count = npc.GetComponent<ProductSeed>().productDatas.Count;
        }

        for (int i = 1; i <= count; i++)
        {
            GameObject Pruductgame = Instantiate(bulletPrefab, bulletContent);
            Pruductgame.GetComponent<NpcBulletSign>().currentProduct =
                npc.GetComponent<ProductSeed>().productDatas[npc.GetComponent<ProductSeed>().productDatas.Count - i];
            Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.seedSpeed;
        }
    }

    void ShowPeasantBullet(Transform npc)
    {
        int count = 9;
        if (npc.GetComponent<ProductMelon>().productDatas.Count < 9)
        {
            count = npc.GetComponent<ProductMelon>().productDatas.Count;
        }


        for (int i = 1; i <= count; i++)
        {
            //Debug.Log(i+"||"+ baseMapRole.GetComponent<ProductMelon>().productDatas.Count);
            GameObject Pruductgame = Instantiate(bulletPrefab, bulletContent);
            Pruductgame.GetComponent<NpcBulletSign>().currentProduct =
                npc.GetComponent<ProductMelon>().productDatas[npc.GetComponent<ProductMelon>().productDatas.Count - i];
            Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.normallpp;
        }
    }

    void ShowMerchantBullet(Transform npc)
    {
        BaseMapRole baseMapRole = npc.GetComponent<BaseMapRole>();
        for (int i = 0; i < baseMapRole.warehouse.Count; i++)
        {

            GameObject Pruductgame = Instantiate(bulletPrefab, bulletContent);
            Pruductgame.GetComponent<NpcBulletSign>().currentProduct =
                baseMapRole.warehouse[i];
            switch (baseMapRole.warehouse[i].bulletType)
            {
                case BulletType.Bomb:
                    Pruductgame.GetComponent<Image>().sprite = AOE;
                    break;
                case BulletType.NormalPP:
                    Pruductgame.GetComponent<Image>().sprite = normallpp;
                    break;

                case BulletType.Lightning:
                    Pruductgame.GetComponent<Image>().sprite = lightning;
                    break;

                case BulletType.summon:
                    Pruductgame.GetComponent<Image>().sprite = tow;
                    break;

            }

        }
    }

    void ShowDealerBullet(Transform npc)
    {
        BaseMapRole baseMapRole = npc.GetComponent<BaseMapRole>();
        for (int i = 0; i < baseMapRole.warehouse.Count; i++)
        {
            GameObject Pruductgame = Instantiate(bulletPrefab, bulletContent);
            Pruductgame.GetComponent<NpcBulletSign>().currentProduct =
                baseMapRole.warehouse[i];
            switch (baseMapRole.warehouse[i].bulletType)
            {
                case BulletType.Bomb:
                    Pruductgame.GetComponent<Image>().sprite = AOE;
                    break;
                case BulletType.NormalPP:
                    Pruductgame.GetComponent<Image>().sprite = normallpp;
                    break;
                case BulletType.Lightning:
                    Pruductgame.GetComponent<Image>().sprite = lightning;
                    break;
                case BulletType.summon:
                    Pruductgame.GetComponent<Image>().sprite = tow;
                    break;
            }
        }
    }

    void HideAll()
    {
        seedProp.SetActive(false);
        peasantProp.SetActive(false);
        merchantProp.SetActive(false);
        dealerProp.SetActive(false);
    }

    void ClearBulletContent()
    {
        for(int i=0; i< bulletContent.childCount; i++)
        {
            Destroy(bulletContent.GetChild(i).gameObject);
        }
    }

    public void Reset()
    {
        ClearBulletContent();
        bulletWarehourse.SetActive(false);
    }
}
