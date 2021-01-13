using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TextOnChange : MonoBehaviour
{
    private string currentText = "";

    //private Color originColor;
    // Start is called before the first frame update
    void Start()
    {
        currentText = GetComponent<Text>().text;
        //originColor = GetComponent<Text>().color;
    }

    //private bool isOnScale = false;
    public void ShowText(string newText, string content)
    {
        if (!newText.Equals(currentText)&& gameObject.activeInHierarchy)
        {
            /*if (isOnScale)
            {
                DOTween.Kill("TextScale");
                transform.DOScale(1, 0.02f).Play();
                GetComponent<Text>().color = originColor;
            }
            isOnScale = true;*/
            currentText = newText;
            GetComponent<Text>().text = newText;
            /*if (int.Parse(newText) > 0&& int.Parse(newText)>int.Parse(currentText.Equals("")?"0": currentText))
            {
                GetComponent<Text>().color = Color.green;
            }
            else
            {
                GetComponent<Text>().color = Color.red;
            }*/
            if (content.Contains("质监站"))
            {
                MissionManager.My.ShowTipText("产品输送中....", Color.green);
            }

            if (int.Parse(newText) == 0)
            {
                MissionManager.My.HideTip();
            }
            transform.DOScale(1.5f, 0.5f).SetId("TextScale").OnComplete(() =>
            {
                transform.DOScale(1, 0.5f).Play();
            }).Play();
        }
    }
}
