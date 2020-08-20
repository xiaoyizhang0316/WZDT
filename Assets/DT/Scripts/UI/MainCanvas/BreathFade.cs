using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 周期性呼吸淡入/淡出特效
/// </summary>
public class BreathFade : MonoBehaviour
{
    public bool isStart = false;

    public void BreathIn()
    {
        isStart = true;
        GetComponent<Image>().DOFade(0.3f, 1.5f).OnComplete(BreathOut).Play().timeScale = 1f / DOTween.timeScale;
    }


    public void BreathOut()
    {
        GetComponent<Image>().DOFade(0.1f, 1.5f).OnComplete(BreathIn).Play().timeScale = 1f / DOTween.timeScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && !isStart)
        {
            BreathIn();
        }
    }
}
