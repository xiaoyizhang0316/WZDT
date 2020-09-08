using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class SoftFTE_Base : MonoBehaviour
{
    public Button nextButton;

    public float entryTime;

    public virtual void Init(RoleType type)
    {
        nextButton.onClick.AddListener(() =>
        {
            NextStep();
            gameObject.SetActive(false);
        });
        nextButton.interactable = false;
    }

    public void Play()
    {
        StartCoroutine(Check());
    }

    public virtual IEnumerator Check()
    {
        yield break;
    }

    public virtual void NextStep()
    {
        SoftFTE.My.PlayNext();
    }
    



}
