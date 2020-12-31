using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_1_5_OpenCG1 : MonoBehaviour
{
    public void PrologueOn()
    {
        //CameraPlay.WidescreenH_ON(Color.black, 1);
    }

    public void PrologueOff()
    {
        gameObject.SetActive(false);
    }
}
