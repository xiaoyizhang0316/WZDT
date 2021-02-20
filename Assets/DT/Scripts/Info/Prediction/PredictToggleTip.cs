using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictToggleTip : MonoBehaviour
{
    public void ShowTip()
    {
        FloatWindow.My.Init("花费10点MAGA值获取更多的预测信息！");
    }
    
    public void HideTip()
    {
        FloatWindow.My.Hide();
    }
}
