using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_Manager : MonoSingleton<FTE_2_5_Manager>
{
    /*public int sweetKillNum = 0;
    public int crispKillNum = 0;
    public int softKillNum = 0;*/
    public int packageKillNum = 0;
    public int saleKillNum = 0;
    public int nolikeKillNum = 0;
    

    public bool isClearGoods = false;

    public bool needSkip = false;

    public void CheckTasteKill(int index)
    {
        switch (index)
        {
            case 0:
                packageKillNum += 1;
                break;
            case 1:
                saleKillNum += 1;
                break;
            case 2:
                nolikeKillNum += 1;
                break;
        }
    }
}
