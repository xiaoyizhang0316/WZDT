using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineDoFade : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public float offsetY;

    public float offset;

    public Sprite normal;
    public Sprite buff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( gameObject.transform.parent.parent.name != "LinesBuffRole")
        {
            gameObject.GetComponent<Image>().sprite = buff;
            offsetY -= offset;
            GetComponent<Image>().material.mainTextureOffset = new Vector2(0, offsetY);
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = normal;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
     transform.GetChild(1).   GetComponent<Image>().DOFade(1, 0.3f).Play();
     transform.GetChild(1).  transform.GetChild(0). GetComponent<Text>().DOFade(1, 0.3f).Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(1).  GetComponent<Image>().DOFade(0.3f, 0.3f).Play();
        transform.GetChild(1).   transform.GetChild(0). GetComponent<Text>().DOFade(0.3f, 0.3f).Play();
    }
}
