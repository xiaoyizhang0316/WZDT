using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;

public class FTE_0_5_3 : BaseGuideStep
{

    public BaseMapRole maprole;
    // Start is called before the first frame update
 
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
      NewCanvasUI.My.GameNormal();
      maprole.OnMoved += ChangeColor;
     FTE_0_5Manager.My.UpRole( FTE_0_5Manager.My.seerJC1);
      maprole.ringEffect.SetActive(true);
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            PlayerData.My.MapRole[i].tradeButton.transform.localScale = Vector3.zero;
            
            PlayerData.My.MapRole[i].tradeButton.gameObject.SetActive(true);
            PlayerData.My.MapRole[i].tradeButton.transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutBounce).Play();
        }
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator StepEnd()
    {
       
        missiondatas.data[0].isFinish= true; 
        
      yield break; 
    }

    public override bool ChenkEnd()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if(   PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
                PlayerData.My.MapRole[i].ringEffect.SetActive(true);
        }
        TradeManager.My.HideAllIcon();
        if (maprole.warehouse.Count > 10)
        {
            return true;
        }
        else
        {
            missiondatas.data[0].currentNum = maprole.warehouse.Count; 
            return false;

        }

      
    }


    public void ChangeColor(ProductData data)
    {
        if (data.damage > 0)
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.seerJC1_ran,FTE_0_5Manager.My.sg );
        }

        else
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.seerJC1_ran,FTE_0_5Manager.My.sr ); 
        }
    }
}
