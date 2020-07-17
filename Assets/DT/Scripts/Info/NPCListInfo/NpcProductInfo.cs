﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;
using UnityEngine.UI;

public class NpcProductInfo : MonoBehaviour
{
    public Text npcName;
    public Text des;
    public Text timeInv;
    public Text cost;
    public Text risk;

    public Text effect;
    public Text efficiency;

    public GameObject effectBar;
    public GameObject efficiencyBar;

    public Text prop1;
    public Text prop2;

    public Image icon;
    public List<Image> buffs;

    public GameObject bulletWarehourse;

    public Transform bulletContent;

    public GameObject bulletPrefab;

    public Button clearBullets;

    public Sprite AOE;
    public Sprite normallpp;
    public Sprite lightning;
    public Sprite tow;
    public Text level;




    public void SetInfo(Transform npc, BaseSkill baseSkill)
    {
        npcName.text = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleName;
        des.text = baseSkill.skillDesc;
        //timeInv
        cost.text = npc.GetComponent<BaseMapRole>().baseRoleData.tradeCost.ToString();
        risk.text = npc.GetComponent<BaseMapRole>().baseRoleData.riskResistance.ToString();

        effect.text = npc.GetComponent<BaseMapRole>().baseRoleData.effect.ToString();
        efficiency.text = npc.GetComponent<BaseMapRole>().baseRoleData.efficiency.ToString();

        SetBar(npc.GetComponent<BaseMapRole>().baseRoleData.effect, npc.GetComponent<BaseMapRole>().baseRoleData.efficiency);
        prop1.text = (npc.GetComponent<BaseMapRole>().baseRoleData.efficiency / 20f).ToString("#.#") + "/s";
        prop2.text = (npc.GetComponent<BaseMapRole>().baseRoleData.effect).ToString() + "%";

        timeInv.text = (1.0f / npc.GetComponent<BaseMapRole>().baseRoleData.efficiency).ToString("#.##");
        icon.sprite = Resources.Load<Sprite>("Sprite/RoleLogo/" + npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleType.ToString() +
            (npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level == 0 ? 1 : npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level).ToString());
        level.text = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level.ToString();
        clearBullets.onClick.RemoveAllListeners();
        clearBullets.onClick.AddListener(() => {
            NewCanvasUI.My.Panel_Delete.SetActive(true);
            string str = "确定要清空仓库吗？";
            DeleteUIManager.My.Init(str, () => {
                //PlayerData.My.GetMapRoleById(npc.baseRoleData.ID).ClearWarehouse();
                npc.GetComponent<BaseMapRole>().ClearWarehouse();
                SetInfo(npc, baseSkill);
                ClearBulletContent();
            });
        });
        ShowBullets(npc);
        bulletWarehourse.SetActive(true);
        int i = 0;
        foreach (var sp in buffs)
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
            i++;
        }

        if (npc.GetComponent<NPC>().isCanSeeEquip)
        {
            int a = 0;
            for (int j = baseSkill.buffList.Count; j < buffs.Count; j++)
            {
                if (a < npc.GetComponent<NPC>().NPCBuffList.Count)
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
            ProductMerchant pm;
            npc.TryGetComponent<ProductMerchant>(out pm);
            if (pm != null)
            {
                a = 0;
                for (int j = baseSkill.buffList.Count + npc.GetComponent<NPC>().NPCBuffList.Count; j < buffs.Count; j++)
                {
                    if (a< pm.specialBuffList.Count)
                    {
                        buffs[j].sprite = Resources.Load<Sprite>("Sprite/Buff/" + pm.specialBuffList[a]);
                        buffs[j].GetComponent<BuffText>().InitBuff(GameDataMgr.My.GetBuffDataByID(pm.specialBuffList[a]));
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
    }

    void SetBar(int effct, int effcy)
    {
        efficiencyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(effcy / 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
        effectBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(effct / 120f * 150f,
                effectBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
    }

    void ShowBullets(Transform npc)
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
    void ClearBulletContent()
    {
        for (int i = 0; i < bulletContent.childCount; i++)
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