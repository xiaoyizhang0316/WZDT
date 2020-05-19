using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNpc : MonoBehaviour
{
    public bool isLock;
    
    /// <summary>
    /// 当前是否课件
    /// </summary>
    public bool isCansee;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract bool LockCondition();
}
