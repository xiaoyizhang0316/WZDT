using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TextBling : MonoBehaviour
{
    Tween tween;
    Color startColor;
    Color endColor;
    int value;
    //bool bling = false;
    // Start is called before the first frame update
    void Start()
    {
        //StartBling(Color.green);
    }

    public void StartBling(Color endColor)
    {
        if (this.endColor == endColor)
        {
            return;
        }
        this.endColor = endColor;
        if (startColor == null)
        {
            startColor = GetComponent<Text>().color;
        }
        //bling = true;
        StartTextBling();
        StartCoroutine(TweenStop());
    }

    public void StartBling(int values)
    {
        startColor = GetComponent<Text>().color;
        //if (values > value)
        //{
           // endColor = Color.red;
        //}
        //else
        //{
            endColor = Color.green;
        //}
        value = values;
        //bling = true;
        //GetComponent<Text>().text = value.ToString();
        StartTextBling();
        StartCoroutine(Stop());
    }

    void StartTextBling()
    {
        //bling = true;
        //if(bling)
            tween = GetComponent<Text>().DOColor(startColor, 0.4f).OnComplete(() => {
                GetComponent<Text>().DOColor(endColor, 0.4f).OnComplete(() => StartTextBling());
            });
    }

    IEnumerator TweenStop()
    {
        yield return new WaitForSeconds(2);
        tween.Kill();
        //bling = false;
        GetComponent<Text>().color = endColor;
        startColor = endColor;
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(2);
        tween.Kill();
        //bling = false;
        GetComponent<Text>().color = startColor;
        //startColor = endColor;
    }
}
