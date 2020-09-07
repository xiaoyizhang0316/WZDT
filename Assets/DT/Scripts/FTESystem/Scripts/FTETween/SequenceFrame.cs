using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SequenceFrame : BaseTween,IPointerClickHandler
{
    public List<Sprite> spriteList;

    private bool isPlaying = false;

    public GameObject targetPanel;

    public IEnumerator PlayEffect()
    {
        isPlaying = true;
        Image img = GetComponent<Image>();
        for (int i = 0; i < spriteList.Count; i++)
        {
            img.sprite = spriteList[i];
            yield return new WaitForSeconds(0.03f);
        }
        isPlaying = false;
        targetPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public override void Move()
    {
        if (!isPlaying)
            StartCoroutine(PlayEffect());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Move();
    }
}
