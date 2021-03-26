using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class CommonText : MonoSingletonDontDestroy<CommonText>
{

    /// <summary>
    /// 阶段任务
    /// </summary>
    public List<string> Text;

    
    /// <summary>
    /// 阶段任务需要完成的数量
    /// </summary>
    
    public List<int> count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
