using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockRoleFTE : MonoBehaviour
{

    public int seed;
    public int peasant;
    public int merchant;

    public int dealet;

    public GameObject seedobj;
    public GameObject peasantobj;
    public GameObject merchantobj;
    public GameObject dealetobj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.My.seedCount <seed)
        {
            seedobj.SetActive(false);
        }
        else
        {
            seedobj.SetActive(true);

        }
        if (PlayerData.My.peasantCount <peasant)
        {
            peasantobj.SetActive(false);
        }
        else
        {
            peasantobj.SetActive(true);

        }
        if (PlayerData.My.merchantCount <merchant)
        {
            merchantobj.SetActive(false);
        }
        else
        {
            merchantobj.SetActive(true);

        }
        if (PlayerData.My.dealerCount <dealet)
        {
            dealetobj.SetActive(false);
        }
        else
        {
            dealetobj.SetActive(true);

        }
        
    }
    
    
}
