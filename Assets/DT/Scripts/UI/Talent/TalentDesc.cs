using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TalentDesc : MonoBehaviour
{
    public Text title;

    public Text content;

    public Image talentImg;

    public void Init(TalentItem item)
    {
        GetComponent<RectTransform>().DOAnchorPos(new Vector2(67f, -462f),0.5f).Play();
        title.text = item.talentTitle;
        content.text = item.talentDesc;
        talentImg.sprite = item.statusSprites[0];
    }

    public void Close()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-230f, -462f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
