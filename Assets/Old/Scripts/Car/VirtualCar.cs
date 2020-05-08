using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class VirtualCar : MonoBehaviour
{
    public Transform target;

    public Tweener tweener;

    public float speed;

    public float startSpeed;

    public float startTime;

    public float endTime;

    public float normalTime;

    public Dictionary<int, float> buffList;

    public void Init(Transform _target)
    {
        target = _target;
        startSpeed = 10f;
        speed = startSpeed;
        buffList = new Dictionary<int, float>();
        startTime = Time.time;
        normalTime = Vector3.Distance(transform.position, target.position) / speed;
        Move();
        //StartMove();
    }

    public void Move()
    {
        tweener = transform.DOMove(target.position, Vector3.Distance(transform.position, target.position) / speed).OnComplete(() => RecordTime()).SetEase(Ease.Linear);
    }

    public void Stop()
    {
        tweener.Kill();
    }

    public void RecordTime()
    {
        endTime = Time.time;
        float result = (endTime - startTime) / normalTime;
        GetComponentInParent<CarMove>().actualTimePercent = result;
        GetComponentInParent<CarMove>().MoveCar();
        //print("实际时间： " + result);
        Destroy(gameObject, 0f);
    }

    /// <summary>
    /// 增加buff
    /// </summary>
    /// <param name="id"></param>
    /// <param name="effect"></param>
    public void AddBuff(int id, float effect)
    {
        if (buffList.ContainsKey(id))
        {
            if (Mathf.Abs(buffList[id] - effect) > 0.01f)
            {
                buffList[id] = effect;
                RecheckBuff();
            }
            else
                return;
        }

        else
        {
            buffList.Add(id, effect);
            RecheckBuff();
        }
    }

    /// <summary>
    /// 删除buff
    /// </summary>
    /// <param name="id"></param>
    public void RemoveBuff(int id)
    {
        if (buffList.ContainsKey(id))
        {
            buffList.Remove(id);
            RecheckBuff();
        }
    }

    /// <summary>
    /// 重新计算速度
    /// </summary>
    public void RecheckBuff()
    {
        Stop();
        float speedAdd = 1f;
        speed = startSpeed;
        if (buffList.Count > 0)
        {
            foreach (var v in buffList)
            {
                speedAdd += v.Value;
            }
        }
        speed = speed * speedAdd;
        Move();
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
