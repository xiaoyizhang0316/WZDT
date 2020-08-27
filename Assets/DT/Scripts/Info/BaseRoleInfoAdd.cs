using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class BaseRoleInfoAdd : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Init();
    }

    public abstract void Init();

    public abstract void UpdateBuff();

    public abstract void UpdateBar();
}
