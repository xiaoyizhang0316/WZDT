using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public BaseMapRole role;
    public bool IsOpen;
    // Start is called before the first frame update
    void Start()
    {
        if (IsOpen)
        {
            UnleashSkills();
        }

        role = GetComponent<BaseMapRole>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 释放技能
    /// </summary>
    public abstract void  Skill ();

    public void UnleashSkills()
    {
    
           Skill();
        if (IsOpen)
        {
            UnleashSkills();
        }
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
