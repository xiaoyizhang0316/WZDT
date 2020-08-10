using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoManager : MonoBehaviour
{
    public Button avaL;
    public Button avaR;
    public Button colorR;
    public Button colorL;
    public Button eyeR;
    public Button eyeL;
    public Button mouthL;
    public Button mouthR;
    public Button otherR;
    public Button otherL;
    public Button Random;


    public Text avaText;
    public Text colorText;
    public Text eyeText;
    public Text mouthText;
    public Text otherText;
    public Text RandomText;

    public Transform gameTF;


    public InputField input;

    public Button button;

    public Text tip;
    // Start is called before the first frame update
    void Start()
    {
        
        
        avaL.interactable = false;
        colorL.interactable = false;
        eyeL.interactable = false;
        mouthL.interactable = false;
        otherL.interactable = false;
        AvatarManager.My.AvatarTF = gameTF;
        AvatarManager.My.ShowAcatar(3, 5, 1, 14, 4);

        avaL.onClick.AddListener(() =>
        {
            if (AvatarManager.My.currentAvatarIndex == 1)
            {
                avaL.interactable = false;
                avaText.text = AvatarManager.My.currentAvatarIndex.ToString();

                AvatarManager.My.currentAvatarIndex--;
                AvatarManager.My.SetAvatar(AvatarManager.My.currentAvatarIndex);
            }
            else
            {
                avaR.interactable = true;
                avaText.text = AvatarManager.My.currentAvatarIndex.ToString();
                AvatarManager.My.currentAvatarIndex--;
                AvatarManager.My.SetAvatar(AvatarManager.My.currentAvatarIndex);
            }
        });
        avaR.onClick.AddListener(() =>
        {
            if (AvatarManager.My.currentAvatarIndex == AvatarManager.My.AvatarPrb.Count - 2)
            {
                avaR.interactable = false;


                AvatarManager.My.currentAvatarIndex++;
                avaText.text = (AvatarManager.My.currentAvatarIndex + 1).ToString();

                AvatarManager.My.SetAvatar(AvatarManager.My.currentAvatarIndex);
            }
            else
            {
                avaL.interactable = true;

                AvatarManager.My.currentAvatarIndex++;
                avaText.text = (AvatarManager.My.currentAvatarIndex + 1).ToString();
                AvatarManager.My.SetAvatar(AvatarManager.My.currentAvatarIndex);
            }
        });

        colorL.onClick.AddListener(() =>
        {
            if (AvatarManager.My.currentcolorIndex == 1)
            {
                colorL.interactable = false;
                colorText.text = AvatarManager.My.currentcolorIndex.ToString();

                AvatarManager.My.currentcolorIndex--;
                AvatarManager.My.SetColor(AvatarManager.My.currentcolorIndex);
            }
            else
            {
                colorR.interactable = true;
                colorText.text = AvatarManager.My.currentcolorIndex.ToString();
                AvatarManager.My.currentcolorIndex--;
                AvatarManager.My.SetColor(AvatarManager.My.currentcolorIndex);
            }
        });
        colorR.onClick.AddListener(() =>
        {
            if (AvatarManager.My.currentcolorIndex == AvatarManager.My.perColor.Count - 2)
            {
                colorR.interactable = false;


                AvatarManager.My.currentcolorIndex++;
                colorText.text = (AvatarManager.My.currentcolorIndex + 1).ToString();

                AvatarManager.My.SetColor(AvatarManager.My.currentcolorIndex);
            }
            else
            {
                colorL.interactable = true;

                AvatarManager.My.currentcolorIndex++;
                colorText.text = (AvatarManager.My.currentcolorIndex + 1).ToString();
                AvatarManager.My.SetColor(AvatarManager.My.currentcolorIndex);
            }
        });


        eyeL.onClick.AddListener(() =>
        {
            if (AvatarManager.My.currenteyeIndex == 1)
            {
                eyeL.interactable = false;
                eyeText.text = AvatarManager.My.currenteyeIndex.ToString();

                AvatarManager.My.currenteyeIndex--;
                AvatarManager.My.SetAvatarEyes(AvatarManager.My.currenteyeIndex);
            }
            else
            {
                eyeR.interactable = true;
                eyeText.text = AvatarManager.My.currenteyeIndex.ToString();
                AvatarManager.My.currenteyeIndex--;
                AvatarManager.My.SetAvatarEyes(AvatarManager.My.currenteyeIndex);
            }
        });
        eyeR.onClick.AddListener(() =>
        {
            if (AvatarManager.My.currenteyeIndex == AvatarManager.My.eye.Count - 2)
            {
                eyeR.interactable = false;


                AvatarManager.My.currenteyeIndex++;
                eyeText.text = (AvatarManager.My.currenteyeIndex + 1).ToString();

                AvatarManager.My.SetAvatarEyes(AvatarManager.My.currenteyeIndex);
            }
            else
            {
                eyeL.interactable = true;

                AvatarManager.My.currenteyeIndex++;
                eyeText.text = (AvatarManager.My.currenteyeIndex + 1).ToString();
                AvatarManager.My.SetAvatarEyes(AvatarManager.My.currenteyeIndex);
            }
        });

        mouthL.onClick.AddListener(() =>
        {
            if (AvatarManager.My.currentmouthIndex == 1)
            {
                mouthL.interactable = false;
                mouthText.text = AvatarManager.My.currentmouthIndex.ToString();

                AvatarManager.My.currentmouthIndex--;
                AvatarManager.My.SetAvatarMouth(AvatarManager.My.currentmouthIndex);
            }
            else
            {
                mouthR.interactable = true;
                mouthText.text = AvatarManager.My.currentmouthIndex.ToString();
                AvatarManager.My.currentmouthIndex--;
                AvatarManager.My.SetAvatarMouth(AvatarManager.My.currentmouthIndex);
            }
        });
        mouthR.onClick.AddListener(() =>
        {
            if (AvatarManager.My.currentmouthIndex == AvatarManager.My.mouth.Count - 2)
            {
                mouthR.interactable = false;


                AvatarManager.My.currentmouthIndex++;
                mouthText.text = (AvatarManager.My.currentmouthIndex + 1).ToString();

                AvatarManager.My.SetAvatarMouth(AvatarManager.My.currentmouthIndex);
            }
            else
            {
                mouthL.interactable = true;

                AvatarManager.My.currentmouthIndex++;
                mouthText.text = (AvatarManager.My.currentmouthIndex + 1).ToString();
                AvatarManager.My.SetAvatarMouth(AvatarManager.My.currentmouthIndex);
            }
        });

        otherL.onClick.AddListener(() =>
        {
            if (AvatarManager.My.currentotherIndex == 1)
            {
                otherL.interactable = false;
                otherText.text = AvatarManager.My.currentotherIndex.ToString();

                AvatarManager.My.currentotherIndex--;
                AvatarManager.My.SetAvatarOther(AvatarManager.My.currentotherIndex);
            }
            else
            {
                otherR.interactable = true;
                otherText.text = AvatarManager.My.currentotherIndex.ToString();
                AvatarManager.My.currentotherIndex--;
                AvatarManager.My.SetAvatarOther(AvatarManager.My.currentotherIndex);
            }
        });
        otherR.onClick.AddListener(() =>
        {
            if (AvatarManager.My.currentotherIndex ==
                AvatarManager.My.avatarObj.GetComponent<AvatarSign>().otherParent.childCount - 2)
            {
                otherR.interactable = false;


                AvatarManager.My.currentotherIndex++;
                otherText.text = (AvatarManager.My.currentotherIndex + 1).ToString();

                AvatarManager.My.SetAvatarOther(AvatarManager.My.currentotherIndex);
            }
            else
            {
                otherL.interactable = true;

                AvatarManager.My.currentotherIndex++;
                otherText.text = (AvatarManager.My.currentotherIndex + 1).ToString();
                AvatarManager.My.SetAvatarOther(AvatarManager.My.currentotherIndex);
            }
        });

        Random.onClick.AddListener(() =>
        {
            RandomText.text = (int.Parse(RandomText.text) + 1).ToString();
            AvatarManager.My.ShowAcatar(UnityEngine.Random.Range(0, AvatarManager.My.AvatarPrb.Count - 1),
                UnityEngine.Random.Range(0, AvatarManager.My.eye.Count - 1),
                UnityEngine.Random.Range(0, AvatarManager.My.perColor.Count - 1),
                UnityEngine.Random.Range(0, AvatarManager.My.mouth.Count - 1),
                UnityEngine.Random.Range(0,
                    AvatarManager.My.avatarObj.GetComponent<AvatarSign>().otherParent.childCount - 1));

            avaText.text = (AvatarManager.My.currentAvatarIndex + 1).ToString();
            eyeText.text = (AvatarManager.My.currenteyeIndex + 1).ToString();
            mouthText.text = (AvatarManager.My.currentmouthIndex + 1).ToString();
            otherText.text = (AvatarManager.My.currentotherIndex + 1).ToString();
            colorText.text = (AvatarManager.My.currentcolorIndex + 1).ToString();
          
            avaL.interactable = true;
            avaR.interactable = true;
            colorR.interactable = true;
            colorL.interactable = true;
            eyeR.interactable = true;
            eyeL.interactable = true;
            mouthL.interactable = true;
            mouthR.interactable = true;
            otherR.interactable = true;
            otherL.interactable = true;
            Random.interactable = true;
            if (AvatarManager.My.currentAvatarIndex == 0)
            {
                avaL.interactable  = false;
                
            }

            if (AvatarManager.My.currentAvatarIndex == AvatarManager.My.AvatarPrb.Count - 1)
            {
                avaR.interactable = false;
            }

            if (AvatarManager.My.currenteyeIndex == 0)
            {
                eyeL.interactable = false;
            }

            if (AvatarManager.My.currenteyeIndex == AvatarManager.My.eye.Count - 1)
            {
                eyeR.interactable = false;
            }
            if (AvatarManager.My.currentmouthIndex == 0)
            {
                mouthL.interactable = false;
            }

            if (AvatarManager.My.currentmouthIndex == AvatarManager.My.mouth.Count - 1)
            {
                mouthR.interactable = false;
            }
            
            if (AvatarManager.My.currentcolorIndex == 0)
            {
               colorL.interactable = false;
            }

            if (AvatarManager.My.currentcolorIndex == AvatarManager.My.fadeColor.Count - 1)
            {
                colorR.interactable = false;
            }

            if (AvatarManager.My.currentotherIndex == 0)
            {
                otherL.interactable = false;
            }

            if (AvatarManager.My.currentcolorIndex ==
                AvatarManager.My.avatarObj.GetComponent<AvatarSign>().otherParent.childCount - 1)
            {
                otherR.interactable = false;
            }
        });
        
        button.onClick.AddListener(() =>
        {
            input.text.Trim();
            if (input.text.Length > 0)
            {
                //上传
             NetworkMgr.My.SetPlayerDatas(input.text, AvatarManager.My.currentAvatarIndex.ToString()+"_"+ AvatarManager.My.currenteyeIndex.ToString()+"_"+ AvatarManager.My.currentcolorIndex.ToString()+"_"+ AvatarManager.My.currentmouthIndex.ToString()+"_"+ AvatarManager.My.currentotherIndex,
                 () =>
                 {
                     gameObject.SetActive(false);
                 });
             
            }
          
        });
        if (!string.IsNullOrEmpty(NetworkMgr.My.playerDatas.playerIcon))
        {
       gameObject.SetActive(false); 
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (     input.text.Trim().Length == 0)
        {
            tip.gameObject.SetActive(true);
            button.interactable = false;
        }
        else
        {
            tip.gameObject.SetActive(false);
            button.interactable = true;
        }
    }
}