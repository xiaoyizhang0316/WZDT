using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvValItem : MonoBehaviour
{
    public Text name_text;
    public Text val_text;
    public void Setup(string name, string val)
    {
        name_text.text = name;
        val_text.text = val;
    }
}
