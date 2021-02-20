using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoodsSign : MonoBehaviour
{

    public ProductData productData;



    public ConsumeSign target;
    /// <summary>
    /// 发射者
    /// </summary>
    public BulletLaunch lunch;
    public List<Vector3> path;
    public Tweener twe;
    public Tweener moveTween;
    public BaseMapRole role;
    private float speedAdd = 1f;
    public float speed = 0.7f;

    public Dictionary<double, int> speedBuffList = new Dictionary<double, int>();
    // Start is called before the first frame update
    void Start()
    {
        needUpdate = false;
        

        // InvokeRepeating(" DeleteEffect()",0.1f,0.1f);
    }

    public void DeleteEffect()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
    }

    private int count = 0;
    public void Move()
    {
        if (PlayerData.My.isPrediction)
        {
            foreach(Transform tran in GetComponentsInChildren<Transform>()){//遍历当前物体及其所有子物体
                tran.gameObject.layer = 11;//更改物体的Layer层
            }
        }
        CheckColor();
        if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant)
        {
            speed = 0.5f * (1 - (role.baseRoleData.efficiency > 80 ? 80f : role.baseRoleData.efficiency) / 100f);
        }

        // print("bullet start move" + path[count]);
        moveTween = transform.DOMove(path[count], speed).OnComplete(() =>
        {
            // print("bullet move");
            count++;
            if (count < path.Count)
            {
                Move();
            }
            else
            {
                if (role.warehouse.Count >= role.baseRoleData.bulletCapacity)
                {
                    DataUploadManager.My.AddData(DataEnum.浪费的瓜);
                    Vector3 pos = path[path.Count - 1] + new Vector3(UnityEngine.Random.Range(-3f, 3f), 0, UnityEngine.Random.Range(-3f, 3f));
                    transform.DOMove(pos, speed).OnComplete(()=> {
                        Destroy(this.gameObject, 0.01f);
                    });
                    transform.GetComponent<MeshRenderer>().material.DOColor(Color.black, "_EmissionColor", speed).Play();
                }
                else
                {
                    role.AddPruductToWareHouse(productData);
                    Destroy(this.gameObject, 0.01f);
                }

            }

        }).SetEase(Ease.Linear);
        moveTween.timeScale = speedAdd;
    }

    private bool needUpdate;

    public bool isCopy = false;

    public void OnTriggerEnter(Collider other)
    {
        if (lunch == null)
        {
            return;
        }
        //Debug.Log(other.gameObject.name);
        //Debug.Log(lunch);
        if (other.tag == "Consumer" && (other.GetComponent<ConsumeSign>() == lunch.GetComponent<BaseMapRole>().shootTarget || isCopy))
        {
            if (twe != null && twe.IsPlaying())
            {
                twe.Kill();
                target.OnHit(ref productData);
              
                gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().explosions);
                if (PlayerData.My.dingWei[4])
                {
                    int number = UnityEngine.Random.Range(0, 101);
                    if (number <= 10)
                    {
                        List<ConsumeSign> consumeSigns = FindObjectsOfType<ConsumeSign>().ToList();
                        int targetIndex = -1;
                        float minDis = 99999f;
                        for (int i = 0; i < consumeSigns.Count; i++)
                        {
                            if (consumeSigns[i].GetInstanceID() != target.GetInstanceID() && consumeSigns[i].isCanSelect && minDis >= Vector3.Distance(target.transform.position, consumeSigns[i].transform.position) )
                            {
                                minDis = Vector3.Distance(target.transform.position, consumeSigns[i].transform.position);
                                if (minDis <= 3)
                                {
                                    targetIndex = i;
                                }
                            }
                        }
                        if(targetIndex != -1)
                        {
                            GameObject go = BulletObjectPool.My.GetBullet(BulletType.NormalPP);
                            Debug.Log("初始化子弹" + go.name);
                            go.GetComponent<GoodsSign>().productData = productData;
                            go.GetComponent<GoodsSign>().lunch = lunch;
                            go.GetComponent<GoodsSign>().isCopy = true;
                            go.GetComponent<GoodsSign>().target = consumeSigns[targetIndex];
                            go.transform.SetParent(target.transform);
                            go.GetComponent<BulletEffect>().InitBufflist(go.GetComponent<GoodsSign>().productData.buffList);

                            go.transform.localPosition = new Vector3(0, 0.5f, 0);
                            go.transform.SetParent(target.transform);

                            Debug.Log("初始化拖尾" + go.name);
                            if(!PlayerData.My.isPrediction)
                                go.GetComponent<BulletEffect>().InitBuff(go.GetComponent<BulletEffect>().tile);
                            float flyTime2 = Vector3.Distance(consumeSigns[targetIndex].transform.position, go.transform.position) / 24f;
                            //            gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().Init();
                            if (consumeSigns[targetIndex] == null)
                            {
                                BulletObjectPool.My.RecoveryBullet(go, 0);
                            }
                            else
                            {
                                go.transform.DOMove(consumeSigns[targetIndex].transform.position, flyTime2).SetEase(Ease.Linear).OnComplete(() =>
                                {
                                    if (go.GetComponent<GoodsSign>().target != null)
                                    {
                                        Debug.Log("初始化爆炸" + go.name);
                                        if (!PlayerData.My.isPrediction)
                                        {
                                            go.GetComponent<BulletEffect>().InitBuff(go.GetComponent<BulletEffect>().explosions);
                                        }
                                        
                                        //         gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().StartShoot();
                                        go.GetComponent<GoodsSign>().target.OnHit(ref productData);
                                        BulletObjectPool.My.RecoveryBullet(go, 0.3f);
                                    }
                                });
                            }
                        }
                    }
                }
                //    GetComponentInChildren<ETFXProjectileScript>().StartShoot();
                BulletObjectPool.My.RecoveryBullet(gameObject, 0.3f);
            }
        }
    }

    public void CheckColor()
    {
        switch (productData.bulletType)
        {
            case BulletType.Seed:
                GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.green);
                break;
            case BulletType.NormalPP:
                GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.yellow);
                break;
            case BulletType.Bomb:
                GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.gray);
                break;
            case BulletType.Lightning:
                GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.blue);
                break;
            case BulletType.summon:
                GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
                break;
            default:
                break;
        }
    }

    public void AddSpeedBuff(double roleId, int num)
    {
        if (speedBuffList.ContainsKey(roleId))
            return;
        if (speedBuffList.Count == 0)
        {
            speedBuffList.Add(roleId, num);
            ChangeMoveSpeed();
        }
        else
        {
            List<double> temp = speedBuffList.Keys.ToList();
            if (speedBuffList[temp[0]] >= num)
            {
                return;
            }
            else
            {
                speedBuffList.Remove(temp[0]);
                speedBuffList.Add(roleId, num);
                ChangeMoveSpeed();
            }
        }
    }

    public void RemoveSpeedBuff(double roleId)
    {
        if (!speedBuffList.ContainsKey(roleId))
            return;
        else
        {
            speedBuffList.Remove(roleId);
            ChangeMoveSpeed();
        }
    }

    public void ChangeMoveSpeed()
    {
        speedAdd = 1f;
        moveTween.timeScale = 1f;
        foreach (var v in speedBuffList)
        {
            speedAdd += v.Value / 100f;
        }
        moveTween.timeScale = speedAdd;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "FTE_0-2")
        {
            if (RoleEditor.My.destroyBullets)
            {
                Destroy(gameObject);
            }
        }

        /*if (SceneManager.GetActiveScene().name.Equals("FTE_2.5"))
        {
            if (FTE_2_5_Manager.My.isClearGoods)
            {
                Destroy(gameObject);
            }
        }
        
        if (SceneManager.GetActiveScene().name.Equals("FTE_1.5"))
        {
            if (FTE_1_5_Manager.My.isClearGoods)
            {
                Destroy(gameObject);
            }
        }*/
    }
}
