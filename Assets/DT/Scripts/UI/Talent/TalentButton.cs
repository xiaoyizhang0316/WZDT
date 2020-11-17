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
        try
        {
            if (NetworkMgr.My.levelProgressList.Count >= 4)
            {
                transform.DOScale(1, 0).Play();
            }
            else
            {
                transform.DOScale(0, 0).Play();
            }
        }
        catch (System.Exception ex)
        {

        }
    }

    private bool isRunning = false;

    public void ShowAvailalbeInfo()
    {
        if (isRunning)
        {
            return;
        }
        else
        {
            isRunning = true;
            twe = CanAddTalent.transform.DORotate(new Vector3(0f,0f,-8f),0.03f).Play().OnComplete(()=> {
                twe = CanAddTalent.transform.DORotate(new Vector3(0f, 0f, 8f), 0.06f).Play().OnComplete(() =>
                {
                    twe = CanAddTalent.transform.DORotate(new Vector3(0f, 0f, -8f), 0.06f).Play().OnComplete(() =>
                    {
                        twe = CanAddTalent.transform.DORotate(new Vector3(0f, 0f, 8f), 0.06f).Play().OnComplete(()=> {
                            twe = CanAddTalent.transform.DORotate(new Vector3(0f, 0f, -8f), 0.06f).Play().OnComplete(() =>
                            {
                                twe = CanAddTalent.transform.DORotate(new Vector3(0f, 0f, 8f), 0.06f).Play().OnComplete(() => {
                                    CanAddTalent.transform.DORotate(new Vector3(0f, 0f, 0f), 0f).Play();
                                    CanAddTalent.transform.DORotate(new Vector3(0f, 0f, 0f), 1f).Play().OnComplete(()=> {
                                        isRunning = false;
                                    });
                                });
                            });
                        });
                    });
                });
            });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(0, 0).Play();
        InvokeRepeating("CheckAvailableTalent", 0f, 1f);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
