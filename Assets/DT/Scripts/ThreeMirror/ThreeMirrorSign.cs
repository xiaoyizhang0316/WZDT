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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isUpMove&&count!=0)
        {
            return;
        }
        count++;
        isUpMove = true;
        transform.DOMoveY(   posOut.position .y, 0.3f).OnComplete(() =>
        //transform.DOLocalMoveY(transform.localPosition.y + 40f, 0.3f).OnComplete(() =>
        {
            isUpMove = false; 
            
        }).Play().timeScale = 1f / DOTween.timeScale;
        transform.GetChild(1).transform.DOScale(1.8f, 0.3f).Play().timeScale = 1f / DOTween.timeScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDownMove&&count!=1)
        {
            return;
        }

        count--;
        isDownMove = true;
        transform.DOMoveY(posIn.position.y, 0.3f).OnComplete(() => { isDownMove = false; }).Play().timeScale = 1f / DOTween.timeScale;
        transform.GetChild(1).transform.DOScale(1f, 0.3f).Play().timeScale = 1f / DOTween.timeScale;
    }
}
