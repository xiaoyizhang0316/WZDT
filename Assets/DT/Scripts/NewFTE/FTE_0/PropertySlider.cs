using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PropertySlider : MonoBehaviour, IPointerUpHandler
{
    Slider slider;
    float trueValue = 0;

    public float minValue=0;
    public float maxValue=2;
    public ValueType valueType = ValueType.Effect;

    float lastValue = 0;


    private void Start()
    {
        slider = transform.GetComponent<Slider>();
        slider.maxValue = maxValue;
        slider.minValue = minValue;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnValueChange(slider.value);
    }

    public void OnValueChange(float value)
    {
        if (value > 0.5f && value < 1.5f)
        {
            trueValue = 1;
        }
        else
        {
            if (value >= 1.5f)
            {
                trueValue = 2;
            }
            else
            {
                trueValue = 0;
            }
        }
        slider.value = trueValue;
        if(lastValue == trueValue)
        {
            return;
        }
        SetBaseMapRolePropertys();
    }

    void SetBaseMapRolePropertys()
    {
        if (!RoleEditor.My.isDragEnd)
        {
            transform.parent.GetComponent<FTE_01_Step15>().sliderValue = (int)trueValue;
            transform.parent.GetComponent<FTE_01_Step15>().currentSlider.transform.parent.GetComponent<RoleSet>().RefreshValues((int)lastValue, (int)trueValue, valueType);
        }
        else
        {
            transform.parent.GetComponent<RoleSet>().RefreshValues((int)lastValue, (int)trueValue, valueType);
        }
        lastValue = trueValue;
    }
}
