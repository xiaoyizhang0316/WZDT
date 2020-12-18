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
    // Start is called before the first frame update
    void Start()
    {
        MissionManager.My.signs.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (data.maxNum == 0)
        {
            currentNum.text = "";
        }
        else
        {
            currentNum.text = this.data.currentNum.ToString();
        }
        if (data.isFinish)
        {
            sign.color = Color.green;
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
            currentNum.text = "";
            maxNum.text = "";
        }


    }

    public void OnDestroy()
    {
        MissionManager.My.signs.Remove(this);
    }
}
