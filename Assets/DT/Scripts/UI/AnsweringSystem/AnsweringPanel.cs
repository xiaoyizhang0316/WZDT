using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class AnsweringPanel : MonoSingleton<AnsweringPanel>
{
    public GameObject ansPanel;
    #region component
    public ToggleGroup toggleGroup;

    public List<Toggle> toggles = new List<Toggle>();
    public Transform togglesTransform;
    public Toggle a_toggle;
    public Toggle b_toggle;
    public Toggle c_toggle;
    public Toggle d_toggle;
    public Text question_text;
    public Text error_text;
    public Text index_text;
    //public Button continue_btn;
    public Button next_btn;
    public Button replay_btn;
    public Button wrong_btn;
    public Text passOFail_text;

    public Text qDesc;

    public Text keywords;

    public GameObject mask;
    public GameObject replayPanel;
    public GameObject correct_image;
    public GameObject error_image;
    public GameObject multiple_tag;
    #endregion

    #region 变量
    public int currentStage = 0;
    public List<Question> toDoQuestions = new List<Question>();
    public int toDoQuestionsCount = 0;
    public int questionIndex = 0;
    public int errorCount = 0;
    private Question currentQuestion;
    private AnswerStage currentAnswerStage; 
    private List<Choice> temp = new List<Choice>();

    public List<string> answer = new List<string>();

    List<Question> stageQuestions = new List<Question>();
    int random = 0;
    bool reset = false;
    bool continueGuide = false;
    bool isFail = false;
    Action doEnd;
    string sceneName = "";
    List<int> randomList;
    StringBuilder sb = new StringBuilder();


    public bool isComplete;
    #endregion

    private void Start()
    {
        a_toggle.onValueChanged.AddListener((isOn) => SelectChioce(isOn, a_toggle.GetComponent<ChoiceItem>()));
        b_toggle.onValueChanged.AddListener((isOn) => SelectChioce(isOn, b_toggle.GetComponent<ChoiceItem>()));
        c_toggle.onValueChanged.AddListener((isOn) => SelectChioce(isOn, c_toggle.GetComponent<ChoiceItem>()));
        d_toggle.onValueChanged.AddListener((isOn) => SelectChioce(isOn, d_toggle.GetComponent<ChoiceItem>()));
        next_btn.onClick.AddListener(NextConfirm);
        //continue_btn.onClick.AddListener(Continue);
        replay_btn.onClick.AddListener(Replay);
        wrong_btn.onClick.AddListener(WrongButton);
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "FTE_0-1" || sceneName == "FTE_0-2")
        {

        }else
        {
            if (int.Parse(sceneName.Split('_')[1]) <= NetworkMgr.My.playerDatas.fteProgress)
            {
              
                    isComplete = true;
                for (int i = 0; i < GuideManager.My.baseGuideSteps.Count; i++)
                {
                    if (GuideManager.My.baseGuideSteps[i].TryGetComponent<FTE_answer>(out FTE_answer answer))
                    {
                        GuideManager.My.baseGuideSteps[i].isOpen = false;
                    }
                }
           
            }
        }
        //StartCoroutine(OriginalData.My.ReadQuestionList(() =>
        //{
        //    //InitAnsweringPanel();
        //}));
        //Invoke("InitAnsweringPanel", 2);
        InitAnsweringPanel();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void InitAnsweringPanel()
    {
        InitStage();
    }

    /// <summary>
    /// 开始答题
    /// </summary>
    /// <param name="guideContinue">答题完成是否继续教学，如果继续，doAfter不用传参。否则需传参</param>
    /// <param name="doAfter">答题成功后的操作（不继续教学的操作）</param>
    public void ShowPanel(bool guideContinue, Action doAfter=null)
    {
        isComplete = false;
        continueGuide = guideContinue;
        doEnd = null;
        if (doAfter != null)
        {
            doEnd = doAfter;
        }
        if (sceneName == "FTE_0-1" || sceneName == "FTE_0-2")
        {

        }else
        {
            if (int.Parse(sceneName.Split('_')[1]) < NetworkMgr.My.playerDatas.fteProgress)
            {
                if (continueGuide)
                {
                    isComplete = true;
                }
                else
                {
                    doEnd?.Invoke();
                }
                return;
            }
        }
        
        // show error count
        // 获取数量，题目，和错误限制
        transform.GetComponent<Image>().raycastTarget = true;
        ansPanel.SetActive(true);
        currentAnswerStage = OriginalData.My.answerStages[currentStage];
        print(currentStage);
        if (currentAnswerStage.questionCount == -1)
        {
            gameObject.SetActive(false);
            return;
        }
        toDoQuestionsCount = currentAnswerStage.questionCount;
        print(toDoQuestionsCount);
        errorCount = currentAnswerStage.errorCount;
        error_text.text = errorCount.ToString();
        questionIndex = 0;
        stageQuestions = null;
        InitRandomList();
        toDoQuestions.Clear();
        stageQuestions = OriginalData.My.questionList.GetStageQuestions(currentStage);
        GetToDoQuestion();
        gameObject.SetActive(true);
        ShowQuestion();
    }

    /// <summary>
    /// 获取要做的题目
    /// </summary>
    void GetToDoQuestion()
    {
        Debug.Log("Stage q : " + stageQuestions.Count);
        for(int i=0; i< stageQuestions.Count;)
        {
            Debug.Log(stageQuestions[i].question_id+" necessary: " + stageQuestions[i].necessary);
            if (stageQuestions[i].necessary == 1 && toDoQuestions.Count<= toDoQuestionsCount)
            {
                toDoQuestions.Add(stageQuestions[i]);
                stageQuestions.Remove(stageQuestions[i]);
                continue;
            }
            i++;
        }
        Debug.Log("todo question :" + toDoQuestions.Count);
        Debug.Log("Stage q : " + stageQuestions.Count);
        if (toDoQuestions.Count < toDoQuestionsCount)
        {
            for(int i= toDoQuestions.Count; i< toDoQuestionsCount; i++)
            {
                if (toDoQuestions.Count >= toDoQuestionsCount)
                {
                    break;
                }
                Debug.Log("Stage q : " + stageQuestions.Count);
                random = UnityEngine.Random.Range(0, stageQuestions.Count);
                toDoQuestions.Add(stageQuestions[random]);
                stageQuestions.Remove(stageQuestions[random]);
                
            }
        }
        Debug.Log("Stage q : " + stageQuestions.Count);
        Debug.Log("todo question :" + toDoQuestions.Count);
    }

    /// <summary>
    /// 显示题目
    /// </summary>
    private void ShowQuestion()
    {
        //index_text.text = toDoQuestionsCount + "-" + (questionIndex+1);
        ResetChoices();
        currentQuestion = GetRandomQuestion();
        if(currentQuestion == null)
        {
            mask.SetActive(true);
            //continue_btn.gameObject.SetActive(true);
            questionIndex = 0;
            currentStage += 1;
            return;
        }
        SetQuestionType(currentQuestion.isMultiple);
        string[] qu = currentQuestion.question_text.Split('_');
        index_text.text = (questionIndex + 1) + "/" + toDoQuestionsCount;
        sb.Clear();
        //sb.Append(toDoQuestionsCount + "-" + (questionIndex+1) + "  ");
        //for (int i = 0; i < qu.Length; i++)
        //{
        //    if (i % 2 == 1)
        //    {
        //        sb.Append("<color=green>" + qu[i] + "</color>");
        //        continue;
        //    }
        //    sb.Append(qu[i]);
        //}
        sb.Append(qu[0]);
        question_text.text = sb.ToString();
        if (qu.Length < 2)
        {
            keywords.text = "";
        }
        else
        {

            keywords.text = qu[1] ;
        }

        string[] ans = currentQuestion.choices.Split('_');
        temp.Clear();
        for(int i=0; i < ans.Length; i++)
        {
            temp.Add(new Choice(i, ans[i]));
        }

        for(int i=0; i< togglesTransform.childCount; i++)
        {
            //random = UnityEngine.Random.Range(0, temp.Count);
            //togglesTransform.GetChild(i).GetComponent<ChoiceItem>().Setup(temp[random]);
            //temp.RemoveAt(random);
            togglesTransform.GetChild(i).gameObject.SetActive(false);
            if (i < temp.Count)
            {
                togglesTransform.GetChild(i).GetComponent<ChoiceItem>().Setup(temp[i]);
                togglesTransform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 下一题
    /// </summary>
    private void NextQuestion()
    {
        questionIndex += 1;
        answer.Clear();
        
        if (questionIndex >= toDoQuestionsCount)
        {
            mask.SetActive(true);
            //continue_btn.gameObject.SetActive(true);
            int updateFTE = 999;
            if(sceneName == "FTE_0-1")
            {
                updateFTE = -1;
            }else if(sceneName == "FTE_0-2")
            {
                updateFTE = 0;
            }
            else
            if (int.Parse(sceneName.Split('_')[1]) > NetworkMgr.My.playerDatas.fteProgress)
            {
                updateFTE = int.Parse(sceneName.Split('_')[1]);
            }
            if(updateFTE != 999)
            {

                NetworkMgr.My.UpdatePlayerDatas(updateFTE, 0, "0", () =>
                {
                    
                });
            }
            replay_btn.onClick.RemoveAllListeners();
            replay_btn.onClick.AddListener(() => Continue());
            passOFail_text.text = "答题通过！";
            replayPanel.SetActive(true);
            questionIndex = 0;
            currentStage += 1;
        }
        else
        {
            ShowQuestion();
        }
    }

    /// <summary>
    /// 切换题目，重置选项
    /// </summary>
    private void ResetChoices()
    {
        Debug.Log("reset");
        reset = true;
        for(int i=0; i< togglesTransform.childCount; i++)
        {
            //Debug.Log("------"+i+toggles[i].name);
            togglesTransform.GetChild(i).GetComponent<Toggle>().isOn = false;
        }
        reset = false;
        next_btn.interactable = false;
    }

    /// <summary>
    /// 初始化当前阶段
    /// </summary>
    private void InitStage()
    {
        sceneName = SceneManager.GetActiveScene().name;
        //sceneName = "FTE_0";
        if (sceneName == "FTE_0-1" || sceneName == "FTE_0-2")
        {

        }
        else
        {
            if (int.Parse(sceneName.Split('_')[1]) < NetworkMgr.My.playerDatas.fteProgress)
            {
                return;
            }
        }
        //TODO
        // 获取当前场景的第一个答题阶段
        for (int i = 0; i < OriginalData.My.answerStages.Length; i++)
        {
            if (OriginalData.My.answerStages[i].scene.Equals(sceneName))
            {
                currentStage = OriginalData.My.answerStages[i].stage;
                break;
            }
        }
        //ShowPanel();
    }

    /// <summary>
    /// 选择答案
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="choice"></param>
    private void SelectChioce(bool isOn, ChoiceItem choice)
    {
        if (reset)
        {
            return;
        }
        //mask.SetActive(true);
        if (currentQuestion.isMultiple)
        {
            if (isOn)
            {
                answer.Add(choice.ans_id.ToString());
            }
            else
            {
                answer.Remove(choice.ans_id.ToString());
            }
        }
        else
        {
            answer.Clear();
            if(isOn)
                answer.Add(choice.ans_id.ToString());
        }
        if (answer.Count > 0)
        {
            next_btn.interactable = true;
        }
        else
        {
            next_btn.interactable = false;
        }
    }

    /// <summary>
    /// 多选
    /// </summary>
    private void NextConfirm()
    {
        mask.SetActive(true);
        next_btn.interactable = false;
        if (currentQuestion.IsTrueAnswer(answer))
        {
            StartCoroutine(Next(true));
        }
        else
        {
            qDesc.text = currentQuestion.questionDesc;
            qDesc.gameObject.SetActive(true);
            errorCount -= 1;
            error_text.text = errorCount.ToString();
            if (errorCount <= 0)
            {
                //qDesc.gameObject.SetActive(false);
                //StartCoroutine(ShowReplay());
                isFail = true;
            }
            else
            {
                isFail = false;
            }
            wrong_btn.gameObject.SetActive(true);
            WrongAnswer();
        }
    }

    /// <summary>
    /// 阶段完成，继续
    /// </summary>
    private void Continue()
    {
        currentStage += 1;
        mask.SetActive(false);
        //gameObject.SetActive(false);
        transform.GetComponent<Image>().raycastTarget = false;
        ansPanel.SetActive(false);
        replayPanel.SetActive(false);
        //int updateFTE = 0;
        //if(sceneName == "FTE_0-1" || sceneName == "FTE_0-2")
        //{
        //        updateFTE = 1;
        //}else
        //if(int.Parse(sceneName.Split('_')[1]) <= NetworkMgr.My.playerDatas.fteProgress){
        //    updateFTE = 1;
        //}
        //if(updateFTE == 1)
        //{
        //    NetworkMgr.My.UpdatePlayerDatas(1, 0, () =>
        //    {
        //        if (continueGuide)
        //        {
        //            GuideManager.My.PlayNextIndexGuide();
        //        }
        //        else
        //        {
        //            doEnd?.Invoke();
        //        }
        //    });
        //}
        //else
        //{
            if (continueGuide)
            {
                isComplete = true;
                //  GuideManager.My.PlayNextIndexGuide();
            }
            else
            {
                doEnd?.Invoke();
            }
        //}
    }

    /// <summary>
    /// 重来
    /// </summary>
    private void Replay()
    {
        //PlayerData.My.Reset();
        //SceneManager.LoadScene(sceneName);
        questionIndex = 0;
        replayPanel.SetActive(false);
        error_image.SetActive(false);
        answer.Clear();
        ShowPanel(continueGuide, doEnd);
    }

    /// <summary>
    /// 显示Replay面板
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowReplay()
    {
        error_image.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        replay_btn.onClick.RemoveAllListeners();
        replay_btn.onClick.AddListener(() => Replay());
        passOFail_text.text = "答题失败！请重新来过！";
        replayPanel.SetActive(true);
        mask.SetActive(false);
        qDesc.gameObject.SetActive(false);
        mask.SetActive(false);
    }

    /// <summary>
    /// 切换下一题
    /// </summary>
    /// <param name="correct"></param>
    /// <returns></returns>
    IEnumerator Next(bool correct)
    {
        //if (correct)
        //{
            correct_image.SetActive(true);
        //}
        //else
        //{
            //error_image.SetActive(true);
            
        //}
        yield return new WaitForSeconds(0.3f);
        mask.SetActive(false);
        correct_image.SetActive(false);
        
        NextQuestion();
    }

    /// <summary>
    /// 答题错误
    /// </summary>
    void WrongAnswer()
    {
        error_image.SetActive(true);
    }

    /// <summary>
    /// 继续
    /// </summary>
    void WrongButton()
    {
        
        mask.SetActive(false);
        error_image.SetActive(false);
        qDesc.gameObject.SetActive(false);
        wrong_btn.gameObject.SetActive(false);
        if (isFail)
        {
            next_btn.interactable = false;
            StartCoroutine(ShowReplay());
        }
        else
        {
            NextQuestion();
        }
    }

    /// <summary>
    /// 设置问题类型
    /// </summary>
    /// <param name="isMultiple"></param>
    private void SetQuestionType(bool isMultiple)
    {
        if (isMultiple)
        {
            for(int i=0; i< togglesTransform.childCount; i++)
            {
                togglesTransform.GetChild(i).GetComponent<Toggle>().group = null;
            }
            //next_btn.gameObject.SetActive(true);
            multiple_tag.SetActive(true);
        }
        else
        {
            for (int i = 0; i < toggles.Count; i++)
            {
                togglesTransform.GetChild(i).GetComponent<Toggle>().group = toggleGroup;
            }
            toggleGroup.allowSwitchOff = true;
            //next_btn.gameObject.SetActive(false);
            multiple_tag.SetActive(false);
        }
    }

    Question GetRandomQuestion()
    {
        if (randomList.Count == 0)
        {
            return null;
        }
        int r = randomList[ UnityEngine.Random.Range(0, randomList.Count)];
        randomList.Remove(r);
        Debug.Log(r + "," + toDoQuestions.Count);
        return toDoQuestions[r];
    }

    void InitRandomList()
    {
        randomList = null;
        randomList = new List<int>();
        for(int i=0; i< toDoQuestionsCount; i++)
        {
            randomList.Add(i);
        }
    }
}

public class Choice
{
    public int choice_id;
    public string choice_text;

    public Choice(int id, string text)
    {
        choice_id = id;
        choice_text = text;
    }
}

[Serializable]
public struct AnswerStage
{
    public int stage;
    public string scene;
    public int questionCount;
    public int errorCount;
}