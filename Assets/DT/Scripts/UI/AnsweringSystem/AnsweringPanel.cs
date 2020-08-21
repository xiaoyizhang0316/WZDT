using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class AnsweringPanel : MonoBehaviour
{
    #region component
    public ToggleGroup toggleGroup;
    
    public Toggle[] toggles;
    public Text question_text;
    public Text error_text;
    public Button continue_btn;
    public Button next_btn;
    public Button replay_btn;

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
    string sceneName = "";
    List<int> randomList;
    #endregion

    private void Start()
    {
        for(int i=0; i< toggles.Length; i++)
        {
            toggles[i].onValueChanged.AddListener((isOn) => SelectChioce(isOn, toggles[i].GetComponent<ChoiceItem>()));
        }
        next_btn.onClick.AddListener(NextConfirm);
        continue_btn.onClick.AddListener(Continue);
        replay_btn.onClick.AddListener(Replay);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void InitAnsweringPanel()
    {
        InitStage();
    }

    /// <summary>
    /// 显示答题面板
    /// </summary>
    public void ShowPanel()
    {
        // show error count
        // 获取数量，题目，和错误限制
        currentAnswerStage = OriginalData.My.answerStages[currentStage];
        if (currentAnswerStage.questionCount == -1)
        {
            gameObject.SetActive(false);
            return;
        }
        toDoQuestionsCount = currentAnswerStage.questionCount;
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
        for(int i=0; i< stageQuestions.Count;i++)
        {
            if (stageQuestions[i].necessary == 1 && toDoQuestions.Count<= toDoQuestionsCount)
            {
                toDoQuestions.Add(stageQuestions[i]);
                stageQuestions.RemoveAt(i);
            }
        }

        if (toDoQuestions.Count < toDoQuestionsCount)
        {
            for(int i=0; i< toDoQuestionsCount-toDoQuestions.Count; i++)
            {
                if (toDoQuestions.Count >= toDoQuestionsCount)
                {
                    break;
                }
                random = UnityEngine.Random.Range(0, stageQuestions.Count);
                toDoQuestions.Add(stageQuestions[random]);
                stageQuestions.RemoveAt(random);
            }
        }
    }

    /// <summary>
    /// 显示题目
    /// </summary>
    private void ShowQuestion()
    {
        ResetChoices();
        currentQuestion = GetRandomQuestion();
        if(currentQuestion == null)
        {
            mask.SetActive(true);
            continue_btn.gameObject.SetActive(true);
            questionIndex = 0;
            currentStage += 1;
            return;
        }
        SetQuestionType(currentQuestion.isMultiple);
        string[] qu = currentQuestion.question_text.Split('_');
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < qu.Length; i++)
        {
            if (i % 2 == 1)
            {
                sb.Append("<color=green>" + qu[i] + "</color>");
                continue;
            }
            sb.Append(qu[i]);
        }
        question_text.text = sb.ToString();

        string[] ans = currentQuestion.choices.Split('_');
        temp.Clear();
        for(int i=0; i < ans.Length; i++)
        {
            temp.Add(new Choice(i, ans[i]));
        }

        for(int i=0; i< toggles.Length; i++)
        {
            random = UnityEngine.Random.Range(0, temp.Count);
            toggles[i].GetComponent<ChoiceItem>().Setup(temp[random]);
            temp.RemoveAt(random);
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
            continue_btn.gameObject.SetActive(true);
            questionIndex = 0;
            currentStage += 1;
        }
        else
        {
            NextQuestion();
        }
    }

    /// <summary>
    /// 切换题目，重置选项
    /// </summary>
    private void ResetChoices()
    {
        reset = true;
        for(int i=0; i< toggles.Length; i++)
        {
            toggles[i].isOn = false;
        }
        reset = false;
    }

    /// <summary>
    /// 初始化当前阶段
    /// </summary>
    private void InitStage()
    {
        sceneName = SceneManager.GetActiveScene().name;

        //TODO
        // 获取当前场景的第一个答题阶段
        for(int i = 0; i < OriginalData.My.answerStages.Length; i++)
        {
            if (OriginalData.My.answerStages[i].scene.Equals(sceneName))
            {
                currentStage = OriginalData.My.answerStages[i].stage;
                break;
            }
        }

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
        if (currentQuestion.IsTrueAnswer(answer))
        {
            StartCoroutine(Next(true));
        }
        else
        {
            errorCount -= 1;
            error_text.text = errorCount.ToString();
            if (errorCount <= 0)
            {
                StartCoroutine(ShowReplay());
            }
            else
            {
                StartCoroutine(Next(false));
            }
        }
    }

    /// <summary>
    /// 阶段完成，继续
    /// </summary>
    private void Continue()
    {
        currentStage += 1;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 重来
    /// </summary>
    private void Replay()
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 显示Replay面板
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowReplay()
    {
        error_image.SetActive(true);
        yield return new WaitForSeconds(1);
        replayPanel.SetActive(true);
        mask.SetActive(false);
    }

    /// <summary>
    /// 切换下一题
    /// </summary>
    /// <param name="correct"></param>
    /// <returns></returns>
    IEnumerator Next(bool correct)
    {
        if (correct)
        {
            correct_image.SetActive(true);
        }
        else
        {
            error_image.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        mask.SetActive(false);
        correct_image.SetActive(false);
        error_image.SetActive(false);
        NextQuestion();
    }

    /// <summary>
    /// 设置问题类型
    /// </summary>
    /// <param name="isMultiple"></param>
    private void SetQuestionType(bool isMultiple)
    {
        if (isMultiple)
        {
            for(int i=0; i< toggles.Length; i++)
            {
                toggles[i].group = null;
            }
            //next_btn.gameObject.SetActive(true);
            multiple_tag.SetActive(true);
        }
        else
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].group = toggleGroup;
            }
            toggleGroup.allowSwitchOff = false;
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
        return toDoQuestions[r];
    }

    void InitRandomList()
    {
        randomList = null;
        randomList = new List<int>();
        for(int i=0; i< toDoQuestionsCount; i++)
        {
            randomList[i] = i;
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