using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootTest : MonoBehaviour
{
    public Button test;

    public BulletLaunch lunch;

    public Vector3 target;

    public GameObject game;

    public Transform targettf;

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        test.onClick.AddListener(() =>
        {
            lunch.LanchNormalTest(targettf.position,game,time);
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
