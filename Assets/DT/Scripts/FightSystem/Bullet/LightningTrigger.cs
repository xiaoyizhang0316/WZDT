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
        gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().explosions);
        gameObject.transform.DOScale(1, 0.3f).OnComplete(() =>
        {
            gameObject.GetComponent<BulletEffect>().explosions.SetActive(false);
        });
        Debug.Log("闪电链" + gameObject.name + 1);
        if (gameObject.GetComponent<GoodsSign>().target != null)
        {
            Debug.Log("target"+gameObject.GetComponent<GoodsSign>().target );
            Asign.OnHit(ref data);
        }
        else
        {
            BulletObjectPool.My.RecoveryBullet(gameObject);
        }

        if (GetminDisConsumer())
        {
            gameObject.transform.DOMove(Asign.transform.position, 0.5f).OnComplete(() =>
            {
                   gameObject .GetComponent <BulletEffect>().InitBuff(  gameObject .GetComponent <BulletEffect>().explosions);
                gameObject.transform.DOScale(1, 0.3f).OnComplete(() =>
                {
                    gameObject.GetComponent<BulletEffect>().explosions.SetActive(false);
                });
                Debug.Log("闪电链" + gameObject.name + 2);
                if (gameObject.GetComponent<GoodsSign>().target != null)
                {
                    Asign.OnHit(ref data);
                }
                else
                {
                    BulletObjectPool.My.RecoveryBullet(gameObject);
                }
                if (GetminDisConsumer())
                {
                    gameObject.transform.DOMove(Asign.transform.position, 0.5f).OnComplete(() =>
                    {
                      gameObject .GetComponent <BulletEffect>().InitBuff(  gameObject .GetComponent <BulletEffect>().explosions);
                        gameObject.transform.DOScale(1, 0.3f).OnComplete(() =>
                        {
                            gameObject.GetComponent<BulletEffect>().explosions.SetActive(false);
                        });
                        Debug.Log("闪电链" + gameObject.name + 3);
                        if (gameObject.GetComponent<GoodsSign>().target != null)
                        {
                            Asign.OnHit(ref data);
                        }
                        else
                        {
                            BulletObjectPool.My.RecoveryBullet(gameObject);
                        }
                        if (GetminDisConsumer())
                        {
                            gameObject.transform.DOMove(Asign.transform.position, 0.5f).OnComplete(() =>
                            {
                                gameObject .GetComponent <BulletEffect>().InitBuff(  gameObject .GetComponent <BulletEffect>().explosions);
                                gameObject.transform.DOScale(1, 0.3f).OnComplete(() =>
                                {
                                    Debug.Log("闪电链" + gameObject.name + 4);
                                    gameObject.GetComponent<BulletEffect>().explosions.SetActive(false);
                                    BulletObjectPool.My.RecoveryBullet(gameObject);
                                });
                                if (gameObject.GetComponent<GoodsSign>().target != null)
                                {
                                    Asign.OnHit(ref data);
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
                    find = true;
                }
            }
        }

        return find;
    }
}