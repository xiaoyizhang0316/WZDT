using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MissionSign : MonoBehaviour
{

    public Text contentText;

    public Text currentNum;

    public Text maxNum;

    public MissionData data;

    public Image sign;

    public GameObject success;
    public GameObject numberBg;
    private Color signColor;

    private bool isover;

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

                effectOver();
            }else
            {
                isover = false; 
                success.transform.localScale = Vector3.zero;
                sign.color = signColor;
            }
        }
    }

    public void effectOver()
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
    public void OnDestroy()
    {
        MissionManager.My.signs.Remove(this);
        MissionManager.My.ChangeTital("", signTitle);
    }

    public void ResetSuccess()
    {
        success.transform.DOScale(0, 0.02f).Play();
        isover = false;
    }
}
