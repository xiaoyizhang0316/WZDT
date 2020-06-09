using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI; 

public class ProductDetalUI : MonoSingleton<ProductDetalUI>
{
    public Image Icon;

    public Transform sweetnessFlig;
    public Transform softFlig;

    public Text damage;

    public Text loadingSpeed;

    public Image Time;
    public GameObject panel;
    public Button Close;

    public ProductData data;

    public List<Image> buff;

    public Sprite buffnull;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);  
        Close.onClick.AddListener(() =>
        {
            panel.SetActive(false);  
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitUI(ProductData data,Sprite IconSprite ,float damage,float loadingSpeed )
    {
        this.data = data;
        Icon.sprite = IconSprite; 
          this.damage.text = damage.ToString();
          this.loadingSpeed.text = loadingSpeed.ToString();

          for (int i = 0; i <buff.Count; i++)
          {
              buff[i].sprite = buffnull;
              buff[i].GetComponent<ShowBuffText>().currentbuffData = null;
          }
          for (int i = 0; i <data.buffList.Count; i++)
          {
              buff[i].sprite = Resources.Load<Sprite>("Sprite/Buff/" +data.buffList[i]); 
              buff[i].GetComponent<ShowBuffText>().currentbuffData =     GameDataMgr.My.GetBuffDataByID(data.buffList[i]);
          }
    }
}
