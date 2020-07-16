using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_1001 : MonoBehaviour
{
    private ConsumeSign targetConsume;

    private bool isOpen;

    public void PlayAll()
    {
        ParticleSystem[] list = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < list.Length; i++)
        {
            list[i].Play();
        }
    }

    public void StopAll()
    {
        ParticleSystem[] list = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < list.Length; i++)
        {
            list[i].Stop();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targetConsume = GetComponentInParent<ConsumeSign>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetConsume.currentHealth * 2 > targetConsume.consumeData.maxHealth && !isOpen)
        {
            targetConsume.tweener.timeScale *= 1.3f;
            isOpen = true;
            PlayAll();
        }
        else
        {
            if (isOpen)
            {
                targetConsume.tweener.timeScale /= 1.3f;
                isOpen = false;
                StopAll();
            }
        }
    }
}
