using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextOnChange : MonoBehaviour
{
    private string currentText = "";
    private MissionSign _missionSign;

    //private Color originColor;
    // Start is called before the first frame update
    void Start()
    {
        currentText = GetComponent<Text>().text;
        _missionSign = transform.parent.parent.GetComponent<MissionSign>();
        //originColor = GetComponent<Text>().color;
    }

    private bool isSetStyle = false;
    //private bool isOnScale = false;
    public void ShowText(string newText, string content)
    {
        if (!isSetStyle&&gameObject.activeInHierarchy)
        {
            if (content.Contains("质监站"))
            {
                GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                GetComponent<Text>().color = Color.green;
                /*if (_missionSign.data.isMainmission)
                {
                    transform.GetComponent<RectTransform>().DOAnchorPosX(75, 0.01f).Play().OnPause(() =>
                    {
                        transform.GetComponent<RectTransform>().DOAnchorPosX(75, 0.01f).Play();
                    });
                }
                else
                {
                    transform.GetComponent<RectTransform>().DOAnchorPosX(84, 0.01f).Play().OnPause(() =>
                    {
                        transform.GetComponent<RectTransform>().DOAnchorPosX(84, 0.01f).Play();
                    });
                }*/
            }

            isSetStyle = true;
        }
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
            
            
            if (content.Contains("质监站"))
            {
                GetComponent<Text>().text = newText;
                transform.DOScale(2f, 0.5f).OnComplete(() =>
                {
                    transform.DOScale(1, 0.5f).Play();
                }).Play();
            }
            else
            {
                GetComponent<Text>().text = newText;
            }
            
        }
    }
}
