using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitDanzhongPrb : MonoBehaviour
{

    public Image image;

    public Text damage;

    public Text loadTime;

    public List<Image> buffList;

 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init( Sprite  sprite,string damage,string loadtime,ProductData data)
    {
        image.sprite = sprite;
        this.damage.text =damage.ToString();
        this.loadTime.text = loadtime.ToString();
        for (int i = 0; i <buffList.Count; i++)
        {
            buffList[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < data.buffMaxCount; i++)
        {
                 
            buffList[i].gameObject.SetActive(true);
        }
      

       
        for (int i = 0; i <buffList.Count; i++)
        {
            buffList[i].sprite =RoleUpdateInfo.My. buffNull;
            buffList[i].GetComponent<ShowBuffText>().currentbuffData = null;
        }
        for (int i = 0; i <data.buffList.Count; i++)
        {
            if (i < 4)
            {
                buffList[i].gameObject.SetActive(true);
                buffList[i].sprite = Resources.Load<Sprite>("Sprite/Buff/" + data.buffList[i]);
                buffList[i].GetComponent<ShowBuffText>().currentbuffData = GameDataMgr.My.GetBuffDataByID(data.buffList[i]);
            }
        }

      //  for (int i = 0; i < data.wasteBuffList.Count; i++)
      //  {
      //      if (i +  data.buffList.Count < 4)
      //      {
      //          buffList[i +  data.buffList.Count].gameObject.SetActive(true);
      //          buffList[i+ data.buffList.Count].sprite =  Resources.Load<Sprite>("Sprite/Buff/" +data.wasteBuffList[i]); 
      //          buffList[i +  data.buffList.Count].GetComponent<ShowBuffText>().currentbuffData =   GameDataMgr.My.GetBuffDataByID(data.wasteBuffList[i]);
      //          buffList[i +  data.buffList.Count].transform.GetChild(0).gameObject.SetActive(true);
      //      }
//
      //  }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
