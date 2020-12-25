using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSign : MonoBehaviour
{

    public Text contentText;

    public Text currentNum;

    public Text maxNum;

    public MissionData data;

    public Image sign;

    private Color signColor;
    // Start is called before the first frame update
    void Start()
    {
        MissionManager.My.signs.Add(this);
        signColor = sign.color;
    }

    // Update is called once per frame
    void Update()
    {
        currentNum.text = this.data.currentNum.ToString();
        if (data.isFinish)
        {
            sign.color = Color.green;
        }
        else
        {
            sign.color = signColor;
        }
    }

    public void Init(MissionData data)
    {
        this.data = data;
        contentText.text = this.data.content;
        if (data.maxNum > 0)
        {
            currentNum.text = this.data.currentNum.ToString();
            maxNum.text = "/"+this.data.maxNum.ToString();
        }
        else
        {
            currentNum.gameObject.SetActive(false);
            maxNum.gameObject.SetActive(false);
        }

    }

    public void OnDestroy()
    {
        MissionManager.My.signs.Remove(this);
    }
}
