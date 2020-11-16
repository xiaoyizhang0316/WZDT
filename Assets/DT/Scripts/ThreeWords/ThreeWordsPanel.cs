using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThreeWordsPanel : MonoSingleton<ThreeWordsPanel>
{
    public Text quesion;

    public InputField answer_input;

    public Button submit_btn;

    public GameObject thisPanel;

    public Transform map;

    string input="";
    // Start is called before the first frame update
    void Start()
    {
        submit_btn.onClick.AddListener(Submit);
        answer_input.onValueChanged.AddListener(OnEdit);
        answer_input.onEndEdit.AddListener(OnEndEdit);
        //if(NetworkMgr.My.isUsingHttp&& SceneManager.GetActiveScene().name == "Map")
        //{
        //    Debug.Log(NetworkMgr.My.levelProgressList.Count + "  " + NetworkMgr.My.playerDatas.threeWordsProgress);
        //    if(NetworkMgr.My.levelProgressList.Count >= 4 && NetworkMgr.My.playerDatas.threeWordsProgress==1 )
        //    {
        //        //if (NetworkMgr.My.levelProgressList[NetworkMgr.My.levelProgressList.Count - 1].levelStar[0]=='1')
        //        //{

        //            thisPanel.SetActive(true);
        //            SetQuesion(Questions.questions[NetworkMgr.My.playerDatas.threeWordsProgress]);
        //        //}
        //    }else if(NetworkMgr.My.levelProgressList.Count >= 8 && NetworkMgr.My.playerDatas.threeWordsProgress == 2)
        //    {
        //        thisPanel.SetActive(true);
        //        SetQuesion(Questions.questions[NetworkMgr.My.playerDatas.threeWordsProgress]);
        //    }
        //}
    }

    public void OpenAnswerInputField()
    {
        thisPanel.SetActive(true);
        SetQuesion(Questions.questions[NetworkMgr.My.playerDatas.threeWordsProgress]);
    }

    public void SetQuesion(string questionStr)
    {
        quesion.text = string.Format(questionStr);
    }

    private void OnEdit(string edit)
    {
        submit_btn.interactable = false;
        answer_input.MoveTextEnd(true);
    }

    private void OnEndEdit(string edit)
    {
        if(edit.Replace(" ", "")!="")
            submit_btn.interactable = true;
    }

    private void Submit()
    {
        input = answer_input.text.Replace(" ", "");
        if(input == "")
        {
            HttpManager.My.ShowTip("输入内容不能为空！");
        }
        UploadTw();
    }

    private void UploadTw()
    {
        NetworkMgr.My.UploadThreeWords(input, SubmitSuccess, SubmitFail);
    }

    private void SubmitSuccess()
    {
        NetworkMgr.My.currentAnswer = input;
        //thisPanel.SetActive(false);
        if (SceneManager.GetActiveScene().name != "Map")
        {
            if (NetworkMgr.My.playerDatas.fteProgress == -2)
            {
                NetworkMgr.My.SetPlayerStatus("FTE_0-1", "");
                SceneManager.LoadScene("FTE_0-1");
            }
            else
                    if (NetworkMgr.My.playerDatas.fteProgress == -1)
            {
                NetworkMgr.My.SetPlayerStatus("FTE_0-2", "");
                SceneManager.LoadScene("FTE_0-2");
            }
            else
            {
                NetworkMgr.My.SetPlayerStatus("Map", "");
                SceneManager.LoadScene("Map");
            }

        }
        else
        {
            thisPanel.SetActive(false);
            map.GetComponent<MainMap>().title.text = input;
        }
    }

    private void SubmitFail()
    {

    }
}
