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
    private Color signColor;

    private bool isover;
    // Start is called before the first frame update
    void Start()
    {
        MissionManager.My.signs.Add(this);
        signColor = sign.color;
    }

    // Update is called once per frame
    void Update()
    {
        //currentNum.text = this.data.currentNum.ToString();
        currentNum.GetComponent<TextOnChange>().ShowText(data.currentNum.ToString());
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
                    success.transform.DOScale(1, 0.2f).Play().SetEase(Ease.OutCirc);
                }).Play();
        
            }).Play();
        } 
    }

    public void Init(MissionData data)
    {
        this.data = data;
        contentText.text = this.data.content;
        if (data.maxNum > 0)
        {
            currentNum.text = this.data.currentNum.ToString();
            maxNum.text = "/"+this.data.maxNum.ToString();
        }
        else
        {
            currentNum.gameObject.SetActive(false);
            maxNum.gameObject.SetActive(false);
        }

    }

    public void OnDestroy()
    {
        MissionManager.My.signs.Remove(this);
    }
}
