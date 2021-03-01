using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EffectMove : MonoBehaviour
{
    public Transform startPos;
    public Transform EndPos;

    public float startTime;
    public float backTime;

    public float moveTime;

    public Ease moveease;
    public Ease Baskease;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
      
    }

    public void  Move()
    {
        gameObject.transform.position = startPos.position;
        transform.DOScale(1, startTime).OnComplete(() =>
        {
            gameObject.transform.DOMove(EndPos.position, moveTime).SetEase(moveease).Play();
        }).Play();

    }

    public void  Back()
    {
        gameObject.transform.position = EndPos.position;
     
      
        gameObject.transform.DOMove(startPos.position,moveTime).SetEase(Baskease).Play();
 
    }
}
