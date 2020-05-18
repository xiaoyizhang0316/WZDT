using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;

public class LightningTrigger : MonoBehaviour
{
    public int time;
    ConsumeSign Asign;
    private List<ConsumeSign> ConsumeSigns;

    public void GetTriggerList(ConsumeSign target, ProductData data)
    {
        Asign = target;

        Asign.OnHit(data);
        if (GetminDisConsumer())
        {
            gameObject.transform.DOMove(Asign.transform.position, 0.1f).OnComplete(() =>
            {
                Asign.OnHit(data);
                if (GetminDisConsumer())
                {
                    gameObject.transform.DOMove(Asign.transform.position, 0.1f).OnComplete(() =>
                    {
                        Asign.OnHit(data);
                        if (GetminDisConsumer())
                        {
                            gameObject.transform.DOMove(Asign.transform.position, 0.1f).OnComplete(() =>
                            {
                                Asign.OnHit(data);

                                BulletObjectPool.My.RecoveryBullet(gameObject);
                            });
                        }
                        else
                        {
                            BulletObjectPool.My.RecoveryBullet(gameObject);
                        }
                    });
                }
                else
                {
                    BulletObjectPool.My.RecoveryBullet(gameObject);
                }
            });
        }
        else
        {
            BulletObjectPool.My.RecoveryBullet(gameObject);
        }
    }

    public bool GetminDisConsumer()
    {
        List<ConsumeSign> signs = FindObjectsOfType<ConsumeSign>().ToList();
        bool find = false;
        float dis = 2;
        for (int i = 0; i < signs.Count; i++)
        {
            if (Asign != signs[i] && signs[i].isCanSelect)
            {
                if (Vector3.Distance(Asign.transform.position, signs[i].transform.position) < dis)
                {
                    dis = Vector3.Distance(Asign.transform.position, signs[i].transform.position);
                    Asign = signs[i];
                    find =  true;
                }
            }
        }

        return find;
    }
}