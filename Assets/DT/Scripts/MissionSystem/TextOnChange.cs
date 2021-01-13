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
    public void ShowText(string newText)
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
            transform.DOScale(1.5f, 0.5f).SetId("TextScale").OnComplete(() =>
            {
                transform.DOScale(1, 0.5f).Play();
            }).Play();
        }
    }
}
