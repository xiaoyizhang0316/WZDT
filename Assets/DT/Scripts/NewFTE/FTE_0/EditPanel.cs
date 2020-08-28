using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPanel : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 2;
        slider.minValue = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SliderValueChanged(float value)
    {
        if(value>0.5f && value < 1.5f)
        {
            value = 1;
        }

        if (value >= 1.5f)
        {
            value = 2;
        }

        if (value <= 0.5f)
        {
            value = 0;
        }

    }
}
