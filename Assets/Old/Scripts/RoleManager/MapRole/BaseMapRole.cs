using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public List<TradeSign> startTradeList = new List<TradeSign>();

    public List<TradeSign> endTradeList = new List<TradeSign>();

    public List<int> tasteBuffList = new List<int>();

    public int totalUpgradeCost;

    public RoleSprite roleSprite;

    public GameObject emptyGearSprite;

    public GameObject stopWorkSprite;

    public bool isSell = false;

    public GameObject ringEffect;

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
        encourageLevel = startEncourageLevel;
        if (!isNpc)
        {
            InitAttribute();
        }
        if (baseRoleData.inMap)
        {
            if (!PlayerData.My.RoleData.Contains(GetComponent<BaseMapRole>().baseRoleData))
                PlayerData.My.RoleData.Add(GetComponent<BaseMapRole>().baseRoleData);
            if (!PlayerData.My.MapRole.Contains(GetComponent<BaseMapRole>()))
            {
                PlayerData.My.MapRole.Add(GetComponent<BaseMapRole>());
                /*if (!isNpc)
                {
                    PlayerData.My.RoleCountStatic(GetComponent<BaseMapRole>(), 1);
                }*/
            }
        }
        tradePoint.GetComponent<MeshRenderer>().enabled = false;
        CheckTalentBuff();
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
        result += baseRoleData.gearEncourageAdd;
        if (CheckAllTradeBest())
        {
            result++;
        }
        if (isInit)
            baseRoleData.tradeCost -= encourageLevel * 5;
        //for (int i = 0; i < tradeList.Count; i++)
        //{
        //    if (tradeList[i].tradeData.startRole.Equals(baseRoleData.ID.ToString()))
        //    {
        //        result += tradeList[i].tradeData.dividePercent;
        //    }
        //    else
        //    {
        //        result += 4 - tradeList[i].tradeData.dividePercent;
        //    }
            
        //}
        for (int i = 0; i < startTradeList.Count; i++)
        {
            result += startTradeList[i].tradeData.dividePercent;
            PlayerData.My.GetMapRoleById(double.Parse(startTradeList[i].tradeData.targetRole)).ResetAllBuff();
        }
        for (int i = 0; i < endTradeList.Count; i++)
        {
            result += 0 - endTradeList[i].tradeData.dividePercent;
            PlayerData.My.GetMapRoleById(double.Parse(endTradeList[i].tradeData.targetRole)).ResetAllBuff();
        }
        result = Mathf.Min(10, result);
        result = Mathf.Max(result, -5);
        encourageLevel = result;
        baseRoleData.tradeCost += encourageLevel * 5;
    }

    public bool CheckAllTradeBest()
    {
        if (startTradeList.Count == 0 && endTradeList.Count == 0)
        {
            return false;
        }
        for (int i = 0; i < startTradeList.Count; i++)
        {
            if (!startTradeList[i].isTradeSettingBest())
            {
                return false;
            }
        }
        for (int i = 0; i < endTradeList.Count; i++)
        {
            if (!endTradeList[i].isTradeSettingBest())
            {
                return false;
            }
        }
        return true;
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

    public void ReaddAllBuff()
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            buffList[i].ReaddRoleBuff();
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

    Tweener costTwe;
    List<string> fteList = new List<string>() {"FTE_0.5","FTE_1.5","FTE_2.5" };

    #region 钱，科技值相关
    /// <summary>
    /// 固定消耗成本
    /// </summary>
    public void MonthlyCost()
    {
        if (SceneManager.GetActiveScene().name.Equals("FTE_1"))
        {
            return;
        }
        float time = 20f;
        int costNum = baseRoleData.cost;
        if (fteList.Contains(SceneManager.GetActiveScene().name))
        {
            time = 1f;
            costNum = costNum / 20;
        }
        transform.DORotate(transform.eulerAngles, time).OnComplete(() =>
        {
            StageGoal.My.CostPlayerGold(costNum);
            StageGoal.My.Expend(costNum, ExpendType.ProductCosts, this);
            MonthlyCost();
        });
    }

    private float tempTechAdd = 0f;

    /// <summary>
    /// 固定获得科技点数
    /// </summary>
    public void AddTechPoint()
    {
        float time = 20f;
        float techNum = baseRoleData.techAdd / 3f * 2f;
        if (fteList.Contains(SceneManager.GetActiveScene().name))
        {
            time = 1f;
            techNum = techNum / 20f;
            tempTechAdd += techNum;
            if (tempTechAdd >= 1f)
            {
                techNum += tempTechAdd;
                tempTechAdd = 0f;
            }
        }
        transform.DORotate(transform.eulerAngles, time).OnComplete(() =>
        {
            StageGoal.My.GetTechPoint((int)techNum);
            StageGoal.My.IncomeTp((int)techNum, IncomeTpType.Npc);
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
   public  Action<ProductData>  OnMoved = null;
    public virtual void AddPruductToWareHouse(ProductData data)
    {
        if (warehouse.Count >= baseRoleData.bulletCapacity)
        {
            //DataUploadManager.My.AddData(DataEnum.浪费的瓜);
            return;
        }
        else
        {
            warehouse.Add(data);
            if (OnMoved != null)
            {
                OnMoved(data);
            }
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
        if (PlayerData.My.guanJianZiYuanNengLi[5])
        {
            int totalGold = CountWarehouseIncome();
            StageGoal.My.GetPlayerGold(totalGold);
            StageGoal.My.Income(totalGold,IncomeType.Other,null,"低价处理");
        }
        warehouse.Clear();
    }

    public int CountWarehouseIncome()
    {
        float result = 0;
        for (int i = 0; i < warehouse.Count; i++)
        {
            switch (warehouse[i].bulletType)
            {
                case BulletType.Seed:
                    {
                        result += warehouse[i].damage * 0.1f;
                        break;
                    }
                case BulletType.NormalPP:
                    {
                        result += 10f + warehouse[i].damage * 0.1f;
                        break;
                    }
                case BulletType.Bomb:
                    {
                        result += 70f + warehouse[i].damage * 0.1f;
                        break;
                    }
                case BulletType.Lightning:
                    {
                        result += 115f + warehouse[i].damage * 0.1f;
                        break;
                    }
                case BulletType.summon:
                    {
                        result += 200f + warehouse[i].damage * 0.1f;
                        break;
                    }
                default:
                    break;
            }
        }
        return (int)result;
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
        for (int i = 0; i < tasteBuffList.Count; i++)
        {
            bufflist.Add(tasteBuffList[i]);
        }
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

    public void CheckTalentBuff()
    {
        if (!isNpc)
        {
            if (PlayerData.My.guanJianZiYuanNengLi[1])
            {
                var buff = GameDataMgr.My.GetBuffDataByID(10022);
                BaseBuff baseb = new BaseBuff();
                baseb.Init(buff);
                baseb.SetRoleBuff(null, this, this);
            }
            if (PlayerData.My.guanJianZiYuanNengLi[2])
            {
                var buff = GameDataMgr.My.GetBuffDataByID(10023);
                BaseBuff baseb = new BaseBuff();
                baseb.Init(buff);
                baseb.SetRoleBuff(null, this, this);
            }
        }
        if (baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
        {
            if (PlayerData.My.guanJianZiYuanNengLi[5])
            {
                var buff = GameDataMgr.My.GetBuffDataByID(10026);
                BaseBuff baseb = new BaseBuff();
                baseb.Init(buff);
                baseb.SetRoleBuff(null, this, this);
            }
        }
        if (baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
        {
            if (PlayerData.My.yeWuXiTong[1])
            {
                var buff = GameDataMgr.My.GetBuffDataByID(10032);
                BaseBuff baseb = new BaseBuff();
                baseb.Init(buff);
                baseb.SetRoleBuff(null, this, this);
            }
        }
    }

    public string GetWarehouseJson()
    {
        List<ProductData> data = new List<ProductData>();
         
        for (int i = 0; i <warehouse.Count; i++)
        {
            warehouse[i].RepeatBulletCount = 0;
            bool issame =false;
            for (int j = 0; j <data.Count; j++)
            {
                if (data[j].CheckSame(warehouse[i]))
                {
                    issame = true; 
                    data[j].RepeatBulletCount++;
                    break;
          
                }
            }

            if (!issame)
            {
                data.Add(warehouse[i]);
            }

        }

        List<SendProductData> sendProductddata = new List<SendProductData>();
        for (int i = 0; i < data.Count; i++)
        { 
            sendProductddata.Add(new SendProductData(data[i]));
        }
        SendProductDataList list = new SendProductDataList();
        list.datas = sendProductddata;
        Debug.Log(JsonUtility.ToJson(list) );
        return JsonUtility.ToJson(list);
    }

    
    private void OnDestroy()
    {

    }
    List<string> sceneName = new List<string> { "FTE_1", "FTE_0-1", "FTE_0-2" };

    private float interval = 1f;

    private void Update()
    {
        interval += Time.deltaTime;
        if (interval >= 1f)
        {
            if (!sceneName.Contains(SceneManager.GetActiveScene().name))
            {
                if (!isNpc)
                {
                    if (baseRoleData.EquipList.Count == 0 && baseRoleData.peoPleList.Count == 0 &&
                        (PlayerData.My.GetAvailableWorkerNumber() > 0 || PlayerData.My.GetAvailableEquipNumber() > 0) && baseRoleData.inMap)
                    {
                        emptyGearSprite.SetActive(true);
                    }
                    else
                    {
                        emptyGearSprite.SetActive(false);
                    }
                }
                if (stopWorkSprite != null)
                {
                    if (encourageLevel <= -3 && !(isNpc && npcScript.isLock))
                    {
                        stopWorkSprite.gameObject.SetActive(true);
                    }
                    else
                    {
                        stopWorkSprite.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                if (!isNpc)
                {
                    emptyGearSprite.SetActive(false);
                }
                if (stopWorkSprite != null)
                {
                    stopWorkSprite.gameObject.SetActive(false);
                }
            }
            interval = 0f;
        }
    }
    
    public void TradeLightOn(){
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

    public void TradeLightOff()
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
    }

    private void OnMouseEnter()
    {
        TradeManager.My.ShowAllRelateTradeIcon(baseRoleData.ID.ToString());
    }

    private void OnMouseExit()
    {
        TradeManager.My.HideRelateTradeIcon();
    }
}