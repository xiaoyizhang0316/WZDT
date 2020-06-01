using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpdateRole : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public bool isUpdate;
    public Image hammer;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isUpdate||RoleUpdateInfo.My.currentLevel ==5)
        {
            return;
        }

        RoleUpdateInfo.My.currentRole.baseRoleData = GameDataMgr.My.GetModelData(   RoleUpdateInfo.My.currentRole.baseRoleData .roleType,RoleUpdateInfo.My.nextLevel );
        
        RoleUpdateInfo.My.currentRole.CalculateAllAttribute();
        
        RoleUpdateInfo.My.ReInit( RoleUpdateInfo.My.currentRole);
        isUpdate = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isUpdate )
        {
            return;
        }

        RoleUpdateInfo.My.currentRole.baseRoleData = GameDataMgr.My.GetModelData(   RoleUpdateInfo.My.currentRole.baseRoleData .roleType,RoleUpdateInfo.My.currentLevel  );
        
        RoleUpdateInfo.My.currentRole.CalculateAllAttribute();
        
        RoleUpdateInfo.My.ReInit( RoleUpdateInfo.My.currentRole);   
        isUpdate = false;
    }

    private Tweener tew;
    public void OnPointerClick(PointerEventData eventData)
    {

        if (RoleUpdateInfo.My.currentLevel ==5|| (tew!=null &&  tew.IsPlaying()))
        {
            Debug.Log( hammer.transform.eulerAngles);
            return;
        }
        GetComponent<Image>().raycastTarget = false;
        tew=  hammer.transform.DOLocalRotate(new Vector3(0,0,-42f),0.3f ).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                RoleUpdateInfo.My.currentRole.baseRoleData = GameDataMgr.My.GetModelData(   RoleUpdateInfo.My.currentRole.baseRoleData .roleType,RoleUpdateInfo.My.nextLevel );
                RoleUpdateInfo.My.currentRole.CalculateAllAttribute();
                RoleUpdateInfo.My.currentRole.baseRoleData.roleName =  RoleUpdateInfo.My.roleName;
                RoleUpdateInfo.My.Init( RoleUpdateInfo.My.currentRole);

                tew=   hammer.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.3f);
           
                GetComponent<Image>().raycastTarget = true;

                if ( RoleUpdateInfo.My.currentRole.baseRoleData.level ==5)
                {
                   
                }
                else
                {
                    RoleUpdateInfo.My.currentRole.baseRoleData = GameDataMgr.My.GetModelData(   RoleUpdateInfo.My.currentRole.baseRoleData .roleType,RoleUpdateInfo.My.nextLevel );
        
                    RoleUpdateInfo.My.currentRole.CalculateAllAttribute();
        
                    RoleUpdateInfo.My.ReInit( RoleUpdateInfo.My.currentRole);
                }
 
              
            });

    }
}
