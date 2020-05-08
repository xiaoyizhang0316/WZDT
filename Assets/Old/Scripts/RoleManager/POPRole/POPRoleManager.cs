using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POPRoleManager : MonoBehaviour
{
    public Text rolename;
    public Image capacity;
    public Image efficiency;
    public Image brand; 
    public Image quality;

    public Transform acitvityTF;

    public GameObject acPrb;

    public GameObject Panel_Role;

    public bool InitPOPRole(BaseMapRole baseMapRole)
    {

        for (int i = 0; i <acitvityTF.childCount; i++)
        {
            Destroy(acitvityTF.GetChild(i).gameObject);
        }
        //Debug.Log(baseMapRole.baseRoleData.baseRoleData.roleType.ToString());
        rolename.text = baseMapRole.baseRoleData.baseRoleData.roleName;
        capacity.fillAmount =baseMapRole.baseRoleData.capacity / 150f  ;
        efficiency.fillAmount = baseMapRole.baseRoleData.efficiency / 150f   ;
        brand.fillAmount = baseMapRole.baseRoleData.brand / 150f  ;
        quality.fillAmount = baseMapRole.baseRoleData.quality / 150f  ;

        for (int i = 0; i < baseMapRole.AllPassivitySkills.Count; i++)
        {
            if (!baseMapRole.AllPassivitySkills[i].isLock  )
            {
                var game = Instantiate(acPrb, acitvityTF);
                game.transform.GetChild(0).GetComponent<Text>().text =
                    baseMapRole.AllPassivitySkills[i].SkillName;
            }

        }
        return  true;
        
    }
}
