using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_Limit : MonoBehaviour
{
    public bool needLimit = false;
    public int limitSeedCount = 0;
    public int limitPeasantCount = 0;
    public int limitMerchantCount = 0;
    public int limitDealerCount = 0;
    public GameObject seedButton;
    public GameObject seedLock;
    public GameObject peasantButton;
    public GameObject peasantLock;
    public GameObject merchantButton;
    public GameObject merchantLock;
    public GameObject dealerButton;
    public GameObject dealerLock;

    private void Start()
    {
        needLimit = false;
    }

    private void Update()
    {
        if (needLimit)
        {
            if (PlayerData.My.seedCount >= limitSeedCount && limitSeedCount!=0)
            {
                seedButton.GetComponent<CreatRole_Button>().enabled = false;
                seedLock.SetActive(true);
            }
            else
            {
                seedButton.GetComponent<CreatRole_Button>().enabled = true;
                seedLock.SetActive(false);
            }
            
            if (PlayerData.My.peasantCount >= limitPeasantCount && limitPeasantCount!=0)
            {
                peasantButton.GetComponent<CreatRole_Button>().enabled = false;
                peasantLock.SetActive(true);
            }
            else
            {
                peasantButton.GetComponent<CreatRole_Button>().enabled = true;
                peasantLock.SetActive(false);
            }
            
            if (PlayerData.My.merchantCount >= limitMerchantCount && limitMerchantCount!=0)
            {
                merchantButton.GetComponent<CreatRole_Button>().enabled = false;
                merchantLock.SetActive(true);
            }
            else
            {
                merchantButton.GetComponent<CreatRole_Button>().enabled = true;
                merchantLock.SetActive(false);
            }
            
            if (PlayerData.My.dealerCount >= limitDealerCount && limitDealerCount !=0)
            {
                dealerButton.GetComponent<CreatRole_Button>().enabled = false;
                dealerLock.SetActive(true);
            }
            else
            {
                dealerButton.GetComponent<CreatRole_Button>().enabled = true;
                dealerLock.SetActive(false);
            }
        }
    }
}
