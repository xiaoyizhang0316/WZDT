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
            typeName.text = _type.ToString();
        }
        else
        {
            typeName.text = "???";
            GetComponent<Button>().interactable = false;
        }
    }

    public void Show()
    {
        SoftFTE.My.Init(type);
    }
}
