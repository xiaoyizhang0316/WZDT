using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSequenceFrame : MonoBehaviour
{
    public Image targetImg;

    public List<Sprite> sprites;

    private float timeCount = 0;

    private int currentIndex = 0;

    public float interval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        targetImg.sprite = sprites[currentIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        timeCount += Time.deltaTime;
        if (timeCount > interval)
        {
            timeCount = 0;
            if (currentIndex == sprites.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            targetImg.sprite = sprites[currentIndex];
        }
    }
}
