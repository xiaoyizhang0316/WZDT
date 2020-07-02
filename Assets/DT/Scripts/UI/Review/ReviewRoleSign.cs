using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewRoleSign : MonoBehaviour
{
    public ReviewPanel.ReviewRole role;

    public Vector3 localPos;

    public Vector3 pos;

    public Vector3 parentLocal;

    public Vector3 parentPos;

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
        localPos = transform.localPosition;
        pos = transform.position;
        parentLocal = transform.parent.localPosition;
        parentPos = transform.parent.position;
    }
}
