using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MissionSign : MonoBehaviour
{

    /// <summary>
    /// 内容文本
    /// </summary>
    public Text contentText;

    /// <summary>
    /// 当前完成数量
    /// </summary>
    public Text currentNum;

    /// <summary>
    ///  目标完成数量
    /// </summary>
    public Text maxNum;

    /// <summary>
    ///  当前引导数据
    /// </summary>
    public MissionData data;

    /// <summary>
    /// 背景
    /// </summary>
    public Image sign;

    /// <summary>
    /// 成功标记
    /// </summary>
    public GameObject success;
    
    /// <summary>
    /// 数字背景
    /// </summary>
    public GameObject numberBg;
    
    /// <summary>
    /// 标记的颜色
    /// </summary>
    private Color signColor;

    /// <summary>
    /// 当前是否完成
    /// </summary>
    private bool isover;

    /// <summary>
    /// 当文字被改变的时候
    /// </summary>
    private TextOnChange TextOnChange;
    // Start is called before the first frame update
    void Start()
    {
        MissionManager.My.signs.Add(this);
        signColor = sign.color;
        TextOnChange = currentNum.GetComponent<TextOnChange>();
    }

    // Update is called once per frame
    void Update()
    {
        //currentNum.text = this.data.currentNum.ToString();
        TextOnChange.ShowText(data.currentNum.ToString(), contentText.text);
        if (data.isFail)
        {
            sign.color = Color.red;
        }
        else
        {
            if (data.isFinish )
            {
                sign.color = Color.green;

                EffectOver();
            }else
            {
                isover = false; 
                success.transform.localScale = Vector3.zero;
                sign.color = signColor;
            }
        }
    }

    /// <summary>
    /// 当此任务完成时，播放特效
    /// </summary>
    public void EffectOver()
    {
        if (!isover)
        {
            isover = true;
            success.transform.DOScale(2, 0.4f).OnComplete(() =>
            {
                success.transform.DOShakePosition(0.9f ,8).OnComplete(() =>
                {
                    success.transform.DOScale(1.5f, 0.2f).Play().SetEase(Ease.OutCirc);
                }).Play();
        
            }).Play();
        } 
    }

    /// <summary>
    /// 初始化任务
    /// </summary>
    /// <param name="data"></param>
    /// <param name="title"></param>
    public void Init(MissionData data, string title)
    {
        this.data = data;
        signTitle = title;
        contentText.text = this.data.content;
        if (data.maxNum > 0)
        {
            if (data.maxNum > 1000)
            {
                currentNum.resizeTextForBestFit = false;
                maxNum.resizeTextForBestFit = false;
                currentNum.fontSize = 25;
                maxNum.fontSize = 25;
            }
            currentNum.text = this.data.currentNum.ToString();
            maxNum.text = "/"+this.data.maxNum.ToString();
        }
        else
        {
            currentNum.gameObject.SetActive(false);
            maxNum.gameObject.SetActive(false);
            numberBg.SetActive(false);
        }

    }

    private string signTitle= "";
    /// <summary>
    /// 
    /// </summary>
    public void OnDestroy()
    {
        MissionManager.My.signs.Remove(this);
        MissionManager.My.ChangeTital("", signTitle);
    }

    /// <summary>
    /// 重置成功标记
    /// </summary>
    public void ResetSuccess()
    {
        success.transform.DOScale(0, 0.02f).Play();
        isover = false;
    }
}
