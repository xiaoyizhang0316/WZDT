using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TalentButton : MonoBehaviour
{
    public Image CanAddTalent;

    public Text restPoint;

    public Tweener twe;

    public void CheckAvailableTalent()
    {
        if (TalentPanel.My.totalPoint - TalentPanel.My.usedPoint > 0)
        {
            CanAddTalent.gameObject.SetActive(true);
            restPoint.text = (TalentPanel.My.totalPoint - TalentPanel.My.usedPoint).ToString();
            ShowAvailalbeInfo();
        }
        else
        {
            CanAddTalent.gameObject.SetActive(false);
            twe.Kill();
            isRunning = false;
        }
    }

    private bool isRunning = false;

    public float z;

    public void ShowAvailalbeInfo()
    {
        if (isRunning)
        {
            return;
        }
        else
        {
            isRunning = true;
            twe = CanAddTalent.transform.DOShakeRotation(0.5f, new Vector3(0f, 0f, z),3,90).OnComplete(()=> {
                isRunning = false;
            });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckAvailableTalent", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
