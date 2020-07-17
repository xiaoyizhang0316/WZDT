using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReviewRoleSign : MonoBehaviour,IPointerClickHandler
{
    public ReviewPanel.ReviewRole role;

    public bool isInit;
    public Vector3 pos;
    public Transform startparent;
    public Vector3 localpos;
    public int index;
    public bool isBuffRole;
    public  Color  haveColoe = Color.white; 
    void Awake()
    {
        startparent = transform.parent;
    }

    // Start is called before the first frame update
    void Start()
    {
        ClearRole();
        pos = transform.position;
        localpos = transform.localPosition;
 
    }

    public void InitRole(ReviewPanel.ReviewRole role,bool isbuffRole)
    {
        isBuffRole = isbuffRole;
        isInit = true;
       transform.GetChild(1). GetComponent<Image>().enabled = true;
        this.role = role;

        if (role.isNPC)
        {
            if (Resources.Load<Sprite>("Sprite/npcF/" + role.roleType.ToString()) != null) 
            transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/npcF/"+role.roleType.ToString() );
        }

    }

    public void ChangeParent()
    {
        startparent = transform.parent;
        index = transform.GetSiblingIndex();
        transform.SetParent(ReviewManager.My.roles.transform);
    }

    
    public void ClearRole()
    {
        isInit = false;
        transform.GetChild(1).   GetComponent<Image>().enabled = false;
        isBuffRole = false;
        transform.SetParent(startparent);
        transform.SetSiblingIndex(index);
        haveColoe = Color.white; 
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void OnPointerClick(PointerEventData eventData)
    {
         ReviewBuffManager.My.InitRoleBuff(this);
    }
}
