using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 管理角色在地图中的操作
/// </summary>
public class RolePosSign : MonoBehaviour
{
    /// <summary>
    /// 当前角色是否放在地图中
    /// </summary>
    public bool isRelease;

    /// <summary>
    /// 当前与别的角色有冲突
    /// </summary>
    public bool isCollision;

    public Vector3 startPos;
    public List<Transform> _roleDrags;
    public List<MapSign> MapSigns;
    public void ReSetRolePos()
    {
        startPos = transform.position;
        CommonData.My.hand.transform.position = gameObject.transform.position;
        // CommonData.My.hand.GetComponent<RoleDrag>().currentrole = this;

        //transform.SetParent(CommonData.My.hand.transform);
    }

    public bool IsAllCollision()
    {
        for (int i = 0; i < _roleDrags.Count; i++)
        {
            if (_roleDrags[i].GetComponent<RoleDrag>().isCollision)
            {
                return false;
            }
            else
            {
            }
        }

        return true;
    }
    public void ReleaseRole(Action success,Action defeat)
    {
        int cout = 0;
        Vector3 vec = Vector3.zero;
        for (int i = 0; i < _roleDrags.Count; i++)
        {
            Vector3 fwd = _roleDrags[i].TransformDirection(Vector3.down);
             
            RaycastHit[] hit= Physics.RaycastAll(_roleDrags[i].position, fwd,  100);

            for (int j = 0; j < hit.Length; j++)
            {
               
                if (hit[j].collider.tag.Equals("MapLand"))
                {
                
                    //Debug.Log(hit.collider.gameObject.name);
                    vec = hit[j].collider.transform.position - _roleDrags[i].position;
                    // Debug.Log(vec);
              
                  
                    MapSigns.Add( hit[j].collider.GetComponent<MapSign>());
                    cout++;
                    break;
                }
            }
           
             
              
           
        }

        transform.SetParent(CommonData.My.RoleTF.transform);
         Debug.Log("Count"+cout);
        if (cout == _roleDrags.Count && ExecutionManager.My.SubExecution(ExecutionManager.My.putRole))
        {
            transform.position = transform.position + vec;

                if (!IsAllCollision())
                {
                    if (isRelease)
                    {
                        Debug.Log(2);

                        gameObject.transform.position = startPos;


                    }
                    else
                    {
                        defeat();
                        Destroy(this.gameObject, 0.01f);
                        PlayerData.My.GetRoleById(double.Parse(name)).inMap = false;

                    }
                }
                Tweener tweener = transform.DOMove(new Vector3(transform.position.x, 0, transform.position.z), 0.3f).OnComplete(
                    () =>
                    {
                        success();
                        startPos = gameObject.transform.position;
                        isRelease = true;
                    });
                tweener.SetUpdate(true);
        }

        else  
        {
            if (isRelease)
            {
                Debug.Log(2);

                gameObject.transform.position = startPos;
            }
            else
            {
                defeat();

                Debug.Log(3);
                
                Destroy(this.gameObject, 0.01f);
            }
            
        }


        //  CommonData.My.hand.GetComponent<RoleDrag>().currentrole = null;
    }
}

// Start is called before the first frame update 