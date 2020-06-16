using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeWordsPanel : MonoBehaviour
{
    public Text quesion;

    public InputField answer_input;

    public Button submit_btn;
    // Start is called before the first frame update
    void Start()
    {
        submit_btn.onClick.AddListener(Submit);
        answer_input.onValueChanged.AddListener(OnEdit);
        answer_input.onEndEdit.AddListener(OnEndEdit);
        
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

    }
}
