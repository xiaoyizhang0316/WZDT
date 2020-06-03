using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSign : MonoBehaviour
{

    public string levelName;

    public string content;

    public string mission_1;
    public string mission_2;
    public string mission_3;

    public Button LevelButton;

    public string loadScene;
    // Start is called before the first frame update
    void Start()
    {
        LevelButton.onClick.AddListener(() =>
        {
            Debug.Log("1231231231");
            Init();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        LevelInfoManager.My.Init(levelName,content,mission_1,mission_2,mission_3, () =>
            {
                SceneManager.LoadScene(loadScene);
            });
    }
}
