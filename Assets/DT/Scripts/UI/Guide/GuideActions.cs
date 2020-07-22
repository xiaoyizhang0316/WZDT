using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GuideActions : MonoBehaviour
{
    public List<List<Action>> actionsList;

    public GameObject timer;
   // public List<List<int>> waitTimesList;


    #region scene 0 property
    public List<BaseMapRole> scene0roles;
    public GameObject dustDirtyPoofSoft; //特效
    #endregion

    private void Awake()
    {
        
    }

    public void InitAllActions()
    {
        actionsList = new List<List<Action>>();
        actionsList.Add(Scene1Actions());
    }

    private List<Action> Scene1Actions()
    {
        return new List<Action>() {()=> {
            scene0roles[0].transform.DOMoveY(0.3f, 1f).OnComplete(()=>{
                GameObject go = Instantiate(dustDirtyPoofSoft, scene0roles[0].transform);
                Destroy(go,1f);
                scene0roles[0].transform.DOScale(new Vector3(1.3f,0.8f,1.3f), 0.2f).OnComplete(()=> {
                            scene0roles[0].transform.DOScale(1f, 0.15f);
                    GuideMgr.My.ShowNextStep();
                });
            });
        },
        ()=> {
            scene0roles[1].transform.DOMoveY(0.3f, 1f).OnComplete(()=>{
                GameObject go = Instantiate(dustDirtyPoofSoft, scene0roles[1].transform);
                Destroy(go,1f);
                scene0roles[1].transform.DOScale(new Vector3(1.3f,0.8f,1.3f), 0.2f).OnComplete(()=> {
                            scene0roles[1].transform.DOScale(1f, 0.15f);
                    GuideMgr.My.ShowNextStep();
                });
            });
        },
        ()=> {
            scene0roles[2].transform.DOMoveY(0.3f, 1f).OnComplete(()=>{
                GameObject go = Instantiate(dustDirtyPoofSoft, scene0roles[2].transform);
                Destroy(go,1f);
                scene0roles[2].transform.DOScale(new Vector3(1.3f,0.8f,1.3f), 0.2f).OnComplete(()=> {
                            scene0roles[2].transform.DOScale(1f, 0.15f);
                    GuideMgr.My.ShowNextStep();
                });
            });
        },
        ()=> {
            scene0roles[3].transform.DOMoveY(0.3f, 1f).OnComplete(()=>{
                GameObject go = Instantiate(dustDirtyPoofSoft, scene0roles[3].transform);
                Destroy(go,1f);
                scene0roles[3].transform.DOScale(new Vector3(1.3f,0.8f,1.3f), 0.2f).OnComplete(()=> {
                            scene0roles[3].transform.DOScale(1f, 0.15f);
                    GuideMgr.My.ShowNextStep();
                });
            });
        },
        ()=>{
            TradeManager.My.AutoCreateTrade("0", "1");
            StartCoroutine(DelayExcute(()=>TradeManager.My.AutoCreateTrade("1", "2"), 1f));
            StartCoroutine(DelayExcute(()=>TradeManager.My.AutoCreateTrade("2", "3"), 2f));
            StartCoroutine(DelayExcute(()=>GameObject.Find("Build/ConsumerSpot").GetComponent<Building>().SpawnConsumer(1), 7f));
            //GuideMgr.My.ShowNextStep();
            StartCoroutine(DelayExcute(()=>GuideMgr.My.ShowNextStep(), 20f));
        },
        ()=>{
            PlayerPrefs.SetInt("FTE0End",1);
            if (NetworkMgr.My.isUsingHttp)
            {
                UpdateFte();
            }
            PlayerData.My.Reset();
            SceneManager.LoadScene("FTE_1");
            
        }
        };
    }

    private void UpdateFte()
    {
        NetworkMgr.My.UpdatePlayerDatas(1, 0);
    }


    private IEnumerator DelayExcute(Action action, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        action();
        StopCoroutine("DelayExcute");
    }
}
