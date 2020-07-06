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
    public List<Color> haveColoe; 
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
        GetComponent<Image>().enabled = true;
        this.role = role;
 

     
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
        GetComponent<Image>().enabled = false;
        isBuffRole = false;
        transform.SetParent(startparent);
        transform.SetSiblingIndex(index);
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
