using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 
    [Serializable]
    public class BuffData
    {
        /// <summary>
        /// Buff ID
        /// </summary>
        public int BuffID;

        /// <summary>
        /// Buff名称
        /// </summary>
        public string BuffName;

        /// <summary>
        /// BUFF描述
        /// </summary>
        public string BuffDesc;

        /// <summary>
        /// Buff添加时
        /// </summary>
        public List<string> OnBuffAdd;

        /// <summary>
        /// Buff移除时
        /// </summary>
        public List<string> OnBuffRemove;

        /// <summary>
        /// 交易发生时
        /// </summary>
        public List<string> OnTradeConduct;

        /// <summary>
        /// 消费者进店时
        /// </summary>
        public List<string> OnConsumerInShop;

        /// <summary>
        /// 消费者购物时
        /// </summary>
        public List<string> OnConsumerBuyProduct;

        /// <summary>
        /// 消费者满意度结算时
        /// </summary>
        public List<string> OnConsumerSatisfy;

        /// <summary>
        /// 濒临破产时
        /// </summary>
        public List<string> OnBeforeDead;

        /// <summary>
        /// 生产完成时
        /// </summary>
        public List<string> OnProductionComplete;

        /// <summary>
        /// 周期性时 
        /// </summary>
        public List<string> OnTick;

        /// <summary>
        /// 持续时间
        /// </summary>
        public int duration;

        /// <summary>
        /// 生效间隔
        /// </summary>
        public int interval;

        /// <summary>
        /// 时间点其他骚操作
        /// </summary>
        public Dictionary<int, List<string>> otherFunctions;
    } 

