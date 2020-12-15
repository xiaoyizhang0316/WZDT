using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_TypeWord : MonoBehaviour
{
    private Text txt;

    private string content;

    private int length;

    public bool isTyping = false;

    private void OnEnable()
    {
        if (txt == null)
        {
            txt = GetComponent<Text>();
            content = txt.text;
        }
        txt.text = "";
        isTyping = true;
        TypeWord();
    }

    private void OnDisable()
    {
        txt.text = "";
        length = 0;
    }

    private void TypeWord()
    {
        StartCoroutine(Type());
    }

    private IEnumerator Type()
    {
        while (length<=content.Length)
        {
            if (isTyping == false)
            {
                //TypeOver();
                break;
            }
            txt.text = content.Substring(0, length);
            length++;
            yield return new WaitForSeconds(0.1f);
        }

        TypeOver();
    }

    private void TypeOver()
    {
        txt.text = content;
        length = 0;
        isTyping = false;
    }

    public void StopType()
    {
        if(isTyping)
            isTyping = false;
    }
}
