using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SeedChangeManager : MonoBehaviour
{
    public Button confirm;

    public Button cancle;

    public Action confirmac;
    public Action cancleac;
    public Image hookCrisp_Old;
    public Image hookSweetness_Old;
    public Text seedQulity_Old;
    public Text seedBrand_Old;    
    public Text seedQulity_New;
    public Text seedBrand_New;
    public Image hookCrisp_New;
    public Image hookSweetness_New; 
    // Start is called before the first frame update
    void Start()
    {
        confirm.onClick.AddListener(() =>
        {
            if (confirmac != null)
            {
                confirmac();

            }

        });
        cancle.onClick.AddListener(() =>
        {
            if (cancleac != null)
            {
                cancleac(); 
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitPanel(ProductData oldseed,ProductData newseed,Action confirm, Action cancle)
    {
         confirmac = confirm;
        cancleac = cancle;
    }
}
