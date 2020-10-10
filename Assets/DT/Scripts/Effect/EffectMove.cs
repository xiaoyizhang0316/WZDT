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

    public IEnumerator  Move()
    {
        gameObject.transform.position = startPos.position;
        yield return new WaitForSeconds(startTime);
      
        gameObject.transform.DOMove(EndPos.position,moveTime).SetEase(moveease).Play();
    }

    public IEnumerator Back()
    {
        gameObject.transform.position = EndPos.position;
        yield return new WaitForSeconds(backTime);
      
        gameObject.transform.DOMove(startPos.position,moveTime).SetEase(Baskease).Play();
    }
}
