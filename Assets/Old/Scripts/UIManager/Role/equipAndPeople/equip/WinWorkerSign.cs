using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinWorkerSign : MonoBehaviour
{
    public Image Image_shape;

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

    /// <summary>
    /// 弹药容量
    /// </summary>
    public Text bulletCapacity;
 
    public Text techAdd;
    public Image shapeImageBG;
    public Sprite lv;
    public Sprite lan;
    public Sprite hui;
 
    public int ID; 
    public WorkerData workerData;
 

   
    /// <summary>
    /// UI单个图标初始化
    /// </summary>
    /// <param name="id"></param>
    public void Init(int id )
    {
        ID = id;
      
    
        workerData = GameDataMgr.My.GetWorkerData(id);
        effect.text = workerData.effect.ToString();
        efficiency.text = workerData.efficiency.ToString();
        range.text = workerData.range.ToString();
        riskResistance.text = workerData.riskResistance.ToString();
        tradeCost.text = workerData.tradeCost.ToString();
        if (StageGoal.My.currentType == GameEnum.StageType.Normal  )
        {
            cost.text = (workerData.cost * 2).ToString();
        }
        else
        {
            cost.text = workerData.cost.ToString();
        }
        bulletCapacity.text = workerData.bulletCapacity.ToString();
        
        if (workerData.ProductOrder == 1)
        {
            shapeImageBG.sprite = hui;
        }

        if (workerData.ProductOrder == 2)
        {
            shapeImageBG.sprite = lv;
        }

        if (workerData.ProductOrder == 3)
        {
            shapeImageBG.sprite = lan;
        }

        Image_shape.sprite = Resources.Load<Sprite>(workerData.SpritePath);

        techAdd.text = workerData.techAdd.ToString();
    }


}