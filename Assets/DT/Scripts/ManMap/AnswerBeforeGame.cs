using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnswerBeforeGame : MonoBehaviour
{
    public string answerID="";
    public bool isOpenBeforeTeachLevel=false;
    public string teachSceneName = "";

    private void Start()
    {
        if (isOpenBeforeTeachLevel)
        {
            GetComponent<Button>().onClick.AddListener(()=>OpenAnswerPanel());
        }
    }

    public void OpenAnswerPanel(LevelSign levelSign=null)
    {
        if (string.IsNullOrEmpty(answerID))
        {
            if (isOpenBeforeTeachLevel)
            {
                SceneManager.LoadScene(teachSceneName);
            }
            else
            {
                levelSign?.Init();
            }
        }
        else
        {
            int answer_id = int.Parse(answerID);
            if (isOpenBeforeTeachLevel)
            {
                if (answer_id > NetworkMgr.My.playerDatas.threeWordsProgress)
                {
                    ThreeWordsPanel.My.OpenAnswerInputField(() => { SceneManager.LoadScene(teachSceneName);});
                }
                else
                {
                    SceneManager.LoadScene(teachSceneName);
                }
            }
            else
            {
                if (answer_id > NetworkMgr.My.playerDatas.threeWordsProgress)
                {
                    ThreeWordsPanel.My.OpenAnswerInputField(() => { levelSign?.Init();});
                }
                else
                {
                    levelSign?.Init();
                }
            }
        }
    }
}
