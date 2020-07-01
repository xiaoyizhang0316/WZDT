using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewRoleSign : MonoBehaviour
{
    public ReviewPanel.ReviewRole role;

    public Vector3 pos;
    
    public Vector3 localpos;
    void Awake()
    {
        ReviewManager.My.signs.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        localpos = transform.localPosition;
    }
}
