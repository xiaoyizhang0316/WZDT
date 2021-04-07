using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinEquipSign : MonoBehaviour
{
    public Image Image_shape;

    public Image Image_buff;

    /// <summary>
    /// 效果值
    /// </summary>
    public Text effect;

    /// <summary>
    /// 效率值
    /// </summary>
    public Text efficiency;

    /// <summary>
    /// 范围值
    /// </summary>
    public Text range;

    /// <summary>
    /// 风险抗力
    /// </summary>
    public Text riskResistance;

    /// <summary>
    /// 交易成本
    /// </summary>
    public Text tradeCost;

    /// <summary>
    /// 成本
    /// </summary>
    public Text cost;
  
    public Image shapeImageBG;

 
    public int ID;


    public Sprite lv;
    public Sprite lan;

    public Sprite hui; 

    public GearData gearData; 
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="id"></param>
    /// <param name="_isEquiped"></param>
    public void Init(int id )
    {
        ID = id;
        gearData = GameDataMgr.My.GetGearData(id);
        effect.text = gearData.effect.ToString();
        efficiency.text = gearData.efficiency.ToString();
        range.text = gearData.range.ToString();
        riskResistance.text = gearData.riskResistance.ToString();
        tradeCost.text = gearData.tradeCost.ToString();
        if (StageGoal.My.currentType == GameEnum.StageType.Normal)
        {
            cost.text = (gearData.cost * 2).ToString();
        }
        else
        {
            cost.text = gearData.cost.ToString();
        }

        if (gearData.ProductOrder == 1)
        {
            shapeImageBG.sprite = hui;
        }

        if (gearData.ProductOrder == 2)
        {
            shapeImageBG.sprite = lv;
        }

        if (gearData.ProductOrder == 3)
        {
            shapeImageBG.sprite = lan;
        }

        Image_shape.sprite = Resources.Load<Sprite>(gearData.SpritePath);
        if (gearData.buffList[0] != -1)
        { 
            GetComponentInChildren<WaveBuffSign>().Init(gearData.buffList[0]);
        }
    }
}