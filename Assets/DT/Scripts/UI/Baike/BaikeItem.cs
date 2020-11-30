using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class BaikeItem : MonoBehaviour
{
    public RoleType type;

    public bool isFound;

    public Text typeName;

    public void Init(RoleType _type,bool _isFound)
    {
        isFound = _isFound;
        type = _type;
        if (isFound)
        {
            typeName.text = InitName(_type);
        }
        else
        {
            typeName.text = "未知角色";
            GetComponent<Button>().interactable = false;
        }
    }

    public string InitName(RoleType _type)
    {
        switch (_type)
        {
            case RoleType.Bank:
                return "银行";
            case RoleType.ResearchInstitute:
                return "公关公司";
            case RoleType.Youtuber:
                return "网红公司";
            case RoleType.DataCenter:
                return "大数据中心";
            case RoleType.OrderCompany:
                return "订单公司";
            case RoleType.Marketing:
                return "营销公司";
            default:
                return "未知角色";
                break;
        }
    }

    public void Show()
    {
        SoftFTE.My.InitMap(type);
        BaikePanel.My.Hide();
    }
}
