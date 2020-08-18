using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class FloatWindow : MonoSingleton<FloatWindow>
{
    public Text showText;

    public float x = 2;

    public float y = -2;

    public void Init(Transform _transform, string str)
    {
        showText.text = str;
        Vector3 V = Input.mousePosition;
        Vector3 V2 = new Vector3(V.x - Screen.width / 2 + 150f, V.y - Screen.height / 2);
        transform.localPosition = V2;
        CheckPos();
        //print(Input.mousePosition);
        //print(Camera.main.ViewportToScreenPoint(_transform.position));
        //print(Camera.main.ViewportToWorldPoint(_transform.position));
    }

    public void CheckPos()
    {
        if (transform.localPosition.y + GetComponent<RectTransform>().sizeDelta.y / 2 >= Screen.height / 2)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, Screen.height / 2 - GetComponent<RectTransform>().sizeDelta.y / 2);
        }
        if (transform.localPosition.y + GetComponent<RectTransform>().sizeDelta.y / 2 <= -Screen.height / 2)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -Screen.height / 2 + GetComponent<RectTransform>().sizeDelta.y / 2);
        }
        if (transform.localPosition.x + GetComponent<RectTransform>().sizeDelta.x / 2 >= Screen.width / 2)
        {
            transform.localPosition = new Vector3(Screen.width / 2 - GetComponent<RectTransform>().sizeDelta.x / 2, transform.localPosition.y);
        }
        if (transform.localPosition.x + GetComponent<RectTransform>().sizeDelta.x / 2 <= -Screen.width / 2)
        {
            transform.localPosition = new Vector3(-Screen.width / 2 + GetComponent<RectTransform>().sizeDelta.x / 2, transform.localPosition.y);
        }
    }

    public void Hide()
    {
        transform.position = new Vector3(10000f,0f,0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
