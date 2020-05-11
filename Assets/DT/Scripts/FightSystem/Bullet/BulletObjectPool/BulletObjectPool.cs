using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.Experimental.XR;

namespace DT.Fight.Bullet
{

    public class BulletObjectPool : MonoSingleton<BulletObjectPool>
    {
        public int StartBulletCount=5;

        public Transform bulletTF;
        /// <summary>
        /// 子弹类型
        /// </summary>
          List<GameObject> BulletPrb=new List<GameObject>();
        
        Dictionary<string ,List<GameObject>> pool = new Dictionary<string, List<GameObject>>();
       
        /// <summary>
        /// 初始化子弹
        /// </summary>
        public void InitBulletList()
        {
            string[] bullNames =  Enum.GetNames(typeof(BulletType));
            for (int i = 0; i < bullNames.Length; i++)
            {
                GameObject bulletPrb = Resources.Load<GameObject>("Bullet/" + bullNames[i]);

                if (bulletPrb != null)
                {
                    Debug.Log("bulletPrb"+bulletPrb.name);
                    BulletPrb.Add(bulletPrb);

                    List<GameObject> bulletOBJs= new List<GameObject>();
                    for (int j = 0; j <StartBulletCount; j++)
                    {
                        GameObject bulletOBJ = Instantiate(bulletPrb, bulletTF);
                        bulletOBJ.name = bullNames[i] + "_" + j.ToString();
                        bulletOBJ.SetActive(false); 
                        bulletOBJs.Add(bulletOBJ); 
                    }
                    pool.Add( bullNames[i],bulletOBJs);
                    
                }
                else
                {
                    Debug.Log("在地址"+"Bullet/" + bullNames[i]+"查找不到当前子弹类型："+bullNames[i]);
                }

            }
        }

        /// <summary>
        /// 获取子弹
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public GameObject GetBullet(BulletType type )
        {
            if (pool.ContainsKey(type.ToString()))
            {
                for (int i = 0; i < pool[type.ToString()].Count; i++)
                {
                    if (!pool[type.ToString()][i].activeSelf)
                    {
                        pool[type.ToString()][i].SetActive(true);
                        //当前存在并且有富余的
                        return pool[type.ToString()][i];
                    }
                    
                }
                //当前没有富余的 扩容
                GameObject bulletPrb = Resources.Load<GameObject>("Bullet/" +type.ToString());
                if (bulletPrb != null)
                {
                    GameObject bulletOBJ = Instantiate(bulletPrb, bulletTF);
                    bulletOBJ.name = type.ToString() + "_" + (pool[type.ToString()].Count ).ToString();

                    pool[type.ToString()].Add(bulletOBJ);
                    return bulletOBJ;
                }
                else
                {
                    Debug.Log("在地址"+"Bullet/" + type.ToString()+"查找不到当前子弹类型："+type.ToString());
                    return null; 
                }
            }
            else
            {
                Debug.Log("不包含当前子弹类型 需要再BullType中增加子弹类型 "+type.ToString());
                return null;
            }
        }


        /// <summary>
        /// 回收子弹
        /// </summary>
        /// <param name="OBJ"></param>
        public void RecoveryBullet(GameObject OBJ)
        {
            OBJ.SetActive(false);
            OBJ.transform.SetParent( bulletTF);
            OBJ.transform.localPosition = Vector3.zero;
        }

        // Start is called before the first frame update
        void Start()
        {
            InitBulletList();
        }


      //  public void OnGUI()
      //  {
      //      if (GUILayout.Button("创建子弹NormalPP"))
      //      {
      //          GetBullet(BulletType.NormalPP);
      //      }
      //      if (GUILayout.Button("创建子弹Bomb"))
      //      {
      //          GetBullet(BulletType.Bomb);
      //      }
      //      if (GUILayout.Button("创建子弹Lightning"))
      //      {
      //          GetBullet(BulletType.Lightning);
      //      }
      //      if (GUILayout.Button("回收NormalPP"))
      //      {
      //          RecoveryBullet(pool[BulletType.NormalPP.ToString()] [1] );
      //      }
      //      if (GUILayout.Button("回收Lightning"))
      //      {
      //          RecoveryBullet(pool[BulletType.Lightning.ToString()] [1] );
//
      //      }
      //      if (GUILayout.Button("回收子弹Bomb"))
      //      {
      //          RecoveryBullet(pool[BulletType.Bomb.ToString()] [1] );
//
      //      }
      //    
      //      
      //  }

        // Update is called once per frame
        void Update()
        {

        }
    }
}