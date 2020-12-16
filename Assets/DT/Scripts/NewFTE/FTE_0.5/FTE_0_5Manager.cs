using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_0_5Manager : MonoSingleton<FTE_0_5Manager>
{

    /// <summary>
    /// 阶段任务UI
    /// </summary>
    public  Text contentUI;

    /// <summary>
    /// 阶段任务数量
    /// </summary>
    public Text curerentNumber;

    public Text maxNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeContentUI(string content)
    {
        contentUI.text = content;
    }
}
