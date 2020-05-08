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

    public Text qulity;

    public Text band;

    public Image Time;
    public GameObject panel;
    public Button Close;
    Tweener a;
    Tweener b;
    Tweener  t;
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

    public void InitUI(Sprite IconSprite,int sw,int soft,string qulity,string band,float time)
    {
        Icon.sprite = IconSprite;
        if (a != null && a.IsPlaying())
        {
            a.Kill();
        }
        if (b != null && b.IsPlaying())
        {
            b.Kill();
        }
        if (t != null && t.IsPlaying())
        {
            t.Kill();
        }
          a =      sweetnessFlig.DOLocalMoveX(sw * 10f, 0.2f);
          b =      softFlig.DOLocalMoveX(soft * 10f, 0.2f);
          Debug.Log("剩余时间"+time);
          this.qulity.text = qulity;
          this.band.text = band;
          this.Time.fillAmount = 1 / (60 /time);
          t = this.Time.DOFillAmount(0, time);
    }
}
