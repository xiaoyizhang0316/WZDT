using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework;
using System;

public class TestData : MonoSingleton<TestData>
{
    public AnswerStage[] answerStages;
    public QuestionList questionList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ReadQuestionList(Action doEnd)
    {
        //questionList = JsonUtility.FromJson<QuestionList>(jsonDatas.questions);

        WWW www = new WWW(@"file://" + Application.streamingAssetsPath + @"/Data/Questions.json");
        yield return www;
        if (www.isDone)
        {
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield return null;
            }
            else
            {
                string json = www.text.ToString();
                questionList = JsonUtility.FromJson<QuestionList>(json);
            }
            doEnd();
        }
    }
}
