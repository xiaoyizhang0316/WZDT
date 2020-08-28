using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidePanel : GuidePanelBase
{
    // Start is called before the first frame update
    void Start()
    {
        //actions = new List<Action>() { () => {
        //    Debug.LogError("first");
        //}, () => {
        //    Debug.LogError("second");
        //} };
        //waitTime = new List<float>() {2f, 0f };
        InitGuidePanel();
    }

    /*protected override void InitGuidePanel()
    {
        base.InitGuidePanel();
    }*/
}
