using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPrologue : MonoBehaviour
{
    public void PrologueOn()
    {
        //CameraPlay.WidescreenH_ON(Color.black, 1);
        SaveMenu.instance.Show();
    }

    public void PrologueOff()
    {
        //CameraPlay.WidescreenH_OFF();
        if (SceneManager.GetActiveScene().name == "Map")
        {
            MapGuideManager.My.currentGuideIndex = 2;
            MapGuideManager.My.PlayCurrentIndexGuide();
        }
        SaveMenu.instance.Hide();

        gameObject.SetActive(false);
    }
}