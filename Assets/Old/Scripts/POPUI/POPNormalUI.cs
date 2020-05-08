using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POPNormalUI : MonoBehaviour
{
    public Button YesButton;
    public Button NoButton;
 
    public Text Title;
    public Text content;
    public void InitPOPUI(string title,string content,Action Yse,Action No)
    {
        Title.text = title;
        this.content.text = content;
        
        YesButton.onClick.AddListener(() => Yse());
        NoButton.onClick.AddListener(() => No());
        
    }
}
