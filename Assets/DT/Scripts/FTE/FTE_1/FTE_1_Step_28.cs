using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_Step_28 : BaseStep
{
    public GameObject Mark;

    public UpdateRole button;

    public List<Button> LockButton;
    public List<GameObject> panels;
    // Start is called before the first frame update
    void Start()
    {
        contenText.color = new Color(1,1,1,0);
        nextButton.interactable = false; 
        Mark.SetActive(false);
        InvokeRepeating("CheckStart",0,0.02f); 
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        nextButton.gameObject.SetActive(false);
        for (int i = 0; i < LockButton.Count; i++)
        {
            LockButton[i].interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public override void StartCuttentStep()
    {
        for (int i = 0; i <panels.Count; i++)
        {
            panels[i].gameObject.SetActive(false);
        }
        //Debug.Log("打开升级");
        LockButton[0].interactable = true;
        NewCanvasUI.My.GamePause();
        nextButton.gameObject.SetActive(true);
        contenText.DOFade(1, 0.3f).Play();
        NewCanvasUI.My.Panel_Update.SetActive(true);
        RoleUpdateInfo.My.Init(PlayerData.My.RoleData[0]);
         
            Mark.SetActive(true);
            nextButton.interactable = true;
            FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
    }

    public override void StopCurrentStep()
    {
        //Debug.Log("点击升级");
        nextButton.interactable = false;
        button.UpdateRole1();
        StageGoal.My.CostPlayerGold(RoleUpdateInfo.My.currentRole.baseRoleData.upgradeCost);
        StageGoal.My.Expend(RoleUpdateInfo.My.currentRole.baseRoleData.upgradeCost, ExpendType.AdditionalCosts, null, "升级");
        button.   UpgradeRoleRecord(RoleUpdateInfo.My.currentRole);
        Mark.SetActive(false);
        NewCanvasUI.My.GameNormal();
        NewCanvasUI.My.Panel_Update.SetActive(false);
        gameObject.SetActive(false);
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
        FTESceneManager.My.Steps[28].gameObject.SetActive(true);
        FTESceneManager.My.currentIndex++;

    }

 

    public void CheckStart()
    {
        //Debug.Log("当前时间"+StageGoal.My.timeCount );
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
        if (StageGoal.My.timeCount > 135 &&StageGoal.My.playerGold>3000)
        {
            //Debug.Log("检查打开");
            StartCuttentStep();
            CancelInvoke("CheckStart");
        }

        else
        {
            LockButton[0].interactable = false;
        }
    }
}
