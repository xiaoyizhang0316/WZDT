using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_0_5_5 : BaseGuideStep
{
  
   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        NewCanvasUI.My.Panel_Update.GetComponent<ProductDetalUI>().panel.SetActive(false);
        NewCanvasUI.My.Panel_Update.SetActive(false);
       // dailog.SetActive(true);

        PlayerData.My.GetNewGear(90000);
        PlayerData.My.GetNewGear(90001);
        PlayerData.My.GetNewGear(90002);
        PlayerData.My.GetNewGear(90003);
        PlayerData.My.GetNewGear(90016);
        PlayerData.My.GetNewGear(90017);
        PlayerData.My.GetNewGear(90018);
        PlayerData.My.GetNewGear(90019);
        yield return new WaitForSeconds(1f);
        

    }

    public override IEnumerator StepEnd()
    {
      //  boxobj.SetActive(true );

        for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                PlayerData.My.MapRole[i].emptyGearSprite.transform.DOPunchScale(new Vector3(1.3f,1.3f,1.3f), 1f,1).SetEase(Ease.OutBounce).SetLoops(5).Play();
            }
        }
        yield return new WaitForSeconds(1f);
      //  boxobj.SetActive(false );


    }

 
  
 
}
