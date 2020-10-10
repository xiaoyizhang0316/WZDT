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
        for (int i = 0; i < data.buffList.Count; i++)
        {
            buffs[i].sprite = Resources.Load<Sprite>("Sprite/Buff/" + data.buffList[i]);
            buffs[i].GetComponent<BuffText>().buff = GameDataMgr.My.GetBuffDataByID(data.buffList[i]);
            buffs[i].gameObject.SetActive(true);
        }

        for (int i = data.buffList.Count; i < data.buffMaxCount; i++)
        {
            buffs[i].gameObject.SetActive(true);
        }
    }
}
