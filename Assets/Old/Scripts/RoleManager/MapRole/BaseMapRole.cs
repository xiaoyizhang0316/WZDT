using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;
using static GameEnum;

[Serializable]
public class BaseMapRole : MonoBehaviour
{
    public Role baseRoleData;

    /// <summary>
    /// 仓库
    /// </summary>
    public List<ProductData> warehouse;

    public List<ProductData> trash = new List<ProductData>();

    /// <summary>
    /// 是否是NPC
    /// </summary>
    public bool isNpc = false;

    /// <summary>
    /// 交易列表
    /// </summary>
    public List<TradeSign> tradeList = new List<TradeSign>();

    /// <summary>
    /// 自身buff列表
    /// </summary>
    public List<BaseBuff> buffList;

    /// <summary>
    /// 进入射程的消费者列表
    /// </summary>
    public List<ConsumeSign> shootTargetList = new List<ConsumeSign>();

    /// <summary>
    /// 占地列表x
    /// </summary>
    public int posX;

    /// <summary>
    /// 占地列表y
    /// </summary>
    public int posY;

    /// <summary>
    /// 当前射击目标
    /// </summary>
    public ConsumeSign shootTarget;

    public Transform tradePoint;

    public bool AI;

    public BaseNpc npcScript;

    public BaseExtraSkill extraSkill;

    public List<GameObject> levelModels;

    public int putTime;

    #region UI显示信息

    public float totalProfit;

    public float monthlyProfit;

    public float totalSatisfy;

    public float monthlySatisfy;

    public float totalCost;

    public float rentCost;

    public float operationCost;

    public float activityCost;

    public float tradeCost;

    #endregion
    /// <summary>
    /// 商店   最多不能多于  baseroleData.counter
    /// </summary>
    public List<ProductData> shop;

    public GameObject tradeButton;

    /// <summary>
    /// 激励等级
    /// </summary>
    public int encourageLevel;

    /// <summary>
    /// 初始激励等级
    /// </summary>
    public int startEncourageLevel;

    public RoleSprite roleSprite;

    public void InitBaseRoleData()
    {
        baseRoleData = PlayerData.My.GetRoleById(double.Parse(name));
    }

    /// <summary>
    /// 根据角色模板初始化角色属性
    /// </summary>
    public void InitAttribute()
    {
        baseRoleData.effect = baseRoleData.baseRoleData.effect;
        baseRoleData.efficiency = baseRoleData.baseRoleData.efficiency;
        baseRoleData.riskResistance = baseRoleData.baseRoleData.riskResistance;
        baseRoleData.tradeCost = baseRoleData.baseRoleData.tradeCost;
        baseRoleData.range = baseRoleData.baseRoleData.range;
        baseRoleData.cost = baseRoleData.baseRoleData.cost;
        baseRoleData.bulletCapacity = baseRoleData.baseRoleData.bulletCapacity;
    }

    // Start is called before the first frame update
    void Start()
    {
        buffList = new List<BaseBuff>();
        CheckBuffDuration();
        if (!isNpc)
        {
            InitAttribute();
        }
        if (baseRoleData.inMap)
        {
            if (!PlayerData.My.RoleData.Contains(GetComponent<BaseMapRole>().baseRoleData))
                PlayerData.My.RoleData.Add(GetComponent<BaseMapRole>().baseRoleData);
            if (!PlayerData.My.MapRole.Contains(GetComponent<BaseMapRole>()))
                PlayerData.My.MapRole.Add(GetComponent<BaseMapRole>());
        }
        tradePoint.GetComponent<MeshRenderer>().enabled = false;
    }

    /// <summary>
    /// 根据职责隐藏某些物体
    /// </summary>
    public void CheckRoleDuty()
    {
        if (PlayerData.My.creatRole == PlayerData.My.playerDutyID)
        {
            roleSprite.gameObject.SetActive(false);
        }
        else
        {
            roleSprite.gameObject.SetActive(true);
            roleSprite.CheckSprite();
        }
    }

    /// <summary>
    /// 根据角色等级改变模型
    /// </summary>
    public void CheckLevel()
    {
        if (PlayerData.My.creatRole == PlayerData.My.playerDutyID)
        {
            if (levelModels.Count == 0)
                return;
            List<GameEnum.RoleType> typeList = new List<GameEnum.RoleType> { GameEnum.RoleType.Seed, GameEnum.RoleType.Peasant, GameEnum.RoleType.Merchant, GameEnum.RoleType.Dealer };
            if (!typeList.Contains(baseRoleData.baseRoleData.roleType))
                return;
            if (baseRoleData.baseRoleData.level <= 2)
            {

                levelModels[1].SetActive(false);
                levelModels[2].SetActive(false);
                levelModels[0].SetActive(true);
            }
            else if (baseRoleData.baseRoleData.level <= 4)
            {
                levelModels[0].SetActive(false);
                levelModels[2].SetActive(false);
                levelModels[1].SetActive(true);
            }
            else if (baseRoleData.baseRoleData.level == 5)
            {
                levelModels[0].SetActive(false);
                levelModels[1].SetActive(false);
                levelModels[2].SetActive(true);
            }
        }
        else
        {
            if (roleSprite != null)
            {
                roleSprite.CheckSprite();
            }
            else
            {
                roleSprite = GetComponentInChildren<RoleSprite>();
                roleSprite.CheckSprite();
            }
        }

    }

    /// <summary>
    /// 更新激励等级
    /// </summary>
    public void RecalculateEncourageLevel(bool isInit = false)
    {
        int result = startEncourageLevel;
        if (isInit)
            baseRoleData.tradeCost -= encourageLevel * 5;
        for (int i = 0; i < tradeList.Count; i++)
        {
            if (tradeList[i].tradeData.startRole.Equals(baseRoleData.ID.ToString()))
            {
                result += tradeList[i].tradeData.dividePercent;
            }
            else
            {
                result += 4 - tradeList[i].tradeData.dividePercent;
            }
            PlayerData.My.GetMapRoleById(double.Parse(tradeList[i].tradeData.targetRole)).ResetAllBuff();
        }
        result = Mathf.Min(10, result);
        result = Mathf.Max(result, -5);
        encourageLevel = result;
        baseRoleData.tradeCost += encourageLevel * 5;
    }

    #region 战斗

    /// <summary>
    /// 将目标消费者加入射击范围列表（防止重复）
    /// </summary>
    /// <param name="sign"></param>
    public void AddConsumerIntoShootList(ConsumeSign sign)
    {
        if (shootTargetList.Contains(sign))
            return;
        else
            shootTargetList.Add(sign);
    }

    /// <summary>
    /// 将目标消费者移出射击范围列表
    /// </summary>
    /// <param name="sign"></param>
    public void RemoveConsumerFromShootList(ConsumeSign sign)
    {
        if (!shootTargetList.Contains(sign))
            return;
        else
            shootTargetList.Remove(sign);
    }

    /// <summary>
    /// 选择范围内最近的消费者作为射击目标
    /// </summary>
    public void SetShootTarget()
    {
        ConsumeSign preStatus = shootTarget;
        shootTarget = null;
        if (shootTargetList.Count == 0)
            return;
        float maxDis = 0f;
        for (int i = 0; i < shootTargetList.Count; i++)
        {
            if (shootTargetList[i] == null)
            {
                shootTargetList.RemoveAt(i);
            }
            else if (shootTargetList[i].tweener.ElapsedPercentage(false) > maxDis)
            {
                maxDis = shootTargetList[i].tweener.ElapsedPercentage(false);
                shootTarget = shootTargetList[i];
            }
        }
        if (preStatus == null && shootTarget != null && !GetComponent<BaseSkill>().IsOpen)
        {
            GetComponent<BaseSkill>().ReUnleashSkills();
        }
    }

    #endregion

    #region BUFF结算

    /// <summary>
    /// buff增加时回调
    /// </summary>
    /// <param name="baseBuff"></param>
    public void AddBuff(BaseBuff baseBuff)
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].buffId == baseBuff.buffId)
            {
                return;
                //RemoveBuff(buffList[i]);
            }
        }
        buffList.Add(baseBuff);
        baseBuff.RoleBuffAdd();
    }

    /// <summary>
    /// buff删除时回调
    /// </summary>
    /// <param name="baseBuff"></param>
    public void RemoveBuff(BaseBuff baseBuff)
    {
        baseBuff.RoleBuffRemove();
        buffList.Remove(baseBuff);
        //print("buff消失");
    }

    /// <summary>
    /// 删除指定id的buff
    /// </summary>
    /// <param name="buffId"></param>
    public void RemoveBuffById(int buffId)
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].buffId == buffId)
            {
                RemoveBuff(buffList[i]);
                break;
            }
        }
    }

    /// <summary>
    /// 检测所有buff的持续时间
    /// </summary>
    public void CheckBuffDuration()
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            buffList[i].OnRoleTick();
            if (buffList[i].duration != -1)
            {
                buffList[i].duration--;
                if (buffList[i].duration == 0)
                {
                    //print("buff消失");
                    RemoveBuff(buffList[i]);
                }
            }
        }
        transform.DORotate(transform.eulerAngles, 1f).OnComplete(() =>
        {
            CheckBuffDuration();
        });
    }

    /// <summary>
    /// 重新计算角色属性
    /// </summary>
    public void ResetAllBuff()
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            buffList[i].ResetRoleBuff();
        }
    }

    /// <summary>
    /// 当濒临破产时
    /// </summary>
    public void OnBeforeDead()
    {
        foreach (BaseBuff b in buffList)
        {
            b.OnPlayerBeforeDead();
        }
    }

    #endregion

    #region 钱，科技值相关
    /// <summary>
    /// 固定消耗成本
    /// </summary>
    public void MonthlyCost()
    {
        transform.DORotate(transform.eulerAngles, 20f).OnComplete(() =>
        {
            StageGoal.My.CostPlayerGold(baseRoleData.cost);
            StageGoal.My.Expend(baseRoleData.cost, ExpendType.ProductCosts, this);
            MonthlyCost();
        });
    }

    /// <summary>
    /// 固定获得科技点数
    /// </summary>
    public void AddTechPoint()
    {
        transform.DORotate(transform.eulerAngles, 20f).OnComplete(() =>
        {
            StageGoal.My.GetTechPoint(baseRoleData.techAdd / 3 * 2);
            StageGoal.My.IncomeTp(baseRoleData.techAdd / 3 * 2, IncomeTpType.Npc);
            AddTechPoint();
        });
    }

    #endregion

    #region  移动物品

    /// <summary>
    /// 移动物品到仓库
    /// </summary>
    /// <param name="productData"></param>
    public void MoveGoodsToWareHouse(ProductData productData)
    {

        warehouse.Add(productData);
        //Input.Remove(productData);
    }

    /// <summary>
    /// 将仓库产品输入到商店
    /// </summary>
    /// <param name="productData"></param>
    public void ShiftProductWarehouseToShop(ProductData productData)
    {
        shop.Add(productData);
        //        Debug.Log(warehouse.Count);
        // Debug.Log(warehouse.Contains(productData));
        if (baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
        {
            //todo
            //  productData.Quality += (int)(baseRoleData.quality * 0.2f);
            //   productData.Brand += (int)(baseRoleData.brand * 0.2f);
        }
        warehouse.Remove(productData);
        //Debug.Log(warehouse.Count);
    }

    #endregion

    #region 获取物品

    public void AddPruductToWareHouse(ProductData data)
    {
        if (warehouse.Count > baseRoleData.bulletCapacity)
        {
            DataUploadManager.My.AddData(DataEnum.浪费的瓜);
            return;
        }
        else
        {
            warehouse.Add(data);
        }
    }

    /// <summary>
    /// 查找仓库产品
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ProductData SearchWarehouseProductData(BulletType type)
    {
        ProductData pro = null;
        for (int i = 0; i < warehouse.Count; i++)
        {
            if (warehouse[i].bulletType == type)
            {
                pro = warehouse[i];
                break;
            }
        }
        return pro;
    }

    /// <summary>
    /// 提取仓库产品
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ProductData GetWarehouseProductData(BulletType type)
    {
        ProductData pro = null;
        for (int i = 0; i < warehouse.Count; i++)
        {
            if (warehouse[i].bulletType == type)
            {
                pro = warehouse[i];
                warehouse.Remove(warehouse[i]);
                break;
            }
        }
        return pro;
    }

    /// <summary>
    /// 清空仓库
    /// </summary>
    public void ClearWarehouse()
    {
        DataUploadManager.My.AddData(DataEnum.角色_清仓);
        trash.AddRange(warehouse);
        warehouse.Clear();
    }

    /// <summary>
    /// 清理垃圾
    /// </summary>
    public int ClearTrash(int number)
    {
        if (trash.Count >= number)
        {
            for (int i = 0; i < number; i++)
            {
                trash.RemoveAt(0);
            }
            return number;
        }
        else
        {
            int result = trash.Count;
            for (int i = 0; i < trash.Count; i++)
            {
                trash.RemoveAt(0);
            }
            return result;
        }
    }

    #endregion

    #region 商店/零售

    /// <summary>
    /// 从列表中随机N个Gameobject
    /// </summary>
    /// <param name="list"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public List<GameObject> GetRandom(List<GameObject> list, int num)
    {
        List<GameObject> result = new List<GameObject>();
        if (list.Count <= num)
        {
            return list;
        }
        else
        {
            for (int i = 0; i < num; i++)
            {
                int selectId = UnityEngine.Random.Range(0, list.Count);
                result.Add(list[selectId]);
                list.RemoveAt(selectId);
            }
        }
        return result;
    }

    #endregion
    /// <summary>
    /// 获得所有装备带的BUFF
    /// </summary>
    /// <returns></returns>
    public List<int> GetEquipBuffList()
    {
        List<int> bufflist = new List<int>();
        if (isNpc)
        {
            if (GetComponent<BaseNpc>().isCanSeeEquip)
            {
                for (int i = 0; i < baseRoleData.EquipList.Keys.ToList().Count; i++)
                {
                    GearData data = GameDataMgr.My.GetGearData(baseRoleData.EquipList.Keys.ToList()[i]);
                    foreach (int item in data.buffList)
                    {
                        if (item != -1)
                        {
                            bufflist.Add(item);
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < baseRoleData.EquipList.Keys.ToList().Count; i++)
            {
                GearData data = GameDataMgr.My.GetGearData(baseRoleData.EquipList.Keys.ToList()[i]);
                foreach (int item in data.buffList)
                {
                    if (item != -1)
                    {
                        bufflist.Add(item);
                    }
                }
            }
        }
        return bufflist;
    }

    /// <summary>
    /// 打开自发光效果
    /// </summary>
    public void LightOn(BaseMapRole start)
    {
        if(!isNpc)
        {
            if (start.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product && baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
            {
                if (TradeConstraint.My.CheckTradeConstraint(start.baseRoleData.baseRoleData.roleType, baseRoleData.baseRoleData.roleType))
                {
                    foreach (var item in levelModels)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var t in item.GetComponentsInChildren<Transform>())
                            {

                                t.gameObject.layer = 9;
                            }
                        }
                    }
                    BulletLaunch temp;
                    if (TryGetComponent(out temp))
                    {
                        foreach (var item in temp.paos)
                        {
                            item.transform.GetChild(0).gameObject.layer = 9;
                        }
                    }
                }
                else
                {
                    foreach (var item in levelModels)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var t in item.GetComponentsInChildren<Transform>())
                            {

                                t.gameObject.layer = 10;
                            }
                        }
                    }
                    BulletLaunch temp;
                    if (TryGetComponent(out temp))
                    {
                        foreach (var item in temp.paos)
                        {
                            item.transform.GetChild(0).gameObject.layer = 10;
                        }
                    }
                }
            }
            else
            {
                foreach (var item in levelModels)
                {
                    if (item.activeInHierarchy)
                    {
                        foreach (var t in item.GetComponentsInChildren<Transform>())
                        {

                            t.gameObject.layer = 9;
                        }
                    }
                }
                BulletLaunch temp;
                if (TryGetComponent(out temp))
                {
                    foreach (var item in temp.paos)
                    {
                        item.transform.GetChild(0).gameObject.layer = 9;
                    }
                }
            }
            //MeshRenderer[] temp = GetComponentsInChildren<MeshRenderer>();
            //foreach (MeshRenderer m in temp)
            //{
            //    foreach (Material item in m.materials)
            //    {
            //        item.EnableKeyword("_EMISSION");
            //        item.SetColor("_EmissionColor", Color.HSVToRGB(0.1736111f, 1f, 0.4433962f));
            //    }
            //}
        }
        else if (npcScript.isCanSee && !npcScript.isLock)
        {
            if (start.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product && baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
            {
                if (TradeConstraint.My.CheckTradeConstraint(start.baseRoleData.baseRoleData.roleType, baseRoleData.baseRoleData.roleType))
                {
                    foreach (var item in levelModels)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var t in item.GetComponentsInChildren<Transform>())
                            {
                                t.gameObject.layer = 9;
                            }
                        }
                    }
                    BulletLaunch temp;
                    if (TryGetComponent(out temp))
                    {
                        foreach (var item in temp.paos)
                        {
                            item.transform.GetChild(0).gameObject.layer = 9;
                        }
                    }
                }
                else
                {
                    foreach (var item in levelModels)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var t in item.GetComponentsInChildren<Transform>())
                            {

                                t.gameObject.layer = 10;
                            }
                        }
                    }
                    BulletLaunch temp;
                    if (TryGetComponent(out temp))
                    {
                        foreach (var item in temp.paos)
                        {
                            item.transform.GetChild(0).gameObject.layer = 10;
                        }
                    }
                }
            }
            else
            {
                foreach (var item in levelModels)
                {
                    if (item.activeInHierarchy)
                    {
                        foreach (var t in item.GetComponentsInChildren<Transform>())
                        {
                            t.gameObject.layer = 9;
                        }
                    }
                }
                BulletLaunch temp;
                if (TryGetComponent(out temp))
                {
                    foreach (var item in temp.paos)
                    {
                        item.transform.GetChild(0).gameObject.layer = 9;
                    }
                }
            }  
            //MeshRenderer[] temp = GetComponentsInChildren<MeshRenderer>();
            //foreach (MeshRenderer m in temp)
            //{
            //    foreach (Material item in m.materials)
            //    {
            //        item.EnableKeyword("_EMISSION");
            //        item.SetColor("_EmissionColor", Color.HSVToRGB(0.1736111f, 1f, 0.4433962f));
            //    }
            //}
        }
    }

    /// <summary>
    /// 关闭自发光效果
    /// </summary>
    public void LightOff()
    {
        if (!isNpc)
        {
            foreach (var item in levelModels)
            {
                if (item.activeInHierarchy)
                {
                    foreach (var t in item.GetComponentsInChildren<Transform>())
                    {
                        if (t != null)
                            t.gameObject.layer = 0;
                    }
                }
            }
            BulletLaunch temp;
            if (TryGetComponent(out temp))
            {
                foreach (var item in temp.paos)
                {
                    item.transform.GetChild(0).gameObject.layer = 0;
                }
            }
            //MeshRenderer[] temp = GetComponentsInChildren<MeshRenderer>();
            //foreach (MeshRenderer m in temp)
            //{
            //    foreach (Material item in m.materials)
            //    {
            //        item.DisableKeyword("_EMISSION");
            //    }
            //}
        }
        else if (npcScript.isCanSee && !npcScript.isLock)
        {
            foreach (var item in levelModels)
            {
                if (item.activeInHierarchy)
                {
                    foreach (var t in item.GetComponentsInChildren<Transform>())
                    {
                        if (t != null)
                            t.gameObject.layer = 0;
                    }
                }
            }
            BulletLaunch temp;
            if (TryGetComponent(out temp))
            {
                foreach (var item in temp.paos)
                {
                    item.transform.GetChild(0).gameObject.layer = 0;
                }
            }
            //MeshRenderer[] temp = GetComponentsInChildren<MeshRenderer>();
            //foreach (MeshRenderer m in temp)
            //{
            //    foreach (Material item in m.materials)
            //    {
            //        item.DisableKeyword("_EMISSION");
            //    }
            //}
        }
    }

    /// <summary>
    /// 隐藏配置交易按钮
    /// </summary>
    /// <param name="active"></param>
    public void HideTradeButton(bool active)
    {
        if (PlayerData.My.creatRole != PlayerData.My.playerDutyID)
            return;
        if (tradeButton == null)
            tradeButton = GetComponentInChildren<RoleTradeButton>().transform.parent.gameObject;
        tradeButton.SetActive(active);
    }


    private void OnDestroy()
    {

    }
}