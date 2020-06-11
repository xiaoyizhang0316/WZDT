using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{


    public GameObject boreStomatitis;

    public GameObject tile;

    public GameObject bullet;

    public GameObject explosions;

    public List<int> bufflist;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitBufflist(List<int >buffList)
    {
        this.bufflist = bufflist;
        tile.SetActive(false);
        explosions.SetActive(false);
    }

    public void InitBuff(GameObject effects)
    { 
        effects.SetActive(true);
      
        for (int i = 0; i < effects .transform.childCount; i++)
        {
         //   if (!GetComponent<GoodsSign>().productData.buffList.Contains(int.Parse(effects .transform.GetChild(i).name)))
        if (!bufflist.Contains(int.Parse(effects .transform.GetChild(i).name)))
            {
                    if (effects .transform.GetChild(i).name.Equals("000"))
                {
                    continue;
                }

                effects .transform.GetChild(i).gameObject.SetActive(false);
                //不包含Buff
                
            }
            else
            {
                if (effects .transform.GetChild(i).name.Equals("303"))
                {
                    effects .transform.Find("000").gameObject.SetActive(false);
                }
                if (effects .transform.GetChild(i).name.Equals("304"))
                {
                    effects .transform.Find("000").gameObject.SetActive(false);
                }
                effects .transform.GetChild(i).gameObject.SetActive(true);
            }
        } 
    }
}
