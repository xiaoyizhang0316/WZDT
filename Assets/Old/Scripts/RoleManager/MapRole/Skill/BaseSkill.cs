using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public BaseMapRole role;
    public bool IsOpen;
    // Start is called before the first frame update
    void Start()
    {
        role = GetComponent<BaseMapRole>();
        if (IsOpen)
        {
            UnleashSkills();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 释放技能
    /// </summary>
    public abstract void  Skill ();

    public virtual void UnleashSkills()
    {
 
        float d = 1f /(role.baseRoleData.efficiency * 0.1f);
      
        transform.DOScale(1,d ).OnComplete(() =>
        {
            Skill();
            if (IsOpen)
            {
                UnleashSkills();
            }
        });
    }

    /// <summary>
    /// 重启释放技能
    /// </summary>
    public void ReUnleashSkills()
    {
        IsOpen = true;
        UnleashSkills();
    }

    /// <summary>
    ///  取消技能
    /// </summary>
    public   void CancelSkill()
    {
        IsOpen = false;
    }
}
