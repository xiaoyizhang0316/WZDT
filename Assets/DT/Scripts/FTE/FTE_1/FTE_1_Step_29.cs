using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_Step_29 : BaseStep
{

    public Button sign;

    public List<GameObject> panels;

    public Text landText;
    public List<Button> Unlockbuttons;
    // Start is called before the first frame update
    void Start()
    {
        contenText.color = new Color(1,1,1,0);
       
        landText.color = new Color(1,1,1,0);
        InvokeRepeating("CheckStart",1,1);

        
      
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
        RoleListManager.My. OutButton(); 
        sign.interactable = true;
        MaskManager.My.Open(12,80);
        MaskManager.My.Open(28,130);
        NewCanvasUI.My.GamePause();
       
        contenText.DOFade(1, 0.3f).Play();
        landText.DOFade(1, 0.3f).Play();

        PlayNext();
            
            
            FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
    }

    public override void StopCurrentStep()
    {
        Debug.Log("点击升级");
       
        MaskManager.My.Close(12,0);
        MaskManager.My.Close(28,0);
        for (int i = 0; i <Unlockbuttons.Count; i++)
        {
            Unlockbuttons[i].interactable = true;
        }
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
        gameObject.SetActive(false);
      FTESceneManager.My.PlayNextStep();
      
    }
    public void InitMap()
    {
        for (int i = 0; i <     MapManager.My._mapSigns.Count; i++)
        {
         
            MapManager.My._mapSigns[i].isCanPlace = false;
        }
        MapManager.My.ReleaseLand(16,19);
    }
    public void PlayNext()
    {
        Debug.Log("检测");
        if (!MapManager.My.GetMapSignByXY(16, 19).isCanPlace)
        {
            Debug.Log("检测成功");

            StopCurrentStep();
        }
        else
        {
            gameObject.transform.DOScale(1, 0.1f).OnComplete(() =>
            {
                Debug.Log("检测失败");

                PlayNext();
            }).Play();
        }
    }

    public void CheckStart()
    {
        Debug.Log("当前时间"+StageGoal.My.timeCount +"Game"+gameObject.name);
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
        if (StageGoal.My.timeCount > 190
            )
        {
            Debug.Log("检查打开");
            InitMap();
            StartCuttentStep();
            CancelInvoke("CheckStart");
        }

        else
        {
             
        }
    }
}
