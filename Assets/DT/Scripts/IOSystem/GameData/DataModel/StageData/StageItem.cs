using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

 
    [Serializable]
    public class StageItem
    {
        public string sceneName;

        public string maxConsumer;

        public string startConsumer;

        public string maxBoss;

        public string startBoss;

        public string bankRate;

        public string startWorker;

        public string startEquip;

        public string consumerQualityNeed;
    }

    public class StagesData
    {
        public List<StageItem> stageSigns = new List<StageItem>();
    } 