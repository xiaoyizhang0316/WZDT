using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class SoftFTE_Common : SoftFTE_Base
{
    public Text descText;

    public Image roleImg;

    public override void Init(RoleType type)
    {
        SoftFTEItem item = SoftFTEData.My.GetSoftItemByType(type);
        if (item != null)
        {
            descText.text = item.roleDesc;
        }
        roleImg.sprite = Resources.Load<Sprite>("Sprite/RoleLogo/" + type.ToString() + "1");
        base.Init(type);
    }

    public override IEnumerator Check()
    {
        yield return new WaitForSeconds(entryTime);
        nextButton.interactable = true;
    }
}
