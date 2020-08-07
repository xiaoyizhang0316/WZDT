using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GuideStep : MonoBehaviour
{
    public Button eventBtn;

    public Transform target;
    public Transform hand;
    public Text tip;

    public HandMoveType moveType;
    public float moveTime = 1f;
    public float scale = 1.5f;
    public float scaleTime = 0.5f;

    Vector2 startPos;
    Vector2 endPos;
    bool isOnEnable = false;
    bool isMove = false;

    private void Start()
    {
        //InitStep();   
    }

    void InitStep()
    {
        

        switch (moveType)
        {
            case HandMoveType.Move:
                isMove = true;
                startPos = hand.localPosition;

                endPos = GetEndPos(hand.GetComponent<RectTransform>());
                DoMove();
                break;
            case HandMoveType.Scale:
                isMove = false;
                DoScale();
                break;
        }
        
    }

    void DoMove()
    {
        if (isOnEnable)
        {
            hand.gameObject.SetActive(true);
            hand.DOLocalMove(endPos, moveTime).SetEase(Ease.Linear).OnComplete(() => {
                hand.gameObject.SetActive(false);
                //Debug.LogError(startPos);
                hand.DOLocalMove(startPos, 0.01f).OnComplete(() => {
                    DoMove();
                });
            });
        }

        
    }

    void DoScale()
    {
        if (isOnEnable)
        {
            hand.gameObject.SetActive(true);
            //tip.transform.DOScale(scale, scaleTime);
            hand.DOScale(scale, scaleTime).OnComplete(()=> {
                //tip.transform.DOScale(1, scaleTime);
                hand.DOScale(1, scaleTime).OnComplete(()=> {
                    
                    DoScale();
                });
            });
        }
    }

    Vector2 GetEndPos(RectTransform rect)
    {
        Vector2 targetPos = target.localPosition;
        Vector2 handPos = hand.localPosition;
        Vector2 endPos;
        float x = rect.sizeDelta.x / 2;
        float y = rect.sizeDelta.y / 2;
        float r = Mathf.Sqrt(x * x + y * y);

        float abs_x = Mathf.Abs(targetPos.x - handPos.x);
        float abs_y = Mathf.Abs(targetPos.y - handPos.y);

        float abs_z = Mathf.Sqrt(abs_x * abs_x + abs_y * abs_y);

        float offset_x = r * abs_x / abs_z;
        float offset_y = r * abs_y / abs_z;

        endPos.x = handPos.x - targetPos.x < 0 ? targetPos.x - offset_x : targetPos.x + offset_x;
        endPos.y = handPos.y - targetPos.y < 0 ? targetPos.y - offset_y : targetPos.x + offset_y;

        return endPos;
    }

    private void OnEnable()
    {
        isOnEnable = true;
        if (isMove)
        {
            DoMove();
        }
        else
        {
            DoScale();
        }
    }

    private void OnDisable()
    {
        isOnEnable = false;
    }
}
