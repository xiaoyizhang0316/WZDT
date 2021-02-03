using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_1_5_OpenCG0 : MonoBehaviour
{
    public void PrologueOn()
    {
        //CameraPlay.WidescreenH_ON(Color.black, 1);
        SaveMenu.instance.Show();
    }

    public void PrologueOff()
    {
        gameObject.SetActive(false);
        SaveMenu.instance.Hide();
    }
}
