using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SellMoneySign : MonoBehaviour
{
    int moneyNum;

    public double mapRoleId;

    public Tweener tweener;

    public bool isStart = false;

    public void Init(int num,double id)
    {
        moneyNum = num;
        mapRoleId = id;
        float offsetX = Random.Range(-20f, 20f);
        float offsetY = Random.Range(100f, 200f);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        Vector3 offset = new Vector3(offsetX, offsetY, 0f);
        tweener = transform.DOLocalMove(transform.localPosition + offset, 1f).OnComplete(() =>
        {
            transform.DOScale(1f, 9f).OnComplete(DeleteAndSave);
        });
    }

    public void DeleteAndSave()
    {
        isStart = true;
        BubbleManager.My.sellMoneySigns.Remove(this);
        float targetY = 819f;
        float targetX = Random.Range(192f, 853f);
        Vector3 target = new Vector3(targetX, targetY, 0f);
        transform.DOLocalMove(target, 0.8f).OnComplete(() =>
        {
            //print("收入:" + moneyNum);
            PlayerData.My.GetMapRoleById(mapRoleId).GetMoney(moneyNum);
            Destroy(gameObject, 0.01f);
        }).SetUpdate(true);
    }

    public void CheckBreak()
    {
        if (!isStart)
        {
            tweener.Kill();
            DeleteAndSave();
        }
    }

    public void OnMouseDown()
    {
        //print("点击触发");
        if (!isStart)
        { 
            //print(EventSystem.current.IsPointerOverGameObject());
            if(EventSystem.current.IsPointerOverGameObject())
            {
                BubbleManager.My.BreakRoleBubble(mapRoleId);
            }
        }
    }

    private void Awake()
    {
        BubbleManager.My.sellMoneySigns.Add(this);
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
