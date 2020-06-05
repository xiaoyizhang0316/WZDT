using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBuffSign : MonoBehaviour
{
    public int buffId;

    public void Init(int id)
    {
        buffId = id;
        InitSprite();
    }

    public void InitSprite()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Buff/" + buffId.ToString());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
