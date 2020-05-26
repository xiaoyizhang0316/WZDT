using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConsumerSatisfySign : MonoBehaviour
{
    float satisfyNum;

    public double mapRoleId;

    public Tweener tweener;

    private bool isStart = false;

    public void Init(float num,double id)
    {
        satisfyNum = num;
        mapRoleId = id;
        ChooseSprite();
        float offsetX = Random.Range(-20f, 20f);
        float offsetY = Random.Range(100f, 200f);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        Vector3 offset = new Vector3(offsetX, offsetY, 0f);
        tweener = transform.DOLocalMove(transform.localPosition + offset, 1f).OnComplete(()=> {
            transform.DOScale(1f, 9f).OnComplete(DeleteAndSave).SetUpdate(true);
        });
        //Invoke("Move", time);
    }

    public void ChooseSprite()
    {
        if (satisfyNum >= 0.7f)
            GetComponent<Image>().sprite = BubbleManager.My.consumerSprite[0];
        else if (satisfyNum >= 0.3f && satisfyNum < 0.7f)
            GetComponent<Image>().sprite = BubbleManager.My.consumerSprite[1];
        else if (satisfyNum >= -0.2f && satisfyNum < 0.3f)
            GetComponent<Image>().sprite = BubbleManager.My.consumerSprite[2];
        else if (satisfyNum >= -0.5f && satisfyNum < -0.2f)
            GetComponent<Image>().sprite = BubbleManager.My.consumerSprite[3];
        else if (satisfyNum < -0.5f)
            GetComponent<Image>().sprite = BubbleManager.My.consumerSprite[4];
    }

    public void CheckBreak()
    {
        if(!isStart)
        {
            tweener.Kill();
            DeleteAndSave();
        }
    }

    public void DeleteAndSave()
    {
        isStart = true;
        float targetY = 819f;
        float targetX = Random.Range(-872f, -183f);
        Vector3 target = new Vector3(targetX, targetY, 0f);
        transform.DOLocalMove(target, 0.8f).OnComplete(()=>
        {
            //print("满意度:" + satisfyNum);
            //StageGoal.My.ChangeCustomerSatisfy(satisfyNum);
            BubbleManager.My.consumerSatisfySigns.Remove(this);
            Destroy(gameObject, 0.01f);
        }).SetUpdate(true);
    }

    public void OnMouseDown()
    {
        //print("点击触发");
        if (!isStart)
        {
            //print(EventSystem.current.IsPointerOverGameObject());
            if (EventSystem.current.IsPointerOverGameObject())
            {
                BubbleManager.My.BreakRoleBubble(mapRoleId);
            }
        }
    }

    private void Awake()
    {
        BubbleManager.My.consumerSatisfySigns.Add(this);
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
