using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : IOIntensiveFramework.MonoSingleton.MonoSingleton<AvatarManager>
{

    public List<Sprite> mouth;

    public List<Sprite> eye;

    public List<GameObject> AvatarPrb;

    public Transform AvatarTF;

    [SerializeField]
    public List<Color> perColor;
    [SerializeField]

    public List<Color> fadeColor;

    public int currentAvatarIndex;

    public int currenteyeIndex; 
    public int currentcolorIndex; 
    public int currentmouthIndex; 
    public int currentotherIndex;

    public GameObject avatarObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnGUI()
    {
       
    }

    public void ShowAcatar(int  AvatarIndex, int eyeIndex ,int colorIndex, int mouthIndex,int otherIndex)
    {
        SetAvatar(AvatarIndex);
        SetAvatarEyes(eyeIndex);
        SetColor(colorIndex);
        SetAvatarMouth(mouthIndex);
        SetAvatarOther(otherIndex);
    }

    public void SetAvatar(int index)
    {
        currentAvatarIndex = index;
        for (int i = 0; i <AvatarTF.childCount; i++)
        {
            Destroy(AvatarTF.GetChild(i).gameObject);
        }

         avatarObj =  Instantiate(AvatarPrb[index], AvatarTF);
       
       
    }

    public void SetAvatarEyes(int index)
    {
        currenteyeIndex = index;
        avatarObj.GetComponent<AvatarSign>().eye.sprite = eye[index];
    }

    public void SetAvatarMouth(int index)
    {
        currentmouthIndex = index; 
        avatarObj.GetComponent<AvatarSign>().mouth.sprite = mouth[index];
        
    }
    
    public void SetAvatarOther(int index)
    {
        currentotherIndex = index;
     //   
     for (int i = 0; i <  avatarObj.GetComponent<AvatarSign>().otherParent .childCount; i++)
     {
         avatarObj.GetComponent<AvatarSign>().otherParent .GetChild(i).gameObject.SetActive(false);
     }
     avatarObj.GetComponent<AvatarSign>().otherParent .GetChild(index).gameObject.SetActive(true);
     
    }

    public void SetColor(int index)
    {
        currentcolorIndex = index;
        avatarObj.GetComponent<AvatarSign>().body.color = fadeColor[index];
        avatarObj.GetComponent<AvatarSign>().bodyFade.color = perColor[index];
    }
}
