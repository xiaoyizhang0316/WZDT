using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FTESceneManager : MonoSingleton<FTESceneManager>
{
    public  List<BaseStep> Steps = new List<BaseStep>();

    public GameObject UIFTE;
    public int currentIndex =5;
    // Start is called before the first frame update
    void Start()
    {
        if (currentIndex >= 0 && PlayerPrefs.GetInt("isUseGuide") == 1)
        {
            UIFTE.SetActive(true);

            StartFTE();

        }
        else
        {
            UIFTE.SetActive(false);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFTE()
    {
        if (currentIndex >= Steps.Count)
        {
            return;
        }

        StartFTEFormIndex(currentIndex);
    }

    public void StartFTEFormIndex(int index)
    {
        Steps[index].gameObject.SetActive(true);
        Steps[index].StartCuttentStep();
        Debug.Log("开启当前引导步骤"+index);
    }

    public void PlayNextStep()
    { 
        currentIndex+=1;
        
        StartFTE();

    }
    
    
}
