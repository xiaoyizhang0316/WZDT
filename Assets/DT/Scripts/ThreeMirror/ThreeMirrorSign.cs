using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ThreeMirrorSign : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private bool isUpMove;
    private bool isDownMove;

    public Transform posIn;
    public Transform posOut;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isUpMove&&count!=0)
        {
            return;
        }

        count++;
        isUpMove = true;
        transform.DOLocalMoveY(   posOut.localPosition .y, 0.3f).OnComplete(() =>
        {
            isUpMove = false; 
            
        });
        transform.GetChild(1).transform.DOScale(1.8f, 0.3f) ;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDownMove&&count!=1)
        {
            return;
        }

        count--;
        isDownMove = true;
        transform.DOLocalMoveY(   posIn.localPosition .y, 0.3f).OnComplete(() => { isDownMove = false; });
        transform.GetChild(1).transform.DOScale(1f, 0.3f) ;
    }
}
