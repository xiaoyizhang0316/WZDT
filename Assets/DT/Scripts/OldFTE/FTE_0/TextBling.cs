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
    bool bling = false;
    // Start is called before the first frame update
    void Start()
    {
        //StartBling(Color.green);
    }

    public void StartBling(Color endColor)
    {
        if (startColor == new Color())
        {
            startColor = GetComponent<Text>().color;
        }
        //Debug.Log(gameObject.name+"+"+startColor + "," + endColor);
        if (this.startColor == endColor)
        {
            return;
        }
        this.endColor = endColor;
        
        bling = true;
        StartTextBling();
        //StartCoroutine(TweenStop());
        Invoke("TweenStop", 2);
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
            endColor = Color.yellow;
        //}
        value = values;
        bling = true;
        //GetComponent<Text>().text = value.ToString();
        StartTextBling();
        //StartCoroutine(Stop());
        Invoke("Stop", 2);
    }

    void StartTextBling()
    {
        //bling = true;
        if(bling)
            tween = GetComponent<Text>().DOFade(0, 0.4f).OnComplete(() => {
                GetComponent<Text>().DOFade(1, 0.4f).OnComplete(() => StartTextBling());
            });
    }

    void TweenStop()
    {
        //yield return new WaitForSeconds(2);
        tween.Kill();
        bling = false;
        GetComponent<Text>().color = endColor;
        GetComponent<Text>().DOFade(1, 0.01f);
        startColor = endColor;
    }

    void Stop()
    {
       // yield return new WaitForSeconds(2);
        tween.Kill();
        bling = false;
        //GetComponent<Text>().color = startColor;
        GetComponent<Text>().DOFade(1, 0.01f);
        //startColor = endColor;
        //CancelInvoke("Stop");
    }
}
