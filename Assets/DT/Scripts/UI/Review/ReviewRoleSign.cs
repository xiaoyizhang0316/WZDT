using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewRoleSign : MonoBehaviour
{
    public ReviewPanel.ReviewRole role;

    public bool isInit;
    public Vector3 pos;
    public Transform startparent;
    public Vector3 localpos;
    public int index;
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

    public void InitRole(ReviewPanel.ReviewRole role)
    {
        isInit = true;
        GetComponent<Image>().enabled = true;
        this.role = role;
        startparent = transform.parent;
        index = transform.GetSiblingIndex();
        transform.SetParent(ReviewManager.My.roles.transform);
    }

    public void ClearRole()
    {
        isInit = false;
        GetComponent<Image>().enabled = false;
        
        transform.SetParent(startparent);
        transform.SetSiblingIndex(index);
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
