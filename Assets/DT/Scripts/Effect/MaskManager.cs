using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class MaskManager : MonoSingleton<MaskManager>
{
    public DarkEffect darkEffect;


    public void Open(int Index,int endRadius)
    {
        StartCoroutine(OpenMask(Index,endRadius));
        InvokeRepeating("FadeBG",0,0.05f);
    }
    public void Open( )
    {
     
        InvokeRepeating("FadeBG",0,0.05f);
    }
    public void Close(int Index,int endRadius)
    {
        StartCoroutine(CloseMask(Index ));

        InvokeRepeating("CloseFadeBG",0,0.05f);
    }
    public void Close( )
    { 

        InvokeRepeating("CloseFadeBG",0,0.05f);
    }

    public void FadeMask(int Index,int endRadius)
    {
        StartCoroutine(OpenMask(Index,endRadius));
    }

    public void CloseMaks(int Index )
    {
        StartCoroutine(CloseMask(Index ));

      
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeBG()
    {
        if (darkEffect._darkColor.a <= 0.4f)
        {
            Debug.Log("改变背景颜色"+darkEffect._darkColor.a);
            darkEffect._darkColor = new Color(0,0,0,   darkEffect._darkColor.a+0.32f);
        }
        else
        {
            CancelInvoke("FadeBG");
        }
    }
 
    public void CloseFadeBG()
    {
        if (darkEffect._darkColor.a >0)
        {
            darkEffect._darkColor = new Color(0,0,0,   darkEffect._darkColor.a-0.32f);
        }
        else
        {
            CancelInvoke("CloseFadeBG");
        }
    }

    public IEnumerator OpenMask(int index,int radius)
    {
        darkEffect._items[index].radius = 0;

        for (int i = 0; i <= radius; i++)
        {
            darkEffect._items[index].radius = i;
            i+=4;
            yield return null;
        }
    }
    public IEnumerator CloseMask(int index )
    { 

        for (int i =   darkEffect._items[index].radius ; i >=0; i--)
        {
            darkEffect._items[index].radius = i;
            i-=4;
            yield return null;
        }
    }
}
