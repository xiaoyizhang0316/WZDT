using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (!CommonParams.fteList.Contains(sceneName))
        {

            if (PlayerPrefs.GetInt("isUseGuide") == 0)
            {
                gameObject.SetActive(false);
            }
            else 
            {
                if (float.Parse(sceneName.Split('_')[1]) <=
                    float.Parse(NetworkMgr.My.playerDatas.fte))
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
         
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
