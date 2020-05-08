using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Image = UnityEngine.UI.Image;

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

        seedBrand_Old.text = oldseed.Brand.ToString();
        seedQulity_Old.text = oldseed.Quality.ToString();
        hookCrisp_Old.GetComponent<RectTransform>().localPosition  = new Vector3(oldseed.Crisp*20, 0,0 );
        hookSweetness_Old.GetComponent<RectTransform>().localPosition  = new Vector3(oldseed.Sweetness*20, 0,0 ); 
        seedBrand_New.text = newseed.Brand.ToString();
        seedQulity_New.text = newseed.Quality.ToString();
        hookCrisp_New.GetComponent<RectTransform>().localPosition  = new Vector3(newseed.Crisp*20, 0,0 );
        hookSweetness_New.GetComponent<RectTransform>().localPosition  = new Vector3(newseed.Sweetness*20, 0,0 );  
        confirmac = confirm;
        cancleac = cancle;
    }
}
