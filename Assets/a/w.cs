using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class w : MonoBehaviour
{
    public Text te;
    // Start is called before the first frame update
    void Start()
    {
        te.DOText("100",3,true,ScrambleMode.Numerals);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
