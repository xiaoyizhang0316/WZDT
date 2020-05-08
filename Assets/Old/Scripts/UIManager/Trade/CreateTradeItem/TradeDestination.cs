using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class TradeDestination : MonoBehaviour
{

    /// <summary>
    /// 目的地变化时调用函数
    /// </summary>
    /// <param name="num"></param>
    public void SelectDestination(int num)
    {
        if (num == 0)
        {
            CreateTradeManager.My.selectTradeDestination = TradeDestinationType.Warehouse;
        }
        else if (num == 1)
        {
            CreateTradeManager.My.selectTradeDestination = TradeDestinationType.Import;
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        Toggle[] temp = GetComponentsInChildren<Toggle>();
        if (CreateTradeManager.My.selectTradeDestination == TradeDestinationType.Warehouse)
        {
            temp[0].isOn = true;
            temp[1].isOn = false;
        }
        else if (CreateTradeManager.My.selectTradeDestination == TradeDestinationType.Import)
        {
            temp[0].isOn = false;
            temp[1].isOn = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
