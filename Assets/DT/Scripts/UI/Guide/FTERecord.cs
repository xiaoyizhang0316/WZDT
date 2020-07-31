using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;

public class FTERecord : MonoBehaviour
{

    public List<BaseMapRole> scene0roles;
    public GameObject dustDirtyPoofSoft; //特效

    private ConsumerType ct;

    float waitTime = 0;
    int count = 0;

    int[] nums = new int[] { 3, 5 };

    private void Start()
    {
        StartCoroutine(CreateRoleAndTrade());
        BornEnemy();
    }

    private void BornEnemy()
    {
        StartCoroutine(GameObject.Find("Build/ConsumerSpot").GetComponent<Building>().BornEnemy());
    }

    private IEnumerator CreateRoleAndTrade()
    {
        yield return new WaitForSeconds(0.3f);
        scene0roles[0].transform.DOMoveY(0.3f, 0.5f).OnComplete(() => {
            GameObject go = Instantiate(dustDirtyPoofSoft, scene0roles[0].transform);
            Destroy(go, 1f);
            scene0roles[0].transform.DOScale(new Vector3(1.3f, 0.8f, 1.3f), 0.2f).OnComplete(() => {
                scene0roles[0].transform.DOScale(1f, 0.15f);
                //GuideMgr.My.ShowNextStep();
            });
        });

        yield return new WaitForSeconds(0.3f);
        scene0roles[1].transform.DOMoveY(0.3f, 0.5f).OnComplete(() => {
            GameObject go = Instantiate(dustDirtyPoofSoft, scene0roles[1].transform);
            Destroy(go, 1f);
            scene0roles[1].transform.DOScale(new Vector3(1.3f, 0.8f, 1.3f), 0.2f).OnComplete(() => {
                scene0roles[1].transform.DOScale(1f, 0.15f);
                //GuideMgr.My.ShowNextStep();
            });
        });
        yield return new WaitForSeconds(0.5f);
        
        scene0roles[2].transform.DOMoveY(0.3f, 0.5f).OnComplete(() => {
            GameObject go = Instantiate(dustDirtyPoofSoft, scene0roles[2].transform);
            Destroy(go, 1f);
            scene0roles[2].transform.DOScale(new Vector3(1.3f, 0.8f, 1.3f), 0.2f).OnComplete(() => {
                scene0roles[2].transform.DOScale(1f, 0.15f);
                //GuideMgr.My.ShowNextStep();
            });
        });
        yield return new WaitForSeconds(0.5f);
        
        scene0roles[3].transform.DOMoveY(0.3f, 0.5f).OnComplete(() => {
            GameObject go = Instantiate(dustDirtyPoofSoft, scene0roles[3].transform);
            Destroy(go, 1f);
            scene0roles[3].transform.DOScale(new Vector3(1.3f, 0.8f, 1.3f), 0.2f).OnComplete(() => {
                scene0roles[3].transform.DOScale(1f, 0.15f);
                //GuideMgr.My.ShowNextStep();
            });
        });
        yield return new WaitForSeconds(0.7f);
        TradeManager.My.AutoCreateTrade("0", "1");
        yield return new WaitForSeconds(0.5f);
        TradeManager.My.AutoCreateTrade("1", "2");
        yield return new WaitForSeconds(0.5f);
        TradeManager.My.AutoCreateTrade("2", "3");
        //RoleUp(3);
        StartCoroutine(RolesLevelUp());
    }

    private void RoleUp(float time)
    {
        waitTime = time;
        StartCoroutine(RolesLevelUp());
    }

    private IEnumerator RolesLevelUp()
    {
        yield return new WaitForSeconds(3);
        scene0roles[0].GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level = 3;
        scene0roles[1].GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level = 3;
        scene0roles[2].GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level = 3;
        scene0roles[3].GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level = 3;
        scene0roles[0].transform.DOScale(new Vector3(1.1f, 1.2f, 1.1f), 0.2f).OnComplete(() => {
            scene0roles[0].GetComponent<BaseMapRole>().CheckLevel();
            scene0roles[0].transform.DOScale(Vector3.one, 0.15f);
        });

        scene0roles[1].transform.DOScale(new Vector3(1.1f, 1.2f, 1.1f), 0.2f).OnComplete(() => {
            scene0roles[1].GetComponent<BaseMapRole>().CheckLevel();
            scene0roles[1].transform.DOScale(Vector3.one, 0.15f);
        });

        scene0roles[2].transform.DOScale(new Vector3(1.1f, 1.2f, 1.1f), 0.2f).OnComplete(() => {
            scene0roles[2].GetComponent<BaseMapRole>().CheckLevel();
            scene0roles[2].transform.DOScale(Vector3.one, 0.15f);
        });

        scene0roles[3].transform.DOScale(new Vector3(1.1f, 1.2f, 1.1f), 0.2f).OnComplete(() => {
            scene0roles[3].GetComponent<BaseMapRole>().CheckLevel();
            scene0roles[3].transform.DOScale(Vector3.one, 0.15f);
        });

        yield return new WaitForSeconds(3);
        scene0roles[0].GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level = 5;
        scene0roles[1].GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level = 5;
        scene0roles[2].GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level = 5;
        scene0roles[3].GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level = 5;
        scene0roles[0].transform.DOScale(new Vector3(1.1f, 1.2f, 1.1f), 0.2f).OnComplete(() => {
            scene0roles[0].GetComponent<BaseMapRole>().CheckLevel();
            scene0roles[0].transform.DOScale(Vector3.one, 0.15f);
        });

        scene0roles[1].transform.DOScale(new Vector3(1.1f, 1.2f, 1.1f), 0.2f).OnComplete(() => {
            scene0roles[1].GetComponent<BaseMapRole>().CheckLevel();
            scene0roles[1].transform.DOScale(Vector3.one, 0.15f);
        });

        scene0roles[2].transform.DOScale(new Vector3(1.1f, 1.2f, 1.1f), 0.2f).OnComplete(() => {
            scene0roles[2].GetComponent<BaseMapRole>().CheckLevel();
            scene0roles[2].transform.DOScale(Vector3.one, 0.15f);
        });

        scene0roles[3].transform.DOScale(new Vector3(1.1f, 1.2f, 1.1f), 0.2f).OnComplete(() => {
            scene0roles[3].GetComponent<BaseMapRole>().CheckLevel();
            scene0roles[3].transform.DOScale(Vector3.one, 0.15f);
        });
    }

    private IEnumerator DelayExcute(Action action, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        action();
        StopCoroutine("DelayExcute");
    }
}
