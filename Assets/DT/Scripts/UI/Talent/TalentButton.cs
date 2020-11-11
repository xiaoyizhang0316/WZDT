using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentButton : MonoBehaviour
{
    public Image CanAddTalent;

    public Text restPoint;

    public void CheckAvailableTalent()
    {
        if (TalentPanel.My.totalPoint - TalentPanel.My.usedPoint > 0)
        {
            CanAddTalent.gameObject.SetActive(true);
            restPoint.text = (TalentPanel.My.totalPoint - TalentPanel.My.usedPoint).ToString();
        }
        else
        {
            CanAddTalent.gameObject.SetActive(false);
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
