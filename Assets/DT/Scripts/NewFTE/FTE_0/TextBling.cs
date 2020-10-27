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
        StartTextBling();
        StartCoroutine(TweenStop());
    }

    public void StartBling(int values)
    {
        startColor = GetComponent<Text>().color;
        if (values > value)
        {
            endColor = Color.red;
        }
        else
        {
            endColor = Color.green;
        }
        value = values;
        //GetComponent<Text>().text = value.ToString();
        StartTextBling();
        StartCoroutine(Stop());
    }

    void StartTextBling()
    {
        tween = GetComponent<Text>().DOColor(startColor, 0.4f).OnComplete(() => {
            GetComponent<Text>().DOColor(endColor, 0.4f).OnComplete(() => StartTextBling());
        });
    }

    IEnumerator TweenStop()
    {
        yield return new WaitForSeconds(2);
        tween.Kill();
        GetComponent<Text>().color = endColor;
        startColor = endColor;
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(2);
        tween.Kill();
        GetComponent<Text>().color = startColor;
        //startColor = endColor;
    }
}
