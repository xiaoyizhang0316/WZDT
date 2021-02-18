﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TradeCostTip : MonoBehaviour
{
    public Text tip_text;

    public void ShowTipText(string tip)
    {
        if (SceneManager.GetActiveScene().name.Equals("FTE_1.5"))
        {
            /*if (FTE_1_5_Manager.My.showTradeCost)
            {
                tip_text.text = tip;
            }
            else
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }*/
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        tip_text.text = "0";
    }

    private void Update()
    {
        //transform.LookAt(Camera.main.transform.position+new Vector3(0,180,0));
        if (SceneManager.GetActiveScene().name.Equals("FTE_1.5"))
        {
            /*if (FTE_1_5_Manager.My.showTradeCost == false)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }*/
        }
    }
    
    Camera referenceCamera;
 
    public enum Axis {up, down, left, right, forward, back};
    public bool reverseFace = false; 
    public Axis axis = Axis.up; 
 
    // return a direction based upon chosen axis
    public Vector3 GetAxis (Axis refAxis)
    {
        switch (refAxis)
        {
            case Axis.down:
                return Vector3.down; 
            case Axis.forward:
                return Vector3.forward; 
            case Axis.back:
                return Vector3.back; 
            case Axis.left:
                return Vector3.left; 
            case Axis.right:
                return Vector3.right; 
        }
 
        // default is Vector3.up
        return Vector3.up;         
    }
 
    void  Awake ()
    {
        // if no camera referenced, grab the main camera
        if (!referenceCamera)
            referenceCamera = Camera.main; 
    }
    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate ()
    {
        // rotates the object relative to the camera
        Vector3 targetPos = transform.position + referenceCamera.transform.rotation * (reverseFace ? Vector3.forward : Vector3.back) ;
        Vector3 targetOrientation = referenceCamera.transform.rotation * GetAxis(axis);
        transform.LookAt (targetPos, targetOrientation);
    }
}
