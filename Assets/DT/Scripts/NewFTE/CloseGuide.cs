using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CloseGuide : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GuideManager.My.guideClose = this;
    }

    public void Init()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() =>
        {
        
            GuideManager.My.CloseFTE();
            
        });
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "FTE_0-1" && sceneName != "FTE_0-2")
        {

            if (PlayerPrefs.GetInt("isUseGuide") == 0)
            {
                gameObject.SetActive(false);
            }
            else 
            {
                if (int.Parse(sceneName.Split('_')[1]) <= 4)
                {
                    gameObject.SetActive(AnsweringPanel.My.isComplete);
                }
                gameObject.SetActive(NetworkMgr.My.levelProgressList.Count >= int.Parse(sceneName.Split('_')[1]));
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
