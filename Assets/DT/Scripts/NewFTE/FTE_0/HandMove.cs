using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandMove : MonoBehaviour
{
    public HandType moveType = HandType.Scale;
    bool isActive = false;
    //public Transform target;
    //public bool isSameCanvas = false;
    public Vector3 startPos;
    public Vector3 targetPos;

    private void Start()
    {
        //isActive = true;
        
    }

    private void OnEnable()
    {
        if (!isActive)
        {
            isActive = true;
            
            Move();
        }
    }

    private void OnDisable()
    {
        isActive = false;
    }

    private void Move()
    {
        if (isActive)
        {
            switch (moveType)
            {
                case HandType.Scale:
                    transform.DOScale(1.2f, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        transform.DOScale(1, 0.7f).SetEase(Ease.Linear).OnComplete(Move);
                    });
                    break;
                case HandType.Move:
                    transform.DOLocalMove(targetPos, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        transform.DOLocalMove(startPos, 0.02f).OnComplete(Move);
                    });
                    break;
                case HandType.ClickAndMove:
                    transform.DOLocalMove(targetPos, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        transform.DOLocalMove(startPos, 0.02f).OnComplete(Move);
                    });
                    break;
                default:
                    transform.DOScale(1.2f, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        transform.DOScale(1, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            Move();
                        });
                    });
                    break;
            }
        }
    }
}

public enum HandType
{
    Scale,
    Move,
    ClickAndMove
}
