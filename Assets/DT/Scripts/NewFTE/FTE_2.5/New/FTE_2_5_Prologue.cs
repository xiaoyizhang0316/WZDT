using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FTE_2_5_Prologue : MonoBehaviour
{
    public FTE_2_5_NewGoal1 goal1;
    public void PrologueOn()
    {
        CameraPlay.WidescreenH_ON(Color.black, 1);
    }

    public void PrologueOff()
    {
        CameraPlay.WidescreenH_OFF();
        gameObject.SetActive(false);
        goal1.isEnd = true;
    }
}
