using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseExtraSkill : MonoBehaviour
{
    public bool isOpen;

    public string extraSkillDesc;

    public int maxTradeLimit;

    public virtual void SkillOn()
    {
        isOpen = true;
    }

    public virtual void SkillOff()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        GetComponentInParent<BaseMapRole>().extraSkill = this;
    }
}
