using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewRoleSign : MonoBehaviour
{
    public ReviewPanel.ReviewRole role;

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
        
    }
}
