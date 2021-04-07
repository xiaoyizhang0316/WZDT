using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T8Manager : MonoSingleton<T8Manager>
{
    public GameObject npcmaoyi;
    public GameObject npcnong;

    public GameObject consumerPort1;
    public GameObject consumerPort2;

    public GameObject endPointPort1;
    public GameObject endPointPort2;
    public int packageKillNum = 0;
    public int saleKillNum = 0;
    public int nolikeKillNum = 0;
    public GameObject ggj;
    public GameObject dlg;
    public GameObject tsg;
    // Start is called before the first frame update
    void Start()
    {
        ggj.SetActive(true);
        dlg.SetActive(false);
        tsg.SetActive(false);
        npcmaoyi.SetActive(false);
        consumerPort1.SetActive(false);
        consumerPort2.SetActive(false);
        endPointPort1.SetActive(false);
        endPointPort2.SetActive(false);
        PlayerData.My.playerGears.Clear();
        PlayerData.My.playerWorkers.Clear();
    }
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
