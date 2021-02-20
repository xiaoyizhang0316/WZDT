using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoleListManager : MonoSingleton<RoleListManager>
{
    /// <summary>
    /// 角色列表物体创建预制体
    /// </summary>
    public GameObject roleListSignOBJ;

    public Transform roleListCreatPos;

    public Transform inPos;
    public Transform outPos;

    public Button inButton;
    public Button outButton;

   
    /// <summary>
    /// 切换角色和道具
    /// </summary>
    public Button change;
    
    public GameObject Panel_ChoseRole;


    public List<GameObject> changeObj;
    // Start is called before the first frame update
    void Start()
    {
        change.onClick.AddListener(() => { Change(); });

        outButton.onClick.AddListener(() => { OutButton(); });
        inButton.onClick.AddListener(() =>
        {
            outButton.interactable = false;
            transform.DOMove(inPos.position,0.3f).OnComplete(() =>
            {
                inButton.gameObject.SetActive(false);
                outButton.gameObject.SetActive(true);
                outButton.interactable = true;
            }).Play().timeScale = 1f / DOTween.timeScale;
        });
    }

    public void OutButton(bool isImmediately = false)
    {
        outButton.interactable = false;
        
        if (isImmediately)
        {
            transform.position = outPos.position;
            outButton.gameObject.SetActive(false);
        
            inButton.gameObject.SetActive(true);
          inButton.interactable = true;
        }
        else
        {
 

        transform.DOMove(outPos.position,0.3f).OnComplete(() =>
        {
            outButton.gameObject.SetActive(false);
             inButton.gameObject.SetActive(true);
            inButton.interactable = true;
        }).Play().timeScale = 1f / DOTween.timeScale;
        }
    }

    public void InButton()
    {
        outButton.interactable = false;
        transform.DOMove(inPos.position, 0.3f).OnComplete(() =>
        {
            inButton.gameObject.SetActive(false);
            outButton.gameObject.SetActive(true);
            outButton.interactable = true;
        }).Play().timeScale = 1f / DOTween.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void Change(Action playEnd=null)
    {
        change.interactable = false;
        changeObj[0].transform.DOLocalMoveY(changeObj[0].transform.localPosition.y + 100, 0.2f).OnComplete(() =>
        {
            changeObj[0].transform.SetSiblingIndex( changeObj[0].transform.GetSiblingIndex()-1);
         
            changeObj[0].transform.DOLocalMoveY(changeObj[0].transform.localPosition.y-100, 0.2f).OnComplete(() =>
            { 
                changeObj.Add(changeObj[0]);
                changeObj.RemoveAt(0);
                change.interactable = true;
                if(playEnd!=null)
                playEnd();
            }).Play();
            
        }).Play();
        
    }

    /// <summary>
    /// 创建玩家角色列表  刷新玩家角色列表
    /// </summary>
    //public void UpdateRoleList()
    //{
    //    for (int i = 0; i < PlayerData.My.RoleManager.Count; i++)
    //    {
    //        Destroy(PlayerData.My.RoleManager[i]);
    //    }

    //    PlayerData.My.RoleManager.Clear();
    //    for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
    //    {
    //        if (PlayerData.My.RoleData[i].isNpc)
    //        {
    //            continue;
    //        }

    //        GameObject roleListSign = Instantiate(roleListSignOBJ, roleListCreatPos);
    //        roleListSign.GetComponent<CreatRole_Button>().RolePrb =
    //            Resources.Load<GameObject>(PlayerData.My.RoleData[i].baseRoleData.PrePath);
    //        roleListSign.transform.GetChild(0).GetComponent<Image>().sprite =
    //            Resources.Load<Sprite>(PlayerData.My.RoleData[i].baseRoleData.SpritePath);
    //        roleListSign.name = PlayerData.My.RoleData[i].baseRoleData.roleType.ToString() + "_" +
    //                            PlayerData.My.RoleData[i].ID;
    //        PlayerData.My.RoleManager.Add(roleListSign);
    //        if (PlayerData.My.RoleData[i].inMap)
    //        {
    //            //roleListSign.GetComponent<Button>().interactable = false;
    //            //roleListSign.GetComponent<Image>().raycastTarget = false;
    //        }
    //    }
    //}
}