using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Question
{
    public int stage;
    public int necessary;
    public int question_id;
    public string question_text;

    public string choices;

    public string answer;

    public bool isMultiple;


    public bool IsTrueAnswer(List<string> select)
    {
        if (isMultiple)
        {
            if (select.Count == answer.Length)
            {
                for(int i=0; i< select.Count; i++)
                {
                    if (!answer.Contains(select[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        else
        {
            if (answer.Equals(select[0]))
            {
                return true;
            }
            return false;
        }
    }
}

[Serializable]
public class QuestionList
{
    public List<Question> questions;

    public List<Question> GetStageQuestions(int stage)
    {
        List<Question> qs = new List<Question>();
        for(int i=0; i< questions.Count; i++)
        {
            if(questions[i].stage == stage)
            {
                qs.Add(questions[i]);
            }
        }
        return qs;
    }
}