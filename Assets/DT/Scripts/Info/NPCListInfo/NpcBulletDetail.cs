using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcBulletDetail : MonoBehaviour
{
    public Image bullet;
    public Text loadSpeed;
    public Text damage;

    public ProductData productData;

    public List<Image> buffs;

    public Sprite buffNull;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=> {
            gameObject.SetActive(false);
        });
    }


    public void InitUI(ProductData data, Sprite IconSprite, float damage, float loadingSpeed)
    {
        this.productData = data;
        bullet.sprite = IconSprite;
        this.damage.text = damage.ToString();
        this.loadSpeed.text = loadingSpeed.ToString("F2");
        

        for (int i = 0; i < buffs.Count; i++)
        {
            buffs[i].gameObject.SetActive(false);
            buffs[i].sprite = buffNull;
            buffs[i].GetComponent<BuffText>().buff = null;
        }
        for (int i = 0; i < 4; i++)
        {

            buffs[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        for (int i = 0; i < data.buffList.Count; i++)
        {
            if (i < 4)
            {
                buffs[i].sprite = Resources.Load<Sprite>("Sprite/Buff/" + data.buffList[i]);
                buffs[i].GetComponent<BuffText>().buff = GameDataMgr.My.GetBuffDataByID(data.buffList[i]);
                buffs[i].gameObject.SetActive(true);
            }
        }
        for (int i = data.buffList.Count; i < data.buffMaxCount; i++)
        {
            buffs[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < data.wasteBuffList.Count; i++)
        {
            if (i + data.buffList.Count < 4)
            {
                buffs[i + data.buffList.Count].gameObject.SetActive(true);
                buffs[i + data.buffList.Count].sprite = Resources.Load<Sprite>("Sprite/Buff/" + data.wasteBuffList[i]);
                buffs[i + data.buffList.Count].GetComponent<BuffText>().buff = GameDataMgr.My.GetBuffDataByID(data.wasteBuffList[i]);
                buffs[i + data.buffList.Count].transform.GetChild(0).gameObject.SetActive(true);
            }

        }
    }
}
