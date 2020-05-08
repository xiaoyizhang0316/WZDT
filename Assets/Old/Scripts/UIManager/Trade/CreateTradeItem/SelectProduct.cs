using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectProduct : MonoBehaviour
{
    /// <summary>
    /// 选项更改调用的函数
    /// </summary>
    /// <param name="num"></param>
    public void OnValueChange(int num)
    {
        switch (GetComponent<Dropdown>().value)
        {
            case 0:
                CreateTradeManager.My.selectProduct = GameEnum.ProductType.Seed;
                break;
            case 1:
                CreateTradeManager.My.selectProduct = GameEnum.ProductType.Melon;
                break;
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        Dropdown temp = GetComponent<Dropdown>();
        switch(CreateTradeManager.My.selectProduct)
        {
            case GameEnum.ProductType.Seed:
                temp.value = 0;
                temp.captionText.text = temp.options[0].text;
                break;
            case GameEnum.ProductType.Melon:
                temp.value = 1;
                temp.captionText.text = temp.options[1].text;
                break;
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
