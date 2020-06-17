using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCodeSample : MonoBehaviour
{
    public Text keyCode;
    public Button copy;
    string key = "";

    public void Setup(string code)
    {
        keyCode.text = code;
        key = code;
        copy.onClick.AddListener(Copy);
    }

    private void Copy()
    {
        GUIUtility.systemCopyBuffer = key;
    }
}
