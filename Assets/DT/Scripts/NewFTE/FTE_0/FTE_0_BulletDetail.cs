using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_0_BulletDetail : MonoSingleton<FTE_0_BulletDetail>
{
    public Text bulletFill_Text;
    public Text bulletAtck_Text;

    //public void SetBullet(int seedEffect, int peasantEffect)
    //{
    //    bulletAtck_Text.text = (seedEffect * 10).ToString();
    //    bulletFill_Text.text = (1.1f * (1f - peasantEffect / 150f)).ToString("F2");
    //}

    public void SetBullet(int seedEffect)
    {
        bulletAtck_Text.text = (seedEffect * 10).ToString();
    }

    public void SetBullet1(int peasantEffect)
    {
        bulletFill_Text.text = (1.1f * (1f - peasantEffect / 150f)).ToString("F2");
    }
}
