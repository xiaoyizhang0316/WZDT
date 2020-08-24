using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseStep : MonoBehaviour
{
    public Button nextButton;
    public Text contenText;

    

    public virtual void StartCuttentStep()
    {
        
    }

    public virtual void StopCurrentStep()
    {
        
    }
}
