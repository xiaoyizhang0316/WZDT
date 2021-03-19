using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataUploadManager : IOIntensiveFramework.MonoSingleton.MonoSingleton<DataUploadManager>
{

    Dictionary<string, int> dataDic = new Dictionary<string, int>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (DataEnum d in Enum.GetValues(typeof(DataEnum)))
        {
            dataDic.Add(d.ToString(), 0);
        }
    }

    public void AddData(DataEnum activity)
    {
        dataDic[activity.ToString()]+= 1;
    }

    public void GetStatisticData(DataUpload data)
    {
        int count = 0;
        List<int> keys = TradeManager.My.tradeList.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            if (TradeManager.My.tradeList[keys[i]].isTradeSettingBest())
            {
                count++;
            }
        }
        int usedEquip = 0;
        for (int i = 0; i < PlayerData.My.playerGears.Count; i++)
        {
            if(PlayerData.My.playerGears[i].isEquiped)
            {
                usedEquip++;
            }
        }
        int usedWorker = 0;
        for (int i = 0; i < PlayerData.My.playerWorkers.Count; i++)
        {
            if (PlayerData.My.playerWorkers[i].isEquiped)
            {
                usedWorker++;
            }
        }
        data.trad_optimize = count / (float)TradeManager.My.tradeList.Count;
        data.rightTrade = count;
        data.totalTrade = TradeManager.My.tradeList.Count;
        data.totalEquipNum = PlayerData.My.playerGears.Count;
        data.totalWorkerNum = PlayerData.My.playerWorkers.Count;
        data.usedEquipNum = usedEquip;
        data.usedWorkerNum = usedWorker;
        data.time_PauseTime = StageGoal.My.totalPauseTime;
        data.percentageTime = StageGoal.My.totalMinusGoldTime;
    }

    public DataUpload GetDataUpload()
    {
        DataUpload data = new DataUpload();
        data.trad_tradesFiveSeconds = dataDic[DataEnum.交易_五秒内查看交易的次数.ToString()];
        data.trad_build = dataDic[DataEnum.交易_建交易.ToString()];
        data.trad_delete = dataDic[DataEnum.交易_删交易.ToString()];
        data.trad_DealNpcOrigination = dataDic[DataEnum.交易_发起外部交易.ToString()];
        data.trad_DealSelfOrigination = dataDic[DataEnum.交易_发起的内部交易.ToString()];
        //data.trad_optimize = dataDic[DataEnum.交易_优化率.ToString()];
        data.trad_change = dataDic[DataEnum.交易_改交易.ToString()];
        data.use_dlj = dataDic[DataEnum.使用多棱镜.ToString()];
        data.use_gjj = dataDic[DataEnum.使用广角镜.ToString()];
        data.use_tsj = dataDic[DataEnum.使用透视镜.ToString()];
        //data.time_PauseTime = dataDic[DataEnum.时间_暂停时长.ToString()];
        data.time_PauseTimes = dataDic[DataEnum.时间_暂停次数.ToString()];
        data.wastefulbullet = dataDic[DataEnum.浪费的瓜.ToString()];
        data.consumer_tasteskill = dataDic[DataEnum.消费者_口味击杀.ToString()]+"_" +StageGoal.My.killNumber;
        data.consumer_ClicksProgressBar = dataDic[DataEnum.消费者_点击进度条.ToString()];
        data.checkbullet = dataDic[DataEnum.看子弹属性.ToString()];
        data.percentageTime = dataDic[DataEnum.统计付钱时间占比.ToString()];
        data.equip_add = dataDic[DataEnum.装备_增加.ToString()];
        data.equip_useDetail = PlayerData.My.GetEquipUseDetail();
        data.equip_workerDetail = PlayerData.My.GetWorkerUseDetail();
        data.role_deleteRole = dataDic[DataEnum.角色_删除角色.ToString()];
        data.role_update = dataDic[DataEnum.角色_升级.ToString()];
        data.role_updateSeed = dataDic[DataEnum.角色_升级种子商.ToString()];
        data.role_updatePeasant = dataDic[DataEnum.角色_升级农民.ToString()];
        data.role_updateMerchant = dataDic[DataEnum.角色_升级贸易商.ToString()];
        data.role_updateDealer = dataDic[DataEnum.角色_升级零售商.ToString()];
        data.role_ReleaseRolePercentage = dataDic[DataEnum.角色_放置种子商.ToString()] + ":" + dataDic[DataEnum.角色_放置农民.ToString()]
            + ":" + dataDic[DataEnum.角色_放置贸易商.ToString()] + ":" + dataDic[DataEnum.角色_放置零售商.ToString()]
            ;
        data.role_roleNum = dataDic[DataEnum.角色_放置种子商.ToString()] + dataDic[DataEnum.角色_放置农民.ToString()]
                           + dataDic[DataEnum.角色_放置贸易商.ToString()] + dataDic[DataEnum.角色_放置零售商.ToString()];
        data.role_checkUnlockRole = dataDic[DataEnum.角色_查看未解锁.ToString()];
        data.role_checkselfData = dataDic[DataEnum.角色_查看自己属性.ToString()];
        data.role_checkSeedData = dataDic[DataEnum.角色_查看自己种子商属性.ToString()];
        data.role_checkPeasantData = dataDic[DataEnum.角色_查看自己农民属性.ToString()];
        data.role_checkMerchantData = dataDic[DataEnum.角色_查看自己贸易商属性.ToString()];
        data.role_checkDealerData = dataDic[DataEnum.角色_查看自己零售商属性.ToString()];
        data.role_clearWarehouse = dataDic[DataEnum.角色_清仓.ToString()];
        data.deficitNumber = dataDic[DataEnum.赤字次数.ToString()];
        data.role_checkNpcData = dataDic[DataEnum.角色_查看NPC属性.ToString()];
        data.role_checkBuff = dataDic[DataEnum.角色_查看角色Buff.ToString()];
        GetStatisticData(data);
        return data;
    }

    public void showdic()
    {
        foreach (var VARIABLE in dataDic)
        {
            //Debug.Log(VARIABLE.Key + "Key" + VARIABLE.Value + "Value");
        }
    }

    public void OnGUI()
    {
        //if (GUILayout.Button("show"))
        //{
        //    showdic();
        //}
    }
}
