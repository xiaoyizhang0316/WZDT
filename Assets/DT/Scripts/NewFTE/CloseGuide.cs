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
        if (PlayerPrefs.GetInt("isUseGuide") == 0)
        {
            gameObject.SetActive(false);
        }
        else if (int.Parse(SceneManager.GetActiveScene().name.Split('_')[1]) <= 4)
        {
            gameObject.SetActive(AnsweringPanel.My.isComplete);
        }
        else 
        {
            gameObject.SetActive(NetworkMgr.My.levelProgressList.Count >= int.Parse(SceneManager.GetActiveScene().name.Split('_')[1]));
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
