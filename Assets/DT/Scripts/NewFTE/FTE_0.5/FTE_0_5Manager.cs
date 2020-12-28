using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_0_5Manager : MonoSingleton<FTE_0_5Manager>
{


    public GameObject seerJC1;
    public GameObject seerJC2;
    public GameObject dealerJC1;
    public GameObject dealerJC2;

    public Material sn;
    public Material sr;
    public Material sg;
    public Material bn;
    public Material br;
    public Material bg;
    
    public Renderer seerJC1_ran;
    public Renderer seerJC2_ran;
    public Renderer dealerJC1_ran;
    public Renderer dealerJC2_ran;

    public void Start()
    {
        seerJC1.SetActive(false);
        seerJC2.SetActive(false);
        dealerJC1.SetActive(false);
        dealerJC2.SetActive(false);
    }

    public void UpRole(GameObject role)
    {
        role.SetActive(true);
        role.transform.DOLocalMoveY(0,0.5f).Play();
        
    }

    public void DownRole(GameObject role)
    {
        role.SetActive(false);

        role.transform.DOLocalMoveY(-5,0.5f).Play();
        
    }



    public void ChangeColor(Renderer role,Material mat)
    {
        role.material = mat;
    }
}
