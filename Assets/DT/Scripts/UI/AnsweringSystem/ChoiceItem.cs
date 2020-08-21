using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceItem : MonoBehaviour
{
    public Text ans_text;

    public int ans_id=0;

    public void Setup(Choice choice)
    {
        ans_text.text = choice.choice_text;
        ans_id = choice.choice_id;
    }
}
