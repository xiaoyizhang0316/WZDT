using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoneyDoRotate : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().DOFade(0.3f, 0f).Play();
        transform.GetChild(0). GetComponent<Text>().DOFade(0.3f, 0).Play();
        transform.localEulerAngles = new Vector3(0, 0, -transform.parent.parent.transform.localEulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(0,0,-transform.parent.parent.transform.localEulerAngles.z);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("进入");
        GetComponent<Image>().DOFade(1, 0.3f).Play();
       transform.GetChild(0). GetComponent<Text>().DOFade(1, 0.3f).Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("离开");
        GetComponent<Image>().DOFade(0.3f, 0.3f).Play();
        transform.GetChild(0). GetComponent<Text>().DOFade(0.3f, 0.3f).Play();
    }
}
