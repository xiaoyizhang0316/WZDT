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
        
    }

    // Update is called once per frame
    void Update()
    {
        currentNum.text = this.data.currentNum.ToString();
        if (data.isFinish)
        {
            sign.color = Color.green;
        }
    }

    public void Init(MissionData data)
    {
        this.data = data;
        contentText.text = this.data.content;
        if (data.maxNum >= 0)
        {
            currentNum.text = this.data.currentNum.ToString();
            maxNum.text = "/"+this.data.maxNum.ToString();
        }


    }

    public void OnDestroy()
    {
        MissionManager.My.signs.Remove(this);
    }
}
